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
            this.F5_toolStripSeparator_separator1 = new System.Windows.Forms.ToolStripSeparator();
            this.F5_statusStrip_fileEditorForm = new System.Windows.Forms.StatusStrip();
            this.F5mdi1_toolStripStatusLabel_connectedDatabaseName = new System.Windows.Forms.ToolStripStatusLabel();
            this.F5mdi1_toolStripProgressBar_databaseSyncProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.F5_openFileDialog_openFile = new System.Windows.Forms.OpenFileDialog();
            this.F5_toolStripButton_addUsers = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.F5_toolStrip_fileEditorForm.SuspendLayout();
            this.F5_statusStrip_fileEditorForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // F5_toolStrip_fileEditorForm
            // 
            this.F5_toolStrip_fileEditorForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.F5_toolStripSeparator_separator1,
            this.F5_toolStripButton_addUsers,
            this.toolStripButton1});
            this.F5_toolStrip_fileEditorForm.Location = new System.Drawing.Point(0, 0);
            this.F5_toolStrip_fileEditorForm.Name = "F5_toolStrip_fileEditorForm";
            this.F5_toolStrip_fileEditorForm.Size = new System.Drawing.Size(1420, 26);
            this.F5_toolStrip_fileEditorForm.TabIndex = 5;
            // 
            // F5_toolStripSeparator_separator1
            // 
            this.F5_toolStripSeparator_separator1.Name = "F5_toolStripSeparator_separator1";
            this.F5_toolStripSeparator_separator1.Size = new System.Drawing.Size(6, 26);
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
            // F5_toolStripButton_addUsers
            // 
            this.F5_toolStripButton_addUsers.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.F5_toolStripButton_addUsers.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.F5_toolStripButton_addUsers.Image = ((System.Drawing.Image)(resources.GetObject("F5_toolStripButton_addUsers.Image")));
            this.F5_toolStripButton_addUsers.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.F5_toolStripButton_addUsers.Name = "F5_toolStripButton_addUsers";
            this.F5_toolStripButton_addUsers.Size = new System.Drawing.Size(193, 23);
            this.F5_toolStripButton_addUsers.Text = "Adăugare utilizatori";
            this.F5_toolStripButton_addUsers.Click += new System.EventHandler(this.F5_toolStripButton_addUsers_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 23);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
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
            this.Text = "Editor de fișiere";
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
        private System.Windows.Forms.ToolStripSeparator F5_toolStripSeparator_separator1;
        private System.Windows.Forms.StatusStrip F5_statusStrip_fileEditorForm;
        private System.Windows.Forms.ToolStripStatusLabel F5mdi1_toolStripStatusLabel_connectedDatabaseName;
        private System.Windows.Forms.ToolStripProgressBar F5mdi1_toolStripProgressBar_databaseSyncProgressBar;
        private System.Windows.Forms.OpenFileDialog F5_openFileDialog_openFile;
        private System.Windows.Forms.ToolStripButton F5_toolStripButton_addUsers;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}