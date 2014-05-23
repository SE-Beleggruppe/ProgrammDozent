using System.Collections.Generic;

namespace ProgrammDozent
{
    public class Gruppe
    {
        public string GruppenKennung { get; set; }
        public List<Student> studenten { get; set; }
        public int ThemenNummer { get; set; }
        public string Password { get; set; }
        public string Belegkennung { get; set; }

        public Gruppe(string kennung, int themennummer, string password)
        {
            GruppenKennung = kennung;
            ThemenNummer = ThemenNummer;
            Password = password;
            studenten = new List<Student>();
        }

        public void addStudent(Student student)
        {
            if (studenten == null) studenten = new List<Student>();
            if (student != null) studenten.Add(student);
        }
    }
}
