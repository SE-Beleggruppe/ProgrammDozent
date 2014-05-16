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
    public partial class belegBearbeiten : Form
    {
        public List<Thema> allethemen = new List<Thema>();
        public List<Thema> verfthemen = new List<Thema>();
        public List<Rolle> allerollen = new List<Rolle>();
        public List<Rolle> verfrollen = new List<Rolle>();
        public List<string> allecases = new List<string>();
        public List<string> verfcases = new List<string>();


        public Beleg beleg { get; set; }

        Database database = new Database();

        public belegBearbeiten(string belegKennung)
        {
            InitializeComponent();
            this.beleg = getBelegFromKennung(belegKennung);
            kennungTextBox.Text = beleg.belegKennung;
            passwortTextBox.Text = beleg.passwort;
            semesterTextBox.Text = beleg.semester;
            if (this.beleg.startDatum != null) startDateTimePicker.Value = beleg.startDatum;
            if (this.beleg.endDatum != null) endDateTimePicker.Value = beleg.endDatum;
            minGR.Value = beleg.minMitglieder;
            maxGR.Value = beleg.maxMitglieder;

            //Alle Themen füllen
            foreach (string[] array in database.ExecuteQuery("select * from Thema where Themennummer not in (select Themennummer from Zuordnung_BelegThema where Belegkennung = \""+beleg.belegKennung+"\")"))
            {
                Thema thema = new Thema(Convert.ToInt32(array[0]),array[1]);
                allethemen.Add(thema);
            }
            allethemen.Sort((t1, t2) => t1.aufgabenName.CompareTo(t2.aufgabenName));
            allThemen.DataSource = allethemen;
            allThemen.DisplayMember = "aufgabenName";

            //Verfügbare Themen füllen
            foreach (string[] array in database.ExecuteQuery("select * from Thema where Themennummer in (select Themennummer from Zuordnung_BelegThema where Belegkennung = \"" + beleg.belegKennung + "\")"))
            {
                Thema thema = new Thema(Convert.ToInt32(array[0]), array[1]);
                verfthemen.Add(thema);
            }

            verfthemen.Sort((t1, t2) => t1.aufgabenName.CompareTo(t2.aufgabenName));
            verThemen.DataSource = verfthemen;
            verThemen.DisplayMember = "aufgabenName";

            //Alle Rollen füllen
            foreach (string[] array in database.ExecuteQuery("select * from Rolle where Rolle not in(select Rolle from Zuordnung_BelegRolle where Belegkennung = \"" + beleg.belegKennung + "\")"))
            {
                Rolle rolle = new Rolle(array[0]);
                allerollen.Add(rolle);
            }

            allerollen.Sort((t1, t2) => t1.rolle.CompareTo(t2.rolle));
            allRollen.DataSource = allerollen;
            allRollen.DisplayMember = "rolle";

            //Verfügbare Rollen füllen
            foreach (string[] array in database.ExecuteQuery("select * from Rolle where Rolle in(select Rolle from Zuordnung_BelegRolle where Belegkennung = \"" + beleg.belegKennung + "\")"))
            {
                Rolle rolle = new Rolle(array[0]);
                verfrollen.Add(rolle);
            }

            verfrollen.Sort((t1, t2) => t1.rolle.CompareTo(t2.rolle));
            verRollen.DataSource = verfrollen;
            verRollen.DisplayMember = "rolle";

            //Alle Cases füllen
            foreach (string[] array in database.ExecuteQuery("select Cases.Casekennung from Cases where Cases.Casekennung not in (select Casekennung from Zuordnung_BelegCases)"))
            {
                string oneCase = array[0];
                allecases.Add(oneCase);
            }
            allecases.Sort();
            allCases.DataSource = allecases;

            //Verfügbare Cases füllen
            foreach (string[] array in database.ExecuteQuery("select Cases.Casekennung from Cases where Cases.Casekennung in (select Casekennung from Zuordnung_BelegCases where Belegkennung = \"" + beleg.belegKennung + "\")"))
            {
                string oneCase = array[0];
                verfcases.Add(oneCase);
            }
            verfcases.Sort();
            verCases.DataSource = verfcases;

           
        }

        private void belegBearbeiten_Load(object sender, EventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void speichernbutton_Click(object sender, EventArgs e)
        {
            //Beleg updaten
            string startdatum = startDateTimePicker.Value.Year.ToString() + "-" + startDateTimePicker.Value.Month.ToString() + "-" + startDateTimePicker.Value.Day.ToString();
            string enddatum = endDateTimePicker.Value.Year.ToString() + "-" + endDateTimePicker.Value.Month.ToString() + "-" + endDateTimePicker.Value.Day.ToString();
            string query = "Update Beleg Set Semester = \"" + semesterTextBox.Text + "\", StartDatum = \"" + startdatum + "\",EndDatum = \"" + enddatum + "\",MinAnzMitglieder = " + minGR.Value + ",MaxAnzMitglieder = " + maxGR.Value + ",Passwort = \"" + passwortTextBox.Text + "\" where Belegkennung = \"" + beleg.belegKennung + "\"";
            var ergebnis = database.ExecuteQuery(query);

            //Zuordnung_BelegThema updaten
              //Alle zugehörigen Datensätze löschen
            ergebnis = database.ExecuteQuery("delete from Zuordnung_BelegThema where Belegkennung = \""+beleg.belegKennung+"\"");
              //Inhalt von verfthemen inserten
            foreach (Thema thema in verfthemen)
            {
                ergebnis = database.ExecuteQuery("insert into Zuordnung_BelegThema values(\""+beleg.belegKennung+"\", "+thema.themenNummer+")");
            }

            //Zuordnung_BelegRolle
              //Alle zugehörigen Datensätze löschen
            ergebnis = database.ExecuteQuery("delete from Zuordnung_BelegRolle where Belegkennung = \"" + beleg.belegKennung + "\"");
              //Inhalt von verfthemen inserten
            foreach (Rolle rolle in verfrollen)
            {
                ergebnis = database.ExecuteQuery("insert into Zuordnung_BelegRolle values(\"" + beleg.belegKennung + "\", \"" + rolle.rolle + "\")");
            }

            //Zuordnung_BelegCase
            //Alle zugehörigen Datensätze löschen
            ergebnis = database.ExecuteQuery("delete from Zuordnung_BelegCases where Belegkennung = \"" + beleg.belegKennung + "\"");
            //Inhalt von verfthemen inserten
            foreach (string onecase in verfcases)
            {
                ergebnis = database.ExecuteQuery("insert into Zuordnung_BelegCases values(\"" + beleg.belegKennung + "\", \"" + onecase + "\")");
            }
        }

        private void addButtonThema_Click(object sender, EventArgs e)
        {
            Thema thema = (Thema)allThemen.SelectedItem;
            allethemen.Remove(thema);
            allThemen.DataSource = null;
            allThemen.DataSource = allethemen;
            allThemen.DisplayMember = "aufgabenName";

            verfthemen.Add(thema);
            verfthemen.Sort((t1, t2) => t1.aufgabenName.CompareTo(t2.aufgabenName));
            verThemen.DataSource = null;
            verThemen.DataSource = verfthemen;
            verThemen.DisplayMember = "aufgabenName";
        }

        private void remButtonThema_Click(object sender, EventArgs e)
        {
            Thema thema = (Thema)verThemen.SelectedItem;
            verfthemen.Remove(thema);
            verThemen.DataSource = null;
            verThemen.DataSource = verfthemen;
            verThemen.DisplayMember = "aufgabenName";

            allethemen.Add(thema);
            allethemen.Sort((t1, t2) => t1.aufgabenName.CompareTo(t2.aufgabenName));
            allThemen.DataSource = null;
            allThemen.DataSource = allethemen;
            allThemen.DisplayMember = "aufgabenName";
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }

        private void addButtonRolle_Click(object sender, EventArgs e)
        {
            Rolle rolle = (Rolle)allRollen.SelectedItem;
            allerollen.Remove(rolle);
            allRollen.DataSource = null;
            allRollen.DataSource = allerollen;
            allRollen.DisplayMember = "rolle";

            verfrollen.Add(rolle);
            verfrollen.Sort((t1, t2) => t1.rolle.CompareTo(t2.rolle));
            verRollen.DataSource = null;
            verRollen.DataSource = verfrollen;
            verRollen.DisplayMember = "rolle";
        }

        private void remButtonRolle_Click(object sender, EventArgs e)
        {
            Rolle rolle = (Rolle)verRollen.SelectedItem;
            verfrollen.Remove(rolle);
            verRollen.DataSource = null;
            verRollen.DataSource = verfrollen;
            verRollen.DisplayMember = "rolle";

            allerollen.Add(rolle);
            allerollen.Sort((t1, t2) => t1.rolle.CompareTo(t2.rolle));
            allRollen.DataSource = null;
            allRollen.DataSource = allerollen;
            allRollen.DisplayMember = "rolle";
        }

        private void addButtonCase_Click(object sender, EventArgs e)
        {
            string onecase = (string)allCases.SelectedItem;
            allecases.Remove(onecase);
            allCases.DataSource = null;
            allCases.DataSource = allecases;

            verfcases.Add(onecase);
            verfcases.Sort();
            verCases.DataSource = null;
            verCases.DataSource = verfcases;
        }

        private void remButtonCase_Click(object sender, EventArgs e)
        {
            string onecase = (string)verCases.SelectedItem;
            verfcases.Remove(onecase);
            verCases.DataSource = null;
            verCases.DataSource = verfcases;

            allecases.Add(onecase);
            allecases.Sort();
            allCases.DataSource = null;
            allCases.DataSource = allecases;
        }

        private Beleg getBelegFromKennung(string belegKennung)
        {
            var ergebnis = database.ExecuteQuery("select * from Beleg where Belegkennung = \""+belegKennung+"\"");
            string[] array = ergebnis.First();
            return new Beleg(array[0], array[1], Convert.ToDateTime(array[2]), Convert.ToDateTime(array[3]), Convert.ToInt32(array[4]), Convert.ToInt32(array[5]), array[6]);

        }
    }
}
