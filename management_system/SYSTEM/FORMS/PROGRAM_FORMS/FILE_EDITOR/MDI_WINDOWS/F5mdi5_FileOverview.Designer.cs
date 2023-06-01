namespace management_system
{
    partial class F5mdi5_FileOverview
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
            this.F5mdi5_richTextBox_fileOverview = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // F5mdi5_richTextBox_fileOverview
            // 
            this.F5mdi5_richTextBox_fileOverview.Location = new System.Drawing.Point(12, 12);
            this.F5mdi5_richTextBox_fileOverview.Name = "F5mdi5_richTextBox_fileOverview";
            this.F5mdi5_richTextBox_fileOverview.Size = new System.Drawing.Size(890, 297);
            this.F5mdi5_richTextBox_fileOverview.TabIndex = 0;
            this.F5mdi5_richTextBox_fileOverview.Text = "";
            // 
            // F5mdi5_FileOverview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 311);
            this.Controls.Add(this.F5mdi5_richTextBox_fileOverview);
            this.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "F5mdi5_FileOverview";
            this.Text = "Informații fișier";
            this.Load += new System.EventHandler(this.F5mdi5_FileOverview_Load);
            this.Resize += new System.EventHandler(this.F5mdi5_FileOverview_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox F5mdi5_richTextBox_fileOverview;
    }
}