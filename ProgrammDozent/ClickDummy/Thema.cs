
namespace ProgrammDozent
{
    public class Thema
    {
        public int ThemenNummer { get; set; }
        public string AufgabenName { get; set; }

        public Thema(int themenNummer, string aufgabe)
        {
            AufgabenName = aufgabe;
            ThemenNummer = themenNummer;
        }
    }
}
