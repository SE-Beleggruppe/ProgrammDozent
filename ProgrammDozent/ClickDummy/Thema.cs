using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammDozent
{
    public class Thema
    {
        public int themenNummer { get; set; }
        public string aufgabenName { get; set; }

        public Thema(int themenNummer, string aufgabe)
        {
            this.aufgabenName = aufgabe;
            this.themenNummer = themenNummer;
        }
    }
}
