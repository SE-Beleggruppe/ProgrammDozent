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
    public partial class kontaktForm : Form
    {

        // member
        List<Beleg> Belege = new List<Beleg>();
        List<Gruppe> Gruppen = new List<Gruppe>();
        List<Thema> Themen = new List<Thema>();
        List<Rolle> Rollen = new List<Rolle>();
        List<Student> tempStudent;
        Database database = new Database();

        public kontaktForm()
        {
            InitializeComponent();

            updateBelegData();

            updateThemenData();

            updateGroupData();

            updateRollenData();

            updateFilterBtn();
        }

        private void updateBelegData()
        {
            /*
             * fill Beleg combo box
             * first item is '*'
             */
            Beleg dummyBeleg = new Beleg("*", "", DateTime.Today, DateTime.Today, 0, 0, "");
            Belege.Add(dummyBeleg);

            //comboBoxBeleg.Items.Add()
            foreach (string[] array in database.ExecuteQuery("select * from Beleg"))
            {
                Beleg beleg = new Beleg(array[0], array[1], Convert.ToDateTime(array[2]), Convert.ToDateTime(array[3]), Convert.ToInt32(array[4]), Convert.ToInt32(array[5]), array[6]);
                Belege.Add(beleg);
            }
            comboBoxBeleg.DataSource = Belege;
            comboBoxBeleg.DisplayMember = "BelegKennung";
        }

        private void updateThemenData()
        {
            /*
            * fill 'Thema' combo box
            * first item is '*'
            */
            if (comboBoxBeleg.SelectedItem == null || comboBoxBeleg.SelectedIndex == 0)
            {
                comboBoxBelegthema.DataSource = null;
                comboBoxBelegthema.Items.Clear();
                comboBoxBelegthema.Enabled = false;
                return;
            }
            comboBoxBelegthema.Enabled = true;


            Thema dummyThema = new Thema(9999, "*");
            Themen.Add(dummyThema);

            foreach (string[] array in database.ExecuteQuery("select * from Thema"))
            {
                Thema thema = new Thema(Convert.ToInt32(array[0]), array[1]);
                Themen.Add(thema);
            }

            comboBoxBelegthema.DataSource = Themen;
            comboBoxBelegthema.DisplayMember = "aufgabenName";
        }

        private void updateGroupData()
        {
            /*
             * fill 'Gruppen' combo box
             * first item is '*'
             */
            if (comboBoxBelegthema.SelectedItem == null || comboBoxBelegthema.SelectedIndex == 0)
            {
                comboBoxGruppe.DataSource = null;
                comboBoxGruppe.Items.Clear();
                comboBoxGruppe.Enabled = false;
                return;
            }
            comboBoxGruppe.Enabled = true;
            Beleg selected = (Beleg)comboBoxBeleg.SelectedItem;

            foreach (string[] info in database.ExecuteQuery("select * from Gruppe where Gruppenkennung in (select Gruppenkennung from Zuordnung_GruppeBeleg where Belegkennung=\"" + selected.BelegKennung + "\")"))
            {
                Gruppe temp = new Gruppe(info[0], Convert.ToInt32(info[1]), info[2]);
                temp.Belegkennung = selected.BelegKennung;
                Gruppen.Add(temp);
            }
            comboBoxGruppe.DataSource = null;
            comboBoxGruppe.DataSource = Gruppen;
            comboBoxGruppe.DisplayMember = "gruppenKennung";
        }

        private void updateRollenData()
        {

            foreach (string[] array in database.ExecuteQuery("select * from Rolle"))
            {
                Rolle rolle = new Rolle(array[0]);
                Rollen.Add(rolle);
            }
            comboBoxRolle.DataSource = Rollen;
            comboBoxRolle.DisplayMember = "rolle";
        }

        private void updateFilterBtn() {
            if ((comboBoxBelegthema.SelectedItem == null || comboBoxBelegthema.SelectedIndex == 0) ||
                (comboBoxBelegthema.SelectedItem == null || comboBoxBelegthema.SelectedIndex == 0))
            {
                btnFilter.Enabled = false;
                return;
            }
            btnFilter.Enabled = true;
            
        }

        private void comboBoxBeleg_SelectedIndexChanged(object sender, EventArgs e)
        {
            // refresh 'themen' information
            updateThemenData();
        }


    }
}
