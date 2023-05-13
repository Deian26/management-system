namespace management_system
{
    partial class F5_FileEditorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F5_FileEditorForm));
            this.F5_toolStrip_fileEditorForm = new System.Windows.Forms.ToolStrip();
            this.F5_toolStripDropDownButton_fileButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.F5_ToolStripMenuItem_openFile = new System.Windows.Forms.ToolStripMenuItem();
            this.F5_toolStripSeparator_separator1 = new System.Windows.Forms.ToolStripSeparator();
            this.F5_statusStrip_fileEditorForm = new System.Windows.Forms.StatusStrip();
            this.F5mdi1_toolStripStatusLabel_connectedDatabaseName = new System.Windows.Forms.ToolStripStatusLabel();
            this.F5mdi1_toolStripProgressBar_databaseSyncProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.F5_openFileDialog_openFile = new System.Windows.Forms.OpenFileDialog();
            this.F5_toolStrip_fileEditorForm.SuspendLayout();
            this.F5_statusStrip_fileEditorForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // F5_toolStrip_fileEditorForm
            // 
            this.F5_toolStrip_fileEditorForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.F5_toolStripDropDownButton_fileButton,
            this.F5_toolStripSeparator_separator1});
            this.F5_toolStrip_fileEditorForm.Location = new System.Drawing.Point(0, 0);
            this.F5_toolStrip_fileEditorForm.Name = "F5_toolStrip_fileEditorForm";
            this.F5_toolStrip_fileEditorForm.Size = new System.Drawing.Size(1420, 25);
            this.F5_toolStrip_fileEditorForm.TabIndex = 5;
            // 
            // F5_toolStripDropDownButton_fileButton
            // 
            this.F5_toolStripDropDownButton_fileButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.F5_toolStripDropDownButton_fileButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.F5_ToolStripMenuItem_openFile});
            this.F5_toolStripDropDownButton_fileButton.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.F5_toolStripDropDownButton_fileButton.Image = ((System.Drawing.Image)(resources.GetObject("F5_toolStripDropDownButton_fileButton.Image")));
            this.F5_toolStripDropDownButton_fileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.F5_toolStripDropDownButton_fileButton.Name = "F5_toolStripDropDownButton_fileButton";
            this.F5_toolStripDropDownButton_fileButton.Size = new System.Drawing.Size(48, 22);
            this.F5_toolStripDropDownButton_fileButton.Text = "File";
            this.F5_toolStripDropDownButton_fileButton.Click += new System.EventHandler(this.F5_toolStripDropDownButton_fileButton_Click);
            // 
            // F5_ToolStripMenuItem_openFile
            // 
            this.F5_ToolStripMenuItem_openFile.Name = "F5_ToolStripMenuItem_openFile";
            this.F5_ToolStripMenuItem_openFile.Size = new System.Drawing.Size(102, 22);
            this.F5_ToolStripMenuItem_openFile.Text = "Open";
            this.F5_ToolStripMenuItem_openFile.Click += new System.EventHandler(this.F5_ToolStripMenuItem_openFile_Click);
            // 
            // F5_toolStripSeparator_separator1
            // 
            this.F5_toolStripSeparator_separator1.Name = "F5_toolStripSeparator_separator1";
            this.F5_toolStripSeparator_separator1.Size = new System.Drawing.Size(6, 25);
            // 
            // F5_statusStrip_fileEditorForm
            // 
            this.F5_statusStrip_fileEditorForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.F5mdi1_toolStripStatusLabel_connectedDatabaseName,
            this.F5mdi1_toolStripProgressBar_databaseSyncProgressBar});
            this.F5_statusStrip_fileEditorForm.Location = new System.Drawing.Point(0, 597);
            this.F5_statusStrip_fileEditorForm.Name = "F5_statusStrip_fileEditorForm";
            this.F5_statusStrip_fileEditorForm.Size = new System.Drawing.Size(1420, 22);
            this.F5_statusStrip_fileEditorForm.TabIndex = 4;
            this.F5_statusStrip_fileEditorForm.Text = "F5_statusStrip_status";
            // 
            // F5mdi1_toolStripStatusLabel_connectedDatabaseName
            // 
            this.F5mdi1_toolStripStatusLabel_connectedDatabaseName.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.F5mdi1_toolStripStatusLabel_connectedDatabaseName.Name = "F5mdi1_toolStripStatusLabel_connectedDatabaseName";
            this.F5mdi1_toolStripStatusLabel_connectedDatabaseName.Size = new System.Drawing.Size(112, 17);
            this.F5mdi1_toolStripStatusLabel_connectedDatabaseName.Text = "#DATABASE_NAME#";
            // 
            // F5mdi1_toolStripProgressBar_databaseSyncProgressBar
            // 
            this.F5mdi1_toolStripProgressBar_databaseSyncProgressBar.Name = "F5mdi1_toolStripProgressBar_databaseSyncProgressBar";
            this.F5mdi1_toolStripProgressBar_databaseSyncProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // F5_FileEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1420, 619);
            this.Controls.Add(this.F5_toolStrip_fileEditorForm);
            this.Controls.Add(this.F5_statusStrip_fileEditorForm);
            this.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.IsMdiContainer = true;
            this.Name = "F5_FileEditorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "File Editor";
            this.Load += new System.EventHandler(this.F5_FileEditorForm_Load);
            this.MdiChildActivate += new System.EventHandler(this.F5_FileEditorForm_MdiChildActivate);
            this.Resize += new System.EventHandler(this.F5_FileEditorForm_Resize);
            this.F5_toolStrip_fileEditorForm.ResumeLayout(false);
            this.F5_toolStrip_fileEditorForm.PerformLayout();
            this.F5_statusStrip_fileEditorForm.ResumeLayout(false);
            this.F5_statusStrip_fileEditorForm.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip F5_toolStrip_fileEditorForm;
        private System.Windows.Forms.ToolStripDropDownButton F5_toolStripDropDownButton_fileButton;
        private System.Windows.Forms.ToolStripMenuItem F5_ToolStripMenuItem_openFile;
        private System.Windows.Forms.ToolStripSeparator F5_toolStripSeparator_separator1;
        private System.Windows.Forms.StatusStrip F5_statusStrip_fileEditorForm;
        private System.Windows.Forms.ToolStripStatusLabel F5mdi1_toolStripStatusLabel_connectedDatabaseName;
        private System.Windows.Forms.ToolStripProgressBar F5mdi1_toolStripProgressBar_databaseSyncProgressBar;
        private System.Windows.Forms.OpenFileDialog F5_openFileDialog_openFile;
    }
}