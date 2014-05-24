namespace ProgrammDozent
{
    partial class PdfArchivierung
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonArchivieren = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonArchivieren
            // 
            this.buttonArchivieren.Location = new System.Drawing.Point(81, 102);
            this.buttonArchivieren.Name = "buttonArchivieren";
            this.buttonArchivieren.Size = new System.Drawing.Size(116, 23);
            this.buttonArchivieren.TabIndex = 0;
            this.buttonArchivieren.Text = "Belege archivieren";
            this.buttonArchivieren.UseVisualStyleBackColor = true;
            this.buttonArchivieren.Click += new System.EventHandler(this.buttonArchivieren_Click);
            // 
            // PdfArchivierung
            // 
            this.AcceptButton = this.buttonArchivieren;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.buttonArchivieren);
            this.Name = "PdfArchivierung";
            this.Text = "PdfArchivierung";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonArchivieren;
    }
}