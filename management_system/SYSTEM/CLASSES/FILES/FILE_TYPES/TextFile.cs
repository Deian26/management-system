using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace management_system
{
    /* Text file type class
        - used when a .txt file is encountered
     */
    public class TextFile : GeneralFile
    {
        //VARIABLES
        private long characterCount = 0; //number of characters in the text file

        //CONSTRUCTORS
        public TextFile(string path) : base(path)
        {
            this.fileType = FileEditor.FileType.text; // file type = text

            //count characters
            this.characterCount = File.ReadAllText(path).Replace("\t","").Replace("\n","").Replace("\r","").Replace(" ","").Length;
        }

        //METHODS

        //loads a text file into the given text editor
        public void LoadFile(string path, F5_FileEditorForm f5_fileEditor)
        {
            try
            {
                if (this.file == null) //no text file currently open
                {
                    if (File.Exists(path) == true) //the file exists
                    {
                        this.file = File.Open(path, FileMode.Open); //open text file

                        //load the text into the text editor
                        f5_fileEditor.LoadText(path, this.file);

                    }
                    else //the file does not exist
                    {
                        MessageBox.Show(Utility.displayError("TextEditor_file_does_not_exist") + path.ToString(), "TextEditor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Utility.logDiagnsoticEntry("TextEditor: File does not exist; path: " + path.ToString());
                        Utility.WARNING = true;
                        Start.f0_logIn.F0_timer_errorClear.Stop();
                        Start.f0_logIn.F0_timer_errorClear.Start();

                    }
                }
                else //a text file is already open
                {
                    MessageBox.Show(Utility.displayError("TextEditor_a_file_already_open"), "TextEditor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Utility.logDiagnsoticEntry("TextEditor: Cannot open a text file. A text file is already open. " + path.ToString());
                    Utility.WARNING = true;
                    Start.f0_logIn.F0_timer_errorClear.Stop();
                    Start.f0_logIn.F0_timer_errorClear.Start();
                }


            }
            catch (Exception exception) //error
            {
                Utility.WARNING = true;
                MessageBox.Show(Utility.displayError("TextEditor_failed_to_open_text_file") + path.ToString() + "; " + exception.Message, "TextEditor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Utility.logDiagnsoticEntry("TextEditor: Failed to open a text file; path: " + path.ToString() + "; details: " + exception.ToString());
                Start.f0_logIn.F0_timer_errorClear.Stop();
                Start.f0_logIn.F0_timer_errorClear.Start();
            }
        }

    }
}
