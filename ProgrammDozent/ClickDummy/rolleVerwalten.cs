using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ProgrammDozent
{
    public partial class RolleVerwalten : Form
    {

        private List<Rolle> _rollen;
        readonly Database _database = new Database();

        public RolleVerwalten()
        {
            InitializeComponent();
            this.Text = "Rollen";
            this.StartPosition = FormStartPosition.CenterScreen;

            RefreshRollen();
        }


        private void deleteRolleButton_Click(object sender, EventArgs e)
        {
            var rolle = (Rolle)rollenListBox.SelectedItem;

            if (
                _database.ExecuteQuery(
                    "select * from Rolle where Rolle in(select Rolle from Zuordnung_BelegRolle) and Rolle=\"" +
                    rolle.rolle + "\"").Count != 0)
            {
                MessageBox.Show("Die Rolle " + rolle.rolle +
                                " ist noch in Verwendung und kann nicht gelöscht werden.");
                return;
            }

            _database.ExecuteQuery("delete from Rolle where Rolle =\""+rolle.rolle+"\"");
            RefreshRollen();
        }

        private void RefreshRollen()
        {
            _rollen = new List<Rolle>();
            foreach (var array in _database.ExecuteQuery("select * from Rolle"))
            {
                var rolle = new Rolle(array[0]);
                _rollen.Add(rolle);
            }
            rollenListBox.DataSource = _rollen;
            rollenListBox.DisplayMember = "rolle";
            if (_rollen.Count == 0) deleteRolleButton.Enabled = false;
            else deleteRolleButton.Enabled = true;
        }

        private void newRolleButton_Click(object sender, EventArgs e)
        {
            var eingabe = new Eingabe {textEingabe = new Eingabe.textEingabeHandler(EingabeF)};
            eingabe.Show();
        }

        public void EingabeF(object sender)
        {
            _database.ExecuteQuery("insert into Rolle values(\""+((TextBox)sender).Text +"\")");
            RefreshRollen();
        }
    }
}
