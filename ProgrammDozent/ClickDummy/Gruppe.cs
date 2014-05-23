using System.Collections.Generic;

namespace ProgrammDozent
{
    public class Gruppe
    {
        public string GruppenKennung { get; set; }
        public List<Student> Studenten { get; set; }
        public int ThemenNummer { get; set; }
        public string Password { get; set; }
        public string Belegkennung { get; set; }

        public Gruppe(string kennung, int themennummer, string password)
        {
            GruppenKennung = kennung;
            ThemenNummer = ThemenNummer;
            Password = password;
            Studenten = new List<Student>();
        }

        public void AddStudent(Student student)
        {
            if (Studenten == null) Studenten = new List<Student>();
            if (student != null) Studenten.Add(student);
        }
    }
}
