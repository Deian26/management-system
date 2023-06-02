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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F10_DatabaseTableTools));
            this.F10_comboBox_databaseTableTools = new System.Windows.Forms.ComboBox();
            this.F10_label_toolName = new System.Windows.Forms.Label();
            this.F10_groupBox_tool = new System.Windows.Forms.GroupBox();
            this.F10_panel_searchColumn = new System.Windows.Forms.Panel();
            this.F10_textBox_searchColumn_columnName = new System.Windows.Forms.TextBox();
            this.F10_label_searchColumn_columnName = new System.Windows.Forms.Label();
            this.F10_button_searchColumn_startSearch = new System.Windows.Forms.Button();
            this.F10_textBox_searchColumn_valueToSearch = new System.Windows.Forms.TextBox();
            this.F10_label_searchColumn_valueToSearch = new System.Windows.Forms.Label();
            this.F10_label_searchColumn_results = new System.Windows.Forms.Label();
            this.F10_dataGridView_searchColumn_results = new System.Windows.Forms.DataGridView();
            this.F10_groupBox_tool.SuspendLayout();
            this.F10_panel_searchColumn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.F10_dataGridView_searchColumn_results)).BeginInit();
            this.SuspendLayout();
            // 
            // F10_comboBox_databaseTableTools
            // 
            this.F10_comboBox_databaseTableTools.FormattingEnabled = true;
            this.F10_comboBox_databaseTableTools.Location = new System.Drawing.Point(13, 48);
            this.F10_comboBox_databaseTableTools.Margin = new System.Windows.Forms.Padding(4);
            this.F10_comboBox_databaseTableTools.Name = "F10_comboBox_databaseTableTools";
            this.F10_comboBox_databaseTableTools.Size = new System.Drawing.Size(584, 27);
            this.F10_comboBox_databaseTableTools.TabIndex = 0;
            this.F10_comboBox_databaseTableTools.SelectedIndexChanged += new System.EventHandler(this.F10_comboBox_databaseTableTools_SelectedIndexChanged);
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
            this.F10_groupBox_tool.Controls.Add(this.F10_panel_searchColumn);
            this.F10_groupBox_tool.Location = new System.Drawing.Point(17, 82);
            this.F10_groupBox_tool.Name = "F10_groupBox_tool";
            this.F10_groupBox_tool.Size = new System.Drawing.Size(580, 289);
            this.F10_groupBox_tool.TabIndex = 2;
            this.F10_groupBox_tool.TabStop = false;
            this.F10_groupBox_tool.Text = "Instrument";
            // 
            // F10_panel_searchColumn
            // 
            this.F10_panel_searchColumn.Controls.Add(this.F10_textBox_searchColumn_columnName);
            this.F10_panel_searchColumn.Controls.Add(this.F10_label_searchColumn_columnName);
            this.F10_panel_searchColumn.Controls.Add(this.F10_button_searchColumn_startSearch);
            this.F10_panel_searchColumn.Controls.Add(this.F10_textBox_searchColumn_valueToSearch);
            this.F10_panel_searchColumn.Controls.Add(this.F10_label_searchColumn_valueToSearch);
            this.F10_panel_searchColumn.Controls.Add(this.F10_label_searchColumn_results);
            this.F10_panel_searchColumn.Controls.Add(this.F10_dataGridView_searchColumn_results);
            this.F10_panel_searchColumn.Location = new System.Drawing.Point(12, 25);
            this.F10_panel_searchColumn.Name = "F10_panel_searchColumn";
            this.F10_panel_searchColumn.Size = new System.Drawing.Size(562, 258);
            this.F10_panel_searchColumn.TabIndex = 0;
            // 
            // F10_textBox_searchColumn_columnName
            // 
            this.F10_textBox_searchColumn_columnName.Location = new System.Drawing.Point(146, 9);
            this.F10_textBox_searchColumn_columnName.Name = "F10_textBox_searchColumn_columnName";
            this.F10_textBox_searchColumn_columnName.Size = new System.Drawing.Size(413, 26);
            this.F10_textBox_searchColumn_columnName.TabIndex = 8;
            // 
            // F10_label_searchColumn_columnName
            // 
            this.F10_label_searchColumn_columnName.AutoSize = true;
            this.F10_label_searchColumn_columnName.Location = new System.Drawing.Point(4, 12);
            this.F10_label_searchColumn_columnName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.F10_label_searchColumn_columnName.Name = "F10_label_searchColumn_columnName";
            this.F10_label_searchColumn_columnName.Size = new System.Drawing.Size(126, 19);
            this.F10_label_searchColumn_columnName.TabIndex = 7;
            this.F10_label_searchColumn_columnName.Text = "Nume coloană:";
            // 
            // F10_button_searchColumn_startSearch
            // 
            this.F10_button_searchColumn_startSearch.Location = new System.Drawing.Point(470, 73);
            this.F10_button_searchColumn_startSearch.Name = "F10_button_searchColumn_startSearch";
            this.F10_button_searchColumn_startSearch.Size = new System.Drawing.Size(89, 31);
            this.F10_button_searchColumn_startSearch.TabIndex = 6;
            this.F10_button_searchColumn_startSearch.Text = "Start";
            this.F10_button_searchColumn_startSearch.UseVisualStyleBackColor = true;
            this.F10_button_searchColumn_startSearch.Click += new System.EventHandler(this.F10_button_searchColumn_startSearch_Click);
            // 
            // F10_textBox_searchColumn_valueToSearch
            // 
            this.F10_textBox_searchColumn_valueToSearch.Location = new System.Drawing.Point(146, 41);
            this.F10_textBox_searchColumn_valueToSearch.Name = "F10_textBox_searchColumn_valueToSearch";
            this.F10_textBox_searchColumn_valueToSearch.Size = new System.Drawing.Size(413, 26);
            this.F10_textBox_searchColumn_valueToSearch.TabIndex = 5;
            // 
            // F10_label_searchColumn_valueToSearch
            // 
            this.F10_label_searchColumn_valueToSearch.AutoSize = true;
            this.F10_label_searchColumn_valueToSearch.Location = new System.Drawing.Point(4, 44);
            this.F10_label_searchColumn_valueToSearch.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.F10_label_searchColumn_valueToSearch.Name = "F10_label_searchColumn_valueToSearch";
            this.F10_label_searchColumn_valueToSearch.Size = new System.Drawing.Size(81, 19);
            this.F10_label_searchColumn_valueToSearch.TabIndex = 4;
            this.F10_label_searchColumn_valueToSearch.Text = "Valoare:";
            // 
            // F10_label_searchColumn_results
            // 
            this.F10_label_searchColumn_results.AutoSize = true;
            this.F10_label_searchColumn_results.Location = new System.Drawing.Point(4, 88);
            this.F10_label_searchColumn_results.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.F10_label_searchColumn_results.Name = "F10_label_searchColumn_results";
            this.F10_label_searchColumn_results.Size = new System.Drawing.Size(135, 19);
            this.F10_label_searchColumn_results.TabIndex = 3;
            this.F10_label_searchColumn_results.Text = "Valori găsite:";
            // 
            // F10_dataGridView_searchColumn_results
            // 
            this.F10_dataGridView_searchColumn_results.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.F10_dataGridView_searchColumn_results.Location = new System.Drawing.Point(3, 110);
            this.F10_dataGridView_searchColumn_results.Name = "F10_dataGridView_searchColumn_results";
            this.F10_dataGridView_searchColumn_results.Size = new System.Drawing.Size(556, 145);
            this.F10_dataGridView_searchColumn_results.TabIndex = 0;
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
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "F10_DatabaseTableTools";
            this.Text = "F10_DatabaseTableTools";
            this.Load += new System.EventHandler(this.F10_DatabaseTableTools_Load);
            this.F10_groupBox_tool.ResumeLayout(false);
            this.F10_panel_searchColumn.ResumeLayout(false);
            this.F10_panel_searchColumn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.F10_dataGridView_searchColumn_results)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox F10_comboBox_databaseTableTools;
        private System.Windows.Forms.Label F10_label_toolName;
        private System.Windows.Forms.GroupBox F10_groupBox_tool;
        private System.Windows.Forms.Panel F10_panel_searchColumn;
        private System.Windows.Forms.Label F10_label_searchColumn_valueToSearch;
        private System.Windows.Forms.Label F10_label_searchColumn_results;
        private System.Windows.Forms.DataGridView F10_dataGridView_searchColumn_results;
        private System.Windows.Forms.Button F10_button_searchColumn_startSearch;
        private System.Windows.Forms.TextBox F10_textBox_searchColumn_valueToSearch;
        private System.Windows.Forms.TextBox F10_textBox_searchColumn_columnName;
        private System.Windows.Forms.Label F10_label_searchColumn_columnName;
    }
}