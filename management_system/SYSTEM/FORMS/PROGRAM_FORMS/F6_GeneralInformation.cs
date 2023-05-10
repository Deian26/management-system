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
        private int absolute_heightDifference = 70; //pixels
        private int absolute_widthDifference = 40; //pixels
        //CONSTRUCTORS
        public F6_GeneralInformation(string formTitle, string info)
        {
            InitializeComponent();

            this.F6_richTextBox_info.ReadOnly = true;

            //store the title and information received from the caller form
            this.Text = formTitle;
            this.F6_richTextBox_info.Text = info;
            //this.MinimumSize = Utility.minimumInfoFormSize;
            //this.MaximumSize = Utility.minimumInfoFormSize;
            
            this.refreshControls();

        }

        //UTILITY FUNCTIONS

        //refresh control appearance in the form
        private void refreshControls()
        {
            //resize the text box
            this.F6_richTextBox_info.Height = this.Height - this.absolute_heightDifference;
            this.F6_richTextBox_info.Width = this.Width - this.absolute_widthDifference;
        }

        //EVENT HANDLERS
        private void F6_GeneralInformation_Load(object sender, EventArgs e)
        {
            
        }

        //form is being resized
        private void F6_GeneralInformation_Resize(object sender, EventArgs e)
        {
            this.refreshControls();
        }
    }
}
