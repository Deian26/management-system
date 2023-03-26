using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace management_system
{
    /*  Class used to accurately store XML Node information ; it includes general XML information (name, content, child nodes).
     */
    internal class XMLNode
    {        
        //FIELDS
        private string name, //node name 
                       innerText; //inner text of the XML node
        private List<string> attributes = new List<string>(); //attributes and their values ("attribute1=value1")
        private List<XMLNode> childNodes = new List<XMLNode>(); //child nodes

        //CONSTRUCTORS
        //without inner text
        public XMLNode(string name, List<string> attributes)
        {
            this.name = name;
            this.attributes = attributes;
        }

        //with inner text
        public XMLNode(string name, string innerText, List<string> attributes)
        {
            this.name = name;
            this.innerText = innerText;
            this.attributes = attributes;

        }

        //no child nodes list and no inner text
        public XMLNode(string name)
        {
            this.name = name;
        }

        //no child nodes, but with inner text
        public XMLNode(string name, string innerText)
        {
            this.name = name;
            this.innerText = innerText;
        }

        //METHODS

        //SETTERS
        //set XML node name
        public void setName(string name)
        {
            this.name = name;
        }

        //set XML node inner text
        public void setInnerText(string innerText)
        {
            this.innerText = innerText;
        }

        //set all attributes to the specified attributes string array
        public void setAttributes(List<string> attributes)
        {
            this.attributes = attributes;
        }

        //set only one attributes, specified by index, to the given value
        public void setAttribute(int index, string value)
        {
            this.attributes[index] = value;
        }

        //set only one attribute, specified by name, to the given value
        public void setAttribute(string name, string value)
        {
            for (int i = 0; i<this.attributes.Count; i++)
            {
                if (attributes[i].StartsWith(name) == true) //attribute name found
                {
                    attributes[i] = name + "=" + value;
                    break;
                }
                    
            }
        }

        //append a child node to this XMLNode
        public void addChildNode(XMLNode node)
        {
            try
            {
                this.childNodes.Add(node);
            }catch(Exception exception)
            {
                Utility.DisplayWarning("XMLEditor_cannot_add_custom_node", exception, "Could not add XML node: " + exception.ToString(),false);
            }
        }

        //GETTERS
        
        //get name
        public string getName()
        {
            return this.name;
        }

        //get inner text
        public string getInnerText()
        {
            if (this.innerText==null || this.innerText=="") return null;
            return this.innerText;
        }

        //get all attributes 
        public List<string> getAttributes()
        {
            return this.attributes;
        }

        //get one attribute, by index
        public string getAttribute(int index)
        {
            return this.attributes[index];
        }

        //get the first attribute with the given name
        public string getAttribute(string name)
        {
            foreach(string attribute in attributes)
            {
                if(attribute.StartsWith(name)==true) //attribute name found
                {
                    return attribute;
                }
            }

            return null; //the attribute was not found
        }

        //get all child nodes
        public List<XMLNode> getChildNodes()
        {
            return this.childNodes;
        }

        //get one child node of the current XMLNode, by index
        public XMLNode getChildNode(int index)
        {
            try
            {
                return this.childNodes[index];
            }catch(Exception exception)
            {
                Utility.DisplayWarning("XMLEditor_cannot_get_custom_node",exception, "Could not get a node by index: " + exception.ToString(), false);
            }

            return null; //node could not be returned
        }

        //get the name of the current node and its attributes + their values, in XML node format (< + name + attributes + >)
        public string getNodeText()
        {
            string all_attributes = this.name;

            foreach (string attribute in this.attributes)
                all_attributes += " "+attribute;

            return "<"+all_attributes+">";
        }

        //get the node that marks the end of the content of the current node (</ + name + >)
        public string getEndNode()
        {
            return "</" + this.name + ">";
        }

        //gte the name and attributes, as represented in the tree view control
        public string getNodeTextTreeView()
        {
            string all_attributes = this.name;

            foreach (string attribute in this.attributes)
                all_attributes += " " + attribute;

            return all_attributes;
        }

        //OTHER METHDOS

        //remove the specified child node, by index
        public void removeChildNode(int index)
        {
            try
            {
                this.childNodes.RemoveAt(index);
            }catch(Exception exception)
            {
                Utility.DisplayWarning("XMLEditor_cannot_remove_custom_node", exception, "Could not get a node by index: " + exception.ToString(), false);
            }
        }

        //add attribute at the end of the attribute list
        public void addAttribute(string name, string value)
        {
            try
            {
                this.attributes.Add(name + "=" + value);
            }catch(Exception exception)
            {
                Utility.DisplayWarning("XMLEditor_cannot_add_custom_node_attribute",exception, "Could not add an atribute to an XML node: " + exception.ToString(), false);
            }
        }

        //remove all child nodes
        public void removeChildNodes()
        {
            this.childNodes.Clear();
        }

    }
}
