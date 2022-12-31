using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        //check the username
        private bool validUsername(string username)
        {
            //valid characters: [a-zA-Z0-9_]

            if (username == null || username.Equals("")) return false;

            foreach (char c in username)
                if ((c < 'a' || c > 'z') && (c < 'A' || c > 'Z') && (c<'0' || c>'9') && c != '_') return false;

            return true;
        }

        //check the password (before registering a new user)
        private bool validPassword(string password)
        {
            /*
            the password must:
             * be have least 10 characters
             
            the password must contain:
             * at least one lower case latin alphabet letter [a-z]
             * at least one upper case latin alphabet letter [A-Z]
             * at least one digit [0-9]
             * at least one special character from the list: [~!@#$%^*_]
             */

            //flags
            bool lowercase = false,
                 uppercase = false,
                 digit = false,
                 special_char = false;

            if (password == null || password.Equals("") || password.Length<10) return false;

            foreach (char c in password)
            {
                if (c >= 'a' && c <= 'z') lowercase = true; //lower case letter found
                if (c >= 'A' && c <= 'Z') uppercase = true; //upper case letter found
                if("0123456789".Contains(c)) digit = true; //digit found
                if("~!@#$%^*_".Contains(c)) special_char = true; //special character found
            }

            if(lowercase==true && uppercase==true && digit==true && special_char==true) return true;

            return false;
            
        }

        //EVENTS

        //form load
        private void F0_Login_Load(object sender, EventArgs e)
        {
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
        }

        //form closed
        private void F0_Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        //login
        private void F0_button_login_Click(object sender, EventArgs e)
        {
            bool valid = true;
            bool admin = false;

            //login mechanism

            //check the connection details
            if (this.validUsername(F0_textBox_username.Text) == false)
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
                    switch (Utility.logIn(this.F0_textBox_username.Text, this.F0_textBox_password.Text))
                    {
                        case 0: //admin
                            admin = true;
                            break;  
                        case 1: //user
                            admin = false;
                            break;

                        default: //account not found / error
                            MessageBox.Show(Utility.displayError("Invalid_credentials"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //Application.Exit();
                            Utility.ERR = true;
                            Start.f0_logIn.F0_timer_errorClear.Stop();
                            Start.f0_logIn.F0_timer_errorClear.Start();
                            break;
                    }

                    if (Utility.ERR == false)
                        Utility.setCredentials(this.F0_textBox_username.Text, this.F0_textBox_password.Text.Length, admin); //username, encryption key for the credentials, admin status

                    //open F1_MainForm and hide the LogIn form
                    if (Utility.ERR == false)
                    {
                        Start.f1_mainForm.Show(); //show form
                        this.Hide();
                    }
                }
            }
        }

        //change the system language
        private void F0_button_language_Click(object sender, EventArgs e)
        {
            Utility.nextLanguage(this); //update the language

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
            int data_key, number_of_keys, number_of_usernames=1;
            Random rnd = new Random();
            SqlCommand cmd = null, data_key_cmd = null, username_cmd=null;

            //check the username and password
            if (this.validUsername(this.F0_textBox_username.Text) == false) //invalid username
            {
                this.F0_errorProvider.SetError(this.F0_textBox_username, Utility.displayError("Invalid_username"));
            }
            else if (this.validPassword(this.F0_textBox_password.Text) == false) //invalid password
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
                username_cmd = Utility.getSqlStatement("SELECT * FROM Users WHERE username='" +hashed_username.ToString()+"'");
                number_of_usernames = username_cmd.ExecuteNonQuery();
                if (number_of_usernames != -1) //duplicate usernames
                {
                    this.F0_errorProvider.SetError(this.F0_textBox_username,Utility.displayError("Invalid_username_duplicate"));
                    Utility.ERR = true;
                    Utility.WARNING = true;
                    Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                    Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared
                    return;
                }

                hashed_password = Utility.DB_HASH(this.F0_textBox_password.Text);

                do {
                    data_key = rnd.Next(); //pseudo-random encryption key for files and data other than credentials, unique for each user
                    data_key_cmd = Utility.getSqlStatement("SELECT * FROM Users WHERE data_key=" + data_key.ToString());
                    number_of_keys = data_key_cmd.ExecuteNonQuery();
                } while(number_of_keys != -1); //generate a new data key if the current key already exists

                //write the credentials and data encryption key into the database
                cmd = Utility.getSqlStatement("INSERT INTO Users(username,password,data_key) VALUES('" + hashed_username + "','" + hashed_password + "'," + data_key.ToString() + ")");
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                //display a confirmation message that the user has been registered
                if(Utility.ERR==false && Utility.WARNING==false) MessageBox.Show(Utility.displayMessage("Account_new_user_registered"),"NOTIFICATION",MessageBoxButtons.OK,MessageBoxIcon.Information);
                else MessageBox.Show(Utility.displayError("Account_Error_new_user_registered"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //clear warnings
        private void F0_timer_warningClear_Tick(object sender, EventArgs e)
        {
            this.F0_timer_warningClear.Stop(); //stop this timer
            Utility.WARNING = false; //clear the warning flag
        }
    }
}
