using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgrammDozent
{
    public partial class gruppeBearbeiten : Form
    {
        public delegate void GIsSavedHandler(object sender, EventArgs e);
        public GIsSavedHandler SavedG;

        private bool isNeueGruppe;

        public Gruppe gruppe;
        List<string> Themen = new List<string>();
        Database database = new Database();
        public gruppeBearbeiten(Gruppe gruppe)
        {
            InitializeComponent();
            if (gruppe.GruppenKennung == "na") isNeueGruppe = true;
            this.gruppe = gruppe;
            kennungComboBox.DataSource = getFreieCases();
            passwortTextBox.Text = this.gruppe.Password;
            getThemen();
            leiterLabel.Text = getLeiter();

            if (isNeueGruppe) kennungComboBox.Enabled = true;
        }

        List<string> getFreieCases()
        {
            List<string[]> ergDB = database.ExecuteQuery(
                "select Casekennung from Zuordnung_BelegCases where Casekennung not in(select Gruppenkennung from Zuordnung_GruppeBeleg where Belegkennung=\"" +
                gruppe.Belegkennung + "\") and Belegkennung=\"" + gruppe.Belegkennung + "\"");
            if (ergDB == null) return null;
            List<string> erg = new List<string>();
            foreach (string[] strings in ergDB)
            {
                erg.Add(strings[0]);
            }
            return erg;
        }

        void getThemen()
        {
            foreach (string[] info in database.ExecuteQuery("select Aufgabe from Thema where Themennummer in (select Themennummer from Zuordnung_BelegThema where Belegkennung=\"" + gruppe.Belegkennung + "\")"))
            {
                Themen.Add(info[0]);
            }
            themenComboBox.DataSource = null;
            themenComboBox.DataSource = Themen;
            if(!isNeueGruppe)
                themenComboBox.SelectedItem = database.ExecuteQuery("select Aufgabe from Thema where Themennummer in (select Themennummer from Gruppe where Gruppenkennung=\"" + gruppe.GruppenKennung + "\")").First()[0];
        }

        string getLeiter()
        {
            foreach (string[] info in database.ExecuteQuery("select Nachname, Vorname from Student where sNummer in (select sNummer from Zuordnung_GruppeStudent where Gruppenkennung=\"" + gruppe.GruppenKennung + "\") and Rolle=\"Leitung\""))
            {
                return info[0] + ", " + info[1];
            }
            return "Kein Leiter angegeben.";
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void speichernbutton_Click(object sender, EventArgs e)
        {
            if (!isNeueGruppe) updateGruppe();
            else insertGruppe();

            if (SavedG != null) SavedG(null, null);

            Close();
        }

        void updateGruppe()
        {
            int Themennummer = Convert.ToInt32(database.ExecuteQuery("select Themennummer from Thema where Aufgabe=\"" + themenComboBox.SelectedItem + "\"").First()[0]);
            database.ExecuteQuery("update Gruppe set Themennummer=" + Themennummer + ", Passwort=\"" + passwortTextBox.Text + "\" where Gruppenkennung=\"" + gruppe.GruppenKennung + "\"");
        }

        void insertGruppe()
        {
            int Themennummer = Convert.ToInt32(database.ExecuteQuery("select Themennummer from Thema where Aufgabe=\"" + themenComboBox.SelectedItem + "\"").First()[0]);
            database.ExecuteQuery("insert into Gruppe values(\"" + kennungComboBox.SelectedItem + "\"," + Themennummer + ",\"" + passwortTextBox.Text + "\")");
            database.ExecuteQuery("insert into Zuordnung_GruppeBeleg values(\"" + kennungComboBox.SelectedItem + "\",\"" + gruppe.Belegkennung + "\")");
        }
    }
}
