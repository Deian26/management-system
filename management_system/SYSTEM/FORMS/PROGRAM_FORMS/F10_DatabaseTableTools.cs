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
    /* Class used to contain tools for database table editing
     */
    public partial class F10_DatabaseTableTools : Form
    {
        //VARIABLES
        private F5mdi3_DatabaseTableEditor f5Mdi3_DatabaseTableEditor = null;

        //CONSTRUCTORS
        public F10_DatabaseTableTools(F5mdi3_DatabaseTableEditor f5Mdi3_DatabaseTableEditor)
        {
            InitializeComponent();

            //store table editor form
            this.f5Mdi3_DatabaseTableEditor = f5Mdi3_DatabaseTableEditor;
        }

        //OTHER METHODS

        //EVENT HANDLERS
        private void F10_DatabaseTableTools_Load(object sender, EventArgs e)
        {
            Utility.setLanguage(this); //set language
        }
    }
}
