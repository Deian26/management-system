namespace management_system
{
    partial class F11_AddGroupUsers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F11_AddGroupUsers));
            this.F11_label_username = new System.Windows.Forms.Label();
            this.F11_textBox_username = new System.Windows.Forms.TextBox();
            this.F11_button_addUserToGroup = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // F11_label_username
            // 
            this.F11_label_username.AutoSize = true;
            this.F11_label_username.Font = new System.Drawing.Font("Cascadia Code", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.F11_label_username.Location = new System.Drawing.Point(20, 53);
            this.F11_label_username.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.F11_label_username.Name = "F11_label_username";
            this.F11_label_username.Size = new System.Drawing.Size(219, 23);
            this.F11_label_username.TabIndex = 0;
            this.F11_label_username.Text = "Nume de utilizator:";
            // 
            // F11_textBox_username
            // 
            this.F11_textBox_username.Location = new System.Drawing.Point(243, 49);
            this.F11_textBox_username.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.F11_textBox_username.Name = "F11_textBox_username";
            this.F11_textBox_username.Size = new System.Drawing.Size(426, 31);
            this.F11_textBox_username.TabIndex = 2;
            // 
            // F11_button_addUserToGroup
            // 
            this.F11_button_addUserToGroup.Location = new System.Drawing.Point(515, 123);
            this.F11_button_addUserToGroup.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.F11_button_addUserToGroup.Name = "F11_button_addUserToGroup";
            this.F11_button_addUserToGroup.Size = new System.Drawing.Size(155, 67);
            this.F11_button_addUserToGroup.TabIndex = 3;
            this.F11_button_addUserToGroup.Text = "Adăugare utilizator";
            this.F11_button_addUserToGroup.UseVisualStyleBackColor = true;
            this.F11_button_addUserToGroup.Click += new System.EventHandler(this.F11_button_addUserToGroup_Click);
            // 
            // F11_AddGroupUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 197);
            this.Controls.Add(this.F11_button_addUserToGroup);
            this.Controls.Add(this.F11_textBox_username);
            this.Controls.Add(this.F11_label_username);
            this.Font = new System.Drawing.Font("Cascadia Code", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximumSize = new System.Drawing.Size(702, 244);
            this.MinimumSize = new System.Drawing.Size(702, 244);
            this.Name = "F11_AddGroupUsers";
            this.Text = "Adăugare utilizatori la grup";
            this.Load += new System.EventHandler(this.F11_AddGroupUsers_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label F11_label_username;
        private System.Windows.Forms.TextBox F11_textBox_username;
        private System.Windows.Forms.Button F11_button_addUserToGroup;
    }
}