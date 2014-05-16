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

        public belegBearbeiten(Beleg beleg)
        {
            InitializeComponent();
            this.beleg = beleg;
            kennungTextBox.Text = beleg.belegKennung;
            passwortTextBox.Text = beleg.passwort;
            semesterTextBox.Text = beleg.semester;
            if (this.beleg.startDatum != null) startDateTimePicker.Value = beleg.startDatum;
            if (this.beleg.endDatum != null) endDateTimePicker.Value = beleg.endDatum;
            minGR.Value = beleg.minMitglieder;
            maxGR.Value = beleg.maxMitglieder;

            foreach (string[] array in database.ExecuteQuery("select * from Thema"))
            {
                Thema thema = new Thema(Convert.ToInt32(array[0]),array[1]);
                allethemen.Add(thema);
            }

            allThemen.DataSource = allethemen;
            allThemen.DisplayMember = "aufgabenName";

            foreach (string[] array in database.ExecuteQuery("select * from Rolle"))
            {
                Rolle rolle = new Rolle(array[0]);
                allerollen.Add(rolle);
            }

            allRollen.DataSource = allerollen;
            allRollen.DisplayMember = "rolle";

            foreach (string[] array in database.ExecuteQuery("select Cases.Casekennung from Cases where Cases.Casekennung not in (select Casekennung from Zuordnung_BelegCases)"))
            {
                string oneCase = array[0];
                allecases.Add(oneCase);
            }

            allCases.DataSource = allecases;


           
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
            //SPEICHERN
        }

        private void addButtonThema_Click(object sender, EventArgs e)
        {
            Thema thema = (Thema)allThemen.SelectedItem;
            allethemen.Remove(thema);
            allThemen.DataSource = null;
            allThemen.DataSource = allethemen;
            allThemen.DisplayMember = "aufgabenName";

            verfthemen.Add(thema);
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
    }
}
