namespace management_system
{
    partial class F0_Login
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
            this.F0_textBox_username = new System.Windows.Forms.TextBox();
            this.F0_textBox_password = new System.Windows.Forms.TextBox();
            this.F0_errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.F0_label_username = new System.Windows.Forms.Label();
            this.F0_label_password = new System.Windows.Forms.Label();
            this.F0_label_titleLogIn = new System.Windows.Forms.Label();
            this.F0_button_login = new System.Windows.Forms.Button();
            this.F0_button_register = new System.Windows.Forms.Button();
            this.F0_comboBox_dataBase = new System.Windows.Forms.ComboBox();
            this.F0_label_dataBase = new System.Windows.Forms.Label();
            this.F0_button_language = new System.Windows.Forms.Button();
            this.F0_button_theme = new System.Windows.Forms.Button();
            this.F0_timer_errorClear = new System.Windows.Forms.Timer(this.components);
            this.F0_timer_warningClear = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.F0_errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // F0_textBox_username
            // 
            this.F0_textBox_username.Location = new System.Drawing.Point(154, 75);
            this.F0_textBox_username.Name = "F0_textBox_username";
            this.F0_textBox_username.Size = new System.Drawing.Size(255, 22);
            this.F0_textBox_username.TabIndex = 0;
            this.F0_textBox_username.TextChanged += new System.EventHandler(this.F0_textBox_username_TextChanged);
            // 
            // F0_textBox_password
            // 
            this.F0_textBox_password.Location = new System.Drawing.Point(154, 128);
            this.F0_textBox_password.Name = "F0_textBox_password";
            this.F0_textBox_password.Size = new System.Drawing.Size(255, 22);
            this.F0_textBox_password.TabIndex = 1;
            this.F0_textBox_password.TextChanged += new System.EventHandler(this.F0_textBox_password_TextChanged);
            // 
            // F0_errorProvider
            // 
            this.F0_errorProvider.ContainerControl = this;
            // 
            // F0_label_username
            // 
            this.F0_label_username.AutoSize = true;
            this.F0_label_username.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.F0_label_username.Location = new System.Drawing.Point(12, 74);
            this.F0_label_username.Name = "F0_label_username";
            this.F0_label_username.Size = new System.Drawing.Size(94, 22);
            this.F0_label_username.TabIndex = 2;
            this.F0_label_username.Text = "Username:";
            // 
            // F0_label_password
            // 
            this.F0_label_password.AutoSize = true;
            this.F0_label_password.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.F0_label_password.Location = new System.Drawing.Point(12, 128);
            this.F0_label_password.Name = "F0_label_password";
            this.F0_label_password.Size = new System.Drawing.Size(94, 22);
            this.F0_label_password.TabIndex = 3;
            this.F0_label_password.Text = "Password:";
            // 
            // F0_label_titleLogIn
            // 
            this.F0_label_titleLogIn.AutoSize = true;
            this.F0_label_titleLogIn.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.F0_label_titleLogIn.Location = new System.Drawing.Point(8, 9);
            this.F0_label_titleLogIn.Name = "F0_label_titleLogIn";
            this.F0_label_titleLogIn.Size = new System.Drawing.Size(92, 32);
            this.F0_label_titleLogIn.TabIndex = 4;
            this.F0_label_titleLogIn.Text = "Log in";
            // 
            // F0_button_login
            // 
            this.F0_button_login.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.F0_button_login.Location = new System.Drawing.Point(294, 223);
            this.F0_button_login.Name = "F0_button_login";
            this.F0_button_login.Size = new System.Drawing.Size(115, 45);
            this.F0_button_login.TabIndex = 5;
            this.F0_button_login.Text = "Login";
            this.F0_button_login.UseVisualStyleBackColor = true;
            this.F0_button_login.Click += new System.EventHandler(this.F0_button_login_Click);
            // 
            // F0_button_register
            // 
            this.F0_button_register.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.F0_button_register.Location = new System.Drawing.Point(154, 223);
            this.F0_button_register.Name = "F0_button_register";
            this.F0_button_register.Size = new System.Drawing.Size(115, 45);
            this.F0_button_register.TabIndex = 6;
            this.F0_button_register.Text = "Register";
            this.F0_button_register.UseVisualStyleBackColor = true;
            this.F0_button_register.Click += new System.EventHandler(this.F0_button_register_Click);
            // 
            // F0_comboBox_dataBase
            // 
            this.F0_comboBox_dataBase.FormattingEnabled = true;
            this.F0_comboBox_dataBase.Location = new System.Drawing.Point(154, 178);
            this.F0_comboBox_dataBase.Name = "F0_comboBox_dataBase";
            this.F0_comboBox_dataBase.Size = new System.Drawing.Size(255, 24);
            this.F0_comboBox_dataBase.TabIndex = 8;
            this.F0_comboBox_dataBase.SelectedIndexChanged += new System.EventHandler(this.F0_comboBox_dataBase_SelectedIndexChanged);
            // 
            // F0_label_dataBase
            // 
            this.F0_label_dataBase.AutoSize = true;
            this.F0_label_dataBase.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.F0_label_dataBase.Location = new System.Drawing.Point(12, 180);
            this.F0_label_dataBase.Name = "F0_label_dataBase";
            this.F0_label_dataBase.Size = new System.Drawing.Size(92, 22);
            this.F0_label_dataBase.TabIndex = 9;
            this.F0_label_dataBase.Text = "DataBase:";
            // 
            // F0_button_language
            // 
            this.F0_button_language.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.F0_button_language.Location = new System.Drawing.Point(283, 9);
            this.F0_button_language.Name = "F0_button_language";
            this.F0_button_language.Size = new System.Drawing.Size(60, 30);
            this.F0_button_language.TabIndex = 10;
            this.F0_button_language.Text = "EN";
            this.F0_button_language.UseVisualStyleBackColor = true;
            this.F0_button_language.Click += new System.EventHandler(this.F0_button_language_Click);
            // 
            // F0_button_theme
            // 
            this.F0_button_theme.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.F0_button_theme.Location = new System.Drawing.Point(349, 9);
            this.F0_button_theme.Name = "F0_button_theme";
            this.F0_button_theme.Size = new System.Drawing.Size(60, 30);
            this.F0_button_theme.TabIndex = 11;
            this.F0_button_theme.Text = "TH";
            this.F0_button_theme.UseVisualStyleBackColor = true;
            this.F0_button_theme.Click += new System.EventHandler(this.F0_button_theme_Click);
            // 
            // F0_timer_errorClear
            // 
            this.F0_timer_errorClear.Tick += new System.EventHandler(this.F0_timer_errorClear_Tick);
            // 
            // F0_timer_warningClear
            // 
            this.F0_timer_warningClear.Tick += new System.EventHandler(this.F0_timer_warningClear_Tick);
            // 
            // F0_Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 280);
            this.Controls.Add(this.F0_button_theme);
            this.Controls.Add(this.F0_button_language);
            this.Controls.Add(this.F0_label_dataBase);
            this.Controls.Add(this.F0_comboBox_dataBase);
            this.Controls.Add(this.F0_button_register);
            this.Controls.Add(this.F0_button_login);
            this.Controls.Add(this.F0_label_titleLogIn);
            this.Controls.Add(this.F0_label_password);
            this.Controls.Add(this.F0_label_username);
            this.Controls.Add(this.F0_textBox_password);
            this.Controls.Add(this.F0_textBox_username);
            this.MaximumSize = new System.Drawing.Size(460, 327);
            this.MinimumSize = new System.Drawing.Size(460, 327);
            this.Name = "F0_Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.F0_Login_FormClosed);
            this.Load += new System.EventHandler(this.F0_Login_Load);
            ((System.ComponentModel.ISupportInitialize)(this.F0_errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox F0_textBox_username;
        private System.Windows.Forms.TextBox F0_textBox_password;
        private System.Windows.Forms.ErrorProvider F0_errorProvider;
        private System.Windows.Forms.Label F0_label_password;
        private System.Windows.Forms.Label F0_label_username;
        private System.Windows.Forms.Button F0_button_register;
        private System.Windows.Forms.Button F0_button_login;
        private System.Windows.Forms.Label F0_label_titleLogIn;
        private System.Windows.Forms.Label F0_label_dataBase;
        private System.Windows.Forms.ComboBox F0_comboBox_dataBase;
        private System.Windows.Forms.Button F0_button_language;
        public System.Windows.Forms.Timer F0_timer_errorClear;
        public System.Windows.Forms.Button F0_button_theme;
        private System.Windows.Forms.Timer F0_timer_warningClear;
    }
}

