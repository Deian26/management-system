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

namespace management_system
{
    public partial class F3_UserManagement : Form
    {
        //VARIABLES
        private static string adminString = null;

        //CONSTRUCTORS
        public F3_UserManagement()
        {
            InitializeComponent();
        }


        //EVENT HANDLERS

        //load form
        private void F3_UserManagement_Load(object sender, EventArgs e)
        {
            //set the minimum and maximum sizes for the form
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            CenterToParent();

            //timers
            this.F3_timer_updateTimer.Interval = Utility.updateTimerInterval;
            this.F3_timer_updateTimer.Start();

            //load user rights choices into the combo box
            this.F3_comboBox_userRights.Items.Clear();
            this.F3_comboBox_userRights.Items.Add("User");
            this.F3_comboBox_userRights.Items.Add("Administrator");
            this.F3_comboBox_userRights.SelectedIndex = 0;

            //load language pack
            Utility.setLanguage(this);
        }

        //assign the selected rights to the selected user
        private void F3_button_assignUserRights_Click(object sender, EventArgs e)
        {
            try
            {
                //display errors if the credentials are supplied in an invalid format
                if (!Utility.validUsername(this.F3_textBox_username.Text))
                {
                    
                    this.F3_errorProvider_userManagement.SetError(this.F3_textBox_username, Utility.displayError("Invalid_username"));

                    return;
                }

                if(!Utility.validPassword(this.F3_textBox_password.Text))
                {
                    this.F3_errorProvider_userManagement.SetError(this.F3_textBox_password, Utility.displayError("Invalid_password"));

                    return;
                }

                SqlCommand cmd = Utility.getSqlCommand("INSERT INTO Users(admin) VALUES("+ F3_UserManagement.adminString.ToString()+") WHERE username='" + Utility.DB_HASH(this.F3_textBox_username.Text) + "' and password = '"+Utility.DB_HASH(this.F3_textBox_password.Text)+"'");
                cmd.ExecuteNonQuery();

                cmd.Dispose();

            }catch (Exception exception)
            {
                MessageBox.Show(Utility.displayError("DB_account_not_found") + exception.ToString(), "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning); //display an error message
                //Utility.setDiagnosticEntry("EN: Invalid signature for the XML file: " + XML_path.ToString());
                Utility.WARNING = true;
                Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared
                //Application.Exit(); //trigger an application exit
            }
        }

        //update timer
        private void F3_timer_updateTimer_Tick(object sender, EventArgs e)
        {
            //clear error messages
            this.F3_errorProvider_userManagement.Clear();
        }
        
        //select user rights to assign
        private void F3_comboBox_userRights_SelectedIndexChanged(object sender, EventArgs e)
        {
            Random rnd = new Random();

            switch(this.F3_comboBox_userRights.SelectedItem)
            {
                case "User": //assign user rights (default)
                    F3_UserManagement.adminString = (rnd.Next() % 100).ToString();
                    break;
                case "Administrator": //assign admin rights
                    F3_UserManagement.adminString = Utility.DB_HASH(Utility.ENC_GEN(this.F3_textBox_password.Text, Utility.key));
                    break;

                default: //invalid value
                    break;
            }
        }

        //delete the account (based only on the given username)
        private void F3_button_deleteAccount_Click(object sender, EventArgs e)
        {
            try
            {
                //wait admin confirmation
                DialogResult result = MessageBox.Show("CONFIRMATION", Utility.displayMessage("Account_confirmation_delete"), MessageBoxButtons.OKCancel);

                if (result.Equals(DialogResult.OK)) //deletion confirmed
                {

                    SqlCommand cmd = Utility.getSqlCommand("DELETE FROM Users WHERE username='" + Utility.DB_HASH(this.F3_textBox_username.Text) + "'");
                    cmd.ExecuteNonQuery();

                    cmd.Dispose();
                }

            }catch (Exception exception)
            {
                MessageBox.Show(Utility.displayError("DB_account_not_found") + exception.ToString(), "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning); //display an error message
                //Utility.setDiagnosticEntry("EN: Invalid signature for the XML file: " + XML_path.ToString());
                Utility.WARNING = true;
                Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared
                //Application.Exit(); //trigger an application exit
            }
        }
    }
}
