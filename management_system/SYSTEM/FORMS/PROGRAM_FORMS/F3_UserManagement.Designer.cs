﻿namespace management_system
{
    partial class F3_UserManagement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F3_UserManagement));
            this.F3_label_username = new System.Windows.Forms.Label();
            this.F3_user_rights = new System.Windows.Forms.Label();
            this.F3_textBox_username = new System.Windows.Forms.TextBox();
            this.F3_comboBox_userRights = new System.Windows.Forms.ComboBox();
            this.F3_button_assignUserRights = new System.Windows.Forms.Button();
            this.F3_label_password = new System.Windows.Forms.Label();
            this.F3_textBox_password = new System.Windows.Forms.TextBox();
            this.F3_errorProvider_userManagement = new System.Windows.Forms.ErrorProvider(this.components);
            this.F3_timer_updateTimer = new System.Windows.Forms.Timer(this.components);
            this.F3_button_deleteAccount = new System.Windows.Forms.Button();
            this.F3_label_user_details = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.F3_errorProvider_userManagement)).BeginInit();
            this.SuspendLayout();
            // 
            // F3_label_username
            // 
            this.F3_label_username.AutoSize = true;
            this.F3_label_username.Font = new System.Drawing.Font("Cascadia Code", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.F3_label_username.Location = new System.Drawing.Point(11, 35);
            this.F3_label_username.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.F3_label_username.Name = "F3_label_username";
            this.F3_label_username.Size = new System.Drawing.Size(186, 23);
            this.F3_label_username.TabIndex = 4;
            this.F3_label_username.Text = "Nume utilizator:";
            // 
            // F3_user_rights
            // 
            this.F3_user_rights.AutoSize = true;
            this.F3_user_rights.Font = new System.Drawing.Font("Cascadia Code", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.F3_user_rights.Location = new System.Drawing.Point(11, 119);
            this.F3_user_rights.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.F3_user_rights.Name = "F3_user_rights";
            this.F3_user_rights.Size = new System.Drawing.Size(131, 23);
            this.F3_user_rights.TabIndex = 5;
            this.F3_user_rights.Text = "Permisiuni:";
            // 
            // F3_textBox_username
            // 
            this.F3_textBox_username.Location = new System.Drawing.Point(217, 37);
            this.F3_textBox_username.Name = "F3_textBox_username";
            this.F3_textBox_username.Size = new System.Drawing.Size(199, 23);
            this.F3_textBox_username.TabIndex = 6;
            // 
            // F3_comboBox_userRights
            // 
            this.F3_comboBox_userRights.FormattingEnabled = true;
            this.F3_comboBox_userRights.Location = new System.Drawing.Point(217, 119);
            this.F3_comboBox_userRights.Name = "F3_comboBox_userRights";
            this.F3_comboBox_userRights.Size = new System.Drawing.Size(199, 25);
            this.F3_comboBox_userRights.TabIndex = 7;
            this.F3_comboBox_userRights.SelectedIndexChanged += new System.EventHandler(this.F3_comboBox_userRights_SelectedIndexChanged);
            // 
            // F3_button_assignUserRights
            // 
            this.F3_button_assignUserRights.Font = new System.Drawing.Font("Cascadia Code", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.F3_button_assignUserRights.Location = new System.Drawing.Point(234, 161);
            this.F3_button_assignUserRights.Name = "F3_button_assignUserRights";
            this.F3_button_assignUserRights.Size = new System.Drawing.Size(182, 30);
            this.F3_button_assignUserRights.TabIndex = 10;
            this.F3_button_assignUserRights.Text = "Setare permisiuni";
            this.F3_button_assignUserRights.UseVisualStyleBackColor = true;
            this.F3_button_assignUserRights.Click += new System.EventHandler(this.F3_button_assignUserRights_Click);
            // 
            // F3_label_password
            // 
            this.F3_label_password.AutoSize = true;
            this.F3_label_password.Font = new System.Drawing.Font("Cascadia Code", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.F3_label_password.Location = new System.Drawing.Point(11, 71);
            this.F3_label_password.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.F3_label_password.Name = "F3_label_password";
            this.F3_label_password.Size = new System.Drawing.Size(87, 23);
            this.F3_label_password.TabIndex = 11;
            this.F3_label_password.Text = "Parolă:";
            // 
            // F3_textBox_password
            // 
            this.F3_textBox_password.Location = new System.Drawing.Point(217, 73);
            this.F3_textBox_password.Name = "F3_textBox_password";
            this.F3_textBox_password.Size = new System.Drawing.Size(199, 23);
            this.F3_textBox_password.TabIndex = 12;
            this.F3_textBox_password.UseSystemPasswordChar = true;
            // 
            // F3_errorProvider_userManagement
            // 
            this.F3_errorProvider_userManagement.ContainerControl = this;
            // 
            // F3_timer_updateTimer
            // 
            this.F3_timer_updateTimer.Tick += new System.EventHandler(this.F3_timer_updateTimer_Tick);
            // 
            // F3_button_deleteAccount
            // 
            this.F3_button_deleteAccount.Font = new System.Drawing.Font("Cascadia Code", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.F3_button_deleteAccount.Location = new System.Drawing.Point(12, 161);
            this.F3_button_deleteAccount.Name = "F3_button_deleteAccount";
            this.F3_button_deleteAccount.Size = new System.Drawing.Size(185, 30);
            this.F3_button_deleteAccount.TabIndex = 13;
            this.F3_button_deleteAccount.Text = "Ștergere cont";
            this.F3_button_deleteAccount.UseVisualStyleBackColor = true;
            this.F3_button_deleteAccount.Click += new System.EventHandler(this.F3_button_deleteAccount_Click);
            // 
            // F3_label_user_details
            // 
            this.F3_label_user_details.AutoSize = true;
            this.F3_label_user_details.Font = new System.Drawing.Font("Cascadia Code", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.F3_label_user_details.Location = new System.Drawing.Point(11, 9);
            this.F3_label_user_details.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.F3_label_user_details.Name = "F3_label_user_details";
            this.F3_label_user_details.Size = new System.Drawing.Size(208, 23);
            this.F3_label_user_details.TabIndex = 14;
            this.F3_label_user_details.Text = "Detalii utilizator";
            // 
            // F3_UserManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 203);
            this.Controls.Add(this.F3_label_user_details);
            this.Controls.Add(this.F3_button_deleteAccount);
            this.Controls.Add(this.F3_textBox_password);
            this.Controls.Add(this.F3_label_password);
            this.Controls.Add(this.F3_button_assignUserRights);
            this.Controls.Add(this.F3_comboBox_userRights);
            this.Controls.Add(this.F3_textBox_username);
            this.Controls.Add(this.F3_user_rights);
            this.Controls.Add(this.F3_label_username);
            this.Font = new System.Drawing.Font("Cascadia Code", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "F3_UserManagement";
            this.Text = "Administrare utilizatori";
            this.Load += new System.EventHandler(this.F3_UserManagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.F3_errorProvider_userManagement)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label F3_label_username;
        private System.Windows.Forms.Label F3_user_rights;
        private System.Windows.Forms.TextBox F3_textBox_username;
        private System.Windows.Forms.ComboBox F3_comboBox_userRights;
        private System.Windows.Forms.Button F3_button_assignUserRights;
        private System.Windows.Forms.Label F3_label_password;
        private System.Windows.Forms.TextBox F3_textBox_password;
        private System.Windows.Forms.ErrorProvider F3_errorProvider_userManagement;
        private System.Windows.Forms.Timer F3_timer_updateTimer;
        private System.Windows.Forms.Button F3_button_deleteAccount;
        private System.Windows.Forms.Label F3_label_user_details;
    }
}