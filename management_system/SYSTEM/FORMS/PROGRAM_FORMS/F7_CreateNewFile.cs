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

namespace management_system
{
    /* Form used to create a new file and add it to the current group
     */
    public partial class F7_CreateNewFile : Form
    {
        //VARIABLES

        //CONSTRUCTORS
        public F7_CreateNewFile()
        {
            InitializeComponent();
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

                    //display a message indicating that the new file has been created
                    MessageBox.Show(Utility.displayMessage("F7_new_file_created"),"",MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception exception)
            {
                Utility.DisplayError("Groups_failed_to_create_new_file", exception, "Group: Failed to add a new file from the Tree view MDI window: " + exception.ToString(), false);
            }
        }
    }
    
}
