using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace management_system
{
    public partial class F1_MainForm : Form
    {
        public F1_MainForm()
        {
            InitializeComponent();
        }

        //form closed
        private void F1_MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit(); //exit application
        }

        //form load
        private void F1_MainForm_Load(object sender, EventArgs e)
        {
            Utility.setLanguage(this); //load the language for the current form
            //load account info
            this.F1_label_usernameDisplay.Text = Utility.username;
            this.F1_label_dataBaseNameDisplay.Text = Utility.DB_name;
            if (Utility.admin == true) this.F1_groupBox_accountDetails.Text = "Admin";
            else this.F1_groupBox_accountDetails.Text = "User";
        }

        private void F1_groupBox_mainControls_Enter(object sender, EventArgs e)
        {

        }
    }
}
