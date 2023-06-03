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
    /* Form used to create a new file and add it to the current group
     */
    public partial class F7_CreateNewFile : Form
    {
        //VARIABLES
        private F5_FileEditorForm f5_fileEditorForm = null;
        //CONSTRUCTORS
        public F7_CreateNewFile(F5_FileEditorForm f5_fileEditorForm)
        {
            InitializeComponent();

            //store the F5 form
            this.f5_fileEditorForm=f5_fileEditorForm;
        }

        //UTILITY METHODS
        //check if the file name and extension are valid and returns the file extension if the given string is a valid filename.extension
        private string validFilenameAndExtension(string filenameAndExtension)
        {
            if (filenameAndExtension == null || filenameAndExtension.Equals("")) return null; //empty / null string given -> invalid filename and extension

            string[] name = filenameAndExtension.Split('.');

            if(name.Length!=2) return null; //invalid filename (multiple dots in the name)

            foreach(char c in name[0]) 
            { 
                if((c<'a' || c>'z') &&
                   (c<'A' || c>'Z') &&
                   (c<'0' || c>'9') &&
                   Utility.filename_allowedSpecialCharacters.Contains(c)==false
                    ) //invalid filename
                {
                    return null; //invalid filename
                }    
            }

            return name[1]; //valid filename and extension -> return file extension
        }



        //EVENT HANDLERS
        private void F7_CreateNewFile_Load(object sender, EventArgs e)
        {
            Utility.setLanguage(this); //set language
        }

        //create new file and add it to the current group
        private void F7_button_createNewFile_Click(object sender, EventArgs e)
        {
            try
            {
                string filenameAndExtension = this.F7_textBox_newFileName.Text;
                if (this.validFilenameAndExtension(filenameAndExtension) !=null) //valid file name and extension (filename.extension) given
                {
                    //create the file in the folder of the current group
                    FileStream newFile = File.Create(Utility.currentGroupPath+"\\"+ filenameAndExtension);
                    newFile.Close();
                    if (filenameAndExtension.Split('.')[1].ToLower().Equals("rtf")) //save the file in the RTF format
                    {
                        RichTextBox rtf = new RichTextBox();
                        rtf.Text = "";

                        rtf.SaveFile(Utility.currentGroupPath + "\\" + filenameAndExtension,RichTextBoxStreamType.RichText);
                        
                    }

                    if (filenameAndExtension.Split('.')[1].ToLower().Equals("xml")) //add 'root' element to the XML file (if an XML file is created)
                    {
                        

                        File.WriteAllText(Utility.currentGroupPath + "\\" + filenameAndExtension,"<root></root>");

                    }

                    //display a message indicating that the new file has been created
                    MessageBox.Show(Utility.displayMessage("F7_new_file_created"),"",MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception exception)
            {
                Utility.DisplayError("Groups_failed_to_create_new_file", exception, "Group: Failed to add a new file from the Tree view MDI window: " + exception.ToString(), false);
            }
        }

        //create a new database table using the database table editor
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.F7_textBox_newFileName.Text != null && this.F7_textBox_newFileName.Text != "" && this.F7_textBox_newFileName.Text.Contains('.') == false)
                {
                    F5mdi3_DatabaseTableEditor f5Mdi3_DatabaseTableEditor = new F5mdi3_DatabaseTableEditor(this.f5_fileEditorForm, this.F7_textBox_newFileName.Text, false, true);

                    f5Mdi3_DatabaseTableEditor.Show();

                    this.Close();
                }
            }
            catch (Exception exception)
            {
                Utility.DisplayError("Groups_failed_to_create_new_db_table", exception, "Group: Failed to create a new DB table: \n" + exception.ToString(), false);
            }
        }
    }
    
}
