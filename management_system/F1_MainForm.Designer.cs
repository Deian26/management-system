namespace management_system
{
    partial class F1_MainForm
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
            this.F1_label_greeting = new System.Windows.Forms.Label();
            this.F1_label_usernameDisplay = new System.Windows.Forms.Label();
            this.F1_panel_greeting = new System.Windows.Forms.Panel();
            this.F1_groupBox_mainControls = new System.Windows.Forms.GroupBox();
            this.F1_groupBox_accountDetails = new System.Windows.Forms.GroupBox();
            this.F1_label_dataBaseNameDisplay = new System.Windows.Forms.Label();
            this.F1_panel_greeting.SuspendLayout();
            this.F1_groupBox_mainControls.SuspendLayout();
            this.F1_groupBox_accountDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // F1_label_greeting
            // 
            this.F1_label_greeting.AutoSize = true;
            this.F1_label_greeting.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.F1_label_greeting.Location = new System.Drawing.Point(15, 16);
            this.F1_label_greeting.Name = "F1_label_greeting";
            this.F1_label_greeting.Size = new System.Drawing.Size(127, 22);
            this.F1_label_greeting.TabIndex = 3;
            this.F1_label_greeting.Text = "#GREETING#";
            // 
            // F1_label_usernameDisplay
            // 
            this.F1_label_usernameDisplay.AutoSize = true;
            this.F1_label_usernameDisplay.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.F1_label_usernameDisplay.Location = new System.Drawing.Point(6, 24);
            this.F1_label_usernameDisplay.Name = "F1_label_usernameDisplay";
            this.F1_label_usernameDisplay.Size = new System.Drawing.Size(136, 22);
            this.F1_label_usernameDisplay.TabIndex = 4;
            this.F1_label_usernameDisplay.Text = "#USERNAME#";
            // 
            // F1_panel_greeting
            // 
            this.F1_panel_greeting.Controls.Add(this.F1_label_greeting);
            this.F1_panel_greeting.Location = new System.Drawing.Point(16, 21);
            this.F1_panel_greeting.Name = "F1_panel_greeting";
            this.F1_panel_greeting.Size = new System.Drawing.Size(529, 54);
            this.F1_panel_greeting.TabIndex = 5;
            // 
            // F1_groupBox_mainControls
            // 
            this.F1_groupBox_mainControls.Controls.Add(this.F1_groupBox_accountDetails);
            this.F1_groupBox_mainControls.Controls.Add(this.F1_panel_greeting);
            this.F1_groupBox_mainControls.Location = new System.Drawing.Point(12, 12);
            this.F1_groupBox_mainControls.Name = "F1_groupBox_mainControls";
            this.F1_groupBox_mainControls.Size = new System.Drawing.Size(776, 426);
            this.F1_groupBox_mainControls.TabIndex = 6;
            this.F1_groupBox_mainControls.TabStop = false;
            this.F1_groupBox_mainControls.Enter += new System.EventHandler(this.F1_groupBox_mainControls_Enter);
            // 
            // F1_groupBox_accountDetails
            // 
            this.F1_groupBox_accountDetails.Controls.Add(this.F1_label_dataBaseNameDisplay);
            this.F1_groupBox_accountDetails.Controls.Add(this.F1_label_usernameDisplay);
            this.F1_groupBox_accountDetails.Location = new System.Drawing.Point(551, 13);
            this.F1_groupBox_accountDetails.Name = "F1_groupBox_accountDetails";
            this.F1_groupBox_accountDetails.Size = new System.Drawing.Size(219, 100);
            this.F1_groupBox_accountDetails.TabIndex = 5;
            this.F1_groupBox_accountDetails.TabStop = false;
            this.F1_groupBox_accountDetails.Text = "#USER_ACCESS_RIGHTS#";
            // 
            // F1_label_dataBaseNameDisplay
            // 
            this.F1_label_dataBaseNameDisplay.AutoSize = true;
            this.F1_label_dataBaseNameDisplay.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.F1_label_dataBaseNameDisplay.Location = new System.Drawing.Point(6, 46);
            this.F1_label_dataBaseNameDisplay.Name = "F1_label_dataBaseNameDisplay";
            this.F1_label_dataBaseNameDisplay.Size = new System.Drawing.Size(130, 22);
            this.F1_label_dataBaseNameDisplay.TabIndex = 5;
            this.F1_label_dataBaseNameDisplay.Text = "#DATABASE#";
            // 
            // F1_MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.F1_groupBox_mainControls);
            this.Name = "F1_MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main page";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.F1_MainForm_FormClosed);
            this.Load += new System.EventHandler(this.F1_MainForm_Load);
            this.F1_panel_greeting.ResumeLayout(false);
            this.F1_panel_greeting.PerformLayout();
            this.F1_groupBox_mainControls.ResumeLayout(false);
            this.F1_groupBox_accountDetails.ResumeLayout(false);
            this.F1_groupBox_accountDetails.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label F1_label_greeting;
        private System.Windows.Forms.Label F1_label_usernameDisplay;
        private System.Windows.Forms.Panel F1_panel_greeting;
        private System.Windows.Forms.GroupBox F1_groupBox_mainControls;
        private System.Windows.Forms.GroupBox F1_groupBox_accountDetails;
        private System.Windows.Forms.Label F1_label_dataBaseNameDisplay;
    }
}