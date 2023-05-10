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
        public TextFile(string path) : base(path, FileEditor.FileType.text)
        {
            this.fileType = FileEditor.FileType.text; // file type = text

            //count characters
            this.characterCount = File.ReadAllText(path).Replace("\t","").Replace("\n","").Replace("\r","").Replace(" ","").Length;
        }

        //METHODS

    }
}
