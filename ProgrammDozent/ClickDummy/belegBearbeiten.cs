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
        public List<Thema> themen = new List<Thema>();
        public List<Rolle> rollen = new List<Rolle>();
        public List<string> cases = new List<string>();

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
                themen.Add(thema);
            }

            allThemen.DataSource = themen;
            allThemen.DisplayMember = "aufgabenName";

            foreach (string[] array in database.ExecuteQuery("select * from Rolle"))
            {
                Rolle rolle = new Rolle(array[0]);
                rollen.Add(rolle);
            }

            allRollen.DataSource = rollen;
            allRollen.DisplayMember = "rolle";

            foreach (string[] array in database.ExecuteQuery("select Cases.Casekennung from Cases where Cases.Casekennung not in (select Casekennung from Zuordnung_BelegCases)"))
            {
                string oneCase = array[0];
                cases.Add(oneCase);
            }

            allCases.DataSource = cases;
            allCases.DisplayMember = "CaseKennung";
           
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

        private void addButton_Click(object sender, EventArgs e)
        {
            Thema thema = (Thema)allThemen.SelectedItem;
            
        }

        private void remButton_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }
    }
}
