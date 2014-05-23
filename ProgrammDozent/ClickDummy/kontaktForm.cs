using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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

        // member for filter funct
        List<Gruppe> filterGroups = new List<Gruppe>();
        List<Student> filterStudents = new List<Student>();

        Beleg selBeleg;
        Gruppe selGruppe;
        Thema selThema;
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
            comboBoxBelegthema.DataSource = null;
            comboBoxBelegthema.Items.Clear();
            Themen.Clear();

            if (comboBoxBeleg.SelectedItem == null || comboBoxBeleg.SelectedIndex == 0)
            {
                comboBoxBelegthema.Enabled = false;
                return;
            }
            comboBoxBelegthema.Enabled = true;


            Thema dummyThema = new Thema(9999, "*");
            Themen.Add(dummyThema);

            Beleg selectedBeleg = (Beleg)comboBoxBeleg.SelectedItem;
            selBeleg = selectedBeleg;


            foreach (string[] array in database.ExecuteQuery("select * from Thema where Themennummer in (select Themennummer from Zuordnung_BelegThema where Belegkennung=\"" + selectedBeleg.BelegKennung + "\")"))
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
            if ((comboBoxBeleg.SelectedItem == null || comboBoxBeleg.SelectedIndex == 0) ||
                (comboBoxBelegthema.SelectedItem == null || comboBoxBelegthema.SelectedIndex == 0))
            {
                btnFilter.Enabled = false;
                return;
            }
            btnFilter.Enabled = true;
            
            /*
             * information we have: 
             *  - Beleg
             *  - Belegthema
             *  
             * information we __need__:
             *  - Gruppen[] which match Beleg and Belegthema
             *  - Rollen[] which match Beleg and Belegthema
             */



            // lets query all groups...
            foreach (var groupData in database.ExecuteQuery("select * from Gruppe where Themennummer=" + selThema.ThemenNummer + " and Gruppenkennung in (select Gruppenkennung from Zuordnung_GruppeBeleg where Belegkennung=\"" + selBeleg.BelegKennung + "\")"))
            {
                Gruppe temp = new Gruppe(groupData[0], Convert.ToInt32(groupData[1]), groupData[2]);
                temp.Belegkennung = selBeleg.BelegKennung;
                filterGroups.Add(temp);
            }

            
            if ((comboBoxGruppe.SelectedItem == null || comboBoxGruppe.SelectedIndex == 0) ||
               (comboBoxRolle.SelectedItem == null || comboBoxRolle.SelectedIndex == 0))
            {
                foreach (var group in filterGroups)
                {
                    foreach (var info2 in database.ExecuteQuery("select * from Student where sNummer in (select sNummer from Zuordnung_GruppeStudent where Gruppenkennung=\"" + group.GruppenKennung + "\")"))
                    {
                        Student tmpStud = new Student(info2[2], info2[1], info2[0], info2[3], info2[4]);
                        filterStudents.Add(tmpStud);
                    }
                }

            }
            
        }

        private void comboBoxBeleg_SelectedIndexChanged(object sender, EventArgs e)
        {
            // refresh 'themen' information
            updateThemenData();
        }

        private void comboBoxBelegthema_SelectedIndexChanged(object sender, EventArgs e)
        {
            Thema tmpTema = (Thema)comboBoxBelegthema.SelectedItem;
            selThema = tmpTema;

            // enable filter btn
            updateFilterBtn();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            String mailString = "";
 
            foreach(var studi in filterStudents) {
                
                mailString = String.Concat(mailString,studi.Mail,',');
            }
          
            Process.Start("mailto: " + mailString + "?subject="+selBeleg.BelegKennung);
        }


    }
}
