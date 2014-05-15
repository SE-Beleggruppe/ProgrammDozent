namespace ProgrammDozent
{
    partial class belegBearbeiten
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
            this.kennungTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.semesterTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.startDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.endDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.verThemen = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.allThemen = new System.Windows.Forms.ListBox();
            this.addButton = new System.Windows.Forms.Button();
            this.remButton = new System.Windows.Forms.Button();
            this.speichernbutton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.minGR = new System.Windows.Forms.NumericUpDown();
            this.maxGR = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.allRollen = new System.Windows.Forms.ListBox();
            this.verRollen = new System.Windows.Forms.ListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.passwortTextBox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.minGR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxGR)).BeginInit();
            this.SuspendLayout();
            // 
            // kennungTextBox
            // 
            this.kennungTextBox.Location = new System.Drawing.Point(196, 15);
            this.kennungTextBox.Name = "kennungTextBox";
            this.kennungTextBox.Size = new System.Drawing.Size(200, 20);
            this.kennungTextBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Kennung";
            // 
            // semesterTextBox
            // 
            this.semesterTextBox.Location = new System.Drawing.Point(196, 41);
            this.semesterTextBox.Name = "semesterTextBox";
            this.semesterTextBox.Size = new System.Drawing.Size(200, 20);
            this.semesterTextBox.TabIndex = 5;
            this.semesterTextBox.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Semester";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Start-Datum";
            // 
            // startDateTimePicker
            // 
            this.startDateTimePicker.Location = new System.Drawing.Point(195, 97);
            this.startDateTimePicker.Name = "startDateTimePicker";
            this.startDateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.startDateTimePicker.TabIndex = 7;
            // 
            // endDateTimePicker
            // 
            this.endDateTimePicker.Location = new System.Drawing.Point(195, 123);
            this.endDateTimePicker.Name = "endDateTimePicker";
            this.endDateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.endDateTimePicker.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "End-Datum";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 155);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Mindest-Gruppengröße";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 181);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(122, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Maximale Gruppengröße";
            // 
            // verThemen
            // 
            this.verThemen.FormattingEnabled = true;
            this.verThemen.Location = new System.Drawing.Point(13, 238);
            this.verThemen.Name = "verThemen";
            this.verThemen.Size = new System.Drawing.Size(154, 108);
            this.verThemen.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 207);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Verfügbare Themen";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(272, 210);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Alle Themen";
            // 
            // allThemen
            // 
            this.allThemen.FormattingEnabled = true;
            this.allThemen.Location = new System.Drawing.Point(246, 241);
            this.allThemen.Name = "allThemen";
            this.allThemen.Size = new System.Drawing.Size(149, 108);
            this.allThemen.TabIndex = 17;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(173, 265);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(67, 23);
            this.addButton.TabIndex = 18;
            this.addButton.Text = "<<";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // remButton
            // 
            this.remButton.Location = new System.Drawing.Point(173, 294);
            this.remButton.Name = "remButton";
            this.remButton.Size = new System.Drawing.Size(67, 23);
            this.remButton.TabIndex = 19;
            this.remButton.Text = ">>";
            this.remButton.UseVisualStyleBackColor = true;
            this.remButton.Click += new System.EventHandler(this.remButton_Click);
            // 
            // speichernbutton
            // 
            this.speichernbutton.Location = new System.Drawing.Point(12, 627);
            this.speichernbutton.Name = "speichernbutton";
            this.speichernbutton.Size = new System.Drawing.Size(75, 23);
            this.speichernbutton.TabIndex = 20;
            this.speichernbutton.Text = "Speichern";
            this.speichernbutton.UseVisualStyleBackColor = true;
            this.speichernbutton.Click += new System.EventHandler(this.speichernbutton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(320, 630);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 21;
            this.cancelButton.Text = "Abbrechen";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // minGR
            // 
            this.minGR.Location = new System.Drawing.Point(195, 158);
            this.minGR.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.minGR.Name = "minGR";
            this.minGR.Size = new System.Drawing.Size(120, 20);
            this.minGR.TabIndex = 22;
            // 
            // maxGR
            // 
            this.maxGR.Location = new System.Drawing.Point(196, 187);
            this.maxGR.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.maxGR.Name = "maxGR";
            this.maxGR.Size = new System.Drawing.Size(120, 20);
            this.maxGR.TabIndex = 23;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(174, 425);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(67, 23);
            this.button1.TabIndex = 27;
            this.button1.Text = ">>";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(174, 396);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(67, 23);
            this.button2.TabIndex = 26;
            this.button2.Text = "<<";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // allRollen
            // 
            this.allRollen.FormattingEnabled = true;
            this.allRollen.Location = new System.Drawing.Point(247, 372);
            this.allRollen.Name = "allRollen";
            this.allRollen.Size = new System.Drawing.Size(149, 108);
            this.allRollen.TabIndex = 25;
            // 
            // verRollen
            // 
            this.verRollen.FormattingEnabled = true;
            this.verRollen.Location = new System.Drawing.Point(14, 369);
            this.verRollen.Name = "verRollen";
            this.verRollen.Size = new System.Drawing.Size(154, 108);
            this.verRollen.TabIndex = 24;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 494);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(91, 13);
            this.label9.TabIndex = 28;
            this.label9.Text = "Verfügbare Cases";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(246, 497);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 13);
            this.label10.TabIndex = 29;
            this.label10.Text = "Alle Cases";
            // 
            // passwortTextBox
            // 
            this.passwortTextBox.Location = new System.Drawing.Point(195, 67);
            this.passwortTextBox.Name = "passwortTextBox";
            this.passwortTextBox.PasswordChar = '*';
            this.passwortTextBox.Size = new System.Drawing.Size(200, 20);
            this.passwortTextBox.TabIndex = 35;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(10, 67);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(77, 13);
            this.label14.TabIndex = 34;
            this.label14.Text = "Start-Kennwort";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(246, 356);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(57, 13);
            this.label15.TabIndex = 41;
            this.label15.Text = "Alle Rollen";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(13, 353);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(92, 13);
            this.label16.TabIndex = 40;
            this.label16.Text = "Verfügbare Rollen";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(173, 566);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(67, 23);
            this.button3.TabIndex = 39;
            this.button3.Text = ">>";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(173, 537);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(67, 23);
            this.button4.TabIndex = 38;
            this.button4.Text = "<<";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(246, 513);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(149, 108);
            this.listBox1.TabIndex = 37;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(13, 510);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(154, 108);
            this.listBox2.TabIndex = 36;
            // 
            // belegBearbeiten
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 658);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.passwortTextBox);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.allRollen);
            this.Controls.Add(this.verRollen);
            this.Controls.Add(this.maxGR);
            this.Controls.Add(this.minGR);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.speichernbutton);
            this.Controls.Add(this.remButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.allThemen);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.verThemen);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.endDateTimePicker);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.startDateTimePicker);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.semesterTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.kennungTextBox);
            this.Controls.Add(this.label1);
            this.Name = "belegBearbeiten";
            this.Text = "belegBearbeiten";
            this.Load += new System.EventHandler(this.belegBearbeiten_Load);
            ((System.ComponentModel.ISupportInitialize)(this.minGR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxGR)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox kennungTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox semesterTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker startDateTimePicker;
        private System.Windows.Forms.DateTimePicker endDateTimePicker;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox verThemen;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListBox allThemen;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button remButton;
        private System.Windows.Forms.Button speichernbutton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.NumericUpDown minGR;
        private System.Windows.Forms.NumericUpDown maxGR;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListBox allRollen;
        private System.Windows.Forms.ListBox verRollen;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox passwortTextBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox listBox2;
    }
}