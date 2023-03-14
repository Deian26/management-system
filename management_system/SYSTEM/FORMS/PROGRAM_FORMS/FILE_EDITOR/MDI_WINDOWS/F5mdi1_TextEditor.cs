using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace management_system
{
    /* MDI window
        - text file editor
     */
    public partial class F5mdi1_TextEditor : Form
    {
        //VARIABLES
        private F5_FileEditorForm f5_containerForm = null;

        //file
        private GeneralFile file = null;

        //control appearance
        private int widthOffset = 40;
        private int heightOffset = 90;

        //text editting
        private Color highlightColour = Color.GreenYellow;
        private RichTextBoxFinds searchOptions = RichTextBoxFinds.None;

        //CONSTRUCTORS
        public F5mdi1_TextEditor(F5_FileEditorForm f5_containerForm, GeneralFile file)
        {
            InitializeComponent();

            //store the file
            this.file = file;

            //container form
            this.MdiParent= f5_containerForm;
            this.f5_containerForm = f5_containerForm;

            //initialize text editor textbox
            this.F5mdi1_richTextBox_textEditor.AcceptsTab = true;
            //this.F5mdi1_richTextBox_textEditor.WordWrap = true;
            this.F5mdi1_richTextBox_textEditor.AutoWordSelection= true; 
            //this.F5mdi1_richTextBox_textEditor.AllowDrop= true;
            this.F5mdi1_richTextBox_textEditor.EnableAutoDragDrop= true;
            this.F5mdi1_richTextBox_textEditor.ShowSelectionMargin = true;
            this.F5mdi1_richTextBox_textEditor.ShortcutsEnabled = true;

            //shortcuts
            ContextMenu textboxShortcuts = new ContextMenu();
            MenuItem shortcut;

            //Ctrl+B -> Bold
            shortcut = new MenuItem(
                "Bold",
                new System.EventHandler(this.onShortcut_bold),
                Shortcut.CtrlB
                );
            textboxShortcuts.MenuItems.Add(shortcut);

            //Ctrl+I -> Italic
            shortcut = new MenuItem(
                "Italic",
                new System.EventHandler(this.onShortcut_italic),
                Shortcut.CtrlI
                );
            textboxShortcuts.MenuItems.Add(shortcut);

            //Ctrl+U -> Underlined
            shortcut = new MenuItem(
                "Underline",
                new System.EventHandler(this.onShortcut_underline),
                Shortcut.CtrlU
                );
            textboxShortcuts.MenuItems.Add(shortcut);

            //Ctrl+T -> Strikethrough
            shortcut = new MenuItem(
                "Strikethrough",
                new System.EventHandler(this.onShortcut_strikethrough),
                Shortcut.CtrlT
                );
            textboxShortcuts.MenuItems.Add(shortcut);

            //Alt+UpArrow -> Increase text size
            shortcut = new MenuItem(
                "Increase text size",
                new System.EventHandler(this.onShortcut_increaseTextSize),
                Shortcut.AltUpArrow
                );
            textboxShortcuts.MenuItems.Add(shortcut);

            //Alt+DownArrow -> Increase text size
            shortcut = new MenuItem(
                "Decrease text size",
                new System.EventHandler(this.onShortcut_decreaseTextSize),
                Shortcut.AltDownArrow
                );
            textboxShortcuts.MenuItems.Add(shortcut);

            //Ctrl+S -> save text to file
            shortcut = new MenuItem(
                "Save",
                new System.EventHandler(this.onShortcut_saveText),
                Shortcut.CtrlS
                );
            textboxShortcuts.MenuItems.Add(shortcut);

            //add the shortcuts to the text editor
            this.F5mdi1_richTextBox_textEditor.ContextMenu = textboxShortcuts;
            
        }

        

        //UTILITY FUNCTIONS

        //refresh the appearance of controls in this form
        private void refreshControlsAppearance()
        {
            //text editor
            this.F5mdi1_richTextBox_textEditor.Size = new Size(this.Size.Width-this.widthOffset, this.Size.Height - this.heightOffset);
        }

        //search a word in the text box - RECURSIVE FUNCTION
        private bool searchPhrase(string phrase, int startPosition)
        {
            //determine the search options
            this.searchOptions = RichTextBoxFinds.None; //default value

            if (this.F5mdi1_toolStripMenuItem_matchCaseCheckbox.Checked) //match case
                this.searchOptions |= RichTextBoxFinds.MatchCase;
            if (this.F5mdi1_toolStripMenuItem_matchCaseCheckbox.Checked) //reverse search
                this.searchOptions |= RichTextBoxFinds.Reverse;
            if (this.F5mdi1_toolStripMenuItem_matchCaseCheckbox.Checked) //match whole word
                this.searchOptions |= RichTextBoxFinds.WholeWord;

            int position = this.F5mdi1_richTextBox_textEditor.Find(phrase,startPosition,this.searchOptions); //returns -1 if the phrase not found; otherwise, returns the position of the first string found

            if(position == -1) //phrase not found this iteration => stop search 
            {
                return false;
            }else //phrase found => search again for the next appearance
            {
                //highlight the phrase found in this iteration
                this.F5mdi1_richTextBox_textEditor.Select(position, phrase.Length); 
                this.F5mdi1_richTextBox_textEditor.SelectionBackColor = this.highlightColour;

                //start the search again
                return searchPhrase(phrase, position+1) | true; 
            }
        }

        //count the words in the text editor (separater by whitespace)
        private long countWords()
        {
            long count = 0;
            bool character=false;
            char chr = (char)0x00;

            try
            {
                foreach (char c in this.F5mdi1_richTextBox_textEditor.Text)
                {
                    chr = c;
                    if (Utility.recognizedWhitespaces.Contains(c) == true) //recognized whitespace encountered
                    {
                        if(character == true)
                        {
                            count++;
                            character = false;
                        }
                    }
                    else
                    {
                        character = true;
                    }
                }

                //return number of whitespaces

                return count;
            }catch(Exception exception) 
            {
                Utility.DisplayWarning("TextEditor_unrecognized_character_found_in_text"+"; "+chr.ToString(), exception, null, false);
            }

            return -1; //error parsing the text
        }

        //save the currently open file
        private void saveFile()
        {
            if (((this.F5mdi1_richTextBox_textEditor.Font.Style & FontStyle.Underline) != 0) || ((this.F5mdi1_richTextBox_textEditor.Font.Style & FontStyle.Strikeout) != 0)) //save to a .txt file 
            {
                if (this.file.isDbFile() == true) //database file (in the current database)
                {
                    //DEV
                }else  //local file
                {
                    try
                    {
                        File.WriteAllText(this.file.getFilePath(), this.F5mdi1_richTextBox_textEditor.Text + ".txt"); //write to the specified file (overwrite it if it already exists)

                    }
                    catch (Exception exception)
                    {
                        Utility.DisplayWarning("TextEditor_cannot_save_file" + this.file.getFilePath().ToString(), exception, "Cannot save .txt file at: " + this.file.getFilePath().ToString() + "; " + exception.ToString(), false);
                    }
                }
            }
            else //save to an .rtf file
            {
                if (this.file.isDbFile() == true) //database file (in the current database)
                {
                    //DEV
                }else //local file
                {
                    try
                    {
                        File.WriteAllText(this.file.getFilePath(), this.F5mdi1_richTextBox_textEditor.Text + ".rtf"); //write to the specified file (overwrite it if it already exists); Rich Text Format fileExtension

                    }
                    catch (Exception exception)
                    {
                        Utility.DisplayWarning("TextEditor_cannot_save_file" + this.file.getFilePath().ToString(), exception, "Cannot save .txt file at: " + this.file.getFilePath().ToString() + "; " + exception.ToString(), false);
                    }
                }
            }
        }

        //EVENT HANDLERS
        //shortcut events for the rich textbox control (text editor)

        //set the selected text to bold
        private void onShortcut_bold(object sender, EventArgs e)
        {
            FontStyle style = FontStyle.Regular;

            if (this.F5mdi1_richTextBox_textEditor.SelectionFont.Bold == true) style = this.F5mdi1_richTextBox_textEditor.SelectionFont.Style & (~FontStyle.Bold);
            else style = this.F5mdi1_richTextBox_textEditor.SelectionFont.Style | FontStyle.Bold;

            this.F5mdi1_richTextBox_textEditor.SelectionFont = new Font(this.F5mdi1_richTextBox_textEditor.Font, style);
        }

        //set the selected text to italic
        private void onShortcut_italic(object sender, EventArgs e)
        {
            FontStyle style = FontStyle.Regular;

            if (this.F5mdi1_richTextBox_textEditor.SelectionFont.Italic == true) style = this.F5mdi1_richTextBox_textEditor.SelectionFont.Style & (~FontStyle.Italic);
            else style = this.F5mdi1_richTextBox_textEditor.SelectionFont.Style | FontStyle.Italic;

            this.F5mdi1_richTextBox_textEditor.SelectionFont = new Font(this.F5mdi1_richTextBox_textEditor.Font, style);
        }

        //underline the selected text
        private void onShortcut_underline(object sender, EventArgs e)
        {
            FontStyle style = FontStyle.Regular;
            
            if(this.F5mdi1_richTextBox_textEditor.SelectionFont.Underline == true) style = this.F5mdi1_richTextBox_textEditor.SelectionFont.Style & (~FontStyle.Underline);
            else style = this.F5mdi1_richTextBox_textEditor.SelectionFont.Style | FontStyle.Underline;

            this.F5mdi1_richTextBox_textEditor.SelectionFont = new Font(this.F5mdi1_richTextBox_textEditor.Font, style);
        }

        //strikethrough the selected text
        private void onShortcut_strikethrough(object sender, EventArgs e)
        {
            FontStyle style = FontStyle.Regular;

            if (this.F5mdi1_richTextBox_textEditor.SelectionFont.Strikeout == true) style = this.F5mdi1_richTextBox_textEditor.SelectionFont.Style & (~FontStyle.Strikeout);
            else style = this.F5mdi1_richTextBox_textEditor.SelectionFont.Style | FontStyle.Strikeout;

            this.F5mdi1_richTextBox_textEditor.SelectionFont = new Font(this.F5mdi1_richTextBox_textEditor.Font, style);
        }

        //increase the size of the selected text
        private void onShortcut_increaseTextSize(object sender, EventArgs e)
        {
            float size = this.F5mdi1_richTextBox_textEditor.SelectionFont.SizeInPoints;
            if (size + Utility.textPointSizeIncrement <= Utility.maxPointTextSize) size += Utility.textPointSizeIncrement;

            FontStyle style = this.F5mdi1_richTextBox_textEditor.SelectionFont.Style;

            this.F5mdi1_richTextBox_textEditor.SelectionFont = new Font(this.F5mdi1_richTextBox_textEditor.SelectionFont.FontFamily,size, style);
        }

        //decrease the size of the selected text
        private void onShortcut_decreaseTextSize(object sender, EventArgs e)
        {
            float size = this.F5mdi1_richTextBox_textEditor.SelectionFont.SizeInPoints;
            if(size - Utility.textPointSizeDecrement>=Utility.minPointTextSize) size -= Utility.textPointSizeDecrement;

            FontStyle style = this.F5mdi1_richTextBox_textEditor.SelectionFont.Style;

            this.F5mdi1_richTextBox_textEditor.SelectionFont = new Font(this.F5mdi1_richTextBox_textEditor.SelectionFont.FontFamily,size, style);
        }

        //save text
        private void onShortcut_saveText(object sender, EventArgs e)
        {
            this.saveFile();
        }

        //form load
        private void F5mdi1_TextEditor_Load(object sender, EventArgs e)
        {
            //form settings
            this.MinimumSize = Utility.mdiEditorMinimumSize;

            //preferences
            Utility.setLanguage(this);
            Utility.setTheme(this);

            //control appearance
            this.refreshControlsAppearance();
            
            //load fonts into the corresponding combobox
            this.F5mdi1_toolStripComboBox_font.Items.Clear();

            this.F5mdi1_toolStripComboBox_font.Items.Add(Utility.defaultFontFamily);

            foreach(FontFamily fontFamily in FontFamily.Families)
            {   
                this.F5mdi1_toolStripComboBox_font.Items.Add(fontFamily.Name);
            }

            //display the first font from the combobox
            if(this.F5mdi1_toolStripComboBox_font.Items.Count>=1) this.F5mdi1_toolStripComboBox_font.SelectedIndex = 0;
        }

        //MDI window is being resized
        private void F5mdi1_TextEditor_Resize(object sender, EventArgs e)
        {
            this.refreshControlsAppearance();
        }

        //tool strip
        private void F5mdi1_toolStrip_textEditor_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        //set the colour of the selected text from the textbox
        private void F5mdi1_toolStripButton_textColour_Click(object sender, EventArgs e)
        {
            this.F5mdi1_colorDialog_textColour.ShowDialog();
            Color colour = this.F5mdi1_colorDialog_textColour.Color;
            this.F5mdi1_richTextBox_textEditor.SelectionColor = colour;
        }

        //set the selected text to bold - button
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.onShortcut_bold(sender, e);
        }

        //set the selected text to italic - button
        private void F5mdi1_toolStripButton_italicButton_Click(object sender, EventArgs e)
        {
            this.onShortcut_italic(sender, e);
        }

        //underline the selected text - button
        private void F5mdi1_toolStripButton_underlineButton_Click(object sender, EventArgs e)
        {
            this.onShortcut_underline(sender, e);
        }

        //strike through the selected text - button
        private void F5mdi1_toolStripButton_strikethroughButton_Click(object sender, EventArgs e)
        {
            this.onShortcut_strikethrough(sender, e);
        }

        //change the font for the selected text
        private void F5mdi1_toolStripComboBox_font_Click(object sender, EventArgs e)
        {

        }

        private void F5mdi1_toolStripComboBox_font_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.F5mdi1_toolStripComboBox_font.SelectedItem != null) this.F5mdi1_richTextBox_textEditor.SelectionFont = new Font(this.F5mdi1_toolStripComboBox_font.SelectedItem.ToString(), this.F5mdi1_richTextBox_textEditor.SelectionFont.Size, this.F5mdi1_richTextBox_textEditor.SelectionFont.Style);
            }
            catch (Exception exception)
            {
                Utility.DisplayWarning("Font_not_found", exception, "Font not found: " + this.F5mdi1_toolStripComboBox_font.SelectedItem.ToString(), false);
            }
        }

        //selected text changed
        private void F5mdi1_richTextBox_textEditor_SelectionChanged(object sender, EventArgs e)
        {
            
        }

        //press 'Enter' to start the word search
        private void F5mdi1_toolStripTextBox_searchTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool found = false;

            if(e.KeyChar == 0x0D) //enter key pressed
                found = this.searchPhrase(this.F5mdi1_toolStripTextBox_searchTextbox.Text,0); //start searching the phrase from the beginning of the text box
        }

        //start the phrase search
        private void F5mdi1_toolStripButton_startSearch_Click(object sender, EventArgs e)
        {
            bool found = false;

            found = this.searchPhrase(this.F5mdi1_toolStripTextBox_searchTextbox.Text, 0); //start searching the phrase from the beginning of the text box
        }

        //select the highlight colour
        private void F5mdi1_toolStripMenuItem_highlightColour_Click(object sender, EventArgs e)
        {
            this.F5mdi1_colorDialog_textColour.ShowDialog();
            this.highlightColour = this.F5mdi1_colorDialog_textColour.Color;

            //set the backcolor of the control to the highlight color
            this.F5mdi1_toolStripMenuItem_highlightColour.BackColor = this.highlightColour;
        }

        //text was edited
        private void F5mdi1_richTextBox_textEditor_TextChanged(object sender, EventArgs e)
        {
            this.F5mdi1_toolStripStatusLabel_wordCount.Text = this.countWords().ToString() + " " + Utility.displayMessage("F5_wordCountLabel");
        }

        //save the file
        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            this.saveFile();
        }

        //display the 'About' page
        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                //open the general info form and display the information about the Text Editor

                F6_GeneralInformation f6_generalInformation = new F6_GeneralInformation(Utility.displayMessage("F6_title_textEditor_About"), Utility.displayMessage("F6_info_textEditor_AboutInfo"));

                f6_generalInformation.ShowDialog();

                f6_generalInformation.Close();
            }catch (Exception exception)
            {
                Utility.DisplayWarning("TextEditor_cannot_display_AboutInfo",exception,"Cannot open the file containing 'About' information for the Text Editor; "+exception.ToString(),false);
            }
        }

        //'Print' button pressed
        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            //DEV
            
            //this.F5mdi1_printDocument_currentDocument.

            this.F5mdi1_printPreviewDialog_printPreview.Document = this.F5mdi1_printDocument_currentDocument;
            //open print dialog
            this.F5mdi1_printPreviewDialog_printPreview.ShowDialog();
        }
    }   
}
