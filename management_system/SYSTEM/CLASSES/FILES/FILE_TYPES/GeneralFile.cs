using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace management_system
{
    /* General file type class
         - all of the classes that treat recognized file types are derived from this class
       
       If the file type is not recognized:
         - this class is used to open/view/edit the file
         - the file is treated as a simple text file
     */
    public class GeneralFile
    {
        //VARIABLES
        protected FileStream file = null; //the currently open file
        protected string filePath = null; //the filePath to the currently open and locally stored file
        protected string dbFileIndex = null; //the index of the file (in the file indexing table)
        protected FileEditor.FileType fileType = FileEditor.FileType.noFile; //file type; default: 0 (general file); see FileEditor.cs for the currently supported file types
        protected FileAttributes fileAttributes; //file attributes
        protected DateTime creationDateTime, //the date and time of the creation of the file
                 lastModifiedDateTime, //the last date and time the file was written to
                 lasAccessDateTime; //the last date and time the file was accessed
        protected string fileExtension = null; //the fileExtension of the current file

        //CONSTRUCTORS

        //filePath constructor
        public GeneralFile(string path, FileEditor.FileType fileType)
        {
            try
            {
                this.file = File.Open(path, FileMode.Open); //open the file
                this.filePath = path; //store the given filePath
                


                this.fileAttributes = File.GetAttributes(path); //get file attributes
                this.creationDateTime = File.GetCreationTimeUtc(path); //get file creation date and time
                this.lastModifiedDateTime = File.GetLastWriteTimeUtc(path); //get the last date and time when this file was written to
                this.lasAccessDateTime = File.GetLastAccessTimeUtc(path); //get the last time this file was accessed
                this.fileExtension = FileEditor.extension[(int)this.fileType]; //determine the file fileExtension

                //determine the file type
                this.fileType = fileType;
                //this.fileType = this.determineFileType(path);

                this.file.Close();
            }catch (Exception exception)
            {
                Utility.DisplayWarning("TextEditor_failed_to_open_text_file"+path.ToString(), exception, "Invalid file: " + path.ToString(), false);
            }
        }

        //METHODS

        //determine the type of a file based on the given path
        private FileEditor.FileType determineFileType(string path)
        {
            try
            {
                string[] aux_path = path.Split('.');

                if (aux_path.Length > 2) //more than 1 dot ('.') in the path => invalid path
                    throw new Exception("Path contains more than one dot.");
                else
                {
                    this.fileExtension = aux_path[1]; //store the file fileExtension as a string
                    FileEditor.determineFilType(this.fileExtension); //determine the file type based on the file extension
                }

            }
            catch (Exception exception)
            {
                Utility.DisplayWarning("TextEditor_invalid_path" + path.ToString(), exception, "Invalid path: " + path.ToString(), false);
            }
            return FileEditor.FileType.noFile;
        }
        //close the file stream associated with the currently open file
        public void CloseFile()
        {
            try
            {
                if(this.file!=null)
                    this.file.Close(); //try to close the file

            }
            catch (Exception exception)
            {
                MessageBox.Show(Utility.displayError("TextEditor_file_cannot_be_closed") + filePath.ToString() + "; " + exception.Message, "TextEditor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Utility.logDiagnsoticEntry("TextEditor: File cannot be closed; path: " + filePath.ToString() + "; details: " + exception.ToString());
                Utility.WARNING = true;
                Start.f0_logIn.F0_timer_errorClear.Stop();
                Start.f0_logIn.F0_timer_errorClear.Start();
            }
            finally
            {
                try
                {
                    this.file = null; //mark that there is no currently open file
                }
                catch (Exception exception)
                {
                    Utility.logDiagnsoticEntry("TextEditor: FileStream cannot be freed; path: " + filePath.ToString() + "; details: " + exception.ToString());
                    Utility.WARNING = true;
                    Start.f0_logIn.F0_timer_errorClear.Stop();
                    Start.f0_logIn.F0_timer_errorClear.Start();
                }

            }
        }

        //setters

        //getters
        public FileEditor.FileType getFileType()
        {
            return this.fileType;
        }

        //return 'true' if this file is stored in the connected database and 'false' otherwise (locally stored file)
        public bool isDbFile()
        {
            return this.dbFileIndex!=null;
        }

        //get the file path
        public string getFilePath()
        {
            return this.filePath;
        }
    }
}
