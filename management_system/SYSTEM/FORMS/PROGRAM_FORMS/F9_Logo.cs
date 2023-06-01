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
    /* Class used to display the logo of the application and other information, before the Log in form is displayed.
     */
    public partial class F9_Logo : Form
    {
        //CONSTRUCTORS
        public F9_Logo()
        {
            InitializeComponent();
        }


        //OTHER METHODS

        //EVENT HANDLERS
        private void F9_Logo_Load(object sender, EventArgs e)
        {
            //wait for a set amount of time then display the Login form

            this.F9_timer_logoTimer.Interval = Utility.LOGO_interval;
            this.F9_timer_logoTimer.Start();
        }

        //display the Login form
        private void F9_timer_logoTimer_Tick(object sender, EventArgs e)
        {
            this.Hide();

            Start.f0_logIn.Show();

            //stop this timer
            this.F9_timer_logoTimer.Stop();
        }
    }
}
