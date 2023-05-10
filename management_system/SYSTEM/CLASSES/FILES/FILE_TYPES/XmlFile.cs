using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace management_system.SYSTEM.CLASSES.FILES.FILE_TYPES
{
    /* XML file type class
         - used when a .xml file is encountered
     */
    public class XmlFile : GeneralFile
    {
        //VARIABLES

        //CONSTRUCTORS
        public XmlFile(string path) : base(path, FileEditor.FileType.xml)
        {
            this.fileType = FileEditor.FileType.xml;
        }

        //METHODS
    }
}
