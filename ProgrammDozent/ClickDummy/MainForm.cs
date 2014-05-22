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
        List<Gruppe> Gruppen = new List<Gruppe>();
        List<string> rollen = new List<string>();
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

            mitgliederDataGridView.AllowUserToAddRows = false;
            belegListBox.DataSource = Belege;
            belegListBox.DisplayMember = "Belegkennung";

            belegListBox.DoubleClick += new EventHandler(belegListBox_DoubleClicked);
            gruppenListBox.DoubleClick += new EventHandler(gruppenListBox_DoubleClicked);
        }

         
        private void belegListBox_SelectedIndexChanged(object sender, EventArgs e){
            if (belegListBox.SelectedItem == null) return;
            mitgliederDataGridView.Rows.Clear();
            Beleg selected = (Beleg)belegListBox.SelectedItem;
            updateRollen(selected);
            List<Gruppe> Gruppen = new List<Gruppe>();
            foreach (string[] info in database.ExecuteQuery("select * from Gruppe where Gruppenkennung in (select Gruppenkennung from Zuordnung_GruppeBeleg where Belegkennung=\"" + selected.belegKennung + "\")"))
            {
                Gruppe temp = new Gruppe(info[0], Convert.ToInt32(info[1]), info[2]);
                temp.belegkennung = selected.belegKennung;
                Gruppen.Add(temp);
            }
            gruppenListBox.DataSource = null;
            gruppenListBox.DataSource = Gruppen;
            gruppenListBox.DisplayMember = "gruppenKennung";
        }

        private void updateRollen(Beleg beleg)
        {
            rollen = new List<string>();
            Database db = new Database();
            List<string[]> output = db.ExecuteQuery("Select Rolle from Rolle where Rolle in (select Rolle from Zuordnung_BelegRolle where Belegkennung=\"" + beleg.belegKennung + "\")");
            foreach (string[] info in output)
            {
                rollen.Add(info[0]);
            }
            rollen.Add("na");
        }

        private void belegListBox_DoubleClicked(object sender, EventArgs e)
        {
            belegBearbeiten belegB = new belegBearbeiten(((Beleg)belegListBox.SelectedItem).belegKennung);
            belegB.Show();
        }

        private void gruppenListBox_DoubleClicked(object sender, EventArgs e)
        {
            gruppeBearbeiten gruppeB = new gruppeBearbeiten((Gruppe)gruppenListBox.SelectedItem);
            gruppeB.Show();
        }

        private void gruppenListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gruppenListBox.SelectedItem == null) return;
            Gruppe selected = (Gruppe)gruppenListBox.SelectedItem;
            selected.studenten = null;
            foreach (string[] info2 in database.ExecuteQuery("select * from Student where sNummer in (select sNummer from Zuordnung_GruppeStudent where Gruppenkennung=\"" + selected.gruppenKennung + "\")"))
            {
                selected.addStudent(new Student(info2[2], info2[1], info2[0], info2[3], info2[4]));
            }
            mitgliederDataGridView.Rows.Clear();
            (mitgliederDataGridView.Columns[4] as DataGridViewComboBoxColumn).DataSource = rollen;
            (mitgliederDataGridView.Columns[4] as DataGridViewComboBoxColumn).MinimumWidth = 150;
            (mitgliederDataGridView.Columns[3] as DataGridViewTextBoxColumn).MinimumWidth = 250;
            foreach (Student info in selected.studenten)
            {
                int number = mitgliederDataGridView.Rows.Add();
                mitgliederDataGridView.Rows[number].Cells[0].Value = info.name;
                mitgliederDataGridView.Rows[number].Cells[1].Value = info.vorname;
                mitgliederDataGridView.Rows[number].Cells[2].Value = info.sNummer;
                if (info.sNummer != "na") mitgliederDataGridView.Rows[number].Cells[2].ReadOnly = true;
                mitgliederDataGridView.Rows[number].Cells[3].Value = info.mail;
                mitgliederDataGridView.Rows[number].Cells[4].Value = info.rolle;
            }
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
            Beleg selected = (Beleg)belegListBox.SelectedItem;

            gruppenListBox.DataSource = null;
            gruppenListBox.DataSource = selected.gruppen;
            gruppenListBox.DisplayMember = "gruppenKennung";
        }

        private void mitgliedAnlegen_Click(object sender, EventArgs e)
        {
            Student neu = new Student("na", "na", "na", "na@na.de", "na");
            Gruppe grup = (Gruppe)gruppenListBox.SelectedItem;
            grup.addStudent(neu);
            mitgliederDataGridView.DataSource = null;
            mitgliederDataGridView.DataSource = grup.studenten;
        }

        private void dataGridViewFreigebenButton_Click(object sender, EventArgs e)
        {
            tempStudent = (List<Student>)mitgliederDataGridView.DataSource;
            mitgliederDataGridView.DataSource = null;
            mitgliederDataGridView.DataSource = tempStudent;

            mitgliederDataGridView.Enabled = true;
            saveDataGridViewButton.Enabled = true;
            cancelDataGridViewButton.Enabled = true;
            dataGridViewFreigebenButton.Enabled = false;
        }

        private void saveDataGridViewButton_Click(object sender, EventArgs e)
        {
            saveMitglieder();

            mitgliederDataGridView.Enabled = false;
            saveDataGridViewButton.Enabled = false;
            cancelDataGridViewButton.Enabled = false;
            dataGridViewFreigebenButton.Enabled = true;

            gruppenListBox_SelectedIndexChanged(this, null);
        }

        private void saveMitglieder()
        {
            Gruppe gruppe = (Gruppe)gruppenListBox.SelectedItem;
            for (int i = 0; i < mitgliederDataGridView.Rows.Count; i++)
            {
                string name = (string)mitgliederDataGridView.Rows[i].Cells[0].Value;
                string vorname = (string)mitgliederDataGridView.Rows[i].Cells[1].Value;
                string sNummer = (string)mitgliederDataGridView.Rows[i].Cells[2].Value;
                string mail = (string)mitgliederDataGridView.Rows[i].Cells[3].Value;
                string rolle = (string)mitgliederDataGridView.Rows[i].Cells[4].FormattedValue.ToString();

                if (sNummer != "na" && sNummer != "" && sNummer != null)
                {
                    if (mitgliederDataGridView.Rows[i].Cells[2].ReadOnly) updateStudent(new Student(name, vorname, sNummer, mail, rolle));
                    else insertStudent(new Student(name, vorname, sNummer, mail, rolle), gruppe);
                }
            }
        }

        private void updateStudent(Student student)
        {
            Database db = new Database();
            string query = "update Student set Nachname=\"" + student.name + "\", Vorname=\"" + student.vorname + "\", Mail=\"" + student.mail + "\", Rolle=\"" + student.rolle + "\" where sNummer=\"" + student.sNummer + "\"";
            db.ExecuteQuery(query);
        }

        private void insertStudent(Student student, Gruppe gruppe)
        {
            Database db = new Database();
            string query = "insert into Student values(\"" + student.sNummer + "\",\"" + student.vorname + "\",\"" + student.name + "\",\"" + student.mail + "\",\"" + student.rolle + "\")";
            db.ExecuteQuery(query);
            query = "insert into Zuordnung_GruppeStudent values(\"" + gruppe.gruppenKennung + "\",\"" + student.sNummer + "\")";
            db.ExecuteQuery(query);
        }

        private void cancelDataGridViewButton_Click(object sender, EventArgs e)
        {
            mitgliederDataGridView.Enabled = false;
            saveDataGridViewButton.Enabled = false;
            cancelDataGridViewButton.Enabled = false;
            dataGridViewFreigebenButton.Enabled = true;

            gruppenListBox_SelectedIndexChanged(this, null);
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

        private void studentenDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonArchivieren_Click(object sender, EventArgs e)
        {
            PdfArchivierung archivierung = new PdfArchivierung(database);
            archivierung.Show();
        }


        
    }
}
