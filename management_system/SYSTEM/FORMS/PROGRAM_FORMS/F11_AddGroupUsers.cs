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
    public partial class F11_AddGroupUsers : Form
    {

        //VARIABLES


        //CONSTRUCTORS
        public F11_AddGroupUsers()
        {
            InitializeComponent();
        }


        //EVENT HANDLERS
        private void F11_AddGroupUsers_Load(object sender, EventArgs e)
        {

        }
        
        //add the specified user to the current group
        private void F11_button_addUserToGroup_Click(object sender, EventArgs e)
        {
            try
            {
                //hash the given username and add it into the 'Users' table
                SqlCommand cmd = Utility.getSqlCommand("SELECT * FROM " + Utility.currentGroup.getName() + "_Users WHERE users='" + Utility.DB_HASH(this.F11_textBox_username.Text) + "'");
                SqlDataReader dr = cmd.ExecuteReader();
                int nr = 0;

                while (dr.Read())
                {
                    nr++;
                }

                dr.Close();

                if (nr > 1) //this user already has access to this group
                {
                    MessageBox.Show(Utility.displayMessage("Groups_user_already_added"), "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                cmd = Utility.getSqlCommand("SELECT * FROM Users WHERE username='" + Utility.DB_HASH(this.F11_textBox_username.Text) + "'");
                dr = cmd.ExecuteReader();
                nr = 0;

                while(dr.Read())
                {
                    nr++;
                }

                dr.Close();
                if(nr==0) //the username does not exist
                {
                    MessageBox.Show(Utility.displayMessage("Username_nonexistent"), "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //add the user
                cmd = Utility.getSqlCommand("INSERT INTO " + Utility.currentGroup.getName() + "_Users VALUES('" + Utility.DB_HASH(this.F11_textBox_username.Text) + "')");
                cmd.ExecuteNonQuery();

                //display message
                MessageBox.Show(Utility.displayMessage("Groups_user_added_successfully"), "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                cmd.Dispose();

            }
            catch (Exception exception)
            {
                Utility.DisplayError("Groups_failed_to_add_user_to_group", exception, "Group: Failed to add  user to the group: \n" + exception.ToString(), false);
            }

        }
    }
}
