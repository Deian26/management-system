using management_system;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace management_system
{
    //form used in file editting and as a file explorer (displaying files from the current group)
    public partial class F5_FileEditorForm : Form
    {
        //VARIABLES
        //current group
        internal Group currentGroup = null;

        //appearance variables
        private int offsetX = 0; //pixels -> top of the form to the top of the control
        private int offsetY = 0; //pixels -> left margin of the form to the left margin of the control
        private int rightOffsetX = 20; //absolute value used to position lateral windows in the container form
        private double mdiLateralWindowsWidthPercent = 0.15;
        private double mdiEditorWindowsWidthPercent = 0.70;
        private int mdiWindowsHeightAbsolute = 90; //pixels subtracted from the height of the container form to adjust the height of MDI windows

        private int loadingScreenHideInterval = 500; //the number of milliseconds to wait before hiding the loading screen panel

        //MDI windows
        F5mdi1_TextEditor f5_mdiTextEditor = null;
        F5mdi2_XmlEditor f5_mdiXmlEditor = null;
        F5mdi3_DatabaseTableEditor f5_mdiDatabaseTableEditor = null;
        F5mdi4_FileTreeView f5_mdiFileTreeView = null;
        F5mdi5_FileOverview f5_mdiFileOverview = null;

        //CONSTRUCTORS
        public F5_FileEditorForm(Group group)
        {
            InitializeComponent();

            //store current group details
            this.currentGroup = group;
            Utility.currentGroup = group;
            Utility.currentGroupPath = group.getLocalFilesPath();

            //form settings

            //min size
            this.MinimumSize = Utility.fileEditorFormMinimumSize;

            //construct the common MDI windows
            f5_mdiFileTreeView = new F5mdi4_FileTreeView(this);
            f5_mdiFileOverview = new F5mdi5_FileOverview(this);

            this.IsMdiContainer = true; //mark this form as and MDI container
            this.WindowState = FormWindowState.Maximized; //maximize window

        }

        //UTILITY FUNCTIONS

        //re-calculates the size and position of the controls of this form
        private void refreshControlsAppearance(bool resetEditorsAppearance)
        {

            //MDI windows
            foreach (Form mdi in this.MdiChildren)
            {
                if (mdi.GetType().Name.Equals("F5mdi4_FileTreeView") == true) //file tree view
                {
                    mdi.Show();
                    //mdi.Dock = DockStyle.Left; //dock MDI window to the left of the container form
                    mdi.Location = new Point(this.offsetX, this.offsetY);
                    mdi.Size = new Size((int)(this.Width * this.mdiLateralWindowsWidthPercent), (int)(this.Height - this.mdiWindowsHeightAbsolute));

                }
                else if (mdi.GetType().Name.Equals("F5mdi5_FileOverview") == true) //file overview
                {
                    mdi.Show();
                    //mdi.Dock= DockStyle.Right; //dock MDI window to the right of the container form
                    mdi.Location = new Point(this.Width - (int)(this.Width * this.mdiLateralWindowsWidthPercent) - this.offsetX - this.rightOffsetX, this.offsetY);
                    mdi.Size = new Size((int)(this.Width * this.mdiLateralWindowsWidthPercent), this.Height - this.mdiWindowsHeightAbsolute);

                }
                //change the appearance of the file editors if they are already shown
                else if (mdi.GetType().Name.Equals("F5mdi1_TextEditor") == true && resetEditorsAppearance == true) //text editor mdi
                {
                    this.displayTextEditor(mdi, true);
                }
                else if (mdi.GetType().Name.Equals("F5mdi2_XmlEditor") == true && resetEditorsAppearance == true) //xml editor
                {
                    this.displayXmlEditor(mdi, true);
                }
                else if (mdi.GetType().Name.Equals("F5mdi3_DatabaseTableEditor") == true && resetEditorsAppearance == true) //database table editor
                {
                    this.displayDatabaseTableEditor(mdi, true);
                }

            }

        }

        //display and set the appearance of the text editor MDI window
        private void displayTextEditor(Form mdi, bool show)
        {
            if (show == true) mdi.Show();
            mdi.Location = new Point((int)(this.Width * this.mdiLateralWindowsWidthPercent) + this.offsetX, this.offsetY);
            mdi.Size = new Size((int)(this.Width * this.mdiEditorWindowsWidthPercent) - this.rightOffsetX, (int)(this.Height - this.mdiWindowsHeightAbsolute));

        }

        //display and set the appearance of the XML editor MDI window
        private void displayXmlEditor(Form mdi, bool show)
        {
            if (show == true) mdi.Show();
            mdi.Location = new Point((int)(this.Width * this.mdiLateralWindowsWidthPercent) + this.offsetX, this.offsetY);
            mdi.Size = new Size((int)(this.Width * this.mdiEditorWindowsWidthPercent) - this.rightOffsetX, (int)(this.Height - this.mdiWindowsHeightAbsolute));

        }

        //display and set the appearance of the database table editor MDI window
        private void displayDatabaseTableEditor(Form mdi, bool show)
        {
            if (show == true) mdi.Show();
            mdi.Location = new Point((int)(this.Width * this.mdiLateralWindowsWidthPercent) + this.offsetX, this.offsetY);
            mdi.Size = new Size((int)(this.Width * this.mdiEditorWindowsWidthPercent) - this.rightOffsetX, (int)(this.Height - this.mdiWindowsHeightAbsolute));

        }

        //EVENT HANDLERS
        private void F5_FileEditorForm_Load(object sender, EventArgs e)
        {

            //display the common MDI windows
            this.f5_mdiFileTreeView.Show();
            this.f5_mdiFileOverview.Show();

            //refresh the appearance of the controls from this form
            this.refreshControlsAppearance(true);
        }

        //form is being resized
        private void F5_FileEditorForm_Resize(object sender, EventArgs e)
        {
            //refresh the size and position of the controls of this form
            this.refreshControlsAppearance(false);
        }

        //open file
        private void F5_ToolStripMenuItem_openFile_Click(object sender, EventArgs e)
        {
            try
            {
                //apply a filter to the open file dialog so that onlt .rtf and .txt files can be selected
                //this.F5_openFileDialog_openFile.Filter = ";

                //show the open file dialog
                DialogResult result = this.F5_openFileDialog_openFile.ShowDialog();

                if (result.Equals(DialogResult.OK)) //OK
                {
                    //determine the file type and open the corresponding editor or the text editor, if the file extension is not recognised

                    string filePath = this.F5_openFileDialog_openFile.FileName; //get full file path

                    FileEditor.FileType fileType =  FileEditor.determineFileType(filePath); //get file type

                    GeneralFile file = new GeneralFile(filePath, fileType);

                    //open editor
                    F5mdi1_TextEditor f5Mdi1_TextEditor = null;
                    F5mdi2_XmlEditor f5Mdi2_XmlEditor = null;
                    F5mdi3_DatabaseTableEditor f5Mdi3_DatabaseTableEditor = null;

                    switch (fileType)
                    {
                        case FileEditor.FileType.text: //TXT
                            f5Mdi1_TextEditor = new F5mdi1_TextEditor(this, file);

                            f5Mdi1_TextEditor.Show(); //display MDI window

                            file = null; 

                            break;

                        case FileEditor.FileType.rtf: //RTF
                            f5Mdi1_TextEditor = new F5mdi1_TextEditor(this, file);

                            f5Mdi1_TextEditor.Show(); //display MDI window

                            file = null;
                            break;

                        case FileEditor.FileType.xml: //XML
                            f5Mdi2_XmlEditor = new F5mdi2_XmlEditor(this, file);

                            file = null;
                            break;
                        
                        case FileEditor.FileType.general: //open a text editor
                            f5Mdi1_TextEditor = new F5mdi1_TextEditor(this, file);

                            f5Mdi1_TextEditor.Show(); //display MDI window

                            file = null;
                            break;

                        case FileEditor.FileType.databaseTable: //local Database table (.tbl file)
                            f5Mdi3_DatabaseTableEditor = new F5mdi3_DatabaseTableEditor(this, filePath, true); //'true' = local database file (.tbl)
                            break;


                        default: //general file => open the text editor
                            f5Mdi1_TextEditor = new F5mdi1_TextEditor(this, file);

                            f5Mdi1_TextEditor.Show(); //display MDI window

                            file = null;
                            break;
                    }

                }
            }
            catch (Exception exception)
            {
                Utility.DisplayError("TextEditor_failed_to_open_text_file", exception, "TextEditor: could not open a local file: " + exception.ToString(), false);
            }
        }

        private void F5_toolStripDropDownButton_fileButton_Click(object sender, EventArgs e)
        {

        }

        //MDI window activated
        private void F5_FileEditorForm_MdiChildActivate(object sender, EventArgs e)
        {
            if(this.f5_mdiFileOverview != null)
                this.f5_mdiFileOverview.getCurrentFileOverview();
        }
    }
}
