namespace management_system
{
    partial class F8_AddNewTableColumn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F8_AddNewTableColumn));
            this.F8_label_newColumnName = new System.Windows.Forms.Label();
            this.F8_textBox_newColumnName = new System.Windows.Forms.TextBox();
            this.F8_button_addNewColumn = new System.Windows.Forms.Button();
            this.F8_textBox_newColumnDataType = new System.Windows.Forms.TextBox();
            this.F8_label_newColumnDataType = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // F8_label_newColumnName
            // 
            this.F8_label_newColumnName.AutoSize = true;
            this.F8_label_newColumnName.Location = new System.Drawing.Point(12, 34);
            this.F8_label_newColumnName.Name = "F8_label_newColumnName";
            this.F8_label_newColumnName.Size = new System.Drawing.Size(126, 19);
            this.F8_label_newColumnName.TabIndex = 0;
            this.F8_label_newColumnName.Text = "Nume coloană:";
            // 
            // F8_textBox_newColumnName
            // 
            this.F8_textBox_newColumnName.Location = new System.Drawing.Point(144, 31);
            this.F8_textBox_newColumnName.Name = "F8_textBox_newColumnName";
            this.F8_textBox_newColumnName.Size = new System.Drawing.Size(301, 26);
            this.F8_textBox_newColumnName.TabIndex = 1;
            // 
            // F8_button_addNewColumn
            // 
            this.F8_button_addNewColumn.Location = new System.Drawing.Point(317, 106);
            this.F8_button_addNewColumn.Name = "F8_button_addNewColumn";
            this.F8_button_addNewColumn.Size = new System.Drawing.Size(128, 31);
            this.F8_button_addNewColumn.TabIndex = 2;
            this.F8_button_addNewColumn.Text = "Adăugare";
            this.F8_button_addNewColumn.UseVisualStyleBackColor = true;
            this.F8_button_addNewColumn.Click += new System.EventHandler(this.F8_button_addNewColumn_Click);
            // 
            // F8_textBox_newColumnDataType
            // 
            this.F8_textBox_newColumnDataType.Location = new System.Drawing.Point(144, 63);
            this.F8_textBox_newColumnDataType.Name = "F8_textBox_newColumnDataType";
            this.F8_textBox_newColumnDataType.Size = new System.Drawing.Size(301, 26);
            this.F8_textBox_newColumnDataType.TabIndex = 3;
            // 
            // F8_label_newColumnDataType
            // 
            this.F8_label_newColumnDataType.AutoSize = true;
            this.F8_label_newColumnDataType.Location = new System.Drawing.Point(30, 66);
            this.F8_label_newColumnDataType.Name = "F8_label_newColumnDataType";
            this.F8_label_newColumnDataType.Size = new System.Drawing.Size(90, 19);
            this.F8_label_newColumnDataType.TabIndex = 4;
            this.F8_label_newColumnDataType.Text = "Tip dată:";
            // 
            // F8_AddNewTableColumn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 149);
            this.Controls.Add(this.F8_label_newColumnDataType);
            this.Controls.Add(this.F8_textBox_newColumnDataType);
            this.Controls.Add(this.F8_button_addNewColumn);
            this.Controls.Add(this.F8_textBox_newColumnName);
            this.Controls.Add(this.F8_label_newColumnName);
            this.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "F8_AddNewTableColumn";
            this.Text = "Adăugare coloană nouă";
            this.Load += new System.EventHandler(this.F8_AddNewTableColumn_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label F8_label_newColumnName;
        private System.Windows.Forms.TextBox F8_textBox_newColumnName;
        private System.Windows.Forms.Button F8_button_addNewColumn;
        private System.Windows.Forms.TextBox F8_textBox_newColumnDataType;
        private System.Windows.Forms.Label F8_label_newColumnDataType;
    }
}