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
    /* Form used to display general information, such as:
        - 'About' pages
        - 'Help' pages
        - 'Contact' pages
     */
    public partial class F6_GeneralInformation : Form
    {
        //VARIABLES

        //CONSTRUCTORS
        public F6_GeneralInformation(string formTitle, string info)
        {
            InitializeComponent();

            this.F6_richTextBox_info.ReadOnly = true;

            //store the title and information received from the caller form
            this.Text = formTitle;
            this.F6_richTextBox_info.Text = info;
            this.MinimumSize = Utility.minimumInfoFormSize;
            this.MaximumSize = Utility.minimumInfoFormSize;
            
        }

        //UTILITY FUNCTIONS

        //EVENT HANDLERS
        private void F6_GeneralInformation_Load(object sender, EventArgs e)
        {
            
        }
    }
}
