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
    /* Class used to view images
     */
    public partial class F5mdi6_ImageViewer : Form
    {

        //VARIABLES
        private int width_absoluteDifference = 40; //pixels
        private int height_absoluteDifference = 80; //pixels
        private GeneralFile file = null;
        private F5_FileEditorForm f5_FileEditorForm = null;


        //CONSTRUCTORS

        //image from the current group
        public F5mdi6_ImageViewer(F5_FileEditorForm f5_FileEditorForm, GeneralFile file)
        {
            InitializeComponent();

            //store the necessary details about the file and container form
            this.f5_FileEditorForm = f5_FileEditorForm;
            this.file = file;

            //load given image
            this.F5mdi6_pictureBox_image.Load(this.file.getFilePath());
        }
        //new image
        public F5mdi6_ImageViewer()
        {
            InitializeComponent();
        }

        //OTHER METHODS
        //adjust the controls size when the form is being resized
        private void refreshControls()
        {
            this.F5mdi6_pictureBox_image.Width = this.Width - this.width_absoluteDifference;
            this.F5mdi6_pictureBox_image.Height = this.Height - this.height_absoluteDifference;
        }
        //EVENT HANDLERS
        private void F5mdi6_ImageViewer_Load(object sender, EventArgs e)
        {
            Utility.setLanguage(this); //set language

            this.F5mdi6_pictureBox_image.BorderStyle = BorderStyle.FixedSingle;
        }

        //open image
        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                string imagePath;
                DialogResult result = this.F5mdi6_openFileDialog_loadImage.ShowDialog();

                if(result.Equals(DialogResult.OK)) 
                {
                    imagePath = this.F5mdi6_openFileDialog_loadImage.FileName;

                    this.F5mdi6_pictureBox_image.Load(imagePath);
                }

                
            }catch (Exception exception) 
            {
                Utility.DisplayError("ImageViewer_could_not_load_image",exception,"ImageViewer: Could not load an image: \n"+exception.ToString(),false);
            }


        }

        //add the currently open image to the current group
        private void F5mdi6_toolStripButton_moveImage_Click(object sender, EventArgs e)
        {
            try
            {
                string path = this.F5mdi6_pictureBox_image.ImageLocation;
                string filename = this.F5mdi6_pictureBox_image.ImageLocation.ToString().Split('\\')[this.F5mdi6_pictureBox_image.ImageLocation.ToString().Split('\\').Length-1];

                if(path!=null && path!="")
                {
                    File.Copy(path,Utility.currentGroupPath+"\\"+filename);

                    MessageBox.Show(Utility.displayMessage("ImageViewer_image_successfully_added_to_the_current_group"),"INFO",MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }catch (Exception exception)
            {
                Utility.DisplayError("ImageViewer_could_not_add_image_to_group", exception, "ImageViewer: Could not add an image to the group: \n" + exception.ToString(), false);
            }
        }

        //form is being resized
        private void F5mdi6_ImageViewer_Resize(object sender, EventArgs e)
        {
            this.refreshControls();
        }
    }
}
