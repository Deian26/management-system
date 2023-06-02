namespace management_system
{
    partial class F6_GeneralInformation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F6_GeneralInformation));
            this.F6_richTextBox_info = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // F6_richTextBox_info
            // 
            this.F6_richTextBox_info.Location = new System.Drawing.Point(12, 12);
            this.F6_richTextBox_info.Name = "F6_richTextBox_info";
            this.F6_richTextBox_info.Size = new System.Drawing.Size(960, 437);
            this.F6_richTextBox_info.TabIndex = 0;
            this.F6_richTextBox_info.Text = "";
            // 
            // F6_GeneralInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 461);
            this.Controls.Add(this.F6_richTextBox_info);
            this.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "F6_GeneralInformation";
            this.Text = "Info";
            this.Load += new System.EventHandler(this.F6_GeneralInformation_Load);
            this.Resize += new System.EventHandler(this.F6_GeneralInformation_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox F6_richTextBox_info;
    }
}