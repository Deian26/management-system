namespace management_system
{
    partial class U1_CustomizationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(U1_CustomizationForm));
            this.U1_label_title = new System.Windows.Forms.Label();
            this.U1_tabControl_customization = new System.Windows.Forms.TabControl();
            this.U1_tabPage_languages = new System.Windows.Forms.TabPage();
            this.U1_label_description_addLanguage = new System.Windows.Forms.Label();
            this.U1_tabPage_themes = new System.Windows.Forms.TabPage();
            this.U1_label_description_addTheme = new System.Windows.Forms.Label();
            this.U1_tabControl_customization.SuspendLayout();
            this.U1_tabPage_languages.SuspendLayout();
            this.U1_tabPage_themes.SuspendLayout();
            this.SuspendLayout();
            // 
            // U1_label_title
            // 
            this.U1_label_title.AutoSize = true;
            this.U1_label_title.Font = new System.Drawing.Font("Cascadia Code", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.U1_label_title.Location = new System.Drawing.Point(12, 10);
            this.U1_label_title.Name = "U1_label_title";
            this.U1_label_title.Size = new System.Drawing.Size(417, 37);
            this.U1_label_title.TabIndex = 0;
            this.U1_label_title.Text = "Customize the application";
            // 
            // U1_tabControl_customization
            // 
            this.U1_tabControl_customization.Controls.Add(this.U1_tabPage_languages);
            this.U1_tabControl_customization.Controls.Add(this.U1_tabPage_themes);
            this.U1_tabControl_customization.Location = new System.Drawing.Point(12, 47);
            this.U1_tabControl_customization.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.U1_tabControl_customization.Name = "U1_tabControl_customization";
            this.U1_tabControl_customization.SelectedIndex = 0;
            this.U1_tabControl_customization.Size = new System.Drawing.Size(776, 419);
            this.U1_tabControl_customization.TabIndex = 2;
            // 
            // U1_tabPage_languages
            // 
            this.U1_tabPage_languages.Controls.Add(this.U1_label_description_addLanguage);
            this.U1_tabPage_languages.Location = new System.Drawing.Point(4, 26);
            this.U1_tabPage_languages.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.U1_tabPage_languages.Name = "U1_tabPage_languages";
            this.U1_tabPage_languages.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.U1_tabPage_languages.Size = new System.Drawing.Size(768, 389);
            this.U1_tabPage_languages.TabIndex = 0;
            this.U1_tabPage_languages.Text = "Languages";
            this.U1_tabPage_languages.UseVisualStyleBackColor = true;
            // 
            // U1_label_description_addLanguage
            // 
            this.U1_label_description_addLanguage.AutoSize = true;
            this.U1_label_description_addLanguage.Font = new System.Drawing.Font("Cascadia Code", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.U1_label_description_addLanguage.Location = new System.Drawing.Point(8, 19);
            this.U1_label_description_addLanguage.Name = "U1_label_description_addLanguage";
            this.U1_label_description_addLanguage.Size = new System.Drawing.Size(780, 27);
            this.U1_label_description_addLanguage.TabIndex = 3;
            this.U1_label_description_addLanguage.Text = "This tab allows the addition of new themes to the language pack.";
            // 
            // U1_tabPage_themes
            // 
            this.U1_tabPage_themes.Controls.Add(this.U1_label_description_addTheme);
            this.U1_tabPage_themes.Location = new System.Drawing.Point(4, 26);
            this.U1_tabPage_themes.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.U1_tabPage_themes.Name = "U1_tabPage_themes";
            this.U1_tabPage_themes.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.U1_tabPage_themes.Size = new System.Drawing.Size(768, 389);
            this.U1_tabPage_themes.TabIndex = 1;
            this.U1_tabPage_themes.Text = "Themes";
            this.U1_tabPage_themes.UseVisualStyleBackColor = true;
            // 
            // U1_label_description_addTheme
            // 
            this.U1_label_description_addTheme.AutoSize = true;
            this.U1_label_description_addTheme.Font = new System.Drawing.Font("Cascadia Code", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.U1_label_description_addTheme.Location = new System.Drawing.Point(8, 19);
            this.U1_label_description_addTheme.Name = "U1_label_description_addTheme";
            this.U1_label_description_addTheme.Size = new System.Drawing.Size(744, 27);
            this.U1_label_description_addTheme.TabIndex = 2;
            this.U1_label_description_addTheme.Text = "This tab allows the addition of new themes to the theme pack.";
            // 
            // U1_CustomizationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 478);
            this.Controls.Add(this.U1_tabControl_customization);
            this.Controls.Add(this.U1_label_title);
            this.Font = new System.Drawing.Font("Cascadia Code", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "U1_CustomizationForm";
            this.Text = "Customization";
            this.Load += new System.EventHandler(this.U1_CustomizationForm_Load);
            this.U1_tabControl_customization.ResumeLayout(false);
            this.U1_tabPage_languages.ResumeLayout(false);
            this.U1_tabPage_languages.PerformLayout();
            this.U1_tabPage_themes.ResumeLayout(false);
            this.U1_tabPage_themes.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label U1_label_title;
        private System.Windows.Forms.TabControl U1_tabControl_customization;
        private System.Windows.Forms.TabPage U1_tabPage_languages;
        private System.Windows.Forms.TabPage U1_tabPage_themes;
        private System.Windows.Forms.Label U1_label_description_addTheme;
        private System.Windows.Forms.Label U1_label_description_addLanguage;
    }
}