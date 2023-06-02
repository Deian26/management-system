namespace management_system
{
    partial class F7_CreateNewFile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F7_CreateNewFile));
            this.F7_textBox_newFileName = new System.Windows.Forms.TextBox();
            this.F7_label_newFileNameLabel = new System.Windows.Forms.Label();
            this.F7_button_createNewFile = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // F7_textBox_newFileName
            // 
            this.F7_textBox_newFileName.Location = new System.Drawing.Point(246, 42);
            this.F7_textBox_newFileName.Margin = new System.Windows.Forms.Padding(4);
            this.F7_textBox_newFileName.Name = "F7_textBox_newFileName";
            this.F7_textBox_newFileName.Size = new System.Drawing.Size(457, 26);
            this.F7_textBox_newFileName.TabIndex = 0;
            // 
            // F7_label_newFileNameLabel
            // 
            this.F7_label_newFileNameLabel.AutoSize = true;
            this.F7_label_newFileNameLabel.Location = new System.Drawing.Point(13, 30);
            this.F7_label_newFileNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.F7_label_newFileNameLabel.Name = "F7_label_newFileNameLabel";
            this.F7_label_newFileNameLabel.Size = new System.Drawing.Size(225, 38);
            this.F7_label_newFileNameLabel.TabIndex = 1;
            this.F7_label_newFileNameLabel.Text = "Nume și extensie\r\n(pentru fișiere locale):";
            // 
            // F7_button_createNewFile
            // 
            this.F7_button_createNewFile.Location = new System.Drawing.Point(483, 99);
            this.F7_button_createNewFile.Name = "F7_button_createNewFile";
            this.F7_button_createNewFile.Size = new System.Drawing.Size(220, 49);
            this.F7_button_createNewFile.TabIndex = 2;
            this.F7_button_createNewFile.Text = "Creare fișier local";
            this.F7_button_createNewFile.UseVisualStyleBackColor = true;
            this.F7_button_createNewFile.Click += new System.EventHandler(this.F7_button_createNewFile_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(17, 99);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(220, 49);
            this.button1.TabIndex = 3;
            this.button1.Text = "Creare tabel în baza de date";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // F7_CreateNewFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 160);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.F7_button_createNewFile);
            this.Controls.Add(this.F7_label_newFileNameLabel);
            this.Controls.Add(this.F7_textBox_newFileName);
            this.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "F7_CreateNewFile";
            this.Text = "Creare fișier nou";
            this.Load += new System.EventHandler(this.F7_CreateNewFile_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox F7_textBox_newFileName;
        private System.Windows.Forms.Label F7_label_newFileNameLabel;
        private System.Windows.Forms.Button F7_button_createNewFile;
        private System.Windows.Forms.Button button1;
    }
}