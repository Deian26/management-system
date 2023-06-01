using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace management_system
{
    //form used to access the main utility functions (ADMIN only)
    public partial class U0_UtilityMainForm : Form
    {
        //VARIABLES
        private bool ACTION = false; //flag to mark whether an action is in progress or not
        private string username = "#ERR#"; 
        private string userAccess = "#ERR#";

        //backup english error messages for the diagnostic log
        private Dictionary<string, string> errors_EN = new Dictionary<string, string>();

        //CONSTRUCTORS
        public U0_UtilityMainForm()
        {
            InitializeComponent();
            this.initializeBackupErrorMessages();
        }
        
        //UTILITY FUNCTIONS
        //load the backup english error messages
        private void initializeBackupErrorMessages()
        {
            this.Enabled = false;

        }

        //get the error message specified via the given key
        private string displayError(string key)
        {
            try
            {
                return this.errors_EN[key];
            }catch (Exception exception) 
            {
                MessageBox.Show("EN: ERROR LOADING MESSAGE FROM THE BACKUP ERROR DICTIONARY (UTILITY SERVICE): " + exception.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utility.ERR = true;
                Application.Exit();
                return "#ERR#";
            }
        }
        //set credentials in the utility main form
        public bool setCredentials(string username, string userAccess)
        {
            if (username.Equals("") || username == null ||
                userAccess.Equals("") || userAccess == null)
            {
                MessageBox.Show(Utility.displayError("Code_wrong_function_call")+"U0_UtilityMainForm.setCredentials()","WARNING",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Utility.WARNING= true;
                Start.u0_utilityMainForm.U0_timer_clearErr.Stop();
                Start.u0_utilityMainForm.U0_timer_clearErr.Start();
                
                return false;
            }

            return true;
        }

        //add an entry into the diagnostic log
        private void addDiagnosticEntry(string key)
        {
            try
            {
                //log error
               this.U0_richTextBox_diagnosticLog.Text += DateTime.Now.ToString() + "#" + key+"#\n";
            }catch(Exception exception)
            {
                MessageBox.Show("EN: ERROR ADDING DIAGNOSTIC ENTRY: " + exception.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utility.ERR = true;
                Application.Exit();
            }
        }

        //EVENT HANDLERS

        //start eventLog
        private void eventLog1_EntryWritten(object sender, System.Diagnostics.EntryWrittenEventArgs e)
        {

        }

        //form load
        private void U0_UtilityMainForm_Load(object sender, EventArgs e)
        {

            //form settings
            //this.WindowState = FormWindowState.Minimized;
            this.Enabled = false;
            this.Hide();
            

            //this.MinimumSize = Utility.utilityServiceMainFormSize;
            //this.MaximumSize = Utility.utilityServiceMainFormSize;

            //progress timer
            this.U0_toolStripProgressBar_actionStatus.Visible = false;
            this.U0_toolStripProgressBar_actionStatus.Value = 0;

            //start the diagnostic timer
            this.U0_timer_logTimer.Interval = 100; //ms
            this.U0_timer_logTimer.Start();

            //initiate the notification timer and label
            this.U0_toolStripStatusLabel_notification.Text = "No notifications";
            this.U0_timer_notification.Interval = 5000; //ms
            this.U0_timer_notification.Start();

            //initiate the clear error timer
            this.U0_timer_clearErr.Interval = Utility.clearErrTimeInterval;
            

            //update DataBase notifications timer
            this.U0_timer_updateDBNotif.Interval = Utility.updateDBNotificationsInterval;
            this.U0_timer_notification.Start();

            //diagnostic log
            this.U0_richTextBox_diagnosticLog.Text = "#DIAGNOSTIC START LOG#\n"+DateTime.Now.ToString()+"\n\n";

            //account details
            this.U0_label_username.Text = Utility.username;
        }

        //form closed
        private void U0_UtilityMainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //stop the utility thread
            //Start.utility_thread.Interrupt();
        }

        //log timer
        private void U0_timer_logTimer_Tick(object sender, EventArgs e)
        {
            //this.U0_timer_diagnosticTimer.Start(); //re-start the diagnostic timer
            
            //enable form
            if(Utility.openUtilityService==true)
            {
                if (this.Enabled == false)
                {
                    //account details
                    this.U0_label_username.Text = Utility.username;

                    this.Size = Utility.utilityServiceMainFormSize;
                    this.MinimumSize = Utility.utilityServiceMainFormSize;
                    this.MaximumSize = Utility.utilityServiceMainFormSize;
                    this.WindowState = FormWindowState.Maximized;

                    this.Enabled = true;
                    this.Show();
                }
            }
            else
            {
                this.Enabled = false;
                this.Hide();
                //this.WindowState = FormWindowState.Normal;
            }
            
            //get and display diagnostic information
            //enter errors and warnings into the diagnostic log
            List<string> err_log = new List<string>();
            err_log = Utility.getErrorLog();

            foreach(string error in err_log)
            {
                this.addDiagnosticEntry(error);
            }

            //erase the Utility error log
            Utility.clearErrorLog();

            //error flag
            if (Utility.ERR == true) //error present
            {
                this.U0_toolStripStatusLabel_errorStatus.ForeColor = Color.FromName("RED");
                this.U0_toolStripStatusLabel_errorStatus.Text = "ERROR";
                //DEV
            }
            else //no error present
            {
                this.U0_toolStripStatusLabel_errorStatus.ForeColor = Color.FromName("GREEN");
                this.U0_toolStripStatusLabel_errorStatus.Text = "No errors";
            }

            //warning flag
            if (Utility.WARNING == true || Utility.ERR_MESSAGES == true) //warning present
            {
                this.U0_toolStripStatusLabel_warningStatus.ForeColor = Color.FromName("ORANGE");
                this.U0_toolStripStatusLabel_warningStatus.Text = "WARNING";
                //DEV
            }
            else //no warning present
            {
                this.U0_toolStripStatusLabel_warningStatus.ForeColor = Color.FromName("GREEN");
                this.U0_toolStripStatusLabel_warningStatus.Text = "No warnings";
            }

            //check database connection status
            Dictionary<string,string> connection_details = Utility.getConnDetails();

            if (Utility.ERR == false)
            {
                //get details about the connection and the database
                try
                {
                    //change text color depending on the connection status
                    if (connection_details == null || connection_details["STATE"].Equals("NULL")) //Utility.conn = null
                    {
                        this.U0_label_DataBaseConnectionStatus.ForeColor = Color.DarkGray;
                        this.U0_label_DataBaseConnectionStatus.Text = "Never connected";

                        //set action flag to 'no action ongoing'
                        this.ACTION = false;

                    }
                    else if (connection_details["STATE"].Equals(System.Data.ConnectionState.Closed.ToString())) //connection clossed
                    {
                        this.U0_label_DataBaseConnectionStatus.ForeColor = Color.Red;
                        this.U0_label_DataBaseConnectionStatus.Text = "Disconnected";

                        //set action flag to 'no action ongoing'
                        this.ACTION = false;

                    }
                    else if (connection_details["STATE"].Equals(System.Data.ConnectionState.Broken.ToString())) //connection broken
                    {
                        this.U0_label_DataBaseConnectionStatus.ForeColor = Color.DarkRed;
                        this.U0_label_DataBaseConnectionStatus.Text = "Connection broken";

                        //set action flag to 'no action ongoing'
                        this.ACTION = false;
                    }
                    else if (connection_details["STATE"].Equals(System.Data.ConnectionState.Connecting.ToString())) //connecting
                    {
                        this.U0_label_DataBaseConnectionStatus.ForeColor = Color.Yellow;
                        this.U0_label_DataBaseConnectionStatus.Text = "Connecting...";

                        //set action flag to 'ongoing'
                        this.ACTION = true;
                    }else if(connection_details["STATE"].Equals(System.Data.ConnectionState.Open.ToString())) //open
                    {
                        this.U0_label_DataBaseConnectionStatus.ForeColor = Color.Green;
                        this.U0_label_DataBaseConnectionStatus.Text = "Connected";

                        //set action flag to 'no action ongoing'
                        this.ACTION = false;
                    }
                    else //error
                    {
                        this.U0_label_DataBaseConnectionStatus.ForeColor = Color.Crimson;
                        this.U0_label_DataBaseConnectionStatus.Text = "Error";

                        //set action flag to 'no action ongoing'
                        this.ACTION = false;
                    }


                    if(connection_details!=null) 
                        this.U0_textBox_ConnectedDataBase.Text = connection_details["STATE"].ToString();


                    //action ongoing status update
                    if(this.ACTION==false)
                    {
                        //change mouse cursor
                        Start.u0_utilityMainForm.Cursor = Cursors.Default;

                        //make the progress bar visible and increment its value
                        this.U0_toolStripProgressBar_actionStatus.Visible = false;
                        this.U0_toolStripProgressBar_actionStatus.Value = 0;
                    }
                    else
                    {
                        //change mouse cursor
                        Start.u0_utilityMainForm.Cursor = Cursors.WaitCursor;

                        //make the progress bar visible and increment its value
                        this.U0_toolStripProgressBar_actionStatus.Visible = true;
                        this.U0_toolStripProgressBar_actionStatus.Value++;
                    }

                }
                catch (Exception exception)
                {
                    MessageBox.Show(Utility.displayError("DB_conn_failed") + exception.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); //display an error message
                    Utility.ERR = true;
                    Utility.WARNING = true;
                    Start.u0_utilityMainForm.U0_timer_clearErr.Stop();
                    Start.u0_utilityMainForm.U0_timer_clearErr.Start();
                }
            }

            //clear the diagnostic log rich text box if the length of the text is greater than a specified number of characters
            if (this.U0_richTextBox_diagnosticLog.Text.Length > Utility.maxDiagnosticLogCharacters)
            {
                this.U0_richTextBox_diagnosticLog.Clear();
                //change the status bar notification label
                this.U0_toolStripStatusLabel_notification.Text = "Diagnostic log cleared";
                this.U0_toolStripStatusLabel_notification.ForeColor = Color.Blue;
                //start a timer to clear the notification label
                this.U0_timer_notification.Start();
                //update the diagnostic log rich text box
                this.U0_richTextBox_diagnosticLog.Text = "#DIAGNOSTIC START LOG#\n" + DateTime.Now.ToString() + "\n\n";
            }
            //action in progress
            if (this.ACTION == true)
            {
                this.U0_toolStripProgressBar_actionStatus.Visible = true; //make the progress bar visible
            }
            else
            {
                this.U0_toolStripProgressBar_actionStatus.Value = 0;
                this.U0_toolStripProgressBar_actionStatus.Visible = false; //make the progress bar invisible                
            }
            //DEV
        }

        //reset the connection with the data base
        private void U0_button_resetDataBaseConnection_Click_1(object sender, EventArgs e)
        {
            //close and re-open the data base connection
            Utility.DB_disconnect();

            if(!Utility.getLastDataBaseConnString().Equals("")) Utility.DB_connect(Utility.getLastDataBaseConnString());
            else //no previous connection to any database (since the application start)
            {
                //add information to the diagnostic log
                this.U0_richTextBox_diagnosticLog.Text +=  DateTime.Now+"# No previous connection was made to any database; The database name was null #" +"\n\n";
            }
        }

        //check the XML setting files (including preferences, theme and language packs)
        private void U0_button_diagnosticCheck_XML_Setting_Files_Click(object sender, EventArgs e)
        {
            try
            {
                bool error = false;
                foreach (string path in Utility.pathXmlSettingFiles)
                    if (Utility.checkXML(path) == false) error = true;

                if (error == true) //display message in the utility service main form
                {
                    this.U0_label_XML_setting_files_status.Text = Utility.displayMessage("XML_setting_file_invalid_signature");
                    this.U0_label_XML_setting_files_status.ForeColor = Color.Red;
                }
                else
                {
                    this.U0_label_XML_setting_files_status.Text = Utility.displayMessage("XML_valid_setting_files");
                    this.U0_label_XML_setting_files_status.ForeColor = Color.GreenYellow;
                }
            }catch (Exception exception)
            {
                Utility.DisplayError("UtilityService_failed_to_check_XML_files", exception, "UtilityService: Error checking XML files" + exception.ToString(), true);
            }
        }

        //mouse click on the diagnostic log control
        private void U0_richTextBox_diagnosticLog_MouseDown(object sender, MouseEventArgs e)
        {
            if (Control.MouseButtons == MouseButtons.Right) //right mouse button is currently pressed
            {
                this.U0_contextMenuStrip_diagnosticLog.Show(Control.MousePosition); //display the context menu at coordinates of the mouse cursor
            }
        }

        //click on the 'Save log to file' button from the diagnostic context menu
        private void U0_saveLogToFileToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                this.U0_saveFileDialog_diagnosticLog.FileName = "Diagnostic_log_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString(); //save the text from the diagnostic log to a specified file
                this.U0_saveFileDialog_diagnosticLog.DefaultExt = ".log"; //default file fileExtension
                this.U0_saveFileDialog_diagnosticLog.AddExtension = true; //automatically add fileExtension if omitted
                this.U0_saveFileDialog_diagnosticLog.Filter = "Log text files (*.log) | *.log";

                this.U0_saveFileDialog_diagnosticLog.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(Utility.displayError("Code_error_opening_diagnostic_log_saveFile_dialog") + exception.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Utility.ERR = true;
                Utility.WARNING = true;
                Start.u0_utilityMainForm.U0_timer_clearErr.Stop();
                Start.u0_utilityMainForm.U0_timer_clearErr.Start();
            }
        }

        //click on the 'Save' button
        private void U0_saveFileDialog_diagnosticLog_FileOk(object sender, CancelEventArgs e)
        {
            //save file
            string diagnosticLog = null;
            try
            {
                diagnosticLog = this.U0_richTextBox_diagnosticLog.Text; //save the diagnostic log
                if (this.U0_saveFileDialog_diagnosticLog.CheckPathExists == true) //if the specified filePath exists
                {
                    Stream logFile = this.U0_saveFileDialog_diagnosticLog.OpenFile(); //open the file

                    //save the diagnostic log to the file
                    logFile.Write(Utility.toByteArray(diagnosticLog),0, diagnosticLog.Length);

                    logFile.Close(); //close the file

                }

                //change the status bar notification label
                this.U0_toolStripStatusLabel_notification.Text = "Diagnostic log saved to file";
                this.U0_toolStripStatusLabel_notification.ForeColor = Color.Blue;
                this.U0_timer_notification.Stop();
                this.U0_timer_notification.Start();

            }catch (Exception exception)
            {
                MessageBox.Show(Utility.displayError("Code_error_saving_diagnostic_log") + exception.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Utility.ERR = true;
                Utility.WARNING = true;
                Start.u0_utilityMainForm.U0_timer_clearErr.Stop();
                Start.u0_utilityMainForm.U0_timer_clearErr.Start();
            }
        }

        //clear the diagnostic log rich textbox
        private void U0_clearLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.U0_richTextBox_diagnosticLog.Clear();

            //change the status bar notification label
            this.U0_toolStripStatusLabel_notification.Text = "Diagnostic log cleared";
            this.U0_toolStripStatusLabel_notification.ForeColor = Color.Blue;
            this.U0_timer_notification.Stop();
            this.U0_timer_notification.Start();
        }

        //notification timer
        private void U0_timer_notification_Tick(object sender, EventArgs e)
        {
            //status bar notification label
            this.U0_toolStripStatusLabel_notification.ForeColor = Color.Black;
            this.U0_toolStripStatusLabel_notification.Text = "No notifications"; 
        }

        //open the customization form
        private void U0_button_openCustomizationForm_Click(object sender, EventArgs e)
        {

        }
        
        //clear errors and warnings
        private void U0_timer_clearErr_Tick(object sender, EventArgs e)
        {
            Utility.ERR = false;
            Utility.WARNING = false;
        }

        //toolstrip
        private void U0_toolStrip_utilityService_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        
        //update the notifications table from the database
        private void U0_timer_updateDBNotif_Tick(object sender, EventArgs e)
        {
            Utility.deleteOldDbNotifications(Utility.oldNotificationsLifespanDays); //delete notifications that have been sent more than the specified number of days ago
        }

        private void U0_UtilityMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        //hide the form
        private void U0_toolStripButton_EXIT_Click(object sender, EventArgs e)
        {
            Utility.openUtilityService = false;
            this.Hide();
            this.Enabled = false;
        }

        private void U0_label_username_Click(object sender, EventArgs e)
        {

        }

        private void U0_richTextBox_diagnosticLog_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
