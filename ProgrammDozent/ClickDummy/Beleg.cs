﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammDozent
{
    public class Beleg
    {
        public string semester { get; set; }
        public string passwort { get; set; }
        public string belegKennung { get; set; }
        public string dozent { get; set; }
        public DateTime startDatum { get; set; }
        public DateTime endDatum { get; set; }
        public int minMitglieder { get; set; }
        public int maxMitglieder { get; set; }
        public List<Thema> themen;
        public List<string> cases;

        public List<Gruppe>  gruppen = new List<Gruppe>(); 

        public Beleg(string kennung, string semester, DateTime startDatum, DateTime endDatum, int minM, int maxM, string passwort)
        {
            this.belegKennung = kennung; // Automatisch generieren
            this.passwort = passwort;
            this.semester = semester;
            this.dozent = dozent;
            this.startDatum = startDatum;
            this.endDatum = endDatum;
            this.minMitglieder = minM;
            this.maxMitglieder = maxM;


            Student leiter = new Student("Herzog", "Benjamin", "s12345", "benjamin@herzog.de", "CTO");
            Student stud1 = new Student("Noack", "Markus", "s23456", "noack@markus.de", "Praktikant");
            Student stud2 = new Student("Jobs", "Steve", "s34567", "steve@jobs.com", "CEO");
            Student stud3 = new Student("Gates", "Bill", "s45678", "bill@gates.com", "Programmierer");
            List<Student> mitglieder = new List<Student> {leiter, stud1, stud2, stud3};
            Thema thema = new Thema(1,"Ich bin eine Aufgabe");
            this.gruppen.Add(new Gruppe("Kennung01",leiter,mitglieder,thema,"password"));

            this.themen = new List<Thema>();
            this.themen.Add(new Thema(1,"Dies ist eine tolle Aufgabe"));
        }

        public void addGruppe(Gruppe gruppe)
        {
            this.gruppen.Add(gruppe);
        }
    }
}
