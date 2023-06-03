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

namespace management_system
{
    //user rights management form
    public partial class F3_UserManagement : Form
    {
        //VARIABLES
        private string adminString = null; //the admin string of the user who is granted permission
        private int key=0; // the individual key of the user who is granted permissions

        //CONSTRUCTORS
        public F3_UserManagement()
        {
            InitializeComponent();
        }


        //EVENT HANDLERS

        //load form
        private void F3_UserManagement_Load(object sender, EventArgs e)
        {
            Utility.setLanguage(this); //set language

            this.F3_label_password.Visible = false;
            this.F3_textBox_password.Visible = false;

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
                //get individual key

                this.key = Utility.getDataKey(this.F3_textBox_username.Text);
                
                //calculate admin string
                Random rnd = new Random();
                bool setUser = false;

                switch (this.F3_comboBox_userRights.SelectedItem)
                {
                    case "User": //assign user rights (default)
                        this.adminString = Utility.DB_HASH((rnd.Next() % 100).ToString());
                        setUser = true;
                        break;
                    case "Administrator": //assign admin rights
                        this.adminString = Utility.DB_HASH(Utility.ENC_GEN(this.F3_textBox_password.Text, this.key));
                        break;

                    default: //invalid value
                        break;
                }

                //display errors if the credentials are supplied in an invalid format
                if (!Utility.validUsername(this.F3_textBox_username.Text))
                {

                    this.F3_errorProvider_userManagement.SetError(this.F3_textBox_username, Utility.displayError("Invalid_username"));

                    return;
                }

                if (!Utility.validPassword(this.F3_textBox_password.Text) && setUser == false)
                {
                    this.F3_errorProvider_userManagement.SetError(this.F3_textBox_password, Utility.displayError("Invalid_password"));

                    return;
                }

                SqlCommand cmd;
                if(setUser==true)
                {
                    cmd = Utility.getSqlCommand("UPDATE Users SET admin='" + this.adminString.ToString() + "' WHERE username='" + Utility.DB_HASH(this.F3_textBox_username.Text)+"'");
                }
                else
                {
                    cmd = Utility.getSqlCommand("UPDATE Users SET admin='" + this.adminString.ToString() + "' WHERE username='" + Utility.DB_HASH(this.F3_textBox_username.Text) + "' and password = '" + Utility.DB_HASH(this.F3_textBox_password.Text) + "'");
                }

                cmd.ExecuteNonQuery();

                cmd.Dispose();


            }catch (Exception exception)
            {
                Utility.DisplayError("DB_account_not_found", exception, "SYSTEM: Database account not found: \n" + exception.ToString(), false);
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
            if(this.F3_comboBox_userRights.SelectedIndex == 0) //set to 'user' permissions
            {
                this.F3_label_password.Visible = false;
                this.F3_textBox_password.Visible = false;
            }
            else if(this.F3_comboBox_userRights.SelectedIndex == 1)
            {
                this.F3_label_password.Visible = true;
                this.F3_textBox_password.Visible = true;
            }
        }

        //delete the account (based only on the given username)
        private void F3_button_deleteAccount_Click(object sender, EventArgs e)
        {
            try
            {
                //wait admin confirmation
                DialogResult result = MessageBox.Show(Utility.displayMessage("Account_confirmation_delete"), "CONFIRM", MessageBoxButtons.OKCancel);

                if (result.Equals(DialogResult.OK)) //deletion confirmed
                {
                    //delete account from the database
                    SqlCommand cmd = Utility.getSqlCommand("DELETE FROM Users WHERE username='" + Utility.DB_HASH(this.F3_textBox_username.Text) + "'");
                    cmd.ExecuteNonQuery();

                    cmd.Dispose();

                    //delete local user folder
                    Directory.Delete(Utility.dirPathDATA+"\\"+this.F3_textBox_username.Text,true);
                }

            }catch (Exception exception)
            {
                Utility.DisplayError("DB_account_not_found",exception,"SYSTEM: Failed to delete an account: \n"+exception.ToString(),false);
            }
        }
    }
}
