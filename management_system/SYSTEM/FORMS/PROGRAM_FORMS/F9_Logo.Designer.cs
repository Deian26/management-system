namespace management_system
{
    partial class F9_Logo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F9_Logo));
            this.F9_timer_logoTimer = new System.Windows.Forms.Timer(this.components);
            this.F0_label_LogoTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // F9_timer_logoTimer
            // 
            this.F9_timer_logoTimer.Tick += new System.EventHandler(this.F9_timer_logoTimer_Tick);
            // 
            // F0_label_LogoTitle
            // 
            this.F0_label_LogoTitle.AutoSize = true;
            this.F0_label_LogoTitle.BackColor = System.Drawing.Color.Transparent;
            this.F0_label_LogoTitle.Font = new System.Drawing.Font("Cascadia Code", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.F0_label_LogoTitle.Location = new System.Drawing.Point(32, 399);
            this.F0_label_LogoTitle.Name = "F0_label_LogoTitle";
            this.F0_label_LogoTitle.Size = new System.Drawing.Size(252, 54);
            this.F0_label_LogoTitle.TabIndex = 0;
            this.F0_label_LogoTitle.Text = "Management System\r\nTrif Paul Deian 2023";
            // 
            // F9_Logo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(800, 719);
            this.Controls.Add(this.F0_label_LogoTitle);
            this.Enabled = false;
            this.Font = new System.Drawing.Font("Cascadia Code", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximumSize = new System.Drawing.Size(800, 719);
            this.MinimumSize = new System.Drawing.Size(800, 719);
            this.Name = "F9_Logo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "F9_Logo";
            this.Load += new System.EventHandler(this.F9_Logo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer F9_timer_logoTimer;
        private System.Windows.Forms.Label F0_label_LogoTitle;
    }
}