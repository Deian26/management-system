namespace management_system
{
    partial class F2_MainFormNotificationWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F2_MainFormNotificationWindow));
            this.F1_label_sender = new System.Windows.Forms.Label();
            this.F2_label_dateSent = new System.Windows.Forms.Label();
            this.F2_richTextBox_notificationText = new System.Windows.Forms.RichTextBox();
            this.F2_textBox_sender = new System.Windows.Forms.TextBox();
            this.F2_textBox_dateSent = new System.Windows.Forms.TextBox();
            this.F2_pictureBox_notificationImportance = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.F2_pictureBox_notificationImportance)).BeginInit();
            this.SuspendLayout();
            // 
            // F1_label_sender
            // 
            this.F1_label_sender.AutoSize = true;
            this.F1_label_sender.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.F1_label_sender.Location = new System.Drawing.Point(289, 12);
            this.F1_label_sender.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.F1_label_sender.Name = "F1_label_sender";
            this.F1_label_sender.Size = new System.Drawing.Size(72, 19);
            this.F1_label_sender.TabIndex = 4;
            this.F1_label_sender.Text = "Sender:";
            // 
            // F2_label_dateSent
            // 
            this.F2_label_dateSent.AutoSize = true;
            this.F2_label_dateSent.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.F2_label_dateSent.Location = new System.Drawing.Point(262, 40);
            this.F2_label_dateSent.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.F2_label_dateSent.Name = "F2_label_dateSent";
            this.F2_label_dateSent.Size = new System.Drawing.Size(99, 19);
            this.F2_label_dateSent.TabIndex = 5;
            this.F2_label_dateSent.Text = "Date sent:";
            // 
            // F2_richTextBox_notificationText
            // 
            this.F2_richTextBox_notificationText.Location = new System.Drawing.Point(12, 87);
            this.F2_richTextBox_notificationText.Name = "F2_richTextBox_notificationText";
            this.F2_richTextBox_notificationText.ReadOnly = true;
            this.F2_richTextBox_notificationText.Size = new System.Drawing.Size(510, 162);
            this.F2_richTextBox_notificationText.TabIndex = 6;
            this.F2_richTextBox_notificationText.Text = "";
            // 
            // F2_textBox_sender
            // 
            this.F2_textBox_sender.Location = new System.Drawing.Point(366, 9);
            this.F2_textBox_sender.Name = "F2_textBox_sender";
            this.F2_textBox_sender.ReadOnly = true;
            this.F2_textBox_sender.Size = new System.Drawing.Size(156, 20);
            this.F2_textBox_sender.TabIndex = 7;
            // 
            // F2_textBox_dateSent
            // 
            this.F2_textBox_dateSent.Location = new System.Drawing.Point(366, 39);
            this.F2_textBox_dateSent.Name = "F2_textBox_dateSent";
            this.F2_textBox_dateSent.ReadOnly = true;
            this.F2_textBox_dateSent.Size = new System.Drawing.Size(156, 20);
            this.F2_textBox_dateSent.TabIndex = 8;
            // 
            // F2_pictureBox_notificationImportance
            // 
            this.F2_pictureBox_notificationImportance.Location = new System.Drawing.Point(12, 12);
            this.F2_pictureBox_notificationImportance.Name = "F2_pictureBox_notificationImportance";
            this.F2_pictureBox_notificationImportance.Size = new System.Drawing.Size(82, 69);
            this.F2_pictureBox_notificationImportance.TabIndex = 9;
            this.F2_pictureBox_notificationImportance.TabStop = false;
            // 
            // F2_MainFormNotificationWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 261);
            this.Controls.Add(this.F2_pictureBox_notificationImportance);
            this.Controls.Add(this.F2_textBox_dateSent);
            this.Controls.Add(this.F2_textBox_sender);
            this.Controls.Add(this.F2_richTextBox_notificationText);
            this.Controls.Add(this.F2_label_dateSent);
            this.Controls.Add(this.F1_label_sender);
            this.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(550, 300);
            this.MinimumSize = new System.Drawing.Size(550, 300);
            this.Name = "F2_MainFormNotificationWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "#NOTIFICATION_TITLE#";
            this.Load += new System.EventHandler(this.F2_MainFormNotificationWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.F2_pictureBox_notificationImportance)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label F1_label_sender;
        private System.Windows.Forms.Label F2_label_dateSent;
        private System.Windows.Forms.RichTextBox F2_richTextBox_notificationText;
        private System.Windows.Forms.TextBox F2_textBox_sender;
        private System.Windows.Forms.TextBox F2_textBox_dateSent;
        private System.Windows.Forms.PictureBox F2_pictureBox_notificationImportance;
    }
}