namespace management_system
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
            this.F5mdi4_button_createNewFile = new System.Windows.Forms.Button();
            this.F5mdi4_button_reloadFiles = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // F5mdi4_treeView_currentGroupFiles
            // 
            this.F5mdi4_treeView_currentGroupFiles.Location = new System.Drawing.Point(12, 41);
            this.F5mdi4_treeView_currentGroupFiles.Name = "F5mdi4_treeView_currentGroupFiles";
            this.F5mdi4_treeView_currentGroupFiles.Size = new System.Drawing.Size(776, 397);
            this.F5mdi4_treeView_currentGroupFiles.TabIndex = 0;
            this.F5mdi4_treeView_currentGroupFiles.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.F5mdi4_treeView_currentGroupFiles_AfterSelect);
            this.F5mdi4_treeView_currentGroupFiles.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.F5mdi4_treeView_currentGroupFiles_NodeMouseDoubleClick);
            // 
            // F5mdi4_button_createNewFile
            // 
            this.F5mdi4_button_createNewFile.Location = new System.Drawing.Point(12, 12);
            this.F5mdi4_button_createNewFile.Name = "F5mdi4_button_createNewFile";
            this.F5mdi4_button_createNewFile.Size = new System.Drawing.Size(113, 23);
            this.F5mdi4_button_createNewFile.TabIndex = 1;
            this.F5mdi4_button_createNewFile.Text = "Creare fișier";
            this.F5mdi4_button_createNewFile.UseVisualStyleBackColor = true;
            this.F5mdi4_button_createNewFile.Click += new System.EventHandler(this.F5mdi4_button_createNewFile_Click);
            // 
            // F5mdi4_button_reloadFiles
            // 
            this.F5mdi4_button_reloadFiles.Location = new System.Drawing.Point(131, 12);
            this.F5mdi4_button_reloadFiles.Name = "F5mdi4_button_reloadFiles";
            this.F5mdi4_button_reloadFiles.Size = new System.Drawing.Size(113, 23);
            this.F5mdi4_button_reloadFiles.TabIndex = 2;
            this.F5mdi4_button_reloadFiles.Text = "Reîncărcare";
            this.F5mdi4_button_reloadFiles.UseVisualStyleBackColor = true;
            this.F5mdi4_button_reloadFiles.Click += new System.EventHandler(this.F5mdi4_button_reloadFiles_Click);
            // 
            // F5mdi4_FileTreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.F5mdi4_button_reloadFiles);
            this.Controls.Add(this.F5mdi4_button_createNewFile);
            this.Controls.Add(this.F5mdi4_treeView_currentGroupFiles);
            this.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "F5mdi4_FileTreeView";
            this.Text = "Vizualizare arborescentă";
            this.Load += new System.EventHandler(this.F5mdi4_FileTreeView_Load);
            this.Resize += new System.EventHandler(this.F5mdi4_FileTreeView_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView F5mdi4_treeView_currentGroupFiles;
        private System.Windows.Forms.Button F5mdi4_button_createNewFile;
        private System.Windows.Forms.Button F5mdi4_button_reloadFiles;
    }
}