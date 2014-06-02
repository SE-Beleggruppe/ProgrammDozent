﻿using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgrammDozent
{
    public partial class PdfArchivierung : Form
    {
        Database database = new Database();

        List<Beleg> Belege = new List<Beleg>();
        List<Gruppe> Gruppen = new List<Gruppe>();
        List<string> rollen = new List<string>();
        List<Student> tempStudent;
        public string semester;
        public string speicherPfad;

        public PdfArchivierung(Database database)
        {
            InitializeComponent();
            this.Text = "Archivierung";
            this.StartPosition = FormStartPosition.CenterScreen;

            this.database = database;
            this.semester = database.ExecuteQuery("select Semester from Beleg").ToString();
        }

        private void buttonArchivieren_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog pfadDialog = new FolderBrowserDialog();
            if (pfadDialog.ShowDialog() != DialogResult.Cancel)
            {
                speicherPfad = pfadDialog.SelectedPath;
                using (MemoryStream myMemoryStream = new MemoryStream())
                {
                    Document pdfArchiv = new Document();
                    PdfWriter pdfWriter = PdfWriter.GetInstance(pdfArchiv, myMemoryStream);

                    pdfArchiv.Open();

                    Paragraph header = new Paragraph(new Phrase("Beleg - Archivierung " + semester)) { Alignment = 1, SpacingAfter = 15 };
                    header.Font.SetStyle("underline");
                    pdfArchiv.Add(header);


                    foreach (string[] array in database.ExecuteQuery("select * from Beleg"))
                    {
                        //pro Beleg auszuführender Code
                        //Beleg beleg = new Beleg(array[0], array[1], Convert.ToDateTime(array[2]), Convert.ToDateTime(array[3]), Convert.ToInt32(array[4]), Convert.ToInt32(array[5]), array[6]);
                        //speichern der belegspezifischen Daten in der PDF
                        //string kennung, string semester, DateTime startDatum, DateTime endDatum, int minM, int maxM
                        Paragraph belegabsatz = new Paragraph("Belegkennung: " + array[0] + "\nSemester: " + array[1] + "\nStartdatum: " + array[2] + "\nEnddatum: " + array[3] + "\nMinimale bis maximale Gruppengröße: " + array[4] + "-" + array[5] + "\n");
                        pdfArchiv.Add(belegabsatz);
                        //semester = array[1];
                        PdfPTable nestedtable = new PdfPTable(2) { WidthPercentage = 90, HorizontalAlignment = 0, SpacingBefore = 10, SpacingAfter = 10 };

                        PdfPTable roletable = new PdfPTable(1) { WidthPercentage = 40, HorizontalAlignment = 0 };
                        roletable.AddCell(new PdfPCell(new Phrase("Verfügbare Rollen: ")));
                        foreach (string[] roles in database.ExecuteQuery("select Rolle from Zuordnung_BelegRolle Z where Z.Belegkennung=\"" + array[0] + "\""))
                        {
                            Phrase roletempphrase = new Phrase(roles[0]);
                            roletempphrase.Font.SetStyle("italic");
                            roletempphrase.Font.Size = 11.0f;
                            roletable.AddCell(new PdfPCell(roletempphrase));
                        }
                        nestedtable.AddCell(roletable);

                        PdfPTable topictable = new PdfPTable(1) { WidthPercentage = 40, HorizontalAlignment = 0 };
                        topictable.AddCell(new PdfPCell(new Phrase("Verfügbare Themen: ")));
                        foreach (string[] topics in database.ExecuteQuery("select Aufgabe from Zuordnung_BelegThema Z, Thema T where Z.Belegkennung=\"" + array[0] + "\" and Z.Themennummer=T.Themennummer"))
                        {
                            Phrase topictempphrase = new Phrase(topics[0]);
                            topictempphrase.Font.SetStyle("italic");
                            topictempphrase.Font.Size = 11.0f;
                            topictable.AddCell(new PdfPCell(topictempphrase));
                        }
                        nestedtable.AddCell(topictable);
                        pdfArchiv.Add(nestedtable);
                        pdfArchiv.NewPage();

                        foreach (string[] info in database.ExecuteQuery("select * from Gruppe where Gruppenkennung in (select Gruppenkennung from Zuordnung_GruppeBeleg where Belegkennung=\"" + array[0] + "\")"))
                        {
                            Phrase ptmp = new Phrase(info[0]);
                            ptmp.Font.SetStyle("bold");
                            PdfPTable grptable = new PdfPTable(5) { SpacingAfter = 15, HorizontalAlignment = 0, WidthPercentage = 100 };
                            grptable.SetWidths(new float[] { 20, 20, 10, 30, 20 });
                            PdfPCell grptableHeader = new PdfPCell(ptmp);
                            grptableHeader.Colspan = 2;
                            grptableHeader.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                            grptable.AddCell(grptableHeader);
                            ptmp = new Phrase(database.ExecuteQuery("select Aufgabe from Thema where Themennummer=" + info[1])[0][0]);
                            ptmp.Font.SetStyle("bold");
                            grptableHeader = new PdfPCell(ptmp);
                            grptableHeader.Colspan = 3;
                            grptableHeader.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                            grptable.AddCell(grptableHeader);

                            //speichern der gruppenspezifischen Daten in der PDF
                            foreach (string[] info2 in database.ExecuteQuery("select * from Student where sNummer in (select sNummer from Zuordnung_GruppeStudent where Gruppenkennung=\"" + info[0] + "\")"))
                            {
                                ptmp = new Phrase(info2[2]);
                                ptmp.Font.Size = 11.0f;
                                ptmp.Font.SetStyle("italic");
                                grptable.AddCell(new PdfPCell(ptmp));
                                ptmp = new Phrase(info2[1]);
                                ptmp.Font.Size = 11.0f;
                                ptmp.Font.SetStyle("italic");
                                grptable.AddCell(new PdfPCell(ptmp));
                                ptmp = new Phrase(info2[0]);
                                ptmp.Font.Size = 11.0f;
                                ptmp.Font.SetStyle("italic");
                                grptable.AddCell(new PdfPCell(ptmp));
                                ptmp = new Phrase(info2[3]);
                                ptmp.Font.Size = 11.0f;
                                ptmp.Font.SetStyle("italic");
                                grptable.AddCell(new PdfPCell(ptmp));
                                ptmp = new Phrase(info2[4]);
                                ptmp.Font.Size = 11.0f;
                                ptmp.Font.SetStyle("italic");
                                grptable.AddCell(new PdfPCell(ptmp));
                            }
                            pdfArchiv.Add(grptable);
                        }
                        pdfArchiv.NewPage();
                    }

                    pdfArchiv.Close();

                    byte[] content = myMemoryStream.ToArray();

                    //PDF aus Stream schreiben
                    FileStream fs = File.Create(speicherPfad + "\\archivierung" + semester + ".pdf");
                    fs.Write(content, 0, (int)content.Length);

                    fs.Close();
                    myMemoryStream.Close();

                    Process.Start(speicherPfad + "\\archivierung" + semester + ".pdf");

                    //Tabellen reinigen
                    database.ExecuteQuery("delete from Student");
                    database.ExecuteQuery("delete from Zuordnung_GruppeStudent");
                    database.ExecuteQuery("delete from Gruppe");
                    database.ExecuteQuery("delete from Zuordnung_GruppeBeleg");
                    database.ExecuteQuery("delete from Beleg");
                    database.ExecuteQuery("delete from Zuordnung_BelegThema");
                    database.ExecuteQuery("delete from Zuordnung_BelegRolle");
                    database.ExecuteQuery("delete from Zuordnung_BelegCases");


                    this.Close();
                }
            }

            else
            {
                speicherPfad = "";
            }
        }

        private void abbrechenButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
