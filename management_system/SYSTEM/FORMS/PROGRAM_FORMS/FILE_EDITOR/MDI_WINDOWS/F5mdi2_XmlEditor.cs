using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace management_system.SYSTEM.FORMS.PROGRAM_FORMS
{
    public partial class F5mdi2_XmlEditor : Form
    {
        //VARIABLES
        private F5_FileEditorForm f5_containerForm = null;
        private GeneralFile file = null; //current file

        //XML editor
        XmlDocument xml = new XmlDocument();

        private string newNodeString = "#";

        private int editMode = 0; /* edit mode:
                                     0 - XML tree view
                                     1 - text box
                                   */
        private int maxEditModes = 2; //the number of edit modes currently implemented

        //CONSTRUCTORS
        public F5mdi2_XmlEditor(F5_FileEditorForm f5_containerForm, GeneralFile file)
        {
            InitializeComponent();

            //container form
            this.MdiParent = f5_containerForm;
            this.f5_containerForm = f5_containerForm;

            //editor settings
            this.F5mdi2_treeView_xmlEditor.LabelEdit = true; //allow the user to edit the text from this control
            this.F5mdi2_treeView_xmlEditor.Font = this.Font; //set the editor font to the font of the form
            this.F5mdi2_treeView_xmlEditor.Font = new Font(this.Font.FontFamily,Utility.defaultPointTextSize); //set the text size to the default point size
            this.F5mdi2_treeView_xmlEditor.Nodes.Add(Utility.rootXmlNode); //default (mandatory for the program) XML root node
        }

        //UTILITY FUNCTIONS

        //recursively parse the tree view to load them into memory
        /*
        private void parseTree(TreeNode root, XmlNode memoryRoot)
        {
            foreach(TreeNode node in root.Nodes)
            {
                parseTree(node,memoryRoot); //parse the sub-nodes of this node

                memoryRoot.AppendChild(node); //append node
            }
        }

        //store the data in the current editor into memory
        private void storeTreeDataIntoMemory()
        {
            //clear memory
            this.xml.RemoveChild(this.xml.DocumentElement);

            //load tree nodes into memory
            this.parseTree(this.F5mdi2_treeView_xmlEditor, this.xml.DocumentElement);
        }
        */
        //EVENT HANDLERS
        private void F5mdi2_XmlEditor_Load(object sender, EventArgs e)
        {
            //form settings
            this.MinimumSize = Utility.mdiEditorMinimumSize;
            
        }

        //add a new node in the tree view XML editor control
        private void F5mdi2_toolStripButton_addXmlElement_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.F5mdi2_treeView_xmlEditor.SelectedNode != null) //a node is already selected -> add the new node as a child of the currently selected node
                {
                    this.F5mdi2_treeView_xmlEditor.SelectedNode.Nodes.Add(this.newNodeString);
                    this.F5mdi2_treeView_xmlEditor.SelectedNode.Expand();
                    this.F5mdi2_treeView_xmlEditor.SelectedNode.LastNode.BeginEdit();
                }
                else //no node is selected -> add the new node at the end of the node list
                {
                    this.F5mdi2_treeView_xmlEditor.Nodes.Add(this.newNodeString);
                    this.F5mdi2_treeView_xmlEditor.Nodes[this.F5mdi2_treeView_xmlEditor.Nodes.Count - 1].Expand();
                    this.F5mdi2_treeView_xmlEditor.Nodes[this.F5mdi2_treeView_xmlEditor.Nodes.Count - 1].BeginEdit();
                }
            }catch (Exception exception)
            {
                Utility.DisplayWarning("XmlEditor_cannot_add_tree_node", exception, "Cannot add a new node in the tree control: " + exception.ToString(), false);
            }
        }

        //add or edit an XML node (name and attributes)
        private void F5mdi2_toolStripButton_addAttribute_Click(object sender, EventArgs e)
        {
            try
            {
                if(this.F5mdi2_treeView_xmlEditor.SelectedNode!=null) //try to edit the currently selected node
                {
                    this.F5mdi2_treeView_xmlEditor.SelectedNode.BeginEdit();
                }
                // else -> do not edit any tree node 
                

            }catch (Exception exception)
            {
                Utility.DisplayWarning("XmlEditor_cannot_edit_tree_node", exception, "Cannot edit the XML tree node", false);
            }
        }

        //change the way to edit the XML document: text box or tree view control
        private void F5mdi2_toolStripButton_editMode_Click(object sender, EventArgs e)
        {
            this.editMode++;

            if(this.editMode < this.maxEditModes)
            {
                this.editMode = 0;
            }

            switch(this.editMode) 
            {
                case 0: //XML tree view
                    //this.storeTreeDataIntoMemory();
                    
                    break;
                case 1: //text box
                    break;
                default: //error
                    break;
            }
        }
    }
}
