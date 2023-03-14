using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace management_system.SYSTEM.FORMS.PROGRAM_FORMS.FILE_EDITOR.MDI_WINDOWS
{
    public partial class F5mdi4_FileTreeView : Form
    {
        //VARIABLES
        private F5_FileEditorForm f5_containerForm = null;

        //CONSTRUCTORS
        public F5mdi4_FileTreeView(F5_FileEditorForm f5_containerForm)
        {
            InitializeComponent();

            //container form
            this.MdiParent = f5_containerForm;
            this.f5_containerForm = f5_containerForm;
        }

        //UTILITY FUNCTIONS

        //EVENT HANDLERS
        private void F5mdi4_FileTreeView_Load(object sender, EventArgs e)
        {
            //form settings
            this.MinimumSize = Utility.mdiEditorMinimumSize;
        }
    }
}
