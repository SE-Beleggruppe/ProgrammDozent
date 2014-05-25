namespace ProgrammDozent
{
    partial class ThemenVerwalten
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
            this.themenListBox = new System.Windows.Forms.ListBox();
            this.newThemaButton = new System.Windows.Forms.Button();
            this.addThemaButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // themenListBox
            // 
            this.themenListBox.FormattingEnabled = true;
            this.themenListBox.Location = new System.Drawing.Point(13, 13);
            this.themenListBox.Name = "themenListBox";
            this.themenListBox.Size = new System.Drawing.Size(439, 199);
            this.themenListBox.TabIndex = 0;
            // 
            // newThemaButton
            // 
            this.newThemaButton.Location = new System.Drawing.Point(458, 42);
            this.newThemaButton.Name = "newThemaButton";
            this.newThemaButton.Size = new System.Drawing.Size(164, 23);
            this.newThemaButton.TabIndex = 2;
            this.newThemaButton.Text = "Ausgewähltes Thema löschen";
            this.newThemaButton.UseVisualStyleBackColor = true;
            this.newThemaButton.Click += new System.EventHandler(this.deleteThemaButton_Click);
            // 
            // addThemaButton
            // 
            this.addThemaButton.Location = new System.Drawing.Point(458, 13);
            this.addThemaButton.Name = "addThemaButton";
            this.addThemaButton.Size = new System.Drawing.Size(164, 23);
            this.addThemaButton.TabIndex = 3;
            this.addThemaButton.Text = "Neues Thema";
            this.addThemaButton.UseVisualStyleBackColor = true;
            this.addThemaButton.Click += new System.EventHandler(this.addThemaButton_Click_1);
            // 
            // ThemenVerwalten
            // 
            this.AcceptButton = this.newThemaButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 221);
            this.Controls.Add(this.addThemaButton);
            this.Controls.Add(this.newThemaButton);
            this.Controls.Add(this.themenListBox);
            this.Name = "ThemenVerwalten";
            this.Text = "themenVerwalten";
            this.Load += new System.EventHandler(this.ThemenVerwalten_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox themenListBox;
        private System.Windows.Forms.Button newThemaButton;
        private System.Windows.Forms.Button addThemaButton;
    }
}