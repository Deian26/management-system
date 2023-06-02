using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace management_system
{
    public partial class F5mdi4_FileTreeView : Form
    {
        //VARIABLES
        private F5_FileEditorForm f5_containerForm = null;

        //controls appearance
        private int absolute_heightDifference = 100;//pizels
        private int absolute_widthDifference = 40; //pixels

        //CONSTRUCTORS
        public F5mdi4_FileTreeView(F5_FileEditorForm f5_containerForm)
        {
            InitializeComponent();

            //refresh controls appearance
            this.refreshControls();

            //container form
            this.MdiParent = f5_containerForm;
            this.f5_containerForm = f5_containerForm;

            //add the files of the current group into the tree view
            this.loadGroupFiles();

            //add a context menu to the Tree view control
            MenuItem[] menuItems = new MenuItem[1];
            menuItems[0] = new MenuItem(Utility.displayMessage("F5mdi4_delete_file"), F5mdi4_DeleteFile);
            ContextMenu treeViewContextMenu = new ContextMenu(menuItems);

            this.F5mdi4_treeView_currentGroupFiles.ContextMenu = treeViewContextMenu;

            //disable close option for this form
            this.ControlBox = false;
        }

        //UTILITY FUNCTIONS

        //open the selected file
        private void openFile()
        {
            try
            {
                //if the main nodes ('Local' or 'Database') have been selected, exit the function
                if (this.F5mdi4_treeView_currentGroupFiles.SelectedNode == this.F5mdi4_treeView_currentGroupFiles.Nodes[0] ||
                    this.F5mdi4_treeView_currentGroupFiles.SelectedNode == this.F5mdi4_treeView_currentGroupFiles.Nodes[1])
                    return;


                //check if the selected file is a local file or a database table
                if (this.F5mdi4_treeView_currentGroupFiles.Nodes[0].Nodes.Contains(this.F5mdi4_treeView_currentGroupFiles.SelectedNode)) //local file
                {
                    //check if the file still exists
                    if (File.Exists(this.F5mdi4_treeView_currentGroupFiles.SelectedNode.Name) == false)
                    {
                        Utility.DisplayWarning("FileTreeViewer_invalid_file_or_filepath", new Exception(""), "FileTreeView: Failed to open a file; the file path or name might be invalid (he file might have been deleted). Path: " + this.F5mdi4_treeView_currentGroupFiles.SelectedNode.Name.ToString(), false);
                        return;
                    }

                    //determine file type and open the corresponding file editor

                    //DEV
                    GeneralFile file = null;
                    switch (FileEditor.determineFileType(this.F5mdi4_treeView_currentGroupFiles.SelectedNode.Text))
                    {

                        case FileEditor.FileType.xml: //XML
                            file = new GeneralFile(this.F5mdi4_treeView_currentGroupFiles.SelectedNode.Name, FileEditor.FileType.xml);

                            F5mdi2_XmlEditor f5Mdi2_XmlEditor = new F5mdi2_XmlEditor(this.f5_containerForm, file);

                            f5Mdi2_XmlEditor.Show();


                            file.CloseFile();

                            break;

                        case FileEditor.FileType.text: //TXT 
                            file = new GeneralFile(this.F5mdi4_treeView_currentGroupFiles.SelectedNode.Name, FileEditor.FileType.text);

                            F5mdi1_TextEditor f5Mdi1_TextEditor = new F5mdi1_TextEditor(this.f5_containerForm, file);

                            f5Mdi1_TextEditor.Show();

                            file.CloseFile();
                            break;

                        case FileEditor.FileType.rtf: //RTF
                            file = new GeneralFile(this.F5mdi4_treeView_currentGroupFiles.SelectedNode.Name, FileEditor.FileType.rtf);

                            F5mdi1_TextEditor f5Mdi1_RtfTextEditor = new F5mdi1_TextEditor(this.f5_containerForm, file);

                            f5Mdi1_RtfTextEditor.Show();

                            file.CloseFile();
                            break;

                        case FileEditor.FileType.img: //IMAGE
                            file = new GeneralFile(this.F5mdi4_treeView_currentGroupFiles.SelectedNode.Name, FileEditor.FileType.img);

                            F5mdi6_ImageViewer f5Mdi6_imageViewer = new F5mdi6_ImageViewer(this.f5_containerForm, file);

                            f5Mdi6_imageViewer.Show();

                            file.CloseFile();

                            break;

                        case FileEditor.FileType.noFile: //no file given / no file type

                            break;

                        default: //general file => text file
                            file = new GeneralFile(this.F5mdi4_treeView_currentGroupFiles.SelectedNode.Name, FileEditor.FileType.general);

                            F5mdi1_TextEditor f5Mdi1_generalTextEditor = new F5mdi1_TextEditor(this.f5_containerForm, file);

                            f5Mdi1_generalTextEditor.Show();

                            file.CloseFile();
                            break;
                    }

                }
                else if (this.F5mdi4_treeView_currentGroupFiles.Nodes[1].Nodes.Contains(this.F5mdi4_treeView_currentGroupFiles.SelectedNode)) //database table
                {
                    F5mdi3_DatabaseTableEditor f5Mdi1_databaseTableEditor = new F5mdi3_DatabaseTableEditor(this.f5_containerForm, this.F5mdi4_treeView_currentGroupFiles.SelectedNode.Text, false, false); //1st 'false' = not a local .tbl database table, 2nd 'false' = not a new table

                    f5Mdi1_databaseTableEditor.Show();

                } //else => unknown file location

            }
            catch (Exception exception)
            {
                Utility.DisplayError("FileTreeView_failed_to_load_file", exception, "FileEditor: Failed to load a file from the tree view (file: " + this.F5mdi4_treeView_currentGroupFiles.SelectedNode.Text + ") :" + exception.ToString(), false);
            }
        }

        //refresh controls appearance
        private void refreshControls()
        {
            //resize tree view control
            this.F5mdi4_treeView_currentGroupFiles.Height = this.Height - this.absolute_heightDifference;
            this.F5mdi4_treeView_currentGroupFiles.Width = this.Width - this.absolute_widthDifference;
        }

        //load the files of the current group into the tree view control
        private void loadGroupFiles()
        {
            try
            {
                //clear the tree view list
                this.F5mdi4_treeView_currentGroupFiles.Nodes.Clear();

                //add the default 'Local' and 'Database' nodes
                this.F5mdi4_treeView_currentGroupFiles.Nodes.Add(Utility.displayMessage("F5mdi4_file_tree_view_default_node_Local"));
                this.F5mdi4_treeView_currentGroupFiles.Nodes.Add(Utility.displayMessage("F5mdi4_file_tree_view_default_node_Database"));

                //parse the folder of the current group and load the filenames into the tree view control
                string groupLocalFilePath = this.f5_containerForm.currentGroup.getLocalFilesPath();

                string[] groupLocalFiles = Directory.GetFiles(groupLocalFilePath);
                string filename;

                foreach(string fullFilePath in groupLocalFiles) 
                {
                    filename = fullFilePath.Split('\\')[fullFilePath.Split('\\').Length - 1];

                    this.F5mdi4_treeView_currentGroupFiles.Nodes[0].Nodes.Add(fullFilePath,filename); //Nodes[0] = Local files
                                                                                                      // node key = file path; node text = file name
                }

                //load the names of the database tables associated with this group
                SqlCommand getGroupTables = Utility.getSqlCommand("SELECT * FROM "+this.f5_containerForm.currentGroup.getName()+"_DatabaseFiles"); //get all of the file names from the index of the current group
                SqlDataReader dr = getGroupTables.ExecuteReader();

                int databaseTable = -1; //1 = the current file (read from the _DatabaseFiles database table) is a database table; 0 = the current file is not a database table

                while(dr.Read())
                {
                    databaseTable = dr.GetInt32(2);

                    if(databaseTable == 1) //database table => add the name of the table to the tree view as a database table
                    {
                        this.F5mdi4_treeView_currentGroupFiles.Nodes[1].Nodes.Add(dr.GetString(0),dr.GetString(0)); //Nodes[1] = Database tables
                    }
                }

                dr.Close();
                getGroupTables.Dispose();

            }catch(Exception exception)
            {
                Utility.DisplayError("FileTreeView_failed_to_load_group_files", exception, "FileTreeViewEditor: Failed to load the files of group "+this.f5_containerForm.currentGroup.getName()+" : " + exception.ToString(), false);
            }
        }

        //EVENT HANDLERS
        private void F5mdi4_FileTreeView_Load(object sender, EventArgs e)
        {
            Utility.setLanguage(this); //set language

            //form settings
            this.MinimumSize = Utility.mdiEditorMinimumSize;

            //expand tree nodes
            try
            {
                this.F5mdi4_treeView_currentGroupFiles.ExpandAll();
            }catch(Exception exception)
            {
                Utility.DisplayError("FileTreeView_failed_to_expand_file_tree_view_nodes", exception, "FileTreeViewEditor: Failed to expand the nodes of the file tree view form", false);
            }
        }

        //form is being resized
        private void F5mdi4_FileTreeView_Resize(object sender, EventArgs e)
        {
            this.refreshControls();
        }

        //a file in the tree view control has been selected

        private void F5mdi4_treeView_currentGroupFiles_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            this.openFile();
        }

        //event after a node was selected
        private void F5mdi4_treeView_currentGroupFiles_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        //create a new file
        private void F5mdi4_button_createNewFile_Click(object sender, EventArgs e)
        {
            try
            {
                //display the create new file form (F7 form)
                F7_CreateNewFile f7_createNewFile = new F7_CreateNewFile(this.f5_containerForm);

                f7_createNewFile.ShowDialog();

                f7_createNewFile.Close();

                //re-load group files
                this.loadGroupFiles();

                //expand all tree nodes
                this.F5mdi4_treeView_currentGroupFiles.ExpandAll();

            }
            catch(Exception exception) 
            {
                Utility.DisplayError("Groups_failed_to_create_new_file", exception, "Group: Failed to add a new file from the Tree view MDI window: " + exception.ToString(), false);
            }
        }


        //delete file event handler
        private void F5mdi4_DeleteFile(object sender, EventArgs e)
        {
            try
            {
                if (this.F5mdi4_treeView_currentGroupFiles.SelectedNode == null)
                    return;


                if (this.F5mdi4_treeView_currentGroupFiles.Nodes[0].Nodes.Contains(this.F5mdi4_treeView_currentGroupFiles.SelectedNode)) //local file
                {

                    //local file

                    //get the file path
                    string filepath = this.F5mdi4_treeView_currentGroupFiles.SelectedNode.Name;

                    //display a deletion confirmation dialog box
                    DialogResult deleteFile = MessageBox.Show(Utility.displayMessage("F5mdi4_delete_file_confirmation"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (deleteFile == DialogResult.Yes) //delete file
                    {
                        File.Delete(filepath);

                        //refresh the tree view control (re-load group files)
                        this.loadGroupFiles();

                        //expand all Tree view nodes
                        this.F5mdi4_treeView_currentGroupFiles.ExpandAll();
                    }
                }else if (this.F5mdi4_treeView_currentGroupFiles.Nodes[1].Nodes.Contains(this.F5mdi4_treeView_currentGroupFiles.SelectedNode)) //database table
                {
                    //delete file from the _DatabaseFiles table
                    SqlCommand deleteTable = Utility.getSqlCommand("DELETE FROM "+Utility.currentGroup.getName()+"_DatabaseFiles WHERE filename='"+this.F5mdi4_treeView_currentGroupFiles.SelectedNode.Name + "'");
                    deleteTable.ExecuteNonQuery();

                    //delete table
                    deleteTable = Utility.getDropTableSqlCommand(Utility.currentGroup.getName() + "_" + this.F5mdi4_treeView_currentGroupFiles.SelectedNode.Name);

                    deleteTable.ExecuteNonQuery();

                    deleteTable.Dispose();
                }
            }
            catch(Exception exception)
            {
                Utility.DisplayError("FileTreeView_could_not_delete_file",exception,"FileTreeView: Failed to delete a file: \n"+exception.ToString(),false);
            }
        }

        //re-load group files into the Tree view control
        private void F5mdi4_button_reloadFiles_Click(object sender, EventArgs e)
        {
            this.loadGroupFiles();

            //expand all of the nodes or the Tree view control
            this.F5mdi4_treeView_currentGroupFiles.ExpandAll();
        }


    }
}
