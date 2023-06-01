namespace management_system
{
    partial class F4_NewGroup
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
            this.components = new System.ComponentModel.Container();
            this.F4_textBox_groupName = new System.Windows.Forms.TextBox();
            this.F4_button_createNewGroup = new System.Windows.Forms.Button();
            this.F4_label_groupName = new System.Windows.Forms.Label();
            this.F4_errorProvider_newGroupForm = new System.Windows.Forms.ErrorProvider(this.components);
            this.F4_openFileDialog_chooseNewGroupIcon = new System.Windows.Forms.OpenFileDialog();
            this.F4_button_newGroupIcon = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.F4_errorProvider_newGroupForm)).BeginInit();
            this.SuspendLayout();
            // 
            // F4_textBox_groupName
            // 
            this.F4_textBox_groupName.Location = new System.Drawing.Point(148, 24);
            this.F4_textBox_groupName.Name = "F4_textBox_groupName";
            this.F4_textBox_groupName.Size = new System.Drawing.Size(227, 20);
            this.F4_textBox_groupName.TabIndex = 0;
            // 
            // F4_button_createNewGroup
            // 
            this.F4_button_createNewGroup.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.F4_button_createNewGroup.Location = new System.Drawing.Point(221, 65);
            this.F4_button_createNewGroup.Name = "F4_button_createNewGroup";
            this.F4_button_createNewGroup.Size = new System.Drawing.Size(154, 31);
            this.F4_button_createNewGroup.TabIndex = 1;
            this.F4_button_createNewGroup.Text = "Creează grup";
            this.F4_button_createNewGroup.UseVisualStyleBackColor = true;
            this.F4_button_createNewGroup.Click += new System.EventHandler(this.F4_button_createNewGroup_Click);
            // 
            // F4_label_groupName
            // 
            this.F4_label_groupName.AutoSize = true;
            this.F4_label_groupName.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.F4_label_groupName.Location = new System.Drawing.Point(11, 25);
            this.F4_label_groupName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.F4_label_groupName.Name = "F4_label_groupName";
            this.F4_label_groupName.Size = new System.Drawing.Size(99, 19);
            this.F4_label_groupName.TabIndex = 5;
            this.F4_label_groupName.Text = "Nume grup:";
            // 
            // F4_errorProvider_newGroupForm
            // 
            this.F4_errorProvider_newGroupForm.ContainerControl = this;
            // 
            // F4_button_newGroupIcon
            // 
            this.F4_button_newGroupIcon.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.F4_button_newGroupIcon.Location = new System.Drawing.Point(12, 65);
            this.F4_button_newGroupIcon.Name = "F4_button_newGroupIcon";
            this.F4_button_newGroupIcon.Size = new System.Drawing.Size(130, 31);
            this.F4_button_newGroupIcon.TabIndex = 6;
            this.F4_button_newGroupIcon.Text = "Imagine grup";
            this.F4_button_newGroupIcon.UseVisualStyleBackColor = true;
            this.F4_button_newGroupIcon.Click += new System.EventHandler(this.F4_button_newGroupIcon_Click);
            // 
            // F4_NewGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 108);
            this.Controls.Add(this.F4_button_newGroupIcon);
            this.Controls.Add(this.F4_label_groupName);
            this.Controls.Add(this.F4_button_createNewGroup);
            this.Controls.Add(this.F4_textBox_groupName);
            this.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "F4_NewGroup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Grup nou";
            this.Load += new System.EventHandler(this.F4_NewGroup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.F4_errorProvider_newGroupForm)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox F4_textBox_groupName;
        private System.Windows.Forms.Button F4_button_createNewGroup;
        private System.Windows.Forms.Label F4_label_groupName;
        private System.Windows.Forms.ErrorProvider F4_errorProvider_newGroupForm;
        private System.Windows.Forms.Button F4_button_newGroupIcon;
        private System.Windows.Forms.OpenFileDialog F4_openFileDialog_chooseNewGroupIcon;
    }
}