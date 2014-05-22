using iTextSharp.text;
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

        public PdfArchivierung(Database database)
        {
            InitializeComponent();
            this.database = database;
        }

        private void buttonArchivieren_Click(object sender, EventArgs e)
        {
            using (MemoryStream myMemoryStream = new MemoryStream())
            {
                Document pdfArchiv = new Document();
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfArchiv, myMemoryStream);

                pdfArchiv.Open();

                Paragraph header = new Paragraph(new Phrase("Beleg - Archivierung")) { Alignment = 1, SpacingAfter = 15 };
                header.Font.SetStyle("underline");
                pdfArchiv.Add(header);

                /*PdfPTable table = new PdfPTable(2) { WidthPercentage = 60, HorizontalAlignment = 0 };
                PdfPCell head = new PdfPCell(new Phrase("Your Heading"));
                head.Colspan = 2;
                head.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                table.AddCell(head);
                table.AddCell(new PdfPCell(new Phrase("Beispieltext")));
                table.AddCell(new PdfPCell(new Phrase("Beispieltext")));
                table.AddCell(new PdfPCell(new Phrase("Beispieltext")));
                table.AddCell(new PdfPCell(new Phrase("Beispieltext")));
                table.AddCell(new PdfPCell(new Phrase("Beispieltext")));
                table.AddCell(new PdfPCell(new Phrase("Beispieltext")));
                table.AddCell(new PdfPCell(new Phrase("Beispieltext")));
                table.CompleteRow();
                pdfArchiv.Add(table);
                */
                
                foreach (string[] array in database.ExecuteQuery("select * from Beleg"))
                {
                    //pro Beleg auszuführender Code
                    //Beleg beleg = new Beleg(array[0], array[1], Convert.ToDateTime(array[2]), Convert.ToDateTime(array[3]), Convert.ToInt32(array[4]), Convert.ToInt32(array[5]), array[6]);
                    //speichern der belegspezifischen Daten in der PDF
                    //string kennung, string semester, DateTime startDatum, DateTime endDatum, int minM, int maxM
                    Paragraph belegabsatz = new Paragraph("Belegkennung: "+array[0]+"\nSemester: "+array[1]+"\nStartdatum: "+array[2]+"\nEnddatum: "+array[3]+"\nMinimale bis maximale Gruppengröße: "+array[4]+"-"+array[5]+ "\n");
                    pdfArchiv.Add(belegabsatz);
                    foreach (string[] info in database.ExecuteQuery("select * from Gruppe where Gruppenkennung in (select Gruppenkennung from Zuordnung_GruppeBeleg where Belegkennung=\"" + array[6] + "\")"))
                    {
                        Gruppe gtemp = new Gruppe(info[0], Convert.ToInt32(info[1]), info[2]);
                        gtemp.belegkennung = array[6];
                        //speichern der gruppenspezifischen Daten in der PDF
                        foreach (string[] info2 in database.ExecuteQuery("select * from Student where sNummer in (select sNummer from Zuordnung_GruppeStudent where Gruppenkennung=\"" + gtemp.gruppenKennung + "\")"))
                        {
                            Student stemp = new Student(info2[2], info2[1], info2[0], info2[3], info2[4]);
                            //speichern der studentenspezifischen Daten in der PDF
                            PdfPTable table = new PdfPTable(5) { WidthPercentage = 60, HorizontalAlignment = 0 };
                        }
                    }
                    pdfArchiv.NewPage();
                }
                
                //tbd
                //database.ExecuteQuery("delete * from ...");


                //PDF mit Inhalt füllen


                /*
                PdfPTable table = new PdfPTable(2) { WidthPercentage = 60, HorizontalAlignment = 0 };
                PdfPCell header = new PdfPCell(new Phrase("Your Heading"));
                header.Colspan = 2;
                header.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                //Int32[] widths = { 150, 100 };
                //table.SetWidths(widths);
                table.AddCell(header);
                table.AddCell(new PdfPCell(new Phrase("Beispieltext")));
                table.AddCell(new PdfPCell(new Phrase("Beispieltext")));
                table.AddCell(new PdfPCell(new Phrase("Beispieltext")));
                table.AddCell(new PdfPCell(new Phrase("Beispieltext")));
                table.AddCell(new PdfPCell(new Phrase("Beispieltext")));
                table.AddCell(new PdfPCell(new Phrase("Beispieltext")));
                table.AddCell(new PdfPCell(new Phrase("Beispieltext")));
                table.CompleteRow();
                pdfArchiv.Add(table);

                Paragraph p = new Paragraph("Ein weiterer Beispieltext.\r\nMit 2 Zeilen!");
                pdfArchiv.Add(p);

                */
                pdfArchiv.Close();

                byte[] content = myMemoryStream.ToArray();

                //PDF aus Stream schreiben
                FileStream fs = File.Create("archivierung.pdf");
                fs.Write(content, 0, (int)content.Length);

                fs.Close();
                myMemoryStream.Close();
                
                Process.Start("archivierung.pdf");
                //Application.Exit();
            }
        }
    }
}
