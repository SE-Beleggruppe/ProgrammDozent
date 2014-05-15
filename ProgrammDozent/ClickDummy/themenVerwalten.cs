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
    public partial class themenVerwalten : Form
    {
        private List<Thema> themen;
        Database database = new Database();

        public themenVerwalten()
        {
            InitializeComponent();
            refreshThemen();
        }

        private void deleteThemaButton_Click(object sender, EventArgs e)
        {
            Thema selT = (Thema)themenListBox.SelectedItem;
            themen.Remove(selT);
            themenListBox.DataSource = null;
            themenListBox.DataSource = themen;
            themenListBox.DisplayMember = "aufgabenName";
        }

        private void refreshThemen()
        {
            themen = new List<Thema>();
            foreach (string[] array in database.ExecuteQuery("select * from Thema"))
            {
                Thema thema = new Thema(Convert.ToInt32(array[0]), array[1]);
                themen.Add(thema);
            }
            themenListBox.DataSource = themen;
            themenListBox.DisplayMember = "aufgabenName";
        }

        private void addThemaButton_Click_1(object sender, EventArgs e)
        {
            Eingabe eingabe = new Eingabe();
            eingabe.textEingabe = new Eingabe.textEingabeHandler(eingabeF);
            eingabe.Show();
        }

        public void eingabeF(object sender)
        {
            database.ExecuteQuery("insert into Thema values(\"" + ((TextBox)sender).Text + "\")");
            refreshThemen();
        }
    }
}
