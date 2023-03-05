using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace management_system
{
    public partial class F0_Login : Form
    {
        //VARIABLES

        //CONSTRUCTORS
        public F0_Login()
        {
            InitializeComponent();
        }

        //UTILITY FUNCTIONS

        //clear credential textboxes
        public void ClearCredentials()
        {
            this.F0_textBox_password.Clear();
            this.F0_textBox_username.Clear();
        }

        //EVENTS

        //form load
        private void F0_Login_Load(object sender, EventArgs e)
        {
            //form settings
            this.AllowDrop= false;
            this.MaximizeBox= false;
            //this.MinimizeBox = false;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            //set preference buttons to the correct values
            string[] preferences = Utility.setLoginButtonsText(); //set the correct language and theme on the corresponding buttons in the login form
            this.F0_button_language.Text = preferences[0]; //language
            this.F0_button_theme.Text = preferences[1]; //theme

            //set the help provider and tooltip controls
            //username textbox
            //help provider
            this.F0_helpProvider_help.SetHelpString(this.F0_textBox_username, Utility.displayMessage("Help_register_username")); 
            this.F0_helpProvider_help.SetShowHelp(this.F0_textBox_username,true);

            //password textbox
            //help provider
            this.F0_helpProvider_help.SetHelpString(this.F0_textBox_password, Utility.displayMessage("Help_register_password"));
            this.F0_helpProvider_help.SetShowHelp(this.F0_textBox_password, true);

            //set the timer for the error flags to be cleared
            this.F0_timer_errorClear.Interval = Utility.clearErrTimeInterval; //ms
            this.F0_timer_warningClear.Interval = Utility.clearWarningTimeInterval; //ms
            

            //load database options into the DataBase comboBox
            this.F0_comboBox_dataBase.Items.Clear();
            foreach(string database in Utility.databases_list)
            {
                this.F0_comboBox_dataBase.Items.Add(database);
            }

            if (this.F0_comboBox_dataBase.Items.Count > 0)
                this.F0_comboBox_dataBase.SelectedIndex = 0;

            //set the preferences for the current form (based on the default preferences saved in the XML_preferences)
            Utility.setLanguage(this);
            this.F0_button_theme.Text = Utility.setTheme(this);

            //start the update timer
            this.F0_timer_updateTimer.Interval = Utility.intervalUpdateTimer;
            this.F0_timer_updateTimer.Start();

            //DEBUG - TO BE DELETED AFTER DEVELOPMENT
            timer_debug.Interval = 10;
            timer_debug.Start();


        }

        //form closed
        private void F0_Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        //login
        private void F0_button_login_Click(object sender, EventArgs e)
        {
            bool valid = true;

            //login mechanism

            //check the connection details
            if (Utility.validUsername(F0_textBox_username.Text) == false)
            {
                //display an error
                this.F0_textBox_username.BackColor= Color.Salmon;
                F0_errorProvider.SetError(this.F0_textBox_username, Utility.displayError("Invalid_username"));
                valid = false;
            }

            if(this.F0_textBox_password.Text.Equals(null) || this.F0_textBox_password.Text.Equals(""))
            {
                //display an error
                this.F0_textBox_password.BackColor = Color.Salmon;
                F0_errorProvider.SetError(this.F0_textBox_password, Utility.displayError("Invalid_password"));
                valid = false;
            }

            if(this.F0_comboBox_dataBase.SelectedItem.Equals(""))
            {
                //display an error
                this.F0_comboBox_dataBase.BackColor= Color.Salmon;
                F0_errorProvider.SetError(this.F0_comboBox_dataBase, Utility.displayError("DB_invalid_database"));
                valid = false;
            }

            //connect to the system
            if (valid == true)
            {
                if (!this.F0_comboBox_dataBase.SelectedItem.ToString().Equals(""))
                {
                    
                    if(Utility.ERR==false) Utility.DB_disconnect(); //close the current DataBase connection
                    if (Utility.ERR == false)  Utility.DB_connect(this.F0_comboBox_dataBase.SelectedItem.ToString()); //connect to the database using the selected database from the dropbox menu
                }

                if (Utility.ERR == false)
                {
                    //try to find the given credentials in the data base
                    string adminString = Utility.logIn(this.F0_textBox_username.Text, this.F0_textBox_password.Text);
                    if (adminString == null)
                    {
                    MessageBox.Show(Utility.displayError("Invalid_credentials"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //Application.Exit();
                    Utility.ERR = true;
                    Start.f0_logIn.F0_timer_errorClear.Stop();
                    Start.f0_logIn.F0_timer_errorClear.Start();
                    }
                    

                    if (Utility.ERR == false)
                    { 
                        Utility.setCredentials(this.F0_textBox_username.Text, Utility.getDataKey(this.F0_textBox_username.Text) , adminString, this.F0_textBox_password.Text); //username, encryption key for the credentials, admin status
                        //Utility.setCredentials(this.F0_textBox_username.Text, this.F0_textBox_password.Text.Length, admin);
    
                        //start the main application

                        //load notifications from the database and update the local XML notification file
                        Utility.updateXMLNotificationsFile(Utility.dirPathDATA+"\\"+Utility.username+"\\"+Utility.username+"_notifications.xml");

                        this.ClearCredentials();

                        //open the MainForm
                        Start.f1_mainForm = new F1_MainForm();
                        Start.f1_mainForm.Show(); //show form
                        this.F0_timer_updateTimer.Stop(); //stop the update timer
                        this.Hide();
                    }
                }
            }
        }

        //change the system language
        private void F0_button_language_Click(object sender, EventArgs e)
        {
            Utility.nextLanguage(this); //update the language
            this.F0_toolTip_help.Active = !Utility.ERR_MESSAGES;

        }

        //update username texbox
        private void F0_textBox_username_TextChanged(object sender, EventArgs e)
        {
            this.F0_textBox_username.BackColor= Color.WhiteSmoke;
        }

        //update password texbox
        private void F0_textBox_password_TextChanged(object sender, EventArgs e)
        {
            this.F0_textBox_password.BackColor= Color.WhiteSmoke;
        }

        //update database combobox
        private void F0_comboBox_dataBase_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.F0_comboBox_dataBase.BackColor= Color.WhiteSmoke;
        }

        //clear the error flags
        private void F0_timer_errorClear_Tick(object sender, EventArgs e)
        {
            this.F0_timer_errorClear.Stop(); //stop this timer

            Utility.ERR = false; //clear the error flag after a time (if the error reported is sever enough, the application will receive a shut down trigger when the flag is set)
            //start the clearWarning timer
            this.F0_timer_warningClear.Stop();
            this.F0_timer_warningClear.Start(); 
        }

        //switch to the next theme
        private void F0_button_theme_Click(object sender, EventArgs e)
        {
            Utility.nextTheme(this);

            
        }
        //register a new user (access = user)
        private void F0_button_register_Click(object sender, EventArgs e)
        {
            string hashed_username, hashed_password;
            int data_key;
            SqlDataReader number_of_usernames = null, number_of_keys=null;
            Random rnd = new Random();
            SqlCommand cmd = null, data_key_cmd = null, username_cmd=null;

            //check the username and password
            if (Utility.validUsername(this.F0_textBox_username.Text) == false) //invalid username
            {
                this.F0_errorProvider.SetError(this.F0_textBox_username, Utility.displayError("Invalid_username"));
            }
            else if (Utility.validPassword(this.F0_textBox_password.Text) == false) //invalid password
            {
                this.F0_errorProvider.SetError(this.F0_textBox_password, Utility.displayError("Invalid_password"));
            }
            else //correct username and password
            {
                //connect to the currently selected database
                Utility.DB_connect(this.F0_comboBox_dataBase.SelectedItem.ToString());

                //hash the credentials (username and password)
                hashed_username = Utility.DB_HASH(this.F0_textBox_username.Text);
                //check if the username is unique else exit
                username_cmd = Utility.getSqlCommand("SELECT * FROM Users WHERE username='" +hashed_username.ToString()+"'");
                
                number_of_usernames = username_cmd.ExecuteReader();
                if (number_of_usernames.HasRows) //duplicate usernames
                {
                    this.F0_errorProvider.SetError(this.F0_textBox_username,Utility.displayError("Invalid_username_duplicate"));
                    Utility.ERR = true;
                    Utility.WARNING = true;
                    Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                    Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared
                    return;
                }

                //count users
                int nr_usernames = 0;
                while(number_of_usernames.Read())
                {
                    nr_usernames++;
                }

                number_of_usernames.Close(); //close data reader
                hashed_password = Utility.DB_HASH(this.F0_textBox_password.Text);

                do {
                    data_key = rnd.Next()%100; //pseudo-random encryption key for files and data other than credentials, unique to each user
                    data_key_cmd = Utility.getSqlCommand("SELECT * FROM Users WHERE data_key=" + data_key.ToString());
                    number_of_keys = data_key_cmd.ExecuteReader();
                } while(number_of_keys.HasRows && data_key<5); //generate a new data key if the current key already exists
                number_of_keys.Close(); //close data reader


                //write the credentials and data encryption key into the database
                string adminString = (rnd.Next() % 100).ToString();

                if(nr_usernames==0) //nu users added yet
                {
                    adminString = Utility.ENC_GEN(this.F0_textBox_password.Text,data_key + this.F0_textBox_username.Text.Length); //give the first registered user admin rights
                }

                cmd = Utility.getSqlCommand("INSERT INTO Users(username,password,data_key,admin) VALUES('" + hashed_username + "','" + hashed_password + "'," + data_key.ToString() + ",'"+Utility.DB_HASH(adminString).ToString() +"')");
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                //create a Notifications table in the current database, if one does not already exists
                SqlCommand cmd_init = Utility.getSqlCommand("CREATE  TABLE Notifications(_id INT, _text NVARCHAR(150), _sender NVARCHAR(21), _date NVARCHAR(15), _importance INT, _read INT)");
                Dictionary<string, string> aux_newTableFields = new Dictionary<string, string>
                {
                    { "_id", "INT" },
                    { "_text", "NVARCHAR(150)" },
                    { "_sender", "NVARCHAR(21)" },
                    { "_date", "NVARCHAR(15)" },
                    { "_importance", "INT" },
                    { "_read", "INT" },
                };
                Utility.setCreateTable("Notifications", aux_newTableFields);
                
                //create a local user directory for locally stored data
                bool err_flag = true;
                err_flag = !Utility.setDirectory(Utility.dirPathDATA + "\\" + this.F0_textBox_username.Text);
                if (err_flag == true)
                {
                    Utility.ERR = true;
                }
                //create a local XML notifications file
                err_flag = Utility.ERR = !Utility.createNotificationFile(Utility.dirPathDATA + "\\"+this.F0_textBox_username.Text+"\\" + this.F0_textBox_username.Text + "_notifications.xml", "Notifications", this.F0_textBox_username.Text);
                if (err_flag == true)
                {
                    Utility.ERR = true;
                }
                
                //display a confirmation message that the user has been registered
                if(Utility.ERR==false) MessageBox.Show(Utility.displayMessage("Account_new_user_registered"),"NOTIFICATION",MessageBoxButtons.OK,MessageBoxIcon.Information);
                else MessageBox.Show(Utility.displayError("Account_Error_new_user_registered"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //clear warnings
        private void F0_timer_warningClear_Tick(object sender, EventArgs e)
        {
            this.F0_timer_warningClear.Stop(); //stop this timer
            Utility.WARNING = false; //clear the warning flag
        }

        //DEBUG - TO BE DELETED AFTER DEVELOPMENT
        private void timer_debug_Tick(object sender, EventArgs e)
        {
            
            timer_debug.Stop();
            this.F0_textBox_username.Text = "admin1";
            this.F0_textBox_password.Text = "Admin1234@";
            this.F0_button_login.PerformClick();
            
        }

        //enter the username textbox
        private void F0_textBox_username_MouseEnter(object sender, EventArgs e)
        {
            if (Utility.ERR_MESSAGES == false)
            {
                this.F0_toolTip_help.SetToolTip(this.F0_textBox_username, Utility.displayMessage("Help_register_username")); //associate the tooltip with the control
                //this.F0_toolTip_help.Show(Utility.displayMessage("Help_register_username"), this, Utility.tooltipDuration); 
            }
        }

        //enter the password textbox
        private void F0_textBox_password_MouseEnter(object sender, EventArgs e)
        {
            if (Utility.ERR_MESSAGES == false)
            {
                this.F0_toolTip_help.SetToolTip(this.F0_textBox_password, Utility.displayMessage("Help_register_password")); //associate the tooltip with the control
                //this.F0_toolTip_help.Show(Utility.displayMessage("Help_register_password"), this, Utility.tooltipDuration);
            }
        }

        //enter the database combobox
        private void F0_comboBox_dataBase_MouseEnter(object sender, EventArgs e)
        {
            if (Utility.ERR_MESSAGES == false)
            {
                this.F0_toolTip_help.SetToolTip(this.F0_comboBox_dataBase, Utility.displayMessage("Help_database_combobox")); //associate the tooltip with the control
                //this.F0_toolTip_help.Show(Utility.displayMessage("Help_database_combobox"), this, Utility.tooltipDuration);
            }
        }

        //enter the register new user button
        private void F0_button_register_MouseEnter(object sender, EventArgs e)
        {
            if (Utility.ERR_MESSAGES == false)
            {
                this.F0_toolTip_help.SetToolTip(this.F0_button_register, Utility.displayMessage("Help_register_user")); //associate the tooltip with the control
                //this.F0_toolTip_help.Show(Utility.displayMessage("Help_register_user"), this, Utility.tooltipDuration);
            }
        }

        //enter the login button
        private void F0_button_login_MouseEnter(object sender, EventArgs e)
        {
            if (Utility.ERR_MESSAGES == false)
            {
                this.F0_toolTip_help.SetToolTip(this.F0_button_register, Utility.displayMessage("Help_login")); //associate the tooltip with the control
                //this.F0_toolTip_help.Show(Utility.displayMessage("Help_login"), this, Utility.tooltipDuration);
            }
        }

        //enter the change language button
        private void F0_button_language_MouseEnter(object sender, EventArgs e)
        {
            if (Utility.ERR_MESSAGES == false)
            {
                this.F0_toolTip_help.SetToolTip(this.F0_button_language, Utility.displayMessage("Help_language")); //associate the tooltip with the control
                //this.F0_toolTip_help.Show(Utility.displayMessage("Help_language"), this, Utility.tooltipDuration);
            }
        }

        //enter the change theme button
        private void F0_button_theme_MouseEnter(object sender, EventArgs e)
        {
            if (Utility.ERR_MESSAGES == false)
            {
                this.F0_toolTip_help.SetToolTip(this.F0_button_theme, Utility.displayMessage("Help_theme")); //associate the tooltip with the control
                //this.F0_toolTip_help.Show(Utility.displayMessage("Help_theme"), this, Utility.tooltipDuration);
            }
        }

        //update values
        private void F0_timer_updateTimer_Tick(object sender, EventArgs e)
        {
            if(Utility.ERR==false) this.F0_toolTip_help.Active = !Utility.ERR_MESSAGES; //update the tooltip availability
        }
    }
}
