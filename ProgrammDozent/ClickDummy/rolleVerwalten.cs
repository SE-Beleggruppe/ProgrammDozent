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


        private void deleteRolleButton_Click(object sender, EventArgs e)
        {
            Rolle rolle = (Rolle)rollenListBox.SelectedItem;
            database.ExecuteQuery("delete from Rolle where Rolle =\""+rolle.rolle+"\"");
            refreshRollen();
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
            rollenListBox.DisplayMember = "rolle";
        }

        private void newRolleButton_Click(object sender, EventArgs e)
        {
            Eingabe eingabe = new Eingabe();
            eingabe.textEingabe = new Eingabe.textEingabeHandler(eingabeF);
            eingabe.Show();
            
            
   
        }

        public void eingabeF(object sender)
        {
            database.ExecuteQuery("insert into Rolle values(\""+((TextBox)sender).Text +"\")");
            refreshRollen();
        }
    }
}
