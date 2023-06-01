using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices.WindowsRuntime;
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
                 lastAccessDateTime; //the last date and time the file was accessed
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
                this.lastAccessDateTime = File.GetLastAccessTimeUtc(path); //get the last time this file was accessed

                //determine the file type
                this.fileType = fileType;
                this.fileExtension = FileEditor.extension[(int)this.fileType]; //determine the file fileExtension
                //this.FileType = this.determineFileType(path);

                this.file.Close();
            }catch (Exception exception)
            {
                Utility.DisplayWarning("TextEditor_failed_to_open_text_file", exception, "Invalid file: " + path.ToString(), false);
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
                    FileEditor.determineFileType(this.fileExtension); //determine the file type based on the file extension
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
                Utility.logDiagnosticEntry("TextEditor: File cannot be closed; path: " + filePath.ToString() + "; details: " + exception.ToString());
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
                    Utility.logDiagnosticEntry("TextEditor: FileStream cannot be freed; path: " + filePath.ToString() + "; details: " + exception.ToString());
                    Utility.WARNING = true;
                    Start.f0_logIn.F0_timer_errorClear.Stop();
                    Start.f0_logIn.F0_timer_errorClear.Start();
                }

            }
        }

        //SETTERS

        //GETTERS
        
        //get file name and extension
        public string getFilename()
        {
            try
            {
                string[] path = this.filePath.Split('\\');

                return path[path.Length - 1]; //get filename
            }
            catch (Exception exception)
            {
                Utility.DisplayError("FileOverview_failed_to_get_file_information", exception, "FileOverview: Failed to get file information from the file in the active MDI window: \n" + exception.ToString(), false);
                return null;
            }
        }

        //get file information
        public string[] getFileInfo()
        {
            try
            {
                string[] info = new string[7];

                
                                                    info[0] = this.fileAttributes.ToString();
                if(this.lastAccessDateTime!=null)    info[1] = this.lastAccessDateTime.ToString();
                if(this.lastModifiedDateTime!=null) info[2] = this.lastModifiedDateTime.ToString();
                if(this.fileExtension!=null)        info[3] = this.fileExtension.ToString();
                                                    info[4] = this.fileType.ToString();
                if(this.creationDateTime!=null)     info[5] = this.creationDateTime.ToString();
                if (this.filePath != null)          info[6] = this.filePath.ToString();

                return info;
            }
            catch (Exception exception)
            {
                Utility.DisplayError("FileOverview_failed_to_get_file_information", exception, "FileOverview: Failed to get file information from the file in the active MDI window: \n" + exception.ToString(), false);
                return null;
            }

        }

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
