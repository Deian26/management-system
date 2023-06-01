namespace management_system
{
    partial class F10_DatabaseTableTools
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
            this.F10_comboBox_databaseTableTools = new System.Windows.Forms.ComboBox();
            this.F10_label_toolName = new System.Windows.Forms.Label();
            this.F10_groupBox_tool = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // F10_comboBox_databaseTableTools
            // 
            this.F10_comboBox_databaseTableTools.FormattingEnabled = true;
            this.F10_comboBox_databaseTableTools.Location = new System.Drawing.Point(13, 48);
            this.F10_comboBox_databaseTableTools.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.F10_comboBox_databaseTableTools.Name = "F10_comboBox_databaseTableTools";
            this.F10_comboBox_databaseTableTools.Size = new System.Drawing.Size(584, 27);
            this.F10_comboBox_databaseTableTools.TabIndex = 0;
            // 
            // F10_label_toolName
            // 
            this.F10_label_toolName.AutoSize = true;
            this.F10_label_toolName.Location = new System.Drawing.Point(13, 9);
            this.F10_label_toolName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.F10_label_toolName.Name = "F10_label_toolName";
            this.F10_label_toolName.Size = new System.Drawing.Size(216, 19);
            this.F10_label_toolName.TabIndex = 1;
            this.F10_label_toolName.Text = "Selectați instrumentul:";
            // 
            // F10_groupBox_tool
            // 
            this.F10_groupBox_tool.Location = new System.Drawing.Point(17, 82);
            this.F10_groupBox_tool.Name = "F10_groupBox_tool";
            this.F10_groupBox_tool.Size = new System.Drawing.Size(580, 289);
            this.F10_groupBox_tool.TabIndex = 2;
            this.F10_groupBox_tool.TabStop = false;
            this.F10_groupBox_tool.Text = "Instrument";
            // 
            // F10_DatabaseTableTools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 390);
            this.Controls.Add(this.F10_groupBox_tool);
            this.Controls.Add(this.F10_label_toolName);
            this.Controls.Add(this.F10_comboBox_databaseTableTools);
            this.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "F10_DatabaseTableTools";
            this.Text = "F10_DatabaseTableTools";
            this.Load += new System.EventHandler(this.F10_DatabaseTableTools_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox F10_comboBox_databaseTableTools;
        private System.Windows.Forms.Label F10_label_toolName;
        private System.Windows.Forms.GroupBox F10_groupBox_tool;
    }
}