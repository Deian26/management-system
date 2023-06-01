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
using System.Runtime.ConstrainedExecution;
using System.Security.Permissions;
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

        //text editing
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

            //set the form title to the file name
            if(this.file!=null) this.Text = this.file.getFilePath().Split('\\').Last();

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

            //load the text from the file into the text editor and create a TextFile object for it
            try 
            {
                //this.F5mdi1_richTextBox_textEditor.Text = Utility.DEC_GEN(File.ReadAllText(this.file.getFilePath()),Utility.key);
                if (this.file.getFileType() == FileEditor.FileType.text) this.F5mdi1_richTextBox_textEditor.Text = Utility.DEC_TXT(File.ReadAllText(this.file.getFilePath()), Utility.key);
                else if(this.file.getFileType() == FileEditor.FileType.rtf) this.F5mdi1_richTextBox_textEditor.LoadFile(this.file.getFilePath());
                else this.F5mdi1_richTextBox_textEditor.Text = File.ReadAllText(this.file.getFilePath());

                this.file = new TextFile(this.file.getFilePath());
            }catch(Exception exception) 
            {
                Utility.DisplayError("TextEditor_failed_to_load_text_file_into_textbox", exception,"TextEditor: Failed to load the text into the textbox. "+exception.ToString() ,false);
            }

            //display file extension
            try
            {
                this.F5mdi1_toolStripStatusLabel_fileExtension.Text = this.file.getFilePath().Split('.').Last();
            }catch(Exception exception)
            {
                Utility.logDiagnosticEntry("TextEditor: Failed to display file extension for file: "+this.file.getFilePath()+" ; details: " + exception.ToString());
            }

            //display word count
            this.countWords();
        }



        //UTILITY FUNCTIONS

        //return the file associated with this MDI window
        public GeneralFile getFile()
        {
            return this.file;
        }

        //refresh the appearance of controls in this form
        private void refreshControlsAppearance()
        {
            //text box control size
            this.F5mdi1_richTextBox_textEditor.Size = new Size(this.Size.Width-this.widthOffset, this.Size.Height - this.heightOffset);

        }

        //search a word in the text box - RECURSIVE FUNCTION
        private bool searchPhrase(string phrase, int startPosition)
        {
            //determine the search options
            this.searchOptions = RichTextBoxFinds.None; //default value

            if (this.F5mdi1_toolStripMenuItem_matchCaseCheckbox.Checked) //match case
                this.searchOptions |= RichTextBoxFinds.MatchCase;
            if (this.F5mdi1_toolStripMenuItem_reverseCheckbox.Checked) //reverse search
                this.searchOptions |= RichTextBoxFinds.Reverse;
            if (this.F5mdi1_toolStripMenuItem_wholeWordCheckbox.Checked) //match whole word
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
        private void countWords()
        {
            try
            {
                string[] aux_str = this.F5mdi1_richTextBox_textEditor.Text.Split(' ');
                int count = 0;

                foreach(string aux in aux_str) 
                {
                    if (!aux.Equals(""))
                        count++;
                }

                //display word count
                this.F5mdi1_toolStripStatusLabel_wordCount.Text = count.ToString() + " " + Utility.displayMessage("F5_wordCountLabel");

            }catch(Exception exception) 
            {
                Utility.DisplayWarning("TextEditor_unrecognized_character_found_in_text", exception, null, false);
            }

        }

        //locally save the file open (overwrite the file); the function returns 'true' if the saving operation was successful and 'false' otherwise
        private bool saveTextFile()
        {
            try
            {
                string beforeEncryption = this.F5mdi1_richTextBox_textEditor.Text;
                //this.F5mdi1_richTextBox_textEditor.Text = Utility.ENC_GEN(this.F5mdi1_richTextBox_textEditor.Text, Utility.key);

                if (this.file.getFileType() == FileEditor.FileType.text)
                {
                    this.F5mdi1_richTextBox_textEditor.Text = Utility.ENC_TXT(this.F5mdi1_richTextBox_textEditor.Text, Utility.key);

                    this.F5mdi1_richTextBox_textEditor.SaveFile(this.file.getFilePath(), RichTextBoxStreamType.PlainText);


                    this.F5mdi1_richTextBox_textEditor.Text = beforeEncryption;
                }
                else
                {
                    this.F5mdi1_richTextBox_textEditor.SaveFile(this.file.getFilePath(), RichTextBoxStreamType.RichText);
                }

            }
            catch(Exception exception)
            {
                Utility.DisplayError("TextEditor_could_not_save_same_file",exception, "TextEditor: Could not save the text file: "+this.file.getFilePath().ToString(), false);
                return false;
            }

            return true;
        }

        //locally save the currently open file as an .rtf/.txt file
        private void saveTextFileAs()
        {
            try
            {
                if (!Directory.Exists(Utility.currentGroupPath)) //group folder/RTF folder does not exist locally
                    throw new Exception("Invalid path");

                
                //group folder/RTF exists locally => save .rtf file in the RTF folder (directory)
                //encrypt text
                string aux_string = this.F5mdi1_richTextBox_textEditor.Text;

                //this.F5mdi1_richTextBox_textEditor.Text = Utility.ENC_GEN(this.F5mdi1_richTextBox_textEditor.Text, Utility.key);
                if(this.file.getFileType() == FileEditor.FileType.text)
                    this.F5mdi1_richTextBox_textEditor.Text = Utility.ENC_TXT(this.F5mdi1_richTextBox_textEditor.Text, Utility.key);
                

                //set filter
                this.F5mdi1_saveFileDialog_newRtfFile.Filter = "Rich Text Format | *.rtf|Text file | *.txt";
                //set path
                this.F5mdi1_saveFileDialog_newRtfFile.InitialDirectory= Utility.currentGroupPath + "\\";

                //open save file dialog
                DialogResult result = this.F5mdi1_saveFileDialog_newRtfFile.ShowDialog();
                string path = this.F5mdi1_saveFileDialog_newRtfFile.FileName;

                if (!result.Equals(DialogResult.OK)) // quit
                    return;

                this.F5mdi1_richTextBox_textEditor.SaveFile(path);

                if (this.file.getFileType() == FileEditor.FileType.text)
                    this.F5mdi1_richTextBox_textEditor.Text = aux_string;

            }catch (Exception exception)
            {
                Utility.DisplayError("TextEditor_cannot_save_file", exception, "TextEditor: Could not locally save a file: " + exception.ToString(), false);
            }
        }

        //increase text size
        private void increaseTextSize()
        {
            float size = this.F5mdi1_richTextBox_textEditor.SelectionFont.SizeInPoints;
            if (size + Utility.textPointSizeIncrement <= Utility.maxPointTextSize) size += Utility.textPointSizeIncrement;

            FontStyle style = this.F5mdi1_richTextBox_textEditor.SelectionFont.Style;

            this.F5mdi1_richTextBox_textEditor.SelectionFont = new Font(this.F5mdi1_richTextBox_textEditor.SelectionFont.FontFamily, size, style);
        }

        //decrease text size
        private void decreaseTextSize()
        {
            float size = this.F5mdi1_richTextBox_textEditor.SelectionFont.SizeInPoints;
            if (size - Utility.textPointSizeDecrement >= Utility.minPointTextSize) size -= Utility.textPointSizeDecrement;

            FontStyle style = this.F5mdi1_richTextBox_textEditor.SelectionFont.Style;

            this.F5mdi1_richTextBox_textEditor.SelectionFont = new Font(this.F5mdi1_richTextBox_textEditor.SelectionFont.FontFamily, size, style);
        }

        //EVENT HANDLERS
        //shortcut events for the rich textbox control (text editor)

        //set the selected text to bold
        private void onShortcut_bold(object sender, EventArgs e)
        {
            FontStyle style = FontStyle.Regular;

            if (this.F5mdi1_richTextBox_textEditor.SelectionFont.Bold == true) style = this.F5mdi1_richTextBox_textEditor.SelectionFont.Style & (~FontStyle.Bold);
            else style = this.F5mdi1_richTextBox_textEditor.SelectionFont.Style | FontStyle.Bold;

            this.F5mdi1_richTextBox_textEditor.SelectionFont = new Font(this.F5mdi1_richTextBox_textEditor.SelectionFont, style);
        }

        //set the selected text to italic
        private void onShortcut_italic(object sender, EventArgs e)
        {
            FontStyle style = FontStyle.Regular;

            if (this.F5mdi1_richTextBox_textEditor.SelectionFont.Italic == true) style = this.F5mdi1_richTextBox_textEditor.SelectionFont.Style & (~FontStyle.Italic);
            else style = this.F5mdi1_richTextBox_textEditor.SelectionFont.Style | FontStyle.Italic;

            this.F5mdi1_richTextBox_textEditor.SelectionFont = new Font(this.F5mdi1_richTextBox_textEditor.SelectionFont, style);
        }

        //underline the selected text
        private void onShortcut_underline(object sender, EventArgs e)
        {
            FontStyle style = FontStyle.Regular;
            
            if(this.F5mdi1_richTextBox_textEditor.SelectionFont.Underline == true) style = this.F5mdi1_richTextBox_textEditor.SelectionFont.Style & (~FontStyle.Underline);
            else style = this.F5mdi1_richTextBox_textEditor.SelectionFont.Style | FontStyle.Underline;

            this.F5mdi1_richTextBox_textEditor.SelectionFont = new Font(this.F5mdi1_richTextBox_textEditor.SelectionFont, style);
        }

        //strikethrough the selected text
        private void onShortcut_strikethrough(object sender, EventArgs e)
        {
            FontStyle style = FontStyle.Regular;

            if (this.F5mdi1_richTextBox_textEditor.SelectionFont.Strikeout == true) style = this.F5mdi1_richTextBox_textEditor.SelectionFont.Style & (~FontStyle.Strikeout);
            else style = this.F5mdi1_richTextBox_textEditor.SelectionFont.Style | FontStyle.Strikeout;

            this.F5mdi1_richTextBox_textEditor.SelectionFont = new Font(this.F5mdi1_richTextBox_textEditor.SelectionFont, style);
        }

        //increase the size of the text
        private void onShortcut_increaseTextSize(object sender, EventArgs e)
        {
            this.increaseTextSize();
        }

        //decrease the size of the text
        private void onShortcut_decreaseTextSize(object sender, EventArgs e)
        {
            this.decreaseTextSize();
        }

        //save text
        private void onShortcut_saveText(object sender, EventArgs e)
        {
            this.saveTextFileAs();
        }

        //form load
        private void F5mdi1_TextEditor_Load(object sender, EventArgs e)
        {
            Utility.setLanguage(this); //set language

            //form settings
            this.MinimumSize = Utility.mdiEditorMinimumSize;
            this.F5mdi1_toolStripStatusLabel_databaseSyncStatus.Visible = false;
            this.F5mdi1_toolStripStatusLabel_localFileSaveStatus.Visible = false;
            this.F5mdi1_toolStripStatusLabel_localFileSaveStatus.Font = new Font(this.F5mdi1_toolStripStatusLabel_localFileSaveStatus.Font,FontStyle.Bold);
            this.F5mdi1_toolStripStatusLabel_databaseSyncStatus.Font = new Font(this.F5mdi1_toolStripStatusLabel_databaseSyncStatus.Font,FontStyle.Bold);


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

            //start timer
            this.F5mdi1_timer_textEditor.Interval = 3000; //ms
            this.F5mdi1_timer_textEditor.Start(); //start timer
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
                if (this.F5mdi1_toolStripComboBox_font.SelectedItem != null) 
                    this.F5mdi1_richTextBox_textEditor.SelectionFont = new Font(this.F5mdi1_toolStripComboBox_font.SelectedItem.ToString(), this.F5mdi1_richTextBox_textEditor.SelectionFont.Size, this.F5mdi1_richTextBox_textEditor.SelectionFont.Style);
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
            //this.countWords();
        }

        //save the file into the database (serialize local file then upload it into the database)
        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            if(this.saveTextFile()==true) //file successfully saved
            {
                this.F5mdi1_toolStripStatusLabel_localFileSaveStatus.ForeColor = Color.Green;
                this.F5mdi1_toolStripStatusLabel_localFileSaveStatus.Visible = true;
                this.F5mdi1_toolStripStatusLabel_localFileSaveStatus.Text = Utility.displayMessage("F5mdi1_local_file_save_successful");
            }
            else //file not saved
            {
                this.F5mdi1_toolStripStatusLabel_localFileSaveStatus.ForeColor = Color.Red;
                this.F5mdi1_toolStripStatusLabel_localFileSaveStatus.Visible = true;
                this.F5mdi1_toolStripStatusLabel_localFileSaveStatus.Text = Utility.displayMessage("F5mdi1_local_file_save_unsuccessful");
            }
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
            //DEV - OPTIONAL
            
            //this.F5mdi1_printDocument_currentDocument.

            this.F5mdi1_printPreviewDialog_printPreview.Document = this.F5mdi1_printDocument_currentDocument;
            //open print dialog
            this.F5mdi1_printPreviewDialog_printPreview.ShowDialog();
        }

        //replace all words matching the text in the search textbox with the text from the replace textbox
        private void F5mdi1_toolStripMenuItem_replaceAll_Click(object sender, EventArgs e)
        {
            try
            {
                if(this.F5mdi1_toolStripTextBox_searchTextbox.Text!="" && this.F5mdi1_toolStripTextBox_replaceTextBox.Text!="")
                    this.F5mdi1_richTextBox_textEditor.Text = this.F5mdi1_richTextBox_textEditor.Text.Replace(this.F5mdi1_toolStripTextBox_searchTextbox.Text, this.F5mdi1_toolStripTextBox_replaceTextBox.Text);
            }catch (Exception exception)
            {
                Utility.DisplayError("TextEditor_cannot_replace_phrase", exception, "Could not replace the text from the search box:" + this.F5mdi1_toolStripTextBox_searchTextbox.Text.ToString() + " with the text from the 'Replace all' textbox: " + this.F5mdi1_toolStripTextBox_replaceTextBox.Text.ToString() + " : " + exception.ToString(), false);
                return;
            }

            //replace successful
            MessageBox.Show(Utility.displayMessage("Replace_text_successful"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //text box was clicked
        private void F5mdi1_richTextBox_textEditor_Click(object sender, EventArgs e)
        {

        }

        //clear the text highlited in the phrase search
        private void F5mdi1_toolStripButton_clearSearchedTextHighlights_Click(object sender, EventArgs e)
        {
            this.F5mdi1_richTextBox_textEditor.SelectAll();
            this.F5mdi1_richTextBox_textEditor.SelectionBackColor = Color.White;
            this.F5mdi1_richTextBox_textEditor.DeselectAll();
        }

        //save the file as a local .rtf file
        private void F5mdi1_toolStripMenuItem_saveAsButton_Click(object sender, EventArgs e)
        {
            if(Utility.uploadFileIntoDB(this.file.getFilePath())==true) //upload successful
            {
                this.F5mdi1_toolStripStatusLabel_databaseSyncStatus.Text = Utility.displayMessage("F5mdi1_db_file_upload_successful");
                this.F5mdi1_toolStripStatusLabel_databaseSyncStatus.ForeColor = Color.Green;
                this.F5mdi1_toolStripStatusLabel_databaseSyncStatus.Visible = true;
            }else //upload unsuccessful
            {
                this.F5mdi1_toolStripStatusLabel_databaseSyncStatus.Text = Utility.displayMessage("F5mdi1_db_file_upload_unsuccessful");
                this.F5mdi1_toolStripStatusLabel_databaseSyncStatus.ForeColor = Color.Red;
                this.F5mdi1_toolStripStatusLabel_databaseSyncStatus.Visible = true;
            }
        }

        //open a local file into the current editor if saved, will overwrite the current file)
        private void F5mdi1_toolStripButton_openFile_Click(object sender, EventArgs e)
        {
            try
            {
                //apply a filter to the open file dialog so that onlt .rtf and .txt files can be selected
                this.F5mdi1_openFileDialog_openLocalFile.Filter = "Rich Text Format | *.rtf|Text | *.txt";

                //show the open file dialog
                DialogResult result = this.F5mdi1_openFileDialog_openLocalFile.ShowDialog();

                if (result.Equals(DialogResult.OK)) //OK
                {
                    string filePath = this.F5mdi1_openFileDialog_openLocalFile.FileName;
                    if (Path.GetExtension(filePath).ToLower().Equals("rtf")) this.F5mdi1_richTextBox_textEditor.LoadFile(filePath);  //store the text from the selected RTF file into the rich text box control
                    else if (Path.GetExtension(filePath).ToLower().Equals("txt")) //open a simple text file and store the text in the rich text box
                    {
                        this.F5mdi1_richTextBox_textEditor.Text = File.ReadAllText(filePath);
                    }

                    //check if the file has the path to the 'management_system' folder in its path and if so, decrypt the file; otherwise, leave the text as it is
                    if (Utility.managementSystemDataFile(filePath) == true)
                    {
                        this.F5mdi1_richTextBox_textEditor.Text = Utility.DEC_GEN(this.F5mdi1_richTextBox_textEditor.Text, Utility.key); //decrypt text
                    }
                    
                }
            }
            catch (Exception exception)
            {
                Utility.DisplayError("TextEditor_failed_to_open_text_file", exception, "TextEditor: could not open a local file: " + exception.ToString(), false);
            }
        }

        //save the file to a custom location with the same / a different extension
        private void F5mdi1_toolStripMenuItem_saveAsButton_Click_1(object sender, EventArgs e)
        {
            this.saveTextFileAs();
        }

        //timer
        private void F5mdi1_timer_textEditor_Tick(object sender, EventArgs e)
        {
            this.F5mdi1_toolStripStatusLabel_localFileSaveStatus.Visible = false; //hide the local file save status label
            this.F5mdi1_toolStripStatusLabel_databaseSyncStatus.Visible = false; //hide the database file upload label
            this.countWords(); //calculate and display the word count
        }

        private void F5mdi1_toolStripMenuItem_matchCaseCheckbox_Click(object sender, EventArgs e)
        {

        }

        //increase text size - button
        private void F5mdi1_toolStripButton_increaseTextSize_Click(object sender, EventArgs e)
        {
            this.increaseTextSize();
        }

        //decrease text size - button
        private void F5mdi1_toolStripButton_decreaseTextSize_Click(object sender, EventArgs e)
        {
            this.decreaseTextSize();
        }
    }   
}
