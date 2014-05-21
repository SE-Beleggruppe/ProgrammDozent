using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        public PdfArchivierung()
        {
            InitializeComponent();
        }

        private void buttonArchivieren_Click(object sender, EventArgs e)
        {
            using (MemoryStream myMemoryStream = new MemoryStream())
            {
                Document myDocument = new Document();
                PdfWriter myPDFWriter = PdfWriter.GetInstance(myDocument, myMemoryStream);

                myDocument.Open();

                //PDF mit Inhalt füllen
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
                myDocument.Add(table);

                Paragraph p = new Paragraph("Ein weiterer Beispieltext.\r\nMit 2 Zeilen!");
                myDocument.Add(p);

                myDocument.Close();

                byte[] content = myMemoryStream.ToArray();

                //PDF aus Stream schreiben
                FileStream fs = File.Create("archivierung.pdf");
                fs.Write(content, 0, (int)content.Length);
                myDocument.Close();
            }
        }
    }
}
