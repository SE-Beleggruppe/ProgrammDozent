using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ProgrammDozent
{
    public partial class MainForm : Form
    {
        List<Beleg> _belege = new List<Beleg>();
        List<Gruppe> _gruppen = new List<Gruppe>();
        List<string> _rollen = new List<string>();
        List<Student> _tempStudent;
        readonly Database _database = new Database();

        public MainForm()
        {
            InitializeComponent();
            this.Text = "Übersicht";
            this.StartPosition = FormStartPosition.CenterScreen;
            
            UpdateBelege(null);

            mitgliederDataGridView.AllowUserToAddRows = false;
            mitgliederDataGridView.UserDeletingRow += mitgliederDataGridView_UserDeletingRow;

            belegListBox.DoubleClick += belegListBox_DoubleClicked;
            gruppenListBox.DoubleClick += gruppenListBox_DoubleClicked;
        }

        void mitgliederDataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DataGridViewRow rowToDelete = e.Row;
            string sNummerToDelete = (string)rowToDelete.Cells[2].Value;
            if (sNummerToDelete == "na") e.Cancel = true;

            DialogResult dialogResult = MessageBox.Show("Wollen Sie den Studenten " + sNummerToDelete + " wirklich löschen?", "Achtung", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Database db = new Database();
                db.ExecuteQuery("delete from Student where sNummer=\"" + sNummerToDelete + "\"");
                db.ExecuteQuery("delete from Zuordnung_GruppeStudent where sNummer=\"" + sNummerToDelete + "\"");
            }
            else if (dialogResult == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void UpdateBelege(object sender)
        {
            _belege = new List<Beleg>();
            foreach (var array in _database.ExecuteQuery("select * from Beleg"))
            {
                var beleg = new Beleg(array[0], array[1], Convert.ToDateTime(array[2]), Convert.ToDateTime(array[3]), Convert.ToInt32(array[4]), Convert.ToInt32(array[5]), array[6]);
                _belege.Add(beleg);
            }
            belegListBox.DataSource = _belege;
            belegListBox.DisplayMember = "Belegkennung";
        }

         
        private void belegListBox_SelectedIndexChanged(object sender, EventArgs e){
            if (belegListBox.SelectedItem == null) return;
            mitgliederDataGridView.Rows.Clear();
            var selected = (Beleg)belegListBox.SelectedItem;
            UpdateRollen(selected);
            _gruppen = new List<Gruppe>();
            foreach (var info in _database.ExecuteQuery("select * from Gruppe where Gruppenkennung in (select Gruppenkennung from Zuordnung_GruppeBeleg where Belegkennung=\"" + selected.BelegKennung + "\")"))
            {
                var temp = new Gruppe(info[0], Convert.ToInt32(info[1]), info[2]) {Belegkennung = selected.BelegKennung};
                _gruppen.Add(temp);
            }
            gruppenListBox.DataSource = null;
            gruppenListBox.DataSource = _gruppen;
            gruppenListBox.DisplayMember = "gruppenKennung";
        }

        private void UpdateRollen(Beleg beleg)
        {
            _rollen = new List<string>();
            var db = new Database();
            var output = db.ExecuteQuery("Select Rolle from Rolle where Rolle in (select Rolle from Zuordnung_BelegRolle where Belegkennung=\"" + beleg.BelegKennung + "\")");
            foreach (var info in output)
            {
                _rollen.Add(info[0]);
            }
            _rollen.Add("na");
        }

        private void belegListBox_DoubleClicked(object sender, EventArgs e)
        {
            var belegB = new BelegBearbeiten(((Beleg)belegListBox.SelectedItem).BelegKennung, false);
            belegB.Show();
        }

        private void gruppenListBox_DoubleClicked(object sender, EventArgs e)
        {
            var gruppeB = new gruppeBearbeiten((Gruppe)gruppenListBox.SelectedItem, false);
            gruppeB.Show();
        }

        private void gruppenListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gruppenListBox.SelectedItem == null) return;
            Gruppe selected = (Gruppe)gruppenListBox.SelectedItem;
            selected.Studenten = null;
            foreach (var info2 in _database.ExecuteQuery("select * from Student where sNummer in (select sNummer from Zuordnung_GruppeStudent where Gruppenkennung=\"" + selected.GruppenKennung + "\")"))
            {
                selected.AddStudent(new Student(info2[2], info2[1], info2[0], info2[3], info2[4]));
            }
            int studentenCount = 0;
            if (selected.Studenten != null) studentenCount = selected.Studenten.Count;
            if (studentenCount < ((Beleg)belegListBox.SelectedItem).MaxMitglieder)
            {
                for (int i = 0; i < ((Beleg)belegListBox.SelectedItem).MaxMitglieder - studentenCount; i++)
                    selected.AddStudent(new Student("na", "na", "na", "na", "na"));
            }

            mitgliederDataGridView.Rows.Clear();
            (mitgliederDataGridView.Columns[4] as DataGridViewComboBoxColumn).DataSource = _rollen;
            (mitgliederDataGridView.Columns[4] as DataGridViewComboBoxColumn).MinimumWidth = 150;
            (mitgliederDataGridView.Columns[3] as DataGridViewTextBoxColumn).MinimumWidth = 250;
            if (selected.Studenten != null)
                foreach (var info in selected.Studenten)
                {
                    var number = mitgliederDataGridView.Rows.Add();
                    mitgliederDataGridView.Rows[number].Cells[0].Value = info.Name;
                    mitgliederDataGridView.Rows[number].Cells[1].Value = info.Vorname;
                    mitgliederDataGridView.Rows[number].Cells[2].Value = info.SNummer;
                    if (info.SNummer != "na") mitgliederDataGridView.Rows[number].Cells[2].ReadOnly = true;
                    mitgliederDataGridView.Rows[number].Cells[3].Value = info.Mail;
                    mitgliederDataGridView.Rows[number].Cells[4].Value = info.Rolle;

                    if (info.SNummer == "na" && number < ((Beleg)belegListBox.SelectedItem).MinMitglieder)
                        mitgliederDataGridView.Rows[number].DefaultCellStyle.BackColor = Color.Yellow;
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
            BelegBearbeiten dest = new BelegBearbeiten("na", true)
            {
                Saved = new BelegBearbeiten.IsSavedHandler(UpdateBelege)
            };
            dest.Show();
        }


        private void gruppeAnlegenButton_Click(object sender, EventArgs e)
        {
            Gruppe temp = new Gruppe("na", 0, "na");
            temp.Belegkennung = ((Beleg) belegListBox.SelectedItem).BelegKennung;

            if (getFreieCases(temp) == null)
            {
                MessageBox.Show("Für diesen Beleg können keine weiteren Gruppen hinzugefügt werden.");
                return;
            }

            gruppeBearbeiten dest = new gruppeBearbeiten(temp, true);
            dest.SavedG = new gruppeBearbeiten.GIsSavedHandler(belegListBox_SelectedIndexChanged);
            dest.Show();
        }

        List<string> getFreieCases(Gruppe gruppe)
        {
            Database database = new Database();
            List<string[]> ergDB = database.ExecuteQuery(
                "select Casekennung from Zuordnung_BelegCases where Casekennung not in(select Gruppenkennung from Zuordnung_GruppeBeleg where Belegkennung=\"" +
                gruppe.Belegkennung + "\") and Belegkennung=\"" + gruppe.Belegkennung + "\"");
            if (ergDB.Count == 0) return null;
            List<string> erg = new List<string>();
            foreach (string[] strings in ergDB)
            {
                erg.Add(strings[0]);
            }
            return erg;
        }

        private void dataGridViewFreigebenButton_Click(object sender, EventArgs e)
        {
            _tempStudent = (List<Student>)mitgliederDataGridView.DataSource;
            mitgliederDataGridView.DataSource = null;
            mitgliederDataGridView.DataSource = _tempStudent;

            mitgliederDataGridView.Enabled = true;
            saveDataGridViewButton.Enabled = true;
            cancelDataGridViewButton.Enabled = true;
            dataGridViewFreigebenButton.Enabled = false;
        }

        private void saveDataGridViewButton_Click(object sender, EventArgs e)
        {
            if (SaveMitglieder())
            {
                mitgliederDataGridView.Enabled = false;
                saveDataGridViewButton.Enabled = false;
                cancelDataGridViewButton.Enabled = false;
                dataGridViewFreigebenButton.Enabled = true;

                gruppenListBox_SelectedIndexChanged(this, null);
            }
        }

        private bool SaveMitglieder()
        {
            var gruppe = (Gruppe)gruppenListBox.SelectedItem;

            for (var i = 0; i < mitgliederDataGridView.Rows.Count; i++)
            {
                var sNummer = (string)mitgliederDataGridView.Rows[i].Cells[2].Value;
                var mail = (string) mitgliederDataGridView.Rows[i].Cells[3].Value;
                if (sNummer != "na" && mail != "na" && !checkMail(mail))
                {
                    MessageBox.Show(mail + " ist keine gültige Mail-Adresse. Die Daten wurden nicht gespeichert.",
                        "Fehler");
                    return false;
                }
                else if (!mitgliederDataGridView.Rows[i].Cells[2].ReadOnly && sNummer != "na" && !checkSNummer(sNummer))
                {
                    MessageBox.Show(
                        sNummer +
                        " ist keine gültige S-Nummer oder der betreffende Student steht schon in der Datenbank. Die Daten wurden nicht gespeichert.",
                        "Fehler");
                    return false;
                }
            }

            for (var i = 0; i < mitgliederDataGridView.Rows.Count; i++)
            {
                var name = (string)mitgliederDataGridView.Rows[i].Cells[0].Value;
                var vorname = (string)mitgliederDataGridView.Rows[i].Cells[1].Value;
                var sNummer = (string)mitgliederDataGridView.Rows[i].Cells[2].Value;
                var mail = (string)mitgliederDataGridView.Rows[i].Cells[3].Value;
                var rolle = (string)mitgliederDataGridView.Rows[i].Cells[4].FormattedValue.ToString();

                if (sNummer != "na" && !string.IsNullOrEmpty(sNummer))
                {
                    if (mitgliederDataGridView.Rows[i].Cells[2].ReadOnly) updateStudent(new Student(name, vorname, sNummer, mail, rolle));
                    else insertStudent(new Student(name, vorname, sNummer, mail, rolle), gruppe);
                }
            }
            return true;
        }

        private bool checkMail(string mail)
        {
            Regex regExp = new Regex("\\b[!#$%&'*+./0-9=?_`a-z{|}~^-]+@[.0-9a-z-]+\\.[a-z]{2,6}\\b");
            Match match = regExp.Match(mail);
            if (match.Success)
            {
                return true;
            }
            else return false;
        }

        private bool checkSNummer(string sNummer)
        {
            Database db = new Database();
            if (sNummer == "") return false;
            if (sNummer.Length != 6) return false;
            if (!sNummer.StartsWith("s")) return false;
            string nummer = sNummer.Substring(1);
            int n;
            bool isNummer = int.TryParse(nummer, out n);
            if (!isNummer) return false;

            List<string[]> output = db.ExecuteQuery("select * from Student");
            foreach (string[] info in output)
            {
                if (info[0] == sNummer) return false;
            }

            return true;
        }

        private void updateStudent(Student student)
        {
            var db = new Database();
            var query = "update Student set Nachname=\"" + student.Name + "\", Vorname=\"" + student.Vorname + "\", Mail=\"" + student.Mail + "\", Rolle=\"" + student.Rolle + "\" where sNummer=\"" + student.SNummer + "\"";
            db.ExecuteQuery(query);
        }

        private void insertStudent(Student student, Gruppe gruppe)
        {
            var db = new Database();
            var query = "insert into Student values(\"" + student.SNummer + "\",\"" + student.Vorname + "\",\"" + student.Name + "\",\"" + student.Mail + "\",\"" + student.Rolle + "\")";
            db.ExecuteQuery(query);
            query = "insert into Zuordnung_GruppeStudent values(\"" + gruppe.GruppenKennung + "\",\"" + student.SNummer + "\")";
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
            var themenV =  new ThemenVerwalten();
            themenV.Show();
        }

        private void rolleTextBox_Click(object sender, EventArgs e)
        {
            var rolleV = new RolleVerwalten();
            rolleV.Show();
        }

        private void studentenDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonArchivieren_Click(object sender, EventArgs e)
        {
            var archivierung = new PdfArchivierung(_database);
            archivierung.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            kontaktForm kForm = new kontaktForm();
            kForm.Show();
        }

        private void belegLoeschenButton_Click(object sender, EventArgs e)
        {
            Beleg temp = (Beleg)belegListBox.SelectedItem;
            if (temp != null)
            {
                DialogResult dialogResult = MessageBox.Show("Wollen Sie den Beleg " + temp.BelegKennung + " wirklich löschen?", "Achtung", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Database db = new Database();
                    if (db.ExecuteQuery("select * from Zuordnung_GruppeBeleg where Belegkennung=\"" + temp.BelegKennung + "\"").Count != 0)
                    {
                        MessageBox.Show("Dieser Beleg hat noch aktive Gruppen, bitte löschen Sie diese zunächst.");
                        return;
                    }

                    db.ExecuteQuery("delete from Beleg where Belegkennung=\"" + temp.BelegKennung + "\"");
                    db.ExecuteQuery("delete from Zuordnung_BelegCases where Belegkennung=\"" + temp.BelegKennung + "\"");
                    db.ExecuteQuery("delete from Zuordnung_BelegThema where Belegkennung=\"" + temp.BelegKennung + "\"");
                    db.ExecuteQuery("delete from Zuordnung_BelegRolle where Belegkennung=\"" + temp.BelegKennung + "\"");
                    db.ExecuteQuery("delete from Zuordnung_GruppeBeleg where Belegkennung=\"" + temp.BelegKennung + "\"");
                    UpdateBelege(null);
                }
            }
        }

        private void gruppeLoeschenButton_Click(object sender, EventArgs e)
        {
            Gruppe temp = (Gruppe) gruppenListBox.SelectedItem;
            if (temp != null)
            {
                DialogResult dialogResult = MessageBox.Show("Wollen Sie die Gruppe " + temp.GruppenKennung + " wirklich löschen?", "Achtung", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Database db = new Database();
                    if (db.ExecuteQuery("select * from Zuordnung_GruppeStudent where Gruppenkennung=\"" + temp.GruppenKennung + "\"").Count != 0)
                    {
                        MessageBox.Show("Dieser Beleg hat noch aktive Studenten, bitte löschen Sie diese zunächst.");
                        return;
                    }

                    db.ExecuteQuery("delete from Gruppe where Gruppenkennung=\"" + temp.GruppenKennung + "\"");
                    db.ExecuteQuery("delete from Zuordnung_GruppeStudent where Gruppenkennung=\"" + temp.GruppenKennung + "\"");
                    db.ExecuteQuery("delete from Zuordnung_GruppeBeleg where Gruppenkennung=\"" + temp.GruppenKennung + "\"");

                    belegListBox_SelectedIndexChanged(this, null);
                }

                
            }
        }


        
    }
}
