using management_system.SYSTEM.CLASSES.FILES.FILE_TYPES;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
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
        //add a new group to the list view control with the details specified in the F4_NewGroup dialog form
        public void addNewGroup(Image image, string name)
        {
            try
            {
                if (image == null || name == null) throw new Exception("");
                
                this.F1_listView_groups.LargeImageList.Images.Add(name, image);
                this.F1_listView_groups.Items.Add(name, this.F1_listView_groups.LargeImageList.Images.Count - 1);
            }catch(Exception exception)
            {
                Utility.DisplayError("Groups_error_loading_groups_in_listView_control",exception,"Group: Failed to load a group into the list view control of the main form",false);
            }
        }

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

            //groups list view settings
            this.F1_listView_groups.MultiSelect= false;

            //add a context menu to the groups list view control
            MenuItem[] menuItems = new MenuItem[1];
            
            menuItems[0] = new MenuItem(Utility.displayMessage("F1_delete_group_context_menu_message"), F1_DeleteGroup);

            ContextMenu groupsMenu = new ContextMenu(menuItems);

            this.F1_listView_groups.ContextMenu = groupsMenu;
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

            //set the shutdown timer interval
            this.F1_timer_shutdown.Interval = Utility.shutdownTimerInterval;

            //start shutdown notification timer
            this.F1_timer_shutdownNotification.Interval = Utility.shutdownTimerInterval - Utility.updateTimerInterval/4;
            this.F1_timer_shutdownNotification.Start();

            //load groups from the database into memory
            Utility.loadGroupsIntoMemory();

            //load groups from memory into the list view control
            //this.loadGroups();

            //link an icon list to the groups listview
            this.F1_listView_groups.LargeImageList = this.groupIcons;

            //display groups
            this.displayGroups();




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

                                //Utility.markNotificationAsRead(id);
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
            this.groupIcons.Images.Clear();
            this.groupIcons.Images.Add(Utility.imgName_defaultGroupIcon, Image.FromFile(Utility.IMG_defaultIconFilePath)); //add the default group icon to the groupIcons list

            //add the groups to the list view control
            foreach (Group group in Utility.userGroups)
            {
                //load group icon
                try
                {
                    this.groupIcons.Images.Add(group.getName(), group.getIcon());

                    //add the group icon into the listview control (the group name is also the image key)
                    this.F1_listView_groups.Items.Add(group.getName(), group.getName());
                }catch(Exception exception)
                {
                    //Utility.DisplayError("Groups_invalid_image",exception,"Group: Invalid image given to for the group creation: "+exception.ToString(),false);
                    
                    Utility.logDiagnosticEntry("Group: Invalid image given to for the group creation: "+exception.ToString());
                    this.F1_listView_groups.Items.Add(group.getName(),Utility.imgName_defaultGroupIcon); //set the default group image as the groups' icon
                }
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

        //about form
        private void F1_toolStripButton_About_Click(object sender, EventArgs e)
        {
            try
            {
                //load the 'About' information into a General Information form and display it
                F6_GeneralInformation f6_GeneralInformation = new F6_GeneralInformation(Utility.displayMessage("F1_aboutPageTitle"), Utility.displayMessage("F1_aboutPageInfo"));

                f6_GeneralInformation.ShowDialog();

                f6_GeneralInformation.Dispose();
                
                    
            }catch(Exception exception)
            {
                Utility.DisplayError("Load_f1_mainForm_about_page_failed", exception, "F1_MainForm: Could not load the F1_MainForm about page", false);
            }
        }

        //display the User manual
        private void userGuideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {   
                F6_GeneralInformation f6_GeneralInformation = new F6_GeneralInformation(Utility.displayMessage("F1_user_manual_form_title"), Utility.displayMessage("F1_user_manual_form_info"));

                f6_GeneralInformation.ShowDialog(); //display the user manual

                f6_GeneralInformation.Dispose();
                
            }
            catch (Exception exception)
            {
                Utility.DisplayError("Load_f1_mainForm_user_manual_page_failed", exception, "MainForm: Failed to load the user manual: " + exception.ToString(), false); ;
            }
        }

        private void guidesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        //display the 'Create group' guide
        private void creatingAGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                F6_GeneralInformation f6_GeneralInformation = new F6_GeneralInformation(Utility.displayMessage("F1_create_group_guide_form_title"), Utility.displayMessage("F1_create_group_guide_form_info"));

                f6_GeneralInformation.ShowDialog(); //display the user manual

                f6_GeneralInformation.Dispose();

            }
            catch (Exception exception)
            {
                Utility.DisplayError("Load_f1_mainForm_create_group_guide_page_failed", exception, "MainForm: Failed to load the 'Create group' guide: " + exception.ToString(), false); ;
            }
        }

        //display the 'Add/Delete files from a group' guide
        private void deletingAGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                F6_GeneralInformation f6_GeneralInformation = new F6_GeneralInformation(Utility.displayMessage("F1_add_delete_files_guide_form_title"), Utility.displayMessage("F1_add_delete_files_guide_form_info"));

                f6_GeneralInformation.ShowDialog(); //display the user manual

                f6_GeneralInformation.Dispose();

            }
            catch (Exception exception)
            {
                Utility.DisplayError("Load_f1_mainForm_add_delete_files_guide_page_failed", exception, "MainForm: Failed to load the 'Add/Delete files from a group' guide: " + exception.ToString(), false); ;
            }
        }

        //display information about the user rights and account system of Management System
        private void userAccessRightsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                F6_GeneralInformation f6_GeneralInformation = new F6_GeneralInformation(Utility.displayMessage("F1_user_rights_information_form_title"), Utility.displayMessage("F1_user_rights_information_form_info"));

                f6_GeneralInformation.ShowDialog(); //display the user manual

                f6_GeneralInformation.Dispose();

            }
            catch (Exception exception)
            {
                Utility.DisplayError("Load_f1_mainForm_user_rights_information_page_failed", exception, "MainForm: Failed to load the 'Add/Delete files from a group' guide: " + exception.ToString(), false); ;
            }
        }

        //a group was selected (double click on an item in the list view control)
        private void F1_listView_groups_ItemActivate(object sender, EventArgs e)
        {
            try
            {
                F5_FileEditorForm f5_fileEditorForm = new F5_FileEditorForm(
                    Utility.userGroups.Find(x => x.getName().Equals(this.F1_listView_groups.SelectedItems[0].Text)) //search for the currently selected group name int the list of user groups and pass the group to the constructor
                    );

                this.Hide(); //hide the main form

                f5_fileEditorForm.ShowDialog(); //display the file editor form

                this.Show(); //show the main form again
            }
            catch (Exception exception)
            {
                Utility.DisplayError("Groups_failed_to_open_group", exception, "Groups: Failed to open the group: " + this.F1_listView_groups.SelectedItems.ToString() + ": " + exception.ToString(), true);
            
            }
        }

        //the list view control is clicked
        private void F1_listView_groups_Click(object sender, EventArgs e)
        {

        }

        //delete group event handler
        private void F1_DeleteGroup(object sender, EventArgs e)
        {
            try
            {
                //confirm the deletion
                DialogResult deleteGroup = MessageBox.Show(Utility.displayMessage("F1_confirm_group_deletion"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (deleteGroup == DialogResult.No) //do not delete the group
                    return;

                //delete group
                if (Utility.deleteGroup(Utility.getGroupByName(this.F1_listView_groups.SelectedItems[0].Text)) == true) //the group was deleted
                {
                    //delete the list view item associated with the deleted group
                    this.F1_listView_groups.SelectedItems[0].Remove();

                    //display a message to indicate that the group has been deleted
                    MessageBox.Show(Utility.displayMessage("F1_group_successfully_deleted"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception exception)
            {
                Utility.DisplayError("Groups_failed_to_delete_group_from_mainForm", exception, "Group: Failed to delete the selected group from the MainForm: " + exception.ToString(), false);
            }

            }
    }
}
