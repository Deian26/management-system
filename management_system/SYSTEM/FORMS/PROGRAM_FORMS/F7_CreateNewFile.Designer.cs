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
            this.F7_textBox_newFileName = new System.Windows.Forms.TextBox();
            this.F7_label_newFileNameLabel = new System.Windows.Forms.Label();
            this.F7_button_createNewFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // F7_textBox_newFileName
            // 
            this.F7_textBox_newFileName.Location = new System.Drawing.Point(242, 42);
            this.F7_textBox_newFileName.Margin = new System.Windows.Forms.Padding(4);
            this.F7_textBox_newFileName.Name = "F7_textBox_newFileName";
            this.F7_textBox_newFileName.Size = new System.Drawing.Size(457, 26);
            this.F7_textBox_newFileName.TabIndex = 0;
            // 
            // F7_label_newFileNameLabel
            // 
            this.F7_label_newFileNameLabel.AutoSize = true;
            this.F7_label_newFileNameLabel.Location = new System.Drawing.Point(13, 45);
            this.F7_label_newFileNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.F7_label_newFileNameLabel.Name = "F7_label_newFileNameLabel";
            this.F7_label_newFileNameLabel.Size = new System.Drawing.Size(225, 19);
            this.F7_label_newFileNameLabel.TabIndex = 1;
            this.F7_label_newFileNameLabel.Text = "#FILENAME_AND_EXTENSION#";
            // 
            // F7_button_createNewFile
            // 
            this.F7_button_createNewFile.Location = new System.Drawing.Point(527, 75);
            this.F7_button_createNewFile.Name = "F7_button_createNewFile";
            this.F7_button_createNewFile.Size = new System.Drawing.Size(172, 28);
            this.F7_button_createNewFile.TabIndex = 2;
            this.F7_button_createNewFile.Text = "#CREATE_NEW_FILE#";
            this.F7_button_createNewFile.UseVisualStyleBackColor = true;
            this.F7_button_createNewFile.Click += new System.EventHandler(this.F7_button_createNewFile_Click);
            // 
            // F7_CreateNewFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 122);
            this.Controls.Add(this.F7_button_createNewFile);
            this.Controls.Add(this.F7_label_newFileNameLabel);
            this.Controls.Add(this.F7_textBox_newFileName);
            this.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "F7_CreateNewFile";
            this.Text = "F7_CreateNewFile";
            this.Load += new System.EventHandler(this.F7_CreateNewFile_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox F7_textBox_newFileName;
        private System.Windows.Forms.Label F7_label_newFileNameLabel;
        private System.Windows.Forms.Button F7_button_createNewFile;
    }
}