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
    /* Class used to view images
     */
    public partial class F5mdi6_ImageViewer : Form
    {
        //CONSTRUCTORS
        public F5mdi6_ImageViewer()
        {
            InitializeComponent();
        }

        //OTHER METHODS

        //EVENT HANDLERS
        private void F5mdi6_ImageViewer_Load(object sender, EventArgs e)
        {
            Utility.setLanguage(this); //set language
        }
    }
}
