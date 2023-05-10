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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F1_MainForm));
            this.F1_label_greeting = new System.Windows.Forms.Label();
            this.F1_label_usernameDisplay = new System.Windows.Forms.Label();
            this.F1_panel_greeting = new System.Windows.Forms.Panel();
            this.F1_groupBox_accountDetails = new System.Windows.Forms.GroupBox();
            this.F1_label_userRightsDisplay = new System.Windows.Forms.Label();
            this.F1_statusStrip_status = new System.Windows.Forms.StatusStrip();
            this.F1_toolStripStatusLabel_DataBaseName = new System.Windows.Forms.ToolStripStatusLabel();
            this.F1_toolStripStatusLabel_separator1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.F1_toolStripStatusLabel_ConnectionStatusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.F1_toolStripStatusLabel_connectionStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.F1_toolStripStatusLabel_separator2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.F1_toolStripStatusLabel_NumberOfNotificationsText = new System.Windows.Forms.ToolStripStatusLabel();
            this.F1_toolStripStatusLabel_NumberOfNotifications = new System.Windows.Forms.ToolStripStatusLabel();
            this.F1_toolStripStatusLabel_separator3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.F1_toolStripStatusLabel_DateTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.F1_toolStripStatusLabel_separator4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.F1_toolStripStatusLabel_notificationText = new System.Windows.Forms.ToolStripStatusLabel();
            this.F1_toolStrip_toolStrip = new System.Windows.Forms.ToolStrip();
            this.F1_toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.F1_toolStripDropDownButton_Help = new System.Windows.Forms.ToolStripDropDownButton();
            this.userGuideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guidesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.creatingAGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deletingAGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userAccessRightsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.F1_toolStripButton_About = new System.Windows.Forms.ToolStripButton();
            this.F1_toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.F1_toolStripButton_openUtilityService = new System.Windows.Forms.ToolStripButton();
            this.F1_toolStripButton_ADMIN_userManagement = new System.Windows.Forms.ToolStripButton();
            this.F2_toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.F1_toolStripButton_LogOut = new System.Windows.Forms.ToolStripButton();
            this.F1_timer_updateTimer = new System.Windows.Forms.Timer(this.components);
            this.F1_notifyIcon_notification = new System.Windows.Forms.NotifyIcon(this.components);
            this.F1_errorProvider_error = new System.Windows.Forms.ErrorProvider(this.components);
            this.F1_helpProvider_help = new System.Windows.Forms.HelpProvider();
            this.F1_listView_Notifications = new System.Windows.Forms.ListView();
            this.F1_timer_shutdown = new System.Windows.Forms.Timer(this.components);
            this.F1_label_groupsTitle = new System.Windows.Forms.Label();
            this.F1_timer_shutdownNotification = new System.Windows.Forms.Timer(this.components);
            this.F1_listView_groups = new System.Windows.Forms.ListView();
            this.F1_button_addNewGroup = new System.Windows.Forms.Button();
            this.F1_openFileDialog_openInformationFile = new System.Windows.Forms.OpenFileDialog();
            this.F1_panel_greeting.SuspendLayout();
            this.F1_groupBox_accountDetails.SuspendLayout();
            this.F1_statusStrip_status.SuspendLayout();
            this.F1_toolStrip_toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.F1_errorProvider_error)).BeginInit();
            this.SuspendLayout();
            // 
            // F1_label_greeting
            // 
            this.F1_label_greeting.AutoSize = true;
            this.F1_label_greeting.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.F1_label_greeting.Location = new System.Drawing.Point(11, 13);
            this.F1_label_greeting.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.F1_label_greeting.Name = "F1_label_greeting";
            this.F1_label_greeting.Size = new System.Drawing.Size(120, 23);
            this.F1_label_greeting.TabIndex = 3;
            this.F1_label_greeting.Text = "#GREETING#";
            // 
            // F1_label_usernameDisplay
            // 
            this.F1_label_usernameDisplay.AutoSize = true;
            this.F1_label_usernameDisplay.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.F1_label_usernameDisplay.Location = new System.Drawing.Point(4, 20);
            this.F1_label_usernameDisplay.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.F1_label_usernameDisplay.Name = "F1_label_usernameDisplay";
            this.F1_label_usernameDisplay.Size = new System.Drawing.Size(120, 23);
            this.F1_label_usernameDisplay.TabIndex = 4;
            this.F1_label_usernameDisplay.Text = "#USERNAME#";
            // 
            // F1_panel_greeting
            // 
            this.F1_panel_greeting.Controls.Add(this.F1_label_greeting);
            this.F1_panel_greeting.Location = new System.Drawing.Point(9, 34);
            this.F1_panel_greeting.Margin = new System.Windows.Forms.Padding(2);
            this.F1_panel_greeting.Name = "F1_panel_greeting";
            this.F1_panel_greeting.Size = new System.Drawing.Size(503, 44);
            this.F1_panel_greeting.TabIndex = 5;
            // 
            // F1_groupBox_accountDetails
            // 
            this.F1_groupBox_accountDetails.Controls.Add(this.F1_label_userRightsDisplay);
            this.F1_groupBox_accountDetails.Controls.Add(this.F1_label_usernameDisplay);
            this.F1_groupBox_accountDetails.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.F1_groupBox_accountDetails.Location = new System.Drawing.Point(516, 23);
            this.F1_groupBox_accountDetails.Margin = new System.Windows.Forms.Padding(2);
            this.F1_groupBox_accountDetails.Name = "F1_groupBox_accountDetails";
            this.F1_groupBox_accountDetails.Padding = new System.Windows.Forms.Padding(2);
            this.F1_groupBox_accountDetails.Size = new System.Drawing.Size(164, 81);
            this.F1_groupBox_accountDetails.TabIndex = 5;
            this.F1_groupBox_accountDetails.TabStop = false;
            this.F1_groupBox_accountDetails.Text = "#ACCOUNT_DETAILS#";
            // 
            // F1_label_userRightsDisplay
            // 
            this.F1_label_userRightsDisplay.AutoSize = true;
            this.F1_label_userRightsDisplay.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.F1_label_userRightsDisplay.Location = new System.Drawing.Point(4, 46);
            this.F1_label_userRightsDisplay.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.F1_label_userRightsDisplay.Name = "F1_label_userRightsDisplay";
            this.F1_label_userRightsDisplay.Size = new System.Drawing.Size(153, 23);
            this.F1_label_userRightsDisplay.TabIndex = 5;
            this.F1_label_userRightsDisplay.Text = "#USER_RIGHTS#";
            // 
            // F1_statusStrip_status
            // 
            this.F1_statusStrip_status.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.F1_statusStrip_status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.F1_toolStripStatusLabel_DataBaseName,
            this.F1_toolStripStatusLabel_separator1,
            this.F1_toolStripStatusLabel_ConnectionStatusText,
            this.F1_toolStripStatusLabel_connectionStatus,
            this.F1_toolStripStatusLabel_separator2,
            this.F1_toolStripStatusLabel_NumberOfNotificationsText,
            this.F1_toolStripStatusLabel_NumberOfNotifications,
            this.F1_toolStripStatusLabel_separator3,
            this.F1_toolStripStatusLabel_DateTime,
            this.F1_toolStripStatusLabel_separator4,
            this.F1_toolStripStatusLabel_notificationText});
            this.F1_statusStrip_status.Location = new System.Drawing.Point(0, 436);
            this.F1_statusStrip_status.Name = "F1_statusStrip_status";
            this.F1_statusStrip_status.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.F1_statusStrip_status.Size = new System.Drawing.Size(692, 26);
            this.F1_statusStrip_status.TabIndex = 7;
            // 
            // F1_toolStripStatusLabel_DataBaseName
            // 
            this.F1_toolStripStatusLabel_DataBaseName.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.F1_toolStripStatusLabel_DataBaseName.Name = "F1_toolStripStatusLabel_DataBaseName";
            this.F1_toolStripStatusLabel_DataBaseName.Size = new System.Drawing.Size(80, 20);
            this.F1_toolStripStatusLabel_DataBaseName.Text = "#DB_NAME#";
            // 
            // F1_toolStripStatusLabel_separator1
            // 
            this.F1_toolStripStatusLabel_separator1.Name = "F1_toolStripStatusLabel_separator1";
            this.F1_toolStripStatusLabel_separator1.Size = new System.Drawing.Size(13, 20);
            this.F1_toolStripStatusLabel_separator1.Text = "|";
            // 
            // F1_toolStripStatusLabel_ConnectionStatusText
            // 
            this.F1_toolStripStatusLabel_ConnectionStatusText.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.F1_toolStripStatusLabel_ConnectionStatusText.Name = "F1_toolStripStatusLabel_ConnectionStatusText";
            this.F1_toolStripStatusLabel_ConnectionStatusText.Size = new System.Drawing.Size(112, 20);
            this.F1_toolStripStatusLabel_ConnectionStatusText.Text = "#STATUS_TEXT#";
            // 
            // F1_toolStripStatusLabel_connectionStatus
            // 
            this.F1_toolStripStatusLabel_connectionStatus.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.F1_toolStripStatusLabel_connectionStatus.Name = "F1_toolStripStatusLabel_connectionStatus";
            this.F1_toolStripStatusLabel_connectionStatus.Size = new System.Drawing.Size(112, 20);
            this.F1_toolStripStatusLabel_connectionStatus.Text = "#CONN_STATUS#";
            // 
            // F1_toolStripStatusLabel_separator2
            // 
            this.F1_toolStripStatusLabel_separator2.Name = "F1_toolStripStatusLabel_separator2";
            this.F1_toolStripStatusLabel_separator2.Size = new System.Drawing.Size(13, 20);
            this.F1_toolStripStatusLabel_separator2.Text = "|";
            // 
            // F1_toolStripStatusLabel_NumberOfNotificationsText
            // 
            this.F1_toolStripStatusLabel_NumberOfNotificationsText.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.F1_toolStripStatusLabel_NumberOfNotificationsText.Name = "F1_toolStripStatusLabel_NumberOfNotificationsText";
            this.F1_toolStripStatusLabel_NumberOfNotificationsText.Size = new System.Drawing.Size(104, 20);
            this.F1_toolStripStatusLabel_NumberOfNotificationsText.Text = "#NOTIF_TEXT#";
            // 
            // F1_toolStripStatusLabel_NumberOfNotifications
            // 
            this.F1_toolStripStatusLabel_NumberOfNotifications.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.F1_toolStripStatusLabel_NumberOfNotifications.Name = "F1_toolStripStatusLabel_NumberOfNotifications";
            this.F1_toolStripStatusLabel_NumberOfNotifications.Size = new System.Drawing.Size(40, 20);
            this.F1_toolStripStatusLabel_NumberOfNotifications.Text = "#NR#";
            // 
            // F1_toolStripStatusLabel_separator3
            // 
            this.F1_toolStripStatusLabel_separator3.Name = "F1_toolStripStatusLabel_separator3";
            this.F1_toolStripStatusLabel_separator3.Size = new System.Drawing.Size(13, 20);
            this.F1_toolStripStatusLabel_separator3.Text = "|";
            // 
            // F1_toolStripStatusLabel_DateTime
            // 
            this.F1_toolStripStatusLabel_DateTime.Name = "F1_toolStripStatusLabel_DateTime";
            this.F1_toolStripStatusLabel_DateTime.Size = new System.Drawing.Size(93, 20);
            this.F1_toolStripStatusLabel_DateTime.Text = "#DATE_TIME";
            // 
            // F1_toolStripStatusLabel_separator4
            // 
            this.F1_toolStripStatusLabel_separator4.Name = "F1_toolStripStatusLabel_separator4";
            this.F1_toolStripStatusLabel_separator4.Size = new System.Drawing.Size(13, 20);
            this.F1_toolStripStatusLabel_separator4.Text = "|";
            // 
            // F1_toolStripStatusLabel_notificationText
            // 
            this.F1_toolStripStatusLabel_notificationText.Name = "F1_toolStripStatusLabel_notificationText";
            this.F1_toolStripStatusLabel_notificationText.Size = new System.Drawing.Size(67, 20);
            this.F1_toolStripStatusLabel_notificationText.Text = "#NOTIF#";
            // 
            // F1_toolStrip_toolStrip
            // 
            this.F1_toolStrip_toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.F1_toolStrip_toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.F1_toolStripSeparator1,
            this.F1_toolStripDropDownButton_Help,
            this.F1_toolStripButton_About,
            this.F1_toolStripSeparator2,
            this.F1_toolStripButton_openUtilityService,
            this.F1_toolStripButton_ADMIN_userManagement,
            this.F2_toolStripSeparator3,
            this.F1_toolStripButton_LogOut});
            this.F1_toolStrip_toolStrip.Location = new System.Drawing.Point(0, 0);
            this.F1_toolStrip_toolStrip.Name = "F1_toolStrip_toolStrip";
            this.F1_toolStrip_toolStrip.Size = new System.Drawing.Size(692, 25);
            this.F1_toolStrip_toolStrip.TabIndex = 8;
            this.F1_toolStrip_toolStrip.Text = "Utility service";
            this.F1_toolStrip_toolStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.F1_toolStrip_toolStrip_ItemClicked);
            // 
            // F1_toolStripSeparator1
            // 
            this.F1_toolStripSeparator1.Name = "F1_toolStripSeparator1";
            this.F1_toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // F1_toolStripDropDownButton_Help
            // 
            this.F1_toolStripDropDownButton_Help.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.F1_toolStripDropDownButton_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userGuideToolStripMenuItem,
            this.guidesToolStripMenuItem,
            this.userAccessRightsToolStripMenuItem});
            this.F1_toolStripDropDownButton_Help.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.F1_toolStripDropDownButton_Help.Image = ((System.Drawing.Image)(resources.GetObject("F1_toolStripDropDownButton_Help.Image")));
            this.F1_toolStripDropDownButton_Help.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.F1_toolStripDropDownButton_Help.Name = "F1_toolStripDropDownButton_Help";
            this.F1_toolStripDropDownButton_Help.Size = new System.Drawing.Size(54, 22);
            this.F1_toolStripDropDownButton_Help.Text = "Help";
            // 
            // userGuideToolStripMenuItem
            // 
            this.userGuideToolStripMenuItem.Name = "userGuideToolStripMenuItem";
            this.userGuideToolStripMenuItem.Size = new System.Drawing.Size(234, 26);
            this.userGuideToolStripMenuItem.Text = "User manual";
            this.userGuideToolStripMenuItem.Click += new System.EventHandler(this.userGuideToolStripMenuItem_Click);
            // 
            // guidesToolStripMenuItem
            // 
            this.guidesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.creatingAGroupToolStripMenuItem,
            this.deletingAGroupToolStripMenuItem});
            this.guidesToolStripMenuItem.Name = "guidesToolStripMenuItem";
            this.guidesToolStripMenuItem.Size = new System.Drawing.Size(234, 26);
            this.guidesToolStripMenuItem.Text = "Guides";
            this.guidesToolStripMenuItem.Click += new System.EventHandler(this.guidesToolStripMenuItem_Click);
            // 
            // creatingAGroupToolStripMenuItem
            // 
            this.creatingAGroupToolStripMenuItem.Name = "creatingAGroupToolStripMenuItem";
            this.creatingAGroupToolStripMenuItem.Size = new System.Drawing.Size(314, 26);
            this.creatingAGroupToolStripMenuItem.Text = "Create/delete a group";
            this.creatingAGroupToolStripMenuItem.Click += new System.EventHandler(this.creatingAGroupToolStripMenuItem_Click);
            // 
            // deletingAGroupToolStripMenuItem
            // 
            this.deletingAGroupToolStripMenuItem.Name = "deletingAGroupToolStripMenuItem";
            this.deletingAGroupToolStripMenuItem.Size = new System.Drawing.Size(314, 26);
            this.deletingAGroupToolStripMenuItem.Text = "Add/remove a file to a group";
            this.deletingAGroupToolStripMenuItem.Click += new System.EventHandler(this.deletingAGroupToolStripMenuItem_Click);
            // 
            // userAccessRightsToolStripMenuItem
            // 
            this.userAccessRightsToolStripMenuItem.Name = "userAccessRightsToolStripMenuItem";
            this.userAccessRightsToolStripMenuItem.Size = new System.Drawing.Size(234, 26);
            this.userAccessRightsToolStripMenuItem.Text = "User access rights";
            this.userAccessRightsToolStripMenuItem.Click += new System.EventHandler(this.userAccessRightsToolStripMenuItem_Click);
            // 
            // F1_toolStripButton_About
            // 
            this.F1_toolStripButton_About.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.F1_toolStripButton_About.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.F1_toolStripButton_About.Image = ((System.Drawing.Image)(resources.GetObject("F1_toolStripButton_About.Image")));
            this.F1_toolStripButton_About.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.F1_toolStripButton_About.Name = "F1_toolStripButton_About";
            this.F1_toolStripButton_About.Size = new System.Drawing.Size(52, 28);
            this.F1_toolStripButton_About.Text = "About";
            this.F1_toolStripButton_About.Click += new System.EventHandler(this.F1_toolStripButton_About_Click);
            // 
            // F1_toolStripSeparator2
            // 
            this.F1_toolStripSeparator2.Name = "F1_toolStripSeparator2";
            this.F1_toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // F1_toolStripButton_openUtilityService
            // 
            this.F1_toolStripButton_openUtilityService.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.F1_toolStripButton_openUtilityService.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.F1_toolStripButton_openUtilityService.Image = ((System.Drawing.Image)(resources.GetObject("F1_toolStripButton_openUtilityService.Image")));
            this.F1_toolStripButton_openUtilityService.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.F1_toolStripButton_openUtilityService.Name = "F1_toolStripButton_openUtilityService";
            this.F1_toolStripButton_openUtilityService.Size = new System.Drawing.Size(132, 28);
            this.F1_toolStripButton_openUtilityService.Text = "Utility service";
            this.F1_toolStripButton_openUtilityService.Click += new System.EventHandler(this.F1_toolStripButton_openUtilityService_Click);
            // 
            // F1_toolStripButton_ADMIN_userManagement
            // 
            this.F1_toolStripButton_ADMIN_userManagement.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.F1_toolStripButton_ADMIN_userManagement.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.F1_toolStripButton_ADMIN_userManagement.Image = ((System.Drawing.Image)(resources.GetObject("F1_toolStripButton_ADMIN_userManagement.Image")));
            this.F1_toolStripButton_ADMIN_userManagement.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.F1_toolStripButton_ADMIN_userManagement.Name = "F1_toolStripButton_ADMIN_userManagement";
            this.F1_toolStripButton_ADMIN_userManagement.Size = new System.Drawing.Size(132, 22);
            this.F1_toolStripButton_ADMIN_userManagement.Text = "User Management";
            this.F1_toolStripButton_ADMIN_userManagement.Click += new System.EventHandler(this.F1_toolStripButton_ADMIN_userManagement_Click);
            // 
            // F2_toolStripSeparator3
            // 
            this.F2_toolStripSeparator3.Name = "F2_toolStripSeparator3";
            this.F2_toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
            // 
            // F1_toolStripButton_LogOut
            // 
            this.F1_toolStripButton_LogOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.F1_toolStripButton_LogOut.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.F1_toolStripButton_LogOut.Image = ((System.Drawing.Image)(resources.GetObject("F1_toolStripButton_LogOut.Image")));
            this.F1_toolStripButton_LogOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.F1_toolStripButton_LogOut.Name = "F1_toolStripButton_LogOut";
            this.F1_toolStripButton_LogOut.Size = new System.Drawing.Size(68, 28);
            this.F1_toolStripButton_LogOut.Text = "Log out";
            this.F1_toolStripButton_LogOut.Click += new System.EventHandler(this.F1_toolStripButton_LogOut_Click);
            // 
            // F1_timer_updateTimer
            // 
            this.F1_timer_updateTimer.Tick += new System.EventHandler(this.F1_timer_updateTimer_Tick_1);
            // 
            // F1_notifyIcon_notification
            // 
            this.F1_notifyIcon_notification.Text = "MainForm1";
            this.F1_notifyIcon_notification.Visible = true;
            this.F1_notifyIcon_notification.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.F1_notifyIcon_notification_MouseDoubleClick);
            // 
            // F1_errorProvider_error
            // 
            this.F1_errorProvider_error.ContainerControl = this;
            // 
            // F1_listView_Notifications
            // 
            this.F1_listView_Notifications.HideSelection = false;
            this.F1_listView_Notifications.Location = new System.Drawing.Point(516, 109);
            this.F1_listView_Notifications.Name = "F1_listView_Notifications";
            this.F1_listView_Notifications.Size = new System.Drawing.Size(164, 328);
            this.F1_listView_Notifications.TabIndex = 9;
            this.F1_listView_Notifications.UseCompatibleStateImageBehavior = false;
            this.F1_listView_Notifications.View = System.Windows.Forms.View.List;
            this.F1_listView_Notifications.SelectedIndexChanged += new System.EventHandler(this.F1_listView_Notifications_SelectedIndexChanged);
            this.F1_listView_Notifications.MouseClick += new System.Windows.Forms.MouseEventHandler(this.F1_listView_Notifications_MouseClick);
            // 
            // F1_timer_shutdown
            // 
            this.F1_timer_shutdown.Tick += new System.EventHandler(this.F1_timer_shutdown_Tick);
            // 
            // F1_label_groupsTitle
            // 
            this.F1_label_groupsTitle.AutoSize = true;
            this.F1_label_groupsTitle.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.F1_label_groupsTitle.Location = new System.Drawing.Point(11, 119);
            this.F1_label_groupsTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.F1_label_groupsTitle.Name = "F1_label_groupsTitle";
            this.F1_label_groupsTitle.Size = new System.Drawing.Size(98, 23);
            this.F1_label_groupsTitle.TabIndex = 4;
            this.F1_label_groupsTitle.Text = "#GROUPS#";
            // 
            // F1_timer_shutdownNotification
            // 
            this.F1_timer_shutdownNotification.Tick += new System.EventHandler(this.F1_timer_shutdownNotification_Tick);
            // 
            // F1_listView_groups
            // 
            this.F1_listView_groups.HideSelection = false;
            this.F1_listView_groups.Location = new System.Drawing.Point(9, 154);
            this.F1_listView_groups.Name = "F1_listView_groups";
            this.F1_listView_groups.Size = new System.Drawing.Size(501, 283);
            this.F1_listView_groups.TabIndex = 10;
            this.F1_listView_groups.UseCompatibleStateImageBehavior = false;
            this.F1_listView_groups.SelectedIndexChanged += new System.EventHandler(this.F1_listView_groups_SelectedIndexChanged);
            // 
            // F1_button_addNewGroup
            // 
            this.F1_button_addNewGroup.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.F1_button_addNewGroup.Location = new System.Drawing.Point(368, 109);
            this.F1_button_addNewGroup.Name = "F1_button_addNewGroup";
            this.F1_button_addNewGroup.Size = new System.Drawing.Size(142, 39);
            this.F1_button_addNewGroup.TabIndex = 11;
            this.F1_button_addNewGroup.Text = "New group";
            this.F1_button_addNewGroup.UseVisualStyleBackColor = true;
            this.F1_button_addNewGroup.Click += new System.EventHandler(this.F1_button_addNewGroup_Click);
            // 
            // F1_MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 462);
            this.Controls.Add(this.F1_button_addNewGroup);
            this.Controls.Add(this.F1_listView_groups);
            this.Controls.Add(this.F1_label_groupsTitle);
            this.Controls.Add(this.F1_listView_Notifications);
            this.Controls.Add(this.F1_groupBox_accountDetails);
            this.Controls.Add(this.F1_toolStrip_toolStrip);
            this.Controls.Add(this.F1_panel_greeting);
            this.Controls.Add(this.F1_statusStrip_status);
            this.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "F1_MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main page";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.F1_MainForm_FormClosed);
            this.Load += new System.EventHandler(this.F1_MainForm_Load);
            this.MouseEnter += new System.EventHandler(this.F1_MainForm_MouseEnter);
            this.F1_panel_greeting.ResumeLayout(false);
            this.F1_panel_greeting.PerformLayout();
            this.F1_groupBox_accountDetails.ResumeLayout(false);
            this.F1_groupBox_accountDetails.PerformLayout();
            this.F1_statusStrip_status.ResumeLayout(false);
            this.F1_statusStrip_status.PerformLayout();
            this.F1_toolStrip_toolStrip.ResumeLayout(false);
            this.F1_toolStrip_toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.F1_errorProvider_error)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label F1_label_greeting;
        private System.Windows.Forms.Label F1_label_usernameDisplay;
        private System.Windows.Forms.Panel F1_panel_greeting;
        private System.Windows.Forms.GroupBox F1_groupBox_accountDetails;
        private System.Windows.Forms.Label F1_label_userRightsDisplay;
        private System.Windows.Forms.StatusStrip F1_statusStrip_status;
        private System.Windows.Forms.ToolStrip F1_toolStrip_toolStrip;
        private System.Windows.Forms.ToolStripStatusLabel F1_toolStripStatusLabel_connectionStatus;
        private System.Windows.Forms.Timer F1_timer_updateTimer;
        private System.Windows.Forms.ToolStripStatusLabel F1_toolStripStatusLabel_DataBaseName;
        private System.Windows.Forms.ToolStripStatusLabel F1_toolStripStatusLabel_separator1;
        private System.Windows.Forms.ToolStripStatusLabel F1_toolStripStatusLabel_separator2;
        private System.Windows.Forms.ToolStripStatusLabel F1_toolStripStatusLabel_NumberOfNotifications;
        private System.Windows.Forms.ToolStripStatusLabel F1_toolStripStatusLabel_ConnectionStatusText;
        private System.Windows.Forms.ToolStripStatusLabel F1_toolStripStatusLabel_NumberOfNotificationsText;
        private System.Windows.Forms.NotifyIcon F1_notifyIcon_notification;
        private System.Windows.Forms.ToolStripStatusLabel F1_toolStripStatusLabel_DateTime;
        private System.Windows.Forms.ToolStripStatusLabel F1_toolStripStatusLabel_separator3;
        private System.Windows.Forms.ErrorProvider F1_errorProvider_error;
        private System.Windows.Forms.HelpProvider F1_helpProvider_help;
        private System.Windows.Forms.ListView F1_listView_Notifications;
        private System.Windows.Forms.ToolStripDropDownButton F1_toolStripDropDownButton_Help;
        private System.Windows.Forms.ToolStripMenuItem userGuideToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guidesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem creatingAGroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deletingAGroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator F1_toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem userAccessRightsToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton F1_toolStripButton_About;
        private System.Windows.Forms.ToolStripSeparator F1_toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton F1_toolStripButton_LogOut;
        private System.Windows.Forms.Timer F1_timer_shutdown;
        private System.Windows.Forms.ToolStripSeparator F2_toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton F1_toolStripButton_ADMIN_userManagement;
        private System.Windows.Forms.Label F1_label_groupsTitle;
        private System.Windows.Forms.Timer F1_timer_shutdownNotification;
        private System.Windows.Forms.ToolStripStatusLabel F1_toolStripStatusLabel_separator4;
        private System.Windows.Forms.ToolStripStatusLabel F1_toolStripStatusLabel_notificationText;
        private System.Windows.Forms.ToolStripButton F1_toolStripButton_openUtilityService;
        private System.Windows.Forms.ListView F1_listView_groups;
        private System.Windows.Forms.Button F1_button_addNewGroup;
        private System.Windows.Forms.OpenFileDialog F1_openFileDialog_openInformationFile;
    }
}