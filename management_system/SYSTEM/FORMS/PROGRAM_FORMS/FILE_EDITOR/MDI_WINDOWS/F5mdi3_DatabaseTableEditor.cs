using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace management_system
{
    public partial class F5mdi3_DatabaseTableEditor : Form
    {
        //VARIABLES
        //database table
        private string databaseTable = null; //database table name : GroupName + _ + TableName

        private F5_FileEditorForm f5_containerForm = null;

        //control appearance
        private int AbsoluteEditorWidthSubtract = 40; //points
        private int AbsoluteEditorHeightSubtract = 100; //points

        //current database table
        private DataTable currentTable = null;
        private List<string> stringColumns = new List<string>();

        //CONSTRUCTORSw
        public F5mdi3_DatabaseTableEditor(F5_FileEditorForm f5_containerForm, string databaseTable, bool localFile)
        {
            InitializeComponent();
            
            //link the specified database table with the data grid view control
            this.databaseTable = databaseTable;
            this.currentTable = new DataTable(this.databaseTable);

            if (localFile == true) //local .tbl file
            {
                this.parseTblFile(this.databaseTable);
            }
            else //database table
            {
                this.refreshDatabaseData();
            }
            //container form
            this.MdiParent = f5_containerForm;
            this.f5_containerForm = f5_containerForm;
        }

        //UTILITY FUNCTIONS

        //parse a locally stored .tbl file (xml format)
        private void parseTblFile(string path)
        {
            try
            {
                //open file
                XmlDocument xml = new XmlDocument();
                xml.Load(path);

                //load the document element (root element)
                XmlNode root = xml.DocumentElement;

                //parse the XML structure and load the column names into memory
                foreach (XmlAttribute column in root.ChildNodes[0].Attributes)
                {
                    this.currentTable.Columns.Add(column.Value); //add column
                }

                //parse the XML file and load the rows into memory
                object[] rowValues = new object[this.currentTable.Columns.Count];
                int i;
                foreach(XmlNode row in root.ChildNodes[1].ChildNodes)
                {
                    //load row values form the .tbl file into an object array
                    i = 0;
                    foreach (XmlAttribute value in row.Attributes)
                        rowValues[i++] = value.Value;

                    this.currentTable.Rows.Add(rowValues); //add values into memory

                   //delete old values from the array to make sure that a missing attribute from the XML does not allow an old value to be written into memory again
                   for (i = 0; i < this.currentTable.Columns.Count; i++)
                        rowValues[i] = null;
                }


            }catch(Exception exception)
            {
                Utility.DisplayError("DataBaseTableEditor_failed_to_parse_local_file_tbl", exception, "DataBaseTableEditor: Failed to parse a loclaly stored .tbl file: " + exception.ToString(), false);
            }
        }

        //get the table values from the connected database
        private void getCurrentTable()
        {
            try
            {
                SqlCommand refresh_cmd = Utility.getSqlCommand("SELECT * FROM " + this.databaseTable.ToString());

                //get columns
                SqlDataReader dr = refresh_cmd.ExecuteReader();
                for(int i = 0;i<dr.FieldCount;i++)
                {
                    //this.stringColumns.Add(dr.GetName(i));

                    this.currentTable.Columns.Add(dr.GetName(i));
                }
                dr.Close();

                //get rows
                dr = refresh_cmd.ExecuteReader();
                object[] row = new object[dr.FieldCount];
                while (dr.Read()) 
                {
                    dr.GetValues(row);
                    this.currentTable.Rows.Add(row);
                }
                
                dr.Close();

            }catch(Exception exception)
            {
                Utility.DisplayError("DataBaseTableEditor_failed_to_open_table", exception,"DataBaseTableEditor: failed to open the table: "+exception.ToString(),false);
            }

        }

        //refresh database data
        private void refreshDatabaseData()
        {
            try
            {

                if (databaseTable != null)
                {
                    this.getCurrentTable();
                    this.F5mdi3_dataGridView_databaseTableEditor.DataSource = this.currentTable;

                }
                else
                    Utility.logDiagnsoticEntry("DatabaseTableEditor: failed to load database table; 'databaseTable' was null");
            }catch(Exception exception)
            {
                Utility.DisplayError("DataBaseTableEditor_failed_to_load_database_table", exception, "DataBaseTableEditor: failed to load database table: " + exception.ToString(), false);
            }
        }

        //refresh controls appearance
        private void refreshControlsAppearance()
        {
            //data grid control
            this.F5mdi3_dataGridView_databaseTableEditor.Width = this.Width - this.AbsoluteEditorWidthSubtract;
            this.F5mdi3_dataGridView_databaseTableEditor.Height = this.Height - this.AbsoluteEditorHeightSubtract;
        }
        
        //save the table as a .tbl file int the specified folder
        private void saveAsTbl(string folderPath)
        {
            try
            {
                //check folder path
                if (!Directory.Exists(folderPath))
                    throw new Exception(Utility.displayError("DataBaseTableEditor_failed_to_locally_save_database_table"));

                //save the table in a .tbl file (XML format)
                //DEV
            }
            catch (Exception exception)
            {
                Utility.DisplayError("DataBaseTableEditor_failed_to_locally_save_database_table", exception, "DataBaseTableEditor: Could not save the table as a local .tbl file: " + exception.ToString(), false);
            }
        }

        //return the database table associated with this MDI window
        public string getTableName()
        {
            return this.databaseTable;
        }

        //EVENT HANDLERS
        private void F5mdi3_DatabaseTableEditor_Load(object sender, EventArgs e)
        {
            //form settings
            this.MinimumSize = Utility.mdiEditorMinimumSize;

            //refresh control appareance
            this.refreshControlsAppearance();
        }

        //form is being resized
        private void F5mdi3_DatabaseTableEditor_Resize(object sender, EventArgs e)
        {
            this.refreshControlsAppearance();
        }

        //toolstrip
        private void F5mdi3_toolStrip_toolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        //save the table as a local .tbl file
        private void F5mdi3_saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //check path
                if (!Directory.Exists(Utility.currentGroupPath)) throw new Exception("Invalid path to the local table subfolder in the local group folder: "+ Utility.currentGroupPath.ToString());

                this.saveAsTbl(Utility.currentGroupPath);

                 
            }catch (Exception exception)
            {
                Utility.DisplayError("", exception, "DataBaseTableEditor: Could not save the table as a local .tbl file: " + exception.ToString(), false);
            }
        }

        //save file in the database
        private void F5mdi3_saveToolStripButton_Click(object sender, EventArgs e)
        {
            SqlCommand upload_cmd = null;
            string values = null;

            try
            {
                //clear old column names
                this.stringColumns.Clear();

                //get column names from the current data table
                foreach(DataColumn column in this.currentTable.Columns)
                {
                    this.stringColumns.Add(column.ColumnName);
                }

                foreach (DataGridViewRow row in this.F5mdi3_dataGridView_databaseTableEditor.Rows)
                {
                    values = "UPDATE "+databaseTable.ToString()+" SET ";

                    if (row.IsNewRow) break; //if this is the last, empty row used to add new rows, skip it

                    for(int i=0; i<row.Cells.Count;i++)
                    {
                        //construct a string containing the new values
                        values += this.stringColumns[i]+"=";

                        //prepend and append thevalue to '
                        values += "'";
                        values += row.Cells[i].Value + "'";
                        

                        if (i < row.Cells.Count - 1) values += ",";
                    }
                    
                    //update the row
                    upload_cmd = Utility.getSqlCommand(values);
                    int result = upload_cmd.ExecuteNonQuery();

                    if (result == -1) //no rows updated
                        throw new Exception("Failed to update connected database table");
                }
            }
            catch (Exception exception)
            {
                Utility.DisplayError("DataBaseTableEditor_failed_to_upload_database_table",exception,"DataBaseTableEditor: failed to upload the database table into the database: "+exception.ToString(),false);
            }
        }

        //open a locally saved database table (.tbl file)
        private void F5mdi3_openToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = this.F5mdi3_openFileDialog_openLocalFile.ShowDialog();

                switch (result)
                {
                    case DialogResult.OK: //open file

                        //clear current table from the data grid control
                        this.currentTable.Clear();
                        this.currentTable.Columns.Clear();

                        //parse the .tbl file
                        this.parseTblFile(this.F5mdi3_openFileDialog_openLocalFile.FileName);

                        break;
                    
                    case DialogResult.Cancel: //cancel openning the file
                        break;
                    
                    case DialogResult.Abort: //cancel openning the file
                            break;
                    
                    default: 
                        throw new Exception("Invalid dialog option chosen: "+result.ToString());
                        break;
                }

            }
            catch (Exception exception)
            {
                Utility.DisplayError("DataBaseTableEditor_failed_to_load_locally_saved_database_file_tbl", exception, "DataBaseTableEditor: Failed to load a locally stored Database file (.tbl)", false);
            }

        }

        //click on a data grid control cell
        private void F5mdi3_dataGridView_databaseTableEditor_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        //Database table editor guide
        private void F5mdi3_helpToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                F6_GeneralInformation f6_GeneralInformation = new F6_GeneralInformation(Utility.displayMessage("F5mdi3_guide_form_title"), Utility.displayMessage("F5mdi3_guide_form_info"));
            }
            catch (Exception exception)
            {
                Utility.DisplayError("DataBaseTableEditor_failed_to_open_editor_guide", exception, "DataBaseTableEditor: Failed to open the editor guide: \n" + exception.ToString(), false);
            }
        }

        //delete a column
        private void F5mdi3_cutToolStripButton_Click(object sender, EventArgs e)
        {

        }

        //print
        private void F5mdi3_printToolStripButton_Click(object sender, EventArgs e)
        {

        }

        //add a new column
        private void F5mdi3_toolStripButton_addColumn_Click(object sender, EventArgs e)
        {
            try
            {
                //open a form to specify the new column name and data type
                F8_AddNewTableColumn f8_AddNewTableColumn = new F8_AddNewTableColumn(this.databaseTable);

                f8_AddNewTableColumn.ShowDialog();

                f8_AddNewTableColumn.Close();

            }catch(Exception exception)
            {
                Utility.DisplayError("DataBaseTableEditor_failed_to_add_new_column_to_table", exception, "DataBaseTableEditor: Failed to add a new column to the table: \n" + exception.ToString(), false);
            }
        }
    }
    }
