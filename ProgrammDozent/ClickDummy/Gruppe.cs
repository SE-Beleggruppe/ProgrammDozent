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

        public Gruppe(string password, string belegkennung)
        {
            this.Password = password;
            this.Belegkennung = belegkennung;
            this.Studenten = new List<Student>();
        }

        public Gruppe(string kennung, int themennummer, string password)
        {
            GruppenKennung = kennung;
            ThemenNummer = themennummer;
            Password = password;
            Studenten = new List<Student>();
        }

        public void AddStudent(Student student)
        {
            if (this.Studenten == null) Studenten = new List<Student>();
            if (student != null) Studenten.Add(student);
        }
    }
}
