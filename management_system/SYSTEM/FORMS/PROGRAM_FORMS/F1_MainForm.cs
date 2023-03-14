using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace management_system
{
    /* main form
        - displays notifications
        - displays groups the user has access to
        - enables access to all of the main functionalities after the log in form
     */
    public partial class F1_MainForm : Form
    {
        //VARIABLES
        private ImageList notification_icons = new ImageList();
        private ImageList groupIcons = new ImageList();

        //CONSTRUCTORS
        public F1_MainForm()
        {
            InitializeComponent();
        }

        //UTILITY FUNCTIONS
        //update notifications
        private void updateNotifications()
        {
            int unread_notifications = 0;
            
            this.F1_listView_Notifications.Items.Clear();

            foreach (Notification notification in Utility.notifications)
            {
                if (!this.F1_listView_Notifications.Items.ContainsKey(notification.getId().ToString()) && notification.getRead()==0)
                    this.F1_listView_Notifications.Items.Add(notification.getId().ToString(), notification.getSender(), notification.getImportance());

                if (notification.getRead() == 0) unread_notifications++;

            }

            //update unread notifications counter
            this.F1_toolStripStatusLabel_NumberOfNotifications.Text = unread_notifications.ToString();
        }

        //restart the shutdown timer
        private void restartShutdownTimer()
        {
            //stop the shutdown timer (if on)
            this.F1_timer_shutdown.Stop();

            //reset the shutdown notification timer
            this.F1_timer_shutdownNotification.Stop();
            this.F1_timer_shutdownNotification.Start();

            //hide notification text
            this.F1_toolStripStatusLabel_notificationText.Visible = false;
        }


        //get updates
        private void getUpdates()
        {
            //database details
            Dictionary <string,string> DB_details = Utility.getConnDetails();
            string DB_name = Utility.DB_name; //database name
            string conn_state = DB_details["STATE"]; //connection state

            //database name
            this.F1_toolStripStatusLabel_DataBaseName.Text = DB_name;

            //connection state
            if (conn_state.Equals(System.Data.ConnectionState.Open.ToString())) //connection open
            {
                this.F1_toolStripStatusLabel_connectionStatus.ForeColor = Color.FromName("Green");
                this.F1_toolStripStatusLabel_connectionStatus.Text = conn_state;

            }
            else //connection is not currently open
            {
                this.F1_toolStripStatusLabel_connectionStatus.ForeColor = Color.FromName("Red");
                this.F1_toolStripStatusLabel_connectionStatus.Text =  conn_state;

                //display notification
                if (this.F1_notifyIcon_notification.Visible == false)
                {
                    this.F1_notifyIcon_notification.Icon = SystemIcons.Error;
                    this.F1_notifyIcon_notification.Text = "Management system:" + Utility.displayError("DB_conn_failed") + ":" + Utility.DB_name;
                }
            }

            //notifications
            this.F1_toolStripStatusLabel_NumberOfNotifications.Text = Utility.notifications.Count.ToString();
            this.F1_listView_Notifications.MultiSelect = false;

            //configure the listview icons
            notification_icons.Images.Add(Bitmap.FromFile(Utility.IMG_notifications_icons[0]));
            notification_icons.Images.Add(Bitmap.FromFile(Utility.IMG_notifications_icons[1]));


            this.F1_listView_Notifications.SmallImageList=notification_icons;

            //load the notifications into the listview control
            foreach (Notification notification in Utility.notifications)
            {
                if (!this.F1_listView_Notifications.Items.ContainsKey(notification.getId().ToString()) && notification.getRead()==0)
                    this.F1_listView_Notifications.Items.Add(notification.getId().ToString(), notification.getSender(), notification.getImportance());
                
            }

            //update date
            this.F1_toolStripStatusLabel_DateTime.Text = DateTime.Now.ToString();

        }



        //EVENT HANDLERS
        //form closed
        private void F1_MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit(); //exit application
        }

        //form load
        private void F1_MainForm_Load(object sender, EventArgs e)
        {

            //priority settings (admin)
            if (Utility.admin == null)
            {
                this.F1_toolStripButton_ADMIN_userManagement.Enabled = false;
                this.F1_toolStripButton_ADMIN_userManagement.Visible = false;
            }
            

            //form settings
            this.AllowDrop = false;
            this.MaximizeBox = false;
            //this.MinimizeBox = false;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            this.F1_toolStripStatusLabel_notificationText.Visible = false;

            //load language pack
            Utility.setLanguage(this);

            //application settings
            Utility.setLanguage(this); //load the language for the current form
            //load messages
            this.F1_toolStripStatusLabel_NumberOfNotificationsText.Text = Utility.displayMessage("F1_Notification_count_text");
            this.F1_toolStripStatusLabel_ConnectionStatusText.Text = Utility.displayMessage("F1_Notification_DB_connection_status_text");

            //load account info
            this.F1_label_usernameDisplay.Text = Utility.username;
            if (Utility.admin != null)
            {
                this.F1_label_userRightsDisplay.BackColor = Color.LightYellow;
                this.F1_label_userRightsDisplay.Text = Utility.displayMessage("Account_access_Admin");
            }
            else
            {
                this.F1_label_userRightsDisplay.BackColor = Color.Gray;
                this.F1_label_userRightsDisplay.Text = Utility.displayMessage("Account_access_User");
            }

            this.F1_groupBox_accountDetails.Text = Utility.displayMessage("Account_details_title");

            //get updates
            this.getUpdates();

            //start update timer
            this.F1_timer_updateTimer.Interval = Utility.updateTimerInterval;
            this.F1_timer_updateTimer.Start();

            //set the shutdonw timer interval
            this.F1_timer_shutdown.Interval = Utility.shutdownTimerInterval;

            //start shutdown notification timer
            this.F1_timer_shutdownNotification.Interval = Utility.shutdownTimerInterval - Utility.updateTimerInterval/4;
            this.F1_timer_shutdownNotification.Start();

            //load groups
            this.loadGroups();

            //display groups
            this.displayGroups();

            //link an icon list to the groups listview
            this.F1_listView_groups.LargeImageList = groupIcons;


        }

        //main groupBox
        private void F1_groupBox_mainControls_Enter(object sender, EventArgs e)
        {
            
        }

        //toolStrip
        private void F1_toolStrip_toolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        //timer used to update values

        private void F1_timer_updateTimer_Tick_1(object sender, EventArgs e)
        {
            //update values
            this.getUpdates();
        }

        //double click on the notification shown in the windows taskbar
        private void F1_notifyIcon_notification_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //DEV
        }

        //notification list view
        private void F1_listView_Notifications_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        // right click on a notification -> delete notification
        private void F1_listView_Notifications_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Right)) //delete the read notification from memory
            {
                int id;
                bool parsed = Int32.TryParse(this.F1_listView_Notifications.SelectedItems[0].Name, out id);

                Utility.markNotificationAsRead(id);


                this.updateNotifications();
            }else if(e.Button.Equals(MouseButtons.Left)) //show notification details
            {
                
                foreach (ListViewItem notification in this.F1_listView_Notifications.SelectedItems)
                {
                    int id;

                    if (Int32.TryParse(notification.Name, out id) == true)
                    {
                        foreach (Notification notif in Utility.notifications)
                        {
                            if (notif.getId().Equals(id)) //search the ID of the selected notification to display its details
                            {
                                //mark the notification as read
                                notif.setRead(1);

                                //display the notification details in a new form
                                F2_MainFormNotificationWindow f2_MainFormNotificationWindow = new F2_MainFormNotificationWindow(notif);

                                f2_MainFormNotificationWindow.StartPosition = FormStartPosition.CenterParent; 


                                f2_MainFormNotificationWindow.ShowDialog();


                                f2_MainFormNotificationWindow.Dispose();

                                Utility.markNotificationAsRead(id);
                            }
                        }
                    }



                }
            }
        }

        //log out
        private void F1_toolStripButton_LogOut_Click(object sender, EventArgs e)
        {
            Utility.logOut(this);
        }


        //log the user out and close the application
        private void F1_timer_shutdown_Tick(object sender, EventArgs e)
        {
            Utility.logOut(this);

            Application.Exit();
        }

        //user management - ADMIN ONLY
        private void F1_toolStripButton_ADMIN_userManagement_Click(object sender, EventArgs e)
        {
            //open the user management form
            F3_UserManagement f3_userManagement = new F3_UserManagement();

            f3_userManagement.ShowDialog();



            f3_userManagement.Close();
        }

        //display a notification that the system will log out and start the shutdown timer
        private void F1_timer_shutdownNotification_Tick(object sender, EventArgs e)
        {
            //stop the notification timer
            this.F1_timer_shutdownNotification.Stop();

            //display notification
            this.F1_toolStripStatusLabel_notificationText.Visible = true;
            this.F1_toolStripStatusLabel_notificationText.BackColor = Color.Red;
            this.F1_toolStripStatusLabel_notificationText.Text = Utility.displayMessage("System_systemShutdownMessage") + ((Utility.shutdownTimerInterval / 4) /1000 / 60).ToString();


            //start the timer until the system logs out
            this.F1_timer_shutdown.Start();
            
        }

        //move the mouse directly over the main form - restart shutdown timer
        private void F1_MainForm_MouseEnter(object sender, EventArgs e)
        {
            this.restartShutdownTimer();
        }

        //open the Utility service main form
        private void F1_toolStripButton_openUtilityService_Click(object sender, EventArgs e)
        {
            //user rights check
            if (Utility.admin == null)
            {
                MessageBox.Show(Utility.displayError("Account_security_utility_service_insufficient_rights"), "SECURITY ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Application.Exit();
                Utility.ERR = true;
                Utility.WARNING = true;
            }
            else
            {
                Utility.openUtilityService = true; //open the utility service
            }
        }

        private void F1_flowLayoutPanel_groups_Paint(object sender, PaintEventArgs e)
        {

        }


        #region groups
        //load the groups this user has access to
        private void loadGroups()
        {
            Utility.loadGroups();

        }

        //display the groups the user has access to
        private void displayGroups()
        {
            //clear the listview control
            this.F1_listView_groups.Items.Clear();

            foreach (Group group in Utility.userGroups)
            {
                //load group icon
                groupIcons.Images.Add(group.getName(),group.getIcon());

                //add the group icon into the listview control
                this.F1_listView_groups.Items.Add(group.getName(),group.getName());
            }

        }
        

        //add a new group
        private void F1_button_addNewGroup_Click(object sender, EventArgs e)
        {

            F4_NewGroup f4_newGroup = new F4_NewGroup();

            //open new group form
            f4_newGroup.ShowDialog();

            //close form
            f4_newGroup.Close();

            //update the groups listview
            this.displayGroups();
        }



        #endregion
    }
}
