using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProgrammDozent;

namespace ProgrammDozent
{
    public partial class MainForm : Form
    {
        List<Beleg> Belege = new List<Beleg>();
        List<Student> tempStudent;
        Database database = new Database();

        public MainForm()
        {
            InitializeComponent();
            
            foreach (string[] array in database.ExecuteQuery("select * from Beleg"))
            {
                Beleg beleg = new Beleg(array[0], array[1], Convert.ToDateTime(array[2]), Convert.ToDateTime(array[3]), Convert.ToInt32(array[4]), Convert.ToInt32(array[5]), array[6] );
                Belege.Add(beleg);
            }
            belegListBox.DataSource = Belege;
            belegListBox.DisplayMember = "Belegkennung";

            belegListBox.DoubleClick += new EventHandler(belegListBox_DoubleClicked);
            gruppenListBox.DoubleClick += new EventHandler(gruppenListBox_DoubleClicked);
        }

         
        private void belegListBox_SelectedIndexChanged(object sender, EventArgs e){
            //if (belegListBox.SelectedItem == null) return;
            //Beleg selected = (Beleg)belegListBox.SelectedItem;
            //gruppenListBox.DataSource = selected.gruppen;
            //gruppenListBox.DisplayMember = "gruppenKennung";
        }

        private void belegListBox_DoubleClicked(object sender, EventArgs e)
        {
            belegBearbeiten belegB = new belegBearbeiten(((Beleg)belegListBox.SelectedItem).belegKennung);
            belegB.Show();
        }

        private void gruppenListBox_DoubleClicked(object sender, EventArgs e)
        {
            gruppeBearbeiten gruppeB = new gruppeBearbeiten();
            gruppeB.Show();
        }

        private void gruppenListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gruppenListBox.SelectedItem == null) return;
            Gruppe selected = (Gruppe)gruppenListBox.SelectedItem;
            studentenDataGridView.DataSource = selected.studenten;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            Application.Exit();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void belegAnlegenButton_Click(object sender, EventArgs e)
        {
            //Beleg newBeleg = new Beleg();
            //Belege.Add(newBeleg);

            //belegListBox.DataSource = null;
            //belegListBox.DataSource = Belege;
            //belegListBox.DisplayMember = "Semester";
        }

        private void gruppeAnlegenButton_Click(object sender, EventArgs e)
        {
            Gruppe newGruppe = new Gruppe();
            Beleg selected = (Beleg)belegListBox.SelectedItem;
            selected.addGruppe(newGruppe);

            gruppenListBox.DataSource = null;
            gruppenListBox.DataSource = selected.gruppen;
            gruppenListBox.DisplayMember = "gruppenKennung";
        }

        private void mitgliedAnlegen_Click(object sender, EventArgs e)
        {
            Student neu = new Student("na", "na", "na", "na@na.de", "na");
            Gruppe grup = (Gruppe)gruppenListBox.SelectedItem;
            grup.addMitglied(neu);
            studentenDataGridView.DataSource = null;
            studentenDataGridView.DataSource = grup.studenten;
        }

        private void dataGridViewFreigebenButton_Click(object sender, EventArgs e)
        {
            tempStudent = (List<Student>)studentenDataGridView.DataSource;
            studentenDataGridView.DataSource = null;
            studentenDataGridView.DataSource = tempStudent;

            studentenDataGridView.Enabled = true;
            saveDataGridViewButton.Enabled = true;
            cancelDataGridViewButton.Enabled = true;
            dataGridViewFreigebenButton.Enabled = false;
        }

        private void saveDataGridViewButton_Click(object sender, EventArgs e)
        {
            Gruppe grup = (Gruppe)gruppenListBox.SelectedItem;
            grup.studenten = tempStudent;
            studentenDataGridView.DataSource = null;
            studentenDataGridView.DataSource = grup.studenten;

            studentenDataGridView.Enabled = false;
            saveDataGridViewButton.Enabled = false;
            cancelDataGridViewButton.Enabled = false;
            dataGridViewFreigebenButton.Enabled = true;
        }

        private void cancelDataGridViewButton_Click(object sender, EventArgs e)
        {
            Gruppe grup = (Gruppe)gruppenListBox.SelectedItem;
            studentenDataGridView.DataSource = null;
            studentenDataGridView.DataSource = grup.studenten;

            studentenDataGridView.Enabled = false;
            saveDataGridViewButton.Enabled = false;
            cancelDataGridViewButton.Enabled = false;
            dataGridViewFreigebenButton.Enabled = true;
        }

        private void themenVerwaltenButton_Click(object sender, EventArgs e)
        {
            themenVerwalten themenV =  new themenVerwalten();
            themenV.Show();
        }

        private void rolleTextBox_Click(object sender, EventArgs e)
        {
            rolleVerwalten rolleV = new rolleVerwalten();
            rolleV.Show();
        }



        
    }
}
