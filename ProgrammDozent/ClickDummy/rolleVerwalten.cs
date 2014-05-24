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
