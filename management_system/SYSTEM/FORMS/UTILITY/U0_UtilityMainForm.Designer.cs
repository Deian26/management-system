namespace management_system
{
    partial class U0_UtilityMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(U0_UtilityMainForm));
            this.U0_groupBox_diagnostics = new System.Windows.Forms.GroupBox();
            this.U0_groupBox_diagnosticSettingFiles = new System.Windows.Forms.GroupBox();
            this.U0_label_XML_setting_files_status = new System.Windows.Forms.Label();
            this.U0_button_diagnosticCheck_XML_Setting_Files = new System.Windows.Forms.Button();
            this.U0_groupBox_diagnosticsDataBaseConnection = new System.Windows.Forms.GroupBox();
            this.U0_textBox_ConnectedDataBase = new System.Windows.Forms.TextBox();
            this.U0_label_ConnectedDataBase = new System.Windows.Forms.Label();
            this.U0_label_DataBaseConnectionStatus = new System.Windows.Forms.Label();
            this.U0_button_resetDataBaseConnection = new System.Windows.Forms.Button();
            this.U0_richTextBox_diagnosticLog = new System.Windows.Forms.RichTextBox();
            this.U0_label_diagnosticsLog = new System.Windows.Forms.Label();
            this.U0_eventLog_utilityService = new System.Diagnostics.EventLog();
            this.U0_statusStrip_utilityService = new System.Windows.Forms.StatusStrip();
            this.U0_toolStripStatusLabel_errorStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.U0_toolStripStatusLabel_separator = new System.Windows.Forms.ToolStripStatusLabel();
            this.U0_toolStripStatusLabel_warningStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.U0_toolStripStatusLabel_separator2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.U0_toolStripStatusLabel_notification = new System.Windows.Forms.ToolStripStatusLabel();
            this.U0_toolStripStatusLabel_separator3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.U0_toolStripProgressBar_actionStatus = new System.Windows.Forms.ToolStripProgressBar();
            this.U0_notifyIcon_utilityService = new System.Windows.Forms.NotifyIcon(this.components);
            this.U0_timer_logTimer = new System.Windows.Forms.Timer(this.components);
            this.U0_contextMenuStrip_diagnosticLog = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.U0_clearLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.U0_saveLogToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.U0_saveFileDialog_diagnosticLog = new System.Windows.Forms.SaveFileDialog();
            this.U0_timer_notification = new System.Windows.Forms.Timer(this.components);
            this.U0_button_openCustomizationForm = new System.Windows.Forms.Button();
            this.U0_groupBox_accountDetails = new System.Windows.Forms.GroupBox();
            this.U0_label_username = new System.Windows.Forms.Label();
            this.U0_timer_clearErr = new System.Windows.Forms.Timer(this.components);
            this.U0_timer_updateDBNotif = new System.Windows.Forms.Timer(this.components);
            this.U0_toolStripButton_EXIT = new System.Windows.Forms.ToolStripButton();
            this.U0_toolStrip_utilityService = new System.Windows.Forms.ToolStrip();
            this.U0_groupBox_diagnostics.SuspendLayout();
            this.U0_groupBox_diagnosticSettingFiles.SuspendLayout();
            this.U0_groupBox_diagnosticsDataBaseConnection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.U0_eventLog_utilityService)).BeginInit();
            this.U0_statusStrip_utilityService.SuspendLayout();
            this.U0_contextMenuStrip_diagnosticLog.SuspendLayout();
            this.U0_groupBox_accountDetails.SuspendLayout();
            this.U0_toolStrip_utilityService.SuspendLayout();
            this.SuspendLayout();
            // 
            // U0_groupBox_diagnostics
            // 
            this.U0_groupBox_diagnostics.Controls.Add(this.U0_groupBox_diagnosticSettingFiles);
            this.U0_groupBox_diagnostics.Controls.Add(this.U0_groupBox_diagnosticsDataBaseConnection);
            this.U0_groupBox_diagnostics.Controls.Add(this.U0_richTextBox_diagnosticLog);
            this.U0_groupBox_diagnostics.Controls.Add(this.U0_label_diagnosticsLog);
            resources.ApplyResources(this.U0_groupBox_diagnostics, "U0_groupBox_diagnostics");
            this.U0_groupBox_diagnostics.Name = "U0_groupBox_diagnostics";
            this.U0_groupBox_diagnostics.TabStop = false;
            // 
            // U0_groupBox_diagnosticSettingFiles
            // 
            this.U0_groupBox_diagnosticSettingFiles.Controls.Add(this.U0_label_XML_setting_files_status);
            this.U0_groupBox_diagnosticSettingFiles.Controls.Add(this.U0_button_diagnosticCheck_XML_Setting_Files);
            resources.ApplyResources(this.U0_groupBox_diagnosticSettingFiles, "U0_groupBox_diagnosticSettingFiles");
            this.U0_groupBox_diagnosticSettingFiles.Name = "U0_groupBox_diagnosticSettingFiles";
            this.U0_groupBox_diagnosticSettingFiles.TabStop = false;
            // 
            // U0_label_XML_setting_files_status
            // 
            resources.ApplyResources(this.U0_label_XML_setting_files_status, "U0_label_XML_setting_files_status");
            this.U0_label_XML_setting_files_status.Name = "U0_label_XML_setting_files_status";
            // 
            // U0_button_diagnosticCheck_XML_Setting_Files
            // 
            resources.ApplyResources(this.U0_button_diagnosticCheck_XML_Setting_Files, "U0_button_diagnosticCheck_XML_Setting_Files");
            this.U0_button_diagnosticCheck_XML_Setting_Files.Name = "U0_button_diagnosticCheck_XML_Setting_Files";
            this.U0_button_diagnosticCheck_XML_Setting_Files.UseVisualStyleBackColor = true;
            this.U0_button_diagnosticCheck_XML_Setting_Files.Click += new System.EventHandler(this.U0_button_diagnosticCheck_XML_Setting_Files_Click);
            // 
            // U0_groupBox_diagnosticsDataBaseConnection
            // 
            this.U0_groupBox_diagnosticsDataBaseConnection.Controls.Add(this.U0_textBox_ConnectedDataBase);
            this.U0_groupBox_diagnosticsDataBaseConnection.Controls.Add(this.U0_label_ConnectedDataBase);
            this.U0_groupBox_diagnosticsDataBaseConnection.Controls.Add(this.U0_label_DataBaseConnectionStatus);
            this.U0_groupBox_diagnosticsDataBaseConnection.Controls.Add(this.U0_button_resetDataBaseConnection);
            resources.ApplyResources(this.U0_groupBox_diagnosticsDataBaseConnection, "U0_groupBox_diagnosticsDataBaseConnection");
            this.U0_groupBox_diagnosticsDataBaseConnection.Name = "U0_groupBox_diagnosticsDataBaseConnection";
            this.U0_groupBox_diagnosticsDataBaseConnection.TabStop = false;
            // 
            // U0_textBox_ConnectedDataBase
            // 
            resources.ApplyResources(this.U0_textBox_ConnectedDataBase, "U0_textBox_ConnectedDataBase");
            this.U0_textBox_ConnectedDataBase.Name = "U0_textBox_ConnectedDataBase";
            this.U0_textBox_ConnectedDataBase.ReadOnly = true;
            // 
            // U0_label_ConnectedDataBase
            // 
            resources.ApplyResources(this.U0_label_ConnectedDataBase, "U0_label_ConnectedDataBase");
            this.U0_label_ConnectedDataBase.Name = "U0_label_ConnectedDataBase";
            // 
            // U0_label_DataBaseConnectionStatus
            // 
            resources.ApplyResources(this.U0_label_DataBaseConnectionStatus, "U0_label_DataBaseConnectionStatus");
            this.U0_label_DataBaseConnectionStatus.Name = "U0_label_DataBaseConnectionStatus";
            // 
            // U0_button_resetDataBaseConnection
            // 
            resources.ApplyResources(this.U0_button_resetDataBaseConnection, "U0_button_resetDataBaseConnection");
            this.U0_button_resetDataBaseConnection.Name = "U0_button_resetDataBaseConnection";
            this.U0_button_resetDataBaseConnection.UseVisualStyleBackColor = true;
            this.U0_button_resetDataBaseConnection.Click += new System.EventHandler(this.U0_button_resetDataBaseConnection_Click_1);
            // 
            // U0_richTextBox_diagnosticLog
            // 
            this.U0_richTextBox_diagnosticLog.DetectUrls = false;
            resources.ApplyResources(this.U0_richTextBox_diagnosticLog, "U0_richTextBox_diagnosticLog");
            this.U0_richTextBox_diagnosticLog.Name = "U0_richTextBox_diagnosticLog";
            this.U0_richTextBox_diagnosticLog.ReadOnly = true;
            this.U0_richTextBox_diagnosticLog.TextChanged += new System.EventHandler(this.U0_richTextBox_diagnosticLog_TextChanged);
            this.U0_richTextBox_diagnosticLog.MouseDown += new System.Windows.Forms.MouseEventHandler(this.U0_richTextBox_diagnosticLog_MouseDown);
            // 
            // U0_label_diagnosticsLog
            // 
            resources.ApplyResources(this.U0_label_diagnosticsLog, "U0_label_diagnosticsLog");
            this.U0_label_diagnosticsLog.Name = "U0_label_diagnosticsLog";
            // 
            // U0_eventLog_utilityService
            // 
            this.U0_eventLog_utilityService.SynchronizingObject = this;
            this.U0_eventLog_utilityService.EntryWritten += new System.Diagnostics.EntryWrittenEventHandler(this.eventLog1_EntryWritten);
            // 
            // U0_statusStrip_utilityService
            // 
            this.U0_statusStrip_utilityService.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.U0_statusStrip_utilityService.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.U0_toolStripStatusLabel_errorStatus,
            this.U0_toolStripStatusLabel_separator,
            this.U0_toolStripStatusLabel_warningStatus,
            this.U0_toolStripStatusLabel_separator2,
            this.U0_toolStripStatusLabel_notification,
            this.U0_toolStripStatusLabel_separator3,
            this.U0_toolStripProgressBar_actionStatus});
            resources.ApplyResources(this.U0_statusStrip_utilityService, "U0_statusStrip_utilityService");
            this.U0_statusStrip_utilityService.Name = "U0_statusStrip_utilityService";
            // 
            // U0_toolStripStatusLabel_errorStatus
            // 
            resources.ApplyResources(this.U0_toolStripStatusLabel_errorStatus, "U0_toolStripStatusLabel_errorStatus");
            this.U0_toolStripStatusLabel_errorStatus.Name = "U0_toolStripStatusLabel_errorStatus";
            // 
            // U0_toolStripStatusLabel_separator
            // 
            this.U0_toolStripStatusLabel_separator.Name = "U0_toolStripStatusLabel_separator";
            resources.ApplyResources(this.U0_toolStripStatusLabel_separator, "U0_toolStripStatusLabel_separator");
            // 
            // U0_toolStripStatusLabel_warningStatus
            // 
            resources.ApplyResources(this.U0_toolStripStatusLabel_warningStatus, "U0_toolStripStatusLabel_warningStatus");
            this.U0_toolStripStatusLabel_warningStatus.Name = "U0_toolStripStatusLabel_warningStatus";
            // 
            // U0_toolStripStatusLabel_separator2
            // 
            this.U0_toolStripStatusLabel_separator2.Name = "U0_toolStripStatusLabel_separator2";
            resources.ApplyResources(this.U0_toolStripStatusLabel_separator2, "U0_toolStripStatusLabel_separator2");
            // 
            // U0_toolStripStatusLabel_notification
            // 
            resources.ApplyResources(this.U0_toolStripStatusLabel_notification, "U0_toolStripStatusLabel_notification");
            this.U0_toolStripStatusLabel_notification.Name = "U0_toolStripStatusLabel_notification";
            // 
            // U0_toolStripStatusLabel_separator3
            // 
            this.U0_toolStripStatusLabel_separator3.Name = "U0_toolStripStatusLabel_separator3";
            resources.ApplyResources(this.U0_toolStripStatusLabel_separator3, "U0_toolStripStatusLabel_separator3");
            // 
            // U0_toolStripProgressBar_actionStatus
            // 
            resources.ApplyResources(this.U0_toolStripProgressBar_actionStatus, "U0_toolStripProgressBar_actionStatus");
            this.U0_toolStripProgressBar_actionStatus.Name = "U0_toolStripProgressBar_actionStatus";
            // 
            // U0_notifyIcon_utilityService
            // 
            resources.ApplyResources(this.U0_notifyIcon_utilityService, "U0_notifyIcon_utilityService");
            // 
            // U0_timer_logTimer
            // 
            this.U0_timer_logTimer.Tick += new System.EventHandler(this.U0_timer_logTimer_Tick);
            // 
            // U0_contextMenuStrip_diagnosticLog
            // 
            this.U0_contextMenuStrip_diagnosticLog.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.U0_contextMenuStrip_diagnosticLog.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.U0_clearLogToolStripMenuItem,
            this.U0_saveLogToFileToolStripMenuItem});
            this.U0_contextMenuStrip_diagnosticLog.Name = "U0_contextMenuStrip_diagnosticLog";
            resources.ApplyResources(this.U0_contextMenuStrip_diagnosticLog, "U0_contextMenuStrip_diagnosticLog");
            // 
            // U0_clearLogToolStripMenuItem
            // 
            this.U0_clearLogToolStripMenuItem.Name = "U0_clearLogToolStripMenuItem";
            resources.ApplyResources(this.U0_clearLogToolStripMenuItem, "U0_clearLogToolStripMenuItem");
            this.U0_clearLogToolStripMenuItem.Click += new System.EventHandler(this.U0_clearLogToolStripMenuItem_Click);
            // 
            // U0_saveLogToFileToolStripMenuItem
            // 
            this.U0_saveLogToFileToolStripMenuItem.Name = "U0_saveLogToFileToolStripMenuItem";
            resources.ApplyResources(this.U0_saveLogToFileToolStripMenuItem, "U0_saveLogToFileToolStripMenuItem");
            this.U0_saveLogToFileToolStripMenuItem.Click += new System.EventHandler(this.U0_saveLogToFileToolStripMenuItem_Click_1);
            // 
            // U0_saveFileDialog_diagnosticLog
            // 
            this.U0_saveFileDialog_diagnosticLog.FileOk += new System.ComponentModel.CancelEventHandler(this.U0_saveFileDialog_diagnosticLog_FileOk);
            // 
            // U0_timer_notification
            // 
            this.U0_timer_notification.Tick += new System.EventHandler(this.U0_timer_notification_Tick);
            // 
            // U0_button_openCustomizationForm
            // 
            resources.ApplyResources(this.U0_button_openCustomizationForm, "U0_button_openCustomizationForm");
            this.U0_button_openCustomizationForm.Name = "U0_button_openCustomizationForm";
            this.U0_button_openCustomizationForm.UseVisualStyleBackColor = true;
            this.U0_button_openCustomizationForm.Click += new System.EventHandler(this.U0_button_openCustomizationForm_Click);
            // 
            // U0_groupBox_accountDetails
            // 
            this.U0_groupBox_accountDetails.Controls.Add(this.U0_label_username);
            resources.ApplyResources(this.U0_groupBox_accountDetails, "U0_groupBox_accountDetails");
            this.U0_groupBox_accountDetails.Name = "U0_groupBox_accountDetails";
            this.U0_groupBox_accountDetails.TabStop = false;
            // 
            // U0_label_username
            // 
            resources.ApplyResources(this.U0_label_username, "U0_label_username");
            this.U0_label_username.Name = "U0_label_username";
            this.U0_label_username.Click += new System.EventHandler(this.U0_label_username_Click);
            // 
            // U0_timer_clearErr
            // 
            this.U0_timer_clearErr.Tick += new System.EventHandler(this.U0_timer_clearErr_Tick);
            // 
            // U0_timer_updateDBNotif
            // 
            this.U0_timer_updateDBNotif.Tick += new System.EventHandler(this.U0_timer_updateDBNotif_Tick);
            // 
            // U0_toolStripButton_EXIT
            // 
            this.U0_toolStripButton_EXIT.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.U0_toolStripButton_EXIT.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.U0_toolStripButton_EXIT, "U0_toolStripButton_EXIT");
            this.U0_toolStripButton_EXIT.ForeColor = System.Drawing.Color.Red;
            this.U0_toolStripButton_EXIT.Name = "U0_toolStripButton_EXIT";
            this.U0_toolStripButton_EXIT.Click += new System.EventHandler(this.U0_toolStripButton_EXIT_Click);
            // 
            // U0_toolStrip_utilityService
            // 
            this.U0_toolStrip_utilityService.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.U0_toolStrip_utilityService.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.U0_toolStripButton_EXIT});
            resources.ApplyResources(this.U0_toolStrip_utilityService, "U0_toolStrip_utilityService");
            this.U0_toolStrip_utilityService.Name = "U0_toolStrip_utilityService";
            this.U0_toolStrip_utilityService.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.U0_toolStrip_utilityService_ItemClicked);
            // 
            // U0_UtilityMainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.U0_groupBox_accountDetails);
            this.Controls.Add(this.U0_button_openCustomizationForm);
            this.Controls.Add(this.U0_statusStrip_utilityService);
            this.Controls.Add(this.U0_toolStrip_utilityService);
            this.Controls.Add(this.U0_groupBox_diagnostics);
            this.Name = "U0_UtilityMainForm";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.U0_UtilityMainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.U0_UtilityMainForm_FormClosed);
            this.Load += new System.EventHandler(this.U0_UtilityMainForm_Load);
            this.U0_groupBox_diagnostics.ResumeLayout(false);
            this.U0_groupBox_diagnostics.PerformLayout();
            this.U0_groupBox_diagnosticSettingFiles.ResumeLayout(false);
            this.U0_groupBox_diagnosticSettingFiles.PerformLayout();
            this.U0_groupBox_diagnosticsDataBaseConnection.ResumeLayout(false);
            this.U0_groupBox_diagnosticsDataBaseConnection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.U0_eventLog_utilityService)).EndInit();
            this.U0_statusStrip_utilityService.ResumeLayout(false);
            this.U0_statusStrip_utilityService.PerformLayout();
            this.U0_contextMenuStrip_diagnosticLog.ResumeLayout(false);
            this.U0_groupBox_accountDetails.ResumeLayout(false);
            this.U0_groupBox_accountDetails.PerformLayout();
            this.U0_toolStrip_utilityService.ResumeLayout(false);
            this.U0_toolStrip_utilityService.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox U0_groupBox_diagnostics;
        private System.Windows.Forms.Label U0_label_diagnosticsLog;
        private System.Diagnostics.EventLog U0_eventLog_utilityService;
        private System.Windows.Forms.StatusStrip U0_statusStrip_utilityService;
        private System.Windows.Forms.ToolStripStatusLabel U0_toolStripStatusLabel_errorStatus;
        private System.Windows.Forms.ToolStripStatusLabel U0_toolStripStatusLabel_warningStatus;
        private System.Windows.Forms.ToolStripProgressBar U0_toolStripProgressBar_actionStatus;
        private System.Windows.Forms.RichTextBox U0_richTextBox_diagnosticLog;
        private System.Windows.Forms.Button U0_button_resetDataBaseConnection;
        private System.Windows.Forms.GroupBox U0_groupBox_diagnosticsDataBaseConnection;
        private System.Windows.Forms.NotifyIcon U0_notifyIcon_utilityService;
        private System.Windows.Forms.Label U0_label_DataBaseConnectionStatus;
        private System.Windows.Forms.TextBox U0_textBox_ConnectedDataBase;
        private System.Windows.Forms.Label U0_label_ConnectedDataBase;
        private System.Windows.Forms.GroupBox U0_groupBox_diagnosticSettingFiles;
        private System.Windows.Forms.Label U0_label_XML_setting_files_status;
        private System.Windows.Forms.Button U0_button_diagnosticCheck_XML_Setting_Files;
        private System.Windows.Forms.Timer U0_timer_logTimer;
        private System.Windows.Forms.ContextMenuStrip U0_contextMenuStrip_diagnosticLog;
        private System.Windows.Forms.ToolStripMenuItem U0_saveLogToFileToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog U0_saveFileDialog_diagnosticLog;
        private System.Windows.Forms.ToolStripMenuItem U0_clearLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel U0_toolStripStatusLabel_separator;
        private System.Windows.Forms.ToolStripStatusLabel U0_toolStripStatusLabel_separator3;
        private System.Windows.Forms.ToolStripStatusLabel U0_toolStripStatusLabel_notification;
        private System.Windows.Forms.ToolStripStatusLabel U0_toolStripStatusLabel_separator2;
        private System.Windows.Forms.Timer U0_timer_notification;
        private System.Windows.Forms.Button U0_button_openCustomizationForm;
        private System.Windows.Forms.GroupBox U0_groupBox_accountDetails;
        private System.Windows.Forms.Label U0_label_username;
        private System.Windows.Forms.Timer U0_timer_clearErr;
        private System.Windows.Forms.Timer U0_timer_updateDBNotif;
        private System.Windows.Forms.ToolStrip U0_toolStrip_utilityService;
        private System.Windows.Forms.ToolStripButton U0_toolStripButton_EXIT;
    }
}