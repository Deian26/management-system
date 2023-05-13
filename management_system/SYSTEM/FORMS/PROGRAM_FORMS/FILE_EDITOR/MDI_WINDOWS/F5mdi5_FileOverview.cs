using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace management_system
{
    public partial class F5mdi5_FileOverview : Form
    {
        //currently active MDI window type
        private enum FileType 
        { 
            noFile=-1, //no file
            txt = 0, //text file
            xml = 1,  //XML file
            databaseTable = 2 // database table
        };

        //VARIABLES
        private F5_FileEditorForm f5_containerForm = null;
        private GeneralFile file = null; //current file
        private string databaseTableName = null; //database table name

        private int absolute_heightDifference = 70; //pixels
        private int absolute_widthDifference = 40; //pixels

        //CONSTRUCTORS
        public F5mdi5_FileOverview(F5_FileEditorForm f5_containerForm)
        {
            InitializeComponent();

            //container form
            this.MdiParent = f5_containerForm;
            this.f5_containerForm = f5_containerForm;

            //form settings
            this.F5mdi5_richTextBox_fileOverview.ReadOnly = true;

            //disable close option for this form
            this.ControlBox = false;
        }

        //UTILITY FUNCTIONS

        //refresh controls appearance
        private void refreshControls()
        {
            //refresh controls size
            this.F5mdi5_richTextBox_fileOverview.Height = this.Height - this.absolute_heightDifference;
            this.F5mdi5_richTextBox_fileOverview.Width = this.Width - this.absolute_widthDifference;
        }

        //determine the currently active MDI window and display details about the file it contains
        public void getCurrentFileOverview()
        {
            
            try
            {
                Form activeMdiWindow = this.f5_containerForm.ActiveMdiChild;//get currently active MDI window
                FileType file = FileType.noFile; //default value

                this.file = null;
                this.databaseTableName = null;

                //determine the type of MDI window currently active and if it is an editor MDI window, display details about it
                switch (activeMdiWindow.GetType().Name)
                {
                    case "F5mdi1_TextEditor": //text / RTF file
                        file = FileType.txt;

                        F5mdi1_TextEditor f5Mdi1_TextEditor = (F5mdi1_TextEditor)activeMdiWindow;

                        this.file = f5Mdi1_TextEditor.getFile();

                        break;

                    case "F5mdi2_XmlEditor": //XML file
                        file = FileType.xml;

                        F5mdi2_XmlEditor f5Mdi2_XmlEditor = (F5mdi2_XmlEditor)activeMdiWindow;

                        this.file = f5Mdi2_XmlEditor.getFile();

                        break;

                    case "F5mdi3_DatabaseTableEditor": //database table (in the connected database or a local .tbl file)
                        file = FileType.databaseTable;

                        F5mdi3_DatabaseTableEditor f5Mdi3_DatabaseTableEditor = (F5mdi3_DatabaseTableEditor)activeMdiWindow;

                        this.databaseTableName = f5Mdi3_DatabaseTableEditor.getTableName();

                        break;


                    default:
                        file = FileType.noFile; //error -> set file to the default value of 'FileType.noFile'
                        break;
                }

                //display general file details of the file contained in the currently active MDI window
                if (file == FileType.databaseTable) //database table
                {
                    //display information

                }else if(file == FileType.noFile) //no file
                {
                    //reset information panels
                }
                else //txt / xml / rtf file
                {
                    //display information
                    string info = "";
                    string[] fileInfo = this.file.getFileInfo(); //get file information
                    //messages to be displayed
                    string[] messages =
                    {
                        "F5mdi5_fileAttributes",
                        "F5mdi5_lastAccessDateTime",
                        "F5mdi5_lastModificationDateTime",
                        "F5mdi5_fileExtension",
                        "F5mdi5_fileType",
                        "F5mdi5_fileCreationDateTime",
                        "F5mdi5_filePath"
                    };

                    //construct the information string
                    info += Utility.displayMessage("F5mdi5_fileName") + ": " + this.file.getFilename()+"\n\n";

                    if (fileInfo != null)
                    {
                        for (int i = 0; i < fileInfo.Length; i++)
                        {
                            if (fileInfo[i] != null)
                                info += Utility.displayMessage(messages[i]) + ": " + fileInfo[i] + "\n\n";
                        }

                        info += Utility.displayMessage("F5mdi5_containerGroup") + ": " + Utility.currentGroup.getName()+"\n\n";

                        info += Utility.displayMessage("F5mdi5_groupAuthor") + ": " + Utility.currentGroup.getAuthor()+"\n\n";
                        info += Utility.displayMessage("F5mdi5_adminGroup") + ": " + Utility.currentGroup.getAdminGroup().ToString()+"\n\n";

                    }
                    //display the information string
                    this.F5mdi5_richTextBox_fileOverview.Text = info;
                }

            }
            catch (Exception exception) 
            {
                Utility.DisplayError("FileOverview_failed_to_load_file_overview",exception,  "FileOverview: Failed to load the details about the currently selected MDI window and the file it contains: \n" + exception.ToString(), false);
            }
        }

        //EVENT HANDLERS
        private void F5mdi5_FileOverview_Load(object sender, EventArgs e)
        {
            //form settings
            this.MinimumSize = Utility.mdiEditorMinimumSize;

        }

        //form is being resized
        private void F5mdi5_FileOverview_Resize(object sender, EventArgs e)
        {
            this.refreshControls();
        }
    }
}
