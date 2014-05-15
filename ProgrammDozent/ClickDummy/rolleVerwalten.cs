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
    public partial class rolleVerwalten : Form
    {
        private List<Rolle> rollen;
        Database database = new Database();

        public rolleVerwalten()
        {
            InitializeComponent();
            refreshRollen();
        }

        private void newThemaButton_Click(object sender, EventArgs e)
        {
            database.ExecuteQuery("insert into Thema values(8,\"Thema3\")");
            refreshRollen();
        }

        private void deleteThemaButton_Click(object sender, EventArgs e)
        {
            Rolle rolle = (Rolle)rollenListBox.SelectedItem;
            rollen.Remove(rolle);
            rollenListBox.DataSource = null;
            rollenListBox.DataSource = rollen;
            rollenListBox.DisplayMember = "aufgabenName";
        }

        private void refreshRollen()
        {
            rollen = new List<Rolle>();
            foreach (string[] array in database.ExecuteQuery("select * from Rolle"))
            {
                Rolle rolle = new Rolle(array[0]);
                rollen.Add(rolle);
            }
            rollenListBox.DataSource = rollen;
            rollenListBox.DisplayMember = "aufgabenName";
        }
    }
}
