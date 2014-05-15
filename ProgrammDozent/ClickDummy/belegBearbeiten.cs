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
        public List<string> rollen = new List<string>();

        public Beleg beleg { get; set; }

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

            
            for (int i = 0; i < 10; i++)
            {
                themen.Add(new Thema("Eine schöne Aufgabe " + i));
            }
            allThemen.DataSource = themen;
            allThemen.DisplayMember = "aufgabenName";

            for (int i = 0; i < 10; i++)
            {
                rollen.Add("Rolle " + i);
            }
            allRollen.DataSource = rollen;
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
