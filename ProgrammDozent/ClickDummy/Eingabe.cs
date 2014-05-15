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
    public partial class Eingabe : Form
    {

        public delegate void textEingabeHandler(object sender);
        public textEingabeHandler textEingabe;

        public Eingabe()
        {
            InitializeComponent();

        }

        private void eingabeButton_Click(object sender, EventArgs e)
        {
            if (textEingabe != null)
            {
                textEingabe(tboEingabe);
            }
            this.Close();
        }
    }
}
