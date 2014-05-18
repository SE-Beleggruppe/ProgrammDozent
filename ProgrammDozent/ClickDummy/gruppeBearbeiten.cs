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
        public Gruppe gruppe;
        List<string> Themen = new List<string>();
        Database database = new Database();
        public gruppeBearbeiten(Gruppe gruppe)
        {
            InitializeComponent();
            this.gruppe = gruppe;
            kennungTextBox.Text = this.gruppe.gruppenKennung;
            passwortTextBox.Text = this.gruppe.password;
            getThemen();
            leiterLabel.Text = getLeiter();
        }

        void getThemen()
        {
            foreach (string[] info in database.ExecuteQuery("select Aufgabe from Thema where Themennummer in (select Themennummer from Zuordnung_BelegThema where Belegkennung=\"" + gruppe.Belegkennung + "\")"))
            {
                Themen.Add(info[0]);
            }
            themenComboBox.DataSource = null;
            themenComboBox.DataSource = Themen;
            themenComboBox.SelectedItem = database.ExecuteQuery("select Aufgabe from Thema where Themennummer in (select Themennummer from Gruppe where Gruppenkennung=\"" + gruppe.gruppenKennung + "\")").First()[0];
        }

        string getLeiter()
        {
            foreach (string[] info in database.ExecuteQuery("select Nachname, Vorname from Student where sNummer in (select sNummer from Zuordnung_GruppeStudent where Gruppenkennung=\"" + gruppe.gruppenKennung + "\") and Rolle=\"Leitung\""))
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
            int Themennummer = Convert.ToInt32(database.ExecuteQuery("select Themennummer from Thema where Aufgabe=\"" + themenComboBox.SelectedItem + "\"").First()[0]);
            database.ExecuteQuery("update Gruppe set Themennummer=" + Themennummer + ", Passwort=\"" + passwortTextBox.Text + "\" where Gruppenkennung=\"" + gruppe.gruppenKennung + "\"");
            MessageBox.Show("Änderungen erfolgreich gespeichert.");
            Close();
        }
    }
}
