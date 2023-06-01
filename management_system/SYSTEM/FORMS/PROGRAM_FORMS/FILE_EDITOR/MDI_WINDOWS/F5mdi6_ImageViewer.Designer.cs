namespace management_system
{
    partial class F5mdi6_ImageViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F5mdi6_ImageViewer));
            this.F5mdi6_pictureBox_image = new System.Windows.Forms.PictureBox();
            this.F5mdi6_toolStrip_image = new System.Windows.Forms.ToolStrip();
            this.F5mdi6_toolStripButton_moveImage = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.F5mdi6_pictureBox_image)).BeginInit();
            this.F5mdi6_toolStrip_image.SuspendLayout();
            this.SuspendLayout();
            // 
            // F5mdi6_pictureBox_image
            // 
            this.F5mdi6_pictureBox_image.Location = new System.Drawing.Point(12, 28);
            this.F5mdi6_pictureBox_image.Name = "F5mdi6_pictureBox_image";
            this.F5mdi6_pictureBox_image.Size = new System.Drawing.Size(776, 410);
            this.F5mdi6_pictureBox_image.TabIndex = 0;
            this.F5mdi6_pictureBox_image.TabStop = false;
            // 
            // F5mdi6_toolStrip_image
            // 
            this.F5mdi6_toolStrip_image.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.F5mdi6_toolStripButton_moveImage});
            this.F5mdi6_toolStrip_image.Location = new System.Drawing.Point(0, 0);
            this.F5mdi6_toolStrip_image.Name = "F5mdi6_toolStrip_image";
            this.F5mdi6_toolStrip_image.Size = new System.Drawing.Size(800, 25);
            this.F5mdi6_toolStrip_image.TabIndex = 1;
            this.F5mdi6_toolStrip_image.Text = "toolStrip1";
            // 
            // F5mdi6_toolStripButton_moveImage
            // 
            this.F5mdi6_toolStripButton_moveImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.F5mdi6_toolStripButton_moveImage.Image = ((System.Drawing.Image)(resources.GetObject("F5mdi6_toolStripButton_moveImage.Image")));
            this.F5mdi6_toolStripButton_moveImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.F5mdi6_toolStripButton_moveImage.Name = "F5mdi6_toolStripButton_moveImage";
            this.F5mdi6_toolStripButton_moveImage.Size = new System.Drawing.Size(149, 22);
            this.F5mdi6_toolStripButton_moveImage.Text = "Adăugare imagine în grup";
            // 
            // F5mdi6_ImageViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.F5mdi6_toolStrip_image);
            this.Controls.Add(this.F5mdi6_pictureBox_image);
            this.Name = "F5mdi6_ImageViewer";
            this.Text = "Vizualizator de imagini";
            this.Load += new System.EventHandler(this.F5mdi6_ImageViewer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.F5mdi6_pictureBox_image)).EndInit();
            this.F5mdi6_toolStrip_image.ResumeLayout(false);
            this.F5mdi6_toolStrip_image.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox F5mdi6_pictureBox_image;
        private System.Windows.Forms.ToolStrip F5mdi6_toolStrip_image;
        private System.Windows.Forms.ToolStripButton F5mdi6_toolStripButton_moveImage;
    }
}