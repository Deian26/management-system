using management_system.SYSTEM.CLASSES.FILES.FILE_TYPES;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml;
using System.Xml.Linq;

namespace management_system
{

    public partial class F5mdi2_XmlEditor : Form
    {
        //VARIABLES
        private F5_FileEditorForm f5_containerForm = null;
        private GeneralFile file = null; //current file

        //control appearance
        private int AbsoluteEditorControlWidthSubtract = 95;//points
        private int AbsoluteEditorControlHeightSubtract = 100; //points

        //search text box
        private RichTextBoxFinds searchOptions = RichTextBoxFinds.None;
        private Color highlightColour = Color.GreenYellow;

        //XML editor
        XMLNode xml = new XMLNode(Utility.rootXmlNode.Name);

        private string newNodeString = "NewNode";

        private int editMode = 0; /* edit mode:
                                     0 - XML tree view
                                     1 - text box
                                   */
        private int maxEditModes = 2; //the number of edit modes currently implemented

        private Color xmlTextBoxDefaultColour = Color.Black;

        //edit modes
        private enum EditMode { 
            treeView = 0, //tree view control
            text = 1 //text box control
            };

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
            this.F5mdi2_richTextBox_xmlEditor.Font = new Font(Utility.defaultFontFamily, Utility.defaultPointTextSize); //set the text box font
            this.F5mdi2_treeView_xmlEditor.ShowNodeToolTips = true; //display tool tip texts when the mouse hovers over a tree view node

            //keep the Tree view and text box controlw deactivated until the user clicks either one (depending on which control is currently visible)
            this.F5mdi2_treeView_xmlEditor.Enabled = false; //deactivate the Tree view control
            this.F5mdi2_richTextBox_xmlEditor.Enabled = false; //deactivate the text box control

            //hide the text box
            this.F5mdi2_richTextBox_xmlEditor.Visible = false;
        }

        //UTILITY FUNCTIONS

        //stores the information contained in the given TreeNode as an XMLNode (according to the conventions for attributes: attribute1=value1,attribute2=value2 etc.)
        private XMLNode treeNodeToXMLNode(TreeNode source)
        {
            try
            {
                string[] attributes = source.Text.Split(' '); //if the name contains ',' => the node has attributes => add the name and the attributes to the XMLNode structure
                                                              //if the name does not contain ',', then only add the name to the XMLNode structure

                List<string> all_attributes = new List<string>();

                //if (source.Nodes.Count != 0) return new XMLNode(""); //return an empty XMLNode if the current node has no nodes

                //get the attributes
                for (int i = 1; i < attributes.Length; i++)
                    all_attributes.Add(attributes[i]);

                XMLNode destination = new XMLNode(attributes[0], source.Name, all_attributes);
                return destination;
            }catch (Exception exception)
            {
                Utility.logDiagnsoticEntry("Could not load Tree Node into memory: "+exception.ToString());
                return new XMLNode("");
            }
        }

        //recursively parse the tree view to load them into memory
        private void parseTree(TreeNode root, XMLNode memoryRoot)
        {
            try
            {

                for (int i = 0; i < root.Nodes.Count; i++)
                {
                    memoryRoot.addChildNode(this.treeNodeToXMLNode(root.Nodes[i])); //add the child nodes of the current node
                    parseTree(root.Nodes[i],memoryRoot.getChildNode(i)); //repeat the parsing for each child node
                    
                }
                
            }
            catch (Exception exception)
            {
                Utility.DisplayWarning("XMLEditor_cannot_save_tree", exception, "Failed to store the XML tree view structure in memory: " + exception.ToString(), false);
            }
        }

        //store the data in the current editor into memory
        private void storeTreeDataIntoMemory()
        {
            //clear memory
            this.xml.removeChildNodes();

            //load mandatory root element into memory
            if(this.F5mdi2_treeView_xmlEditor.Nodes != null) this.xml = this.treeNodeToXMLNode(this.F5mdi2_treeView_xmlEditor.Nodes[0]);

            //load tree nodes into memory
            foreach(TreeNode node in this.F5mdi2_treeView_xmlEditor.Nodes)
                this.parseTree(node, this.xml);
        }

        //load the XML data stored in memory into the text box XML editor
        private void loadXmlDataIntoTextbox(XMLNode root, int indent)
        {
            for (int i = 0; i < indent; i++)
                this.F5mdi2_richTextBox_xmlEditor.AppendText("\t"); //add tabs to indent the current node

            //determine the colour of the current node
            Random rnd = new Random();
            Color colour = Color.Black;

            try
            {
                do
                {
                    colour = Color.FromArgb(rnd.Next());
                } while (colour.Equals(Color.GreenYellow));

            }catch (Exception exception)
            {
                Utility.logDiagnsoticEntry("Could not change the colour of an XML node: " + exception.ToString());
                colour = this.xmlTextBoxDefaultColour; //set the colour of the current XML node to a default value if the colour determination fails 
            }

            int start = this.F5mdi2_richTextBox_xmlEditor.Text.Length;
            this.F5mdi2_richTextBox_xmlEditor.AppendText(root.getNodeText()); //< + name + all attributes + > as a string
            if (root.getInnerText() == null || root.getInnerText() == "") this.F5mdi2_richTextBox_xmlEditor.AppendText("\n"); //if the current node does not have inner text, add an endline after the start node
            this.F5mdi2_richTextBox_xmlEditor.Select(start,this.F5mdi2_richTextBox_xmlEditor.Text.Length-start);
            this.F5mdi2_richTextBox_xmlEditor.SelectionColor = colour;
            this.F5mdi2_richTextBox_xmlEditor.SelectionFont = new Font(this.F5mdi2_richTextBox_xmlEditor.Font,FontStyle.Bold);

            if(root.getChildNodes().Count!=0) //the current node has child nodes
                foreach (XMLNode node in root.getChildNodes())
                {
                    this.loadXmlDataIntoTextbox(node, indent+1);   //parse the child nodes list of the current node
                }
            else //the current node does not have child nodes -> check if the current node has inner text
            {
                if(root.getInnerText()!=null) //the current node contains inner text
                {
                    this.F5mdi2_richTextBox_xmlEditor.AppendText(root.getInnerText());
                }
            }

            

            start = this.F5mdi2_richTextBox_xmlEditor.Text.Length;
            if (root.getInnerText() == null || root.getInnerText() == "")
            {

                this.F5mdi2_richTextBox_xmlEditor.AppendText("\n"); //if the current node does not have inner text, add endline before the end node 

                for (int i = 0; i < indent; i++)
                    this.F5mdi2_richTextBox_xmlEditor.AppendText("\t"); //add tabs to indent the current end node
            }
            
            this.F5mdi2_richTextBox_xmlEditor.AppendText(root.getEndNode() + "\n\n");
            this.F5mdi2_richTextBox_xmlEditor.Select(start, this.F5mdi2_richTextBox_xmlEditor.Text.Length - start);
            this.F5mdi2_richTextBox_xmlEditor.SelectionColor = colour;
            this.F5mdi2_richTextBox_xmlEditor.SelectionFont = new Font(this.F5mdi2_richTextBox_xmlEditor.Font, FontStyle.Bold);
            this.F5mdi2_richTextBox_xmlEditor.DeselectAll();

        }

        //store XML data into the tree view control
        private void loadXmlDataIntoTreeView(XMLNode root)
        {
            this.F5mdi2_treeView_xmlEditor.Nodes.Clear(); //remove nodes

            this.F5mdi2_treeView_xmlEditor.Nodes.Add(root.getInnerText(), root.getNodeTextTreeView()); //add the first node (root)
            this.F5mdi2_treeView_xmlEditor.Nodes[0].ToolTipText = this.F5mdi2_treeView_xmlEditor.Nodes[0].Name; //add the inner text to the tooltip text

            this.parseXmlData(this.xml,this.F5mdi2_treeView_xmlEditor.Nodes[0]);
        }

        //load XML data stored in memory into the tree view XML editor
        private void parseXmlData(XMLNode root, TreeNode treeNode)
        {

            for(int i = 0; i<root.getChildNodes().Count;i++)
            {
                treeNode.Nodes.Add(root.getChildNode(i).getInnerText(), root.getChildNode(i).getNodeTextTreeView());
                treeNode.LastNode.ToolTipText = treeNode.LastNode.Name;
                this.parseXmlData(root.getChildNode(i),treeNode.Nodes[i]);
            }
        }

        //convert the given XmlNode into an XMLNode
        private XMLNode xmlNodeToXMLNode(XmlNode source)
        {
            try
            {
                List<string> all_attributes = new List<string>();

                if (source.NodeType.Equals(XmlNodeType.Element)) //XML node (element)
                {
                    //get the attributes
                    foreach (XmlAttribute attribute in source.Attributes)
                        all_attributes.Add(attribute.Name + "=\"" + attribute.Value + "\"");

                    XMLNode destination = new XMLNode(source.Name, all_attributes);
                    return destination;
                }
                
                return new XMLNode(""); //return an empty XMLNode
            }catch (Exception exception)
            {
                Utility.logDiagnsoticEntry("Could not convert an XmlNode to an XMLNode: " + exception.ToString());
                return new XMLNode("");
            }
        }

        //parse the given XmlDocument and store the data in the specified XMLNode vaiable
        private void parseXmlDocument(XmlNode root, XMLNode memoryRoot)
        {
            try
            {

                for (int i = 0; i < root.ChildNodes.Count; i++)
                {

                    if (root.ChildNodes[i].NodeType.Equals(XmlNodeType.Element))
                    {
                        memoryRoot.addChildNode(this.xmlNodeToXMLNode(root.ChildNodes[i])); //add the child nodes of the current node
                        parseXmlDocument(root.ChildNodes[i], memoryRoot.getChildNode(i)); //repeat the parsing for each child node
                    }
                    else if (root.ChildNodes[i].NodeType.Equals(XmlNodeType.Text)) memoryRoot.setInnerText(root.ChildNodes[i].Value); //add the inner text to the current XMLNode
                     

                }

            }
            catch (Exception exception)
            {
                Utility.DisplayWarning("XMLEditor_cannot_load_textbox_XML_string_into_memory", exception, "Could not load the textbox XML string into memory: " + exception.ToString(), false);
            }
        }

        //store the XML data from the text box into memory
        private void storeTextBoxXmlDataIntoMemory()
        {
            try
            {
                XmlDocument xml_textbox = new XmlDocument();

                //load XML data into a local XmlDocument element
                xml_textbox.LoadXml(this.F5mdi2_richTextBox_xmlEditor.Text);

                //clear the current XML structure from memory
                this.xml.removeChildNodes();

                if (xml_textbox.ChildNodes.Count != 0) //the structure contains at least 1 node
                {
                    this.xml = this.xmlNodeToXMLNode(xml_textbox.ChildNodes[0]); //load the root node

                    //store the XML data in the global 'xml' variable
                    this.parseXmlDocument(xml_textbox.ChildNodes[0], this.xml);
                }

            }catch (Exception exception)
            {
                Utility.DisplayWarning("XMLEditor_cannot_load_textbox_XML_string_into_memory", exception,"Could not load the textbox XML string into memory: "+exception.ToString(),false);
            }
        }

        //refresh control appearance (resize, move)
        private void refreshControlsAppearance()
        {
            //controls width
            this.F5mdi2_treeView_xmlEditor.Width = this.Width - this.AbsoluteEditorControlWidthSubtract;
            this.F5mdi2_richTextBox_xmlEditor.Width = this.Width - this.AbsoluteEditorControlWidthSubtract;

            //control height
            this.F5mdi2_treeView_xmlEditor.Height = this.Height - this.AbsoluteEditorControlHeightSubtract;
            this.F5mdi2_richTextBox_xmlEditor.Height = this.Height - this.AbsoluteEditorControlHeightSubtract;
        }

        //activates or deactivates the XML editor buttons
        private void xmlEditorButtons(bool activated)
        {
            if(activated==true) //activate buttons
            {
                this.F5mdi2_toolStripButton_addXmlElement.Enabled = true;
                this.F5mdi2_toolStripButton_addAttribute.Enabled = true;
                this.F5mdi2_toolStripButton_removeXmlElement.Enabled = true;
            }
            else //deactivate buttons
            {
                this.F5mdi2_toolStripButton_addXmlElement.Enabled = false;
                this.F5mdi2_toolStripButton_addAttribute.Enabled = false;
                this.F5mdi2_toolStripButton_removeXmlElement.Enabled = false;
            }
        }


        //search a word in the text box - RECURSIVE FUNCTION
        private bool searchPhraseTextBox(string phrase, int startPosition)
        {
            //determine the search options
            this.searchOptions = RichTextBoxFinds.None; //default value

            if (this.F5mdi2_toolStripMenuItem_matchCaseCheckbox.Checked) //match case
                this.searchOptions |= RichTextBoxFinds.MatchCase;
            if (this.F5mdi2_toolStripMenuItem_reverseCheckbox.Checked) //reverse search
                this.searchOptions |= RichTextBoxFinds.Reverse;
            if (this.F5mdi2_toolStripMenuItem_wholeWordCheckbox.Checked) //match whole word
                this.searchOptions |= RichTextBoxFinds.WholeWord;

            int position = this.F5mdi2_richTextBox_xmlEditor.Find(phrase, startPosition, this.searchOptions); //returns -1 if the phrase not found; otherwise, returns the position of the first string found

            if (position == -1) //phrase not found this iteration => stop search 
            {
                return false;
            }
            else //phrase found => search again for the next appearance
            {
                //highlight the phrase found in this iteration
                this.F5mdi2_richTextBox_xmlEditor.Select(position, phrase.Length);
                this.F5mdi2_richTextBox_xmlEditor.SelectionBackColor = this.highlightColour;

                //start the search again
                return searchPhraseTextBox(phrase, position + 1) | true;
            }
        }

        //search the phrase in the tree view control
        private void searchPhraseTreeView(string phrase, TreeNode root)
        {
            //this node has child nodes => search the phrase in each node
            foreach(TreeNode node in root.Nodes)
            {
                this.searchPhraseTreeView(phrase, node);
            }

            //the node does not have child node => check if this node contains the searched phrase
            if(root.Text.Contains(phrase)==true)
            {
                root.Expand();
                root.BeginEdit();

            }
        }

        //load a locally stored XML file into the form
        private void loadXmlFile(string path)
        {
            try
            {
                //load XML file into memory
                XmlDocument xml_doc = new XmlDocument();
                xml_doc.Load(path);
                XmlNode root = xml_doc.DocumentElement;

                //display the contents of the file according to the current edit mode
                switch (this.editMode)
                {
                    case (int)F5mdi2_XmlEditor.EditMode.text: //text box
                        this.loadXmlDataIntoTreeView(this.xml);
                        break;

                    case (int)F5mdi2_XmlEditor.EditMode.treeView: //tree view control
                        this.loadXmlDataIntoTextbox(this.xml, 0);
                        break;

                    default:
                        break;
                }


            }
            catch (Exception exception)
            {
                Utility.DisplayError("XMLEditor_cannot_load_local_XML_file", exception, "XmlEditor: Cannot load the locally stored XML file: " + exception.ToString(), false);
            }
        }

        //EVENT HANDLERS
        private void F5mdi2_XmlEditor_Load(object sender, EventArgs e)
        {
            //form settings
            this.MinimumSize = Utility.mdiEditorMinimumSize;

            this.F5mdi2_treeView_xmlEditor.Enabled = true;

            //control appearance
            this.refreshControlsAppearance();

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
                else //no node is selected -> add the new node at the end of the node list of the root node
                {
                    this.F5mdi2_treeView_xmlEditor.Nodes[0].Nodes.Add(this.newNodeString);
                    this.F5mdi2_treeView_xmlEditor.Nodes[0].Expand();
                    this.F5mdi2_treeView_xmlEditor.Nodes[0].Nodes[this.F5mdi2_treeView_xmlEditor.Nodes[0].Nodes.Count - 1].BeginEdit();
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
            //store the XML data contained in the tree into memory
            switch (this.editMode)
            {
                case (int)F5mdi2_XmlEditor.EditMode.treeView: //XML tree view

                    this.storeTreeDataIntoMemory(); //store XML data from the tree view control into memory

                    break;

                case (int)F5mdi2_XmlEditor.EditMode.text: //text box

                    this.storeTextBoxXmlDataIntoMemory(); //store the XML data from the tetxbox into memory

                    break;

                default: //invalid edit mode
                    break;
            }

            this.editMode++; //change edit mode

            if (this.editMode >= this.maxEditModes)
            {
                this.editMode = 0;
            }

            switch (this.editMode)
            {
                case (int)F5mdi2_XmlEditor.EditMode.treeView: //XML tree view
                    //activate XML tree view buttons
                    this.xmlEditorButtons(true);

                    this.F5mdi2_richTextBox_xmlEditor.Enabled = false; //deactivate the text box control

                    this.F5mdi2_treeView_xmlEditor.Enabled = true; //enable the tree view control

                    this.F5mdi2_toolStripButton_editMode.Text = "XML"; //display current edit mode (button label)

                    this.F5mdi2_richTextBox_xmlEditor.Visible = false; //hide text box control

                    this.F5mdi2_treeView_xmlEditor.Nodes.Clear(); //clear tree view control

                    this.loadXmlDataIntoTreeView(this.xml); //load XML data in the treeview

                    this.F5mdi2_treeView_xmlEditor.Visible = true; //display tree view control

                    break;

                case (int)F5mdi2_XmlEditor.EditMode.text: //text box

                    //deactivate XML tree view buttons
                    this.xmlEditorButtons(false);

                    this.F5mdi2_treeView_xmlEditor.Enabled = false; //deactivate the tree view control

                    this.F5mdi2_richTextBox_xmlEditor.Enabled = true; //enable the text box control

                    this.F5mdi2_toolStripButton_editMode.Text = "TXT"; //display current edit mode (button label)

                    this.F5mdi2_treeView_xmlEditor.Visible = false;//hide tree view control

                    this.F5mdi2_richTextBox_xmlEditor.Clear(); //clear text box

                    this.loadXmlDataIntoTextbox(this.xml,0); //load the XML data from memory into the text box control

                    this.F5mdi2_richTextBox_xmlEditor.Visible = true; //display the text box control

                    break;

                default: //invalid edit mode
                    break;
            }
        }

        //tool strip click
        private void F5mdi2_toolStrip_xmlTools_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        //mouse hover over a node
        private void F5mdi2_treeView_xmlEditor_NodeMouseHover(object sender, TreeNodeMouseHoverEventArgs e)
        {
            //display the inner text of the node as a tooltip
            try
            {
                e.Node.ToolTipText = e.Node.Name;
                
            }catch (Exception exception)
            {
                Utility.logDiagnsoticEntry("Could not load inner text from a Tree View node: " + exception.ToString());
            }
        }

        //tree view selection changed
        private void F5mdi2_treeView_xmlEditor_AfterSelect(object sender, TreeViewEventArgs e)
        {
            
        }

        //text box text changed
        private void F5mdi2_richTextBox_xmlEditor_TextChanged(object sender, EventArgs e)
        {
            
        }

        //form is being resized => resize controls
        private void F5mdi2_XmlEditor_Resize(object sender, EventArgs e)
        {
            this.refreshControlsAppearance(); //resize editor controls
        }

        private void F5mdi2_toolStripButton_startSearch_Click(object sender, EventArgs e)
        {
            switch (this.editMode) //search the text depending on the edit mode currently selected
            {
                case (int)F5mdi2_XmlEditor.EditMode.treeView: //XML tree view
                    this.searchPhraseTreeView(this.F5mdi2_toolStripTextBox_searchTextbox.Text, this.F5mdi2_treeView_xmlEditor.Nodes[0]);
                    break;

                case (int)F5mdi2_XmlEditor.EditMode.text: //Text box
                    
                    this.searchPhraseTextBox(this.F5mdi2_toolStripTextBox_searchTextbox.Text, 0);
                    break;

                default:
                    break;
            }


        }

        //delete the selected node and its nodes
        private void F5mdi2_toolStripButton_addXmlElement_Click_1(object sender, EventArgs e)
        {
            try     
            {
                if (this.editMode == (int)F5mdi2_XmlEditor.EditMode.treeView && this.F5mdi2_treeView_xmlEditor.SelectedNode != null && this.F5mdi2_treeView_xmlEditor.SelectedNode!= this.F5mdi2_treeView_xmlEditor.Nodes[0])
                    this.F5mdi2_treeView_xmlEditor.SelectedNode.Remove();
            }catch(Exception exception) 
            {
                Utility.DisplayError("XMLEditor_cannot_remove_custom_node", exception, "XmlEditor: Cannot remove an XML node from the treeview editor: " + exception.ToString(), false);
            }
        }

        //replace all words that match the text in the search text box with the text from the 'Replace all' text box
        private void F5mdi2_toolStripMenuItem_replaceAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.F5mdi2_toolStripTextBox_searchTextbox.Text != "" && this.F5mdi2_toolStripTextBox_replaceTextBox.Text != "")
                    this.F5mdi2_richTextBox_xmlEditor.Text = this.F5mdi2_richTextBox_xmlEditor.Text.Replace(this.F5mdi2_toolStripTextBox_searchTextbox.Text, this.F5mdi2_toolStripTextBox_replaceTextBox.Text);
            }
            catch (Exception exception)
            {
                Utility.DisplayError("TextEditor_cannot_replace_phrase", exception, "Could not replace the text from the search box:" + this.F5mdi2_toolStripTextBox_searchTextbox.Text.ToString() + " with the text from the 'Replace all' textbox: " + this.F5mdi2_toolStripTextBox_replaceTextBox.Text.ToString() + " : " + exception.ToString(), false);
                return;
            }

            //replace successful
            MessageBox.Show(Utility.displayMessage("Replace_text_successful"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //save XML file into the currently connected database
        private void F5mdi2_toolStripButton_save_Click(object sender, EventArgs e)
        {
            //upload the file into the database
            Utility.uploadFileIntoDB(this.file.getFilePath());
        }

        //open locally stored XML file
        private void F5mdi2_toolStripButton_openFile_Click(object sender, EventArgs e)
        {
            try
            {
                //display the open file dialog
                DialogResult result = this.F5mdi2_openFileDialog_openLocalXmlFile.ShowDialog();

                if (!result.Equals(DialogResult.OK)) //quit
                    return;

                //load file
                this.loadXmlFile(this.F5mdi2_openFileDialog_openLocalXmlFile.FileName);

            }catch (Exception exception)
            {
                Utility.DisplayError("XMLEditor_cannot_load_local_XML_file", exception, "XmlEditor: Cannot load the locally stored XML file: "+exception.ToString(), false);
            }
        }

        //locally save the XML file
        private void F5mdi2_toolStripMenuItem_saveAsButton_Click(object sender, EventArgs e)
        {
            try
            {
                int currentEditMode = this.editMode; //to be restored after saving the XML to a local file

                //if the edit mode is not already Text, switch to text mode
                if(!this.editMode.Equals((int)F5mdi2_XmlEditor.EditMode.text))
                {
                    this.editMode = (int)F5mdi2_XmlEditor.EditMode.text; //change edit mode to Text
                    
                    //deactivate XML tree view buttons
                    this.xmlEditorButtons(false);

                    this.F5mdi2_treeView_xmlEditor.Enabled = false; //deactivate the tree view control

                    this.F5mdi2_richTextBox_xmlEditor.Enabled = true; //enable the text box control

                    this.F5mdi2_toolStripButton_editMode.Text = "TXT"; //display current edit mode (button label)

                    this.F5mdi2_treeView_xmlEditor.Visible = false;//hide tree view control

                    this.F5mdi2_richTextBox_xmlEditor.Clear(); //clear text box

                    this.loadXmlDataIntoTextbox(this.xml, 0); //load the XML data from memory into the text box control

                    this.F5mdi2_richTextBox_xmlEditor.Visible = true; //display the text box control
                }

                //set save file dialog file filter to only allow XML files to be saved
                this.F5mdi2_saveFileDialog_newXmlFile.Filter = "Extensible Markup Language | *.xml";

                //open the save file dialog
                DialogResult result = this.F5mdi2_saveFileDialog_newXmlFile.ShowDialog();

                if (!result.Equals(DialogResult.OK)) //quit
                    return;

                //get file path from the save file editor
                string path = this.F5mdi2_saveFileDialog_newXmlFile.FileName;

                //save the text to the specified file
                this.F5mdi2_richTextBox_xmlEditor.SaveFile(path);

                //revert to the previously selected edit mode - only supported for XML and TEXT modes at the moment
                //activate XML tree view buttons
                this.xmlEditorButtons(true);

                this.F5mdi2_richTextBox_xmlEditor.Enabled = false; //deactivate the text box control

                this.F5mdi2_treeView_xmlEditor.Enabled = true; //enable the tree view control

                this.F5mdi2_toolStripButton_editMode.Text = "XML"; //display current edit mode (button label)

                this.F5mdi2_richTextBox_xmlEditor.Visible = false; //hide text box control

                this.F5mdi2_treeView_xmlEditor.Nodes.Clear(); //clear tree view control

                this.loadXmlDataIntoTreeView(this.xml); //load XML data in the treeview

                this.F5mdi2_treeView_xmlEditor.Visible = true; //display tree view control
            }
            catch (Exception exception)
            {
                Utility.DisplayError("XMLEditor_cannot_save_local_XML_file", exception,"XmlEditor: Could not locally save the XML file: "+exception.ToString(),false);
            }
        }
    }
}
