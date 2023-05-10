namespace management_system.SYSTEM.FORMS.PROGRAM_FORMS.FILE_EDITOR.MDI_WINDOWS
{
    partial class F5mdi4_FileTreeView
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
            this.F5mdi4_treeView_currentGroupFiles = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // F5mdi4_treeView_currentGroupFiles
            // 
            this.F5mdi4_treeView_currentGroupFiles.Location = new System.Drawing.Point(12, 12);
            this.F5mdi4_treeView_currentGroupFiles.Name = "F5mdi4_treeView_currentGroupFiles";
            this.F5mdi4_treeView_currentGroupFiles.Size = new System.Drawing.Size(776, 426);
            this.F5mdi4_treeView_currentGroupFiles.TabIndex = 0;
            this.F5mdi4_treeView_currentGroupFiles.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.F5mdi4_treeView_currentGroupFiles_AfterSelect);
            this.F5mdi4_treeView_currentGroupFiles.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.F5mdi4_treeView_currentGroupFiles_NodeMouseDoubleClick);
            // 
            // F5mdi4_FileTreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.F5mdi4_treeView_currentGroupFiles);
            this.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "F5mdi4_FileTreeView";
            this.Text = "File Tree View";
            this.Load += new System.EventHandler(this.F5mdi4_FileTreeView_Load);
            this.Resize += new System.EventHandler(this.F5mdi4_FileTreeView_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView F5mdi4_treeView_currentGroupFiles;
    }
}