using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ProgrammDozent
{
    public partial class ThemenVerwalten : Form
    {
        private List<Thema> _themen;
        readonly Database _database = new Database();

        public ThemenVerwalten()
        {
            InitializeComponent();
            this.Text = "Themen";
            this.StartPosition = FormStartPosition.CenterScreen;

            RefreshThemen();
        }

        private void deleteThemaButton_Click(object sender, EventArgs e)
        {
            var thema = (Thema)themenListBox.SelectedItem;
            _database.ExecuteQuery("delete from Thema where Themennummer =" + thema.ThemenNummer + "");
            RefreshThemen();
        }

        private void RefreshThemen()
        {
            _themen = new List<Thema>();
            foreach (var array in _database.ExecuteQuery("select * from Thema"))
            {
                var thema = new Thema(Convert.ToInt32(array[0]), array[1]);
                _themen.Add(thema);
            }
            themenListBox.DataSource = _themen;
            themenListBox.DisplayMember = "aufgabenName";
        }

        private void addThemaButton_Click_1(object sender, EventArgs e)
        {
            var eingabe = new Eingabe {textEingabe = new Eingabe.textEingabeHandler(EingabeF)};
            eingabe.Show();
        }

        public void EingabeF(object sender)
        {
            _database.ExecuteQuery("insert into Thema values(\"" + ((TextBox)sender).Text + "\")");
            RefreshThemen();
        }

        private void ThemenVerwalten_Load(object sender, EventArgs e)
        {

        }
    }
}
