using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammDozent
{
    public class Gruppe
    {
        public string gruppenKennung { get; set; }
        public List<Student> studenten { get; set; }
        public int themenNummer { get; set; }
        public string password { get; set; }
        public string Belegkennung { get; set; }

        public Gruppe(string kennung, int themennummer, string password)
        {
            this.gruppenKennung = kennung;
            this.themenNummer = themenNummer;
            this.password = password;
            this.studenten = new List<Student>();
        }

        public void addMitglied(Student neu)
        {
            if (this.studenten == null) this.studenten = new List<Student>();
            this.studenten.Add(neu);
        }
    }
}
