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
    public partial class U1_CustomizationForm : Form
    {
        //CONSTRUCTORS
        public U1_CustomizationForm()
        {
            InitializeComponent();
        }

        //EVENT HANDLERS

        //form load
        private void U1_CustomizationForm_Load(object sender, EventArgs e)
        {
            //form settings
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
        }
    }
}
