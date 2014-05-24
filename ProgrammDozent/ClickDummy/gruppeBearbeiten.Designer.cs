namespace ProgrammDozent
{
    partial class gruppeBearbeiten
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
            this.cancelButton = new System.Windows.Forms.Button();
            this.speichernbutton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.leiterLabel = new System.Windows.Forms.Label();
            this.contactLeiterButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.themenComboBox = new System.Windows.Forms.ComboBox();
            this.passwortTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.kennungComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(260, 140);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 41;
            this.cancelButton.Text = "Abbrechen";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // speichernbutton
            // 
            this.speichernbutton.Location = new System.Drawing.Point(15, 140);
            this.speichernbutton.Name = "speichernbutton";
            this.speichernbutton.Size = new System.Drawing.Size(75, 23);
            this.speichernbutton.TabIndex = 40;
            this.speichernbutton.Text = "Speichern";
            this.speichernbutton.UseVisualStyleBackColor = true;
            this.speichernbutton.Click += new System.EventHandler(this.speichernbutton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "Leiter";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Kennung";
            // 
            // leiterLabel
            // 
            this.leiterLabel.AutoSize = true;
            this.leiterLabel.Location = new System.Drawing.Point(134, 41);
            this.leiterLabel.Name = "leiterLabel";
            this.leiterLabel.Size = new System.Drawing.Size(111, 13);
            this.leiterLabel.TabIndex = 42;
            this.leiterLabel.Text = "MAX MUSTERMANN";
            // 
            // contactLeiterButton
            // 
            this.contactLeiterButton.Location = new System.Drawing.Point(260, 36);
            this.contactLeiterButton.Name = "contactLeiterButton";
            this.contactLeiterButton.Size = new System.Drawing.Size(75, 23);
            this.contactLeiterButton.TabIndex = 43;
            this.contactLeiterButton.Text = "Kontaktieren";
            this.contactLeiterButton.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 44;
            this.label3.Text = "Belegthema";
            // 
            // themenComboBox
            // 
            this.themenComboBox.FormattingEnabled = true;
            this.themenComboBox.Location = new System.Drawing.Point(134, 65);
            this.themenComboBox.Name = "themenComboBox";
            this.themenComboBox.Size = new System.Drawing.Size(200, 21);
            this.themenComboBox.TabIndex = 45;
            // 
            // passwortTextBox
            // 
            this.passwortTextBox.Location = new System.Drawing.Point(134, 101);
            this.passwortTextBox.Name = "passwortTextBox";
            this.passwortTextBox.Size = new System.Drawing.Size(200, 20);
            this.passwortTextBox.TabIndex = 47;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 46;
            this.label4.Text = "Passwort";
            // 
            // kennungComboBox
            // 
            this.kennungComboBox.FormattingEnabled = true;
            this.kennungComboBox.Location = new System.Drawing.Point(134, 6);
            this.kennungComboBox.Name = "kennungComboBox";
            this.kennungComboBox.Size = new System.Drawing.Size(200, 21);
            this.kennungComboBox.TabIndex = 48;
            // 
            // gruppeBearbeiten
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 183);
            this.Controls.Add(this.kennungComboBox);
            this.Controls.Add(this.passwortTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.themenComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.contactLeiterButton);
            this.Controls.Add(this.leiterLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.speichernbutton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "gruppeBearbeiten";
            this.Text = "gruppeBearbeiten";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button speichernbutton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label leiterLabel;
        private System.Windows.Forms.Button contactLeiterButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox themenComboBox;
        private System.Windows.Forms.TextBox passwortTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox kennungComboBox;
    }
}