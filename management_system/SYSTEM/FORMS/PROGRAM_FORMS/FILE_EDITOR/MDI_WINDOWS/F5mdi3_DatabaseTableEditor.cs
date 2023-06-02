using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace management_system
{
    public partial class F5mdi3_DatabaseTableEditor : Form
    {
        //VARIABLES
        //instruments window
        private F10_DatabaseTableTools f10_DatabaseTableTools = null;

        //new table
        private bool newTable = false;
        private string tableName = null;

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
        public F5mdi3_DatabaseTableEditor(F5_FileEditorForm f5_containerForm, string databaseTable, bool localFile, bool newTable)
        {
            InitializeComponent();
            
            //link the specified database table with the data grid view control
            this.databaseTable = Utility.currentGroup.getName()+"_"+ databaseTable;
            this.currentTable = new DataTable(this.databaseTable);
            this.newTable = newTable;
            this.Text = databaseTable.ToString();

            if (newTable == true) //the table is new
            {
                //create the table in the current group
                SqlCommand createTable = Utility.getSqlCommand("CREATE TABLE "+Utility.currentGroup.getName()+"_"+databaseTable.ToString()+"(_id INT)");
                createTable.ExecuteNonQuery();

                createTable.Dispose();

                //update variables
                this.tableName = databaseTable;
                this.databaseTable = Utility.currentGroup.getName() + "_" + databaseTable.ToString();
                this.refreshDatabaseData();
            }
            else
            { //table already exists in the database
                if (localFile == true) //local .tbl file
                {
                    this.parseTblFile(this.databaseTable);
                }
                else //database table
                {
                    this.refreshDatabaseData();
                }
            }
            //container form
            this.MdiParent = f5_containerForm;
            this.f5_containerForm = f5_containerForm;
        }

        //UTILITY FUNCTIONS

        //implemented tools

        //search the value int the specified column; returns the index of the row which contains the value and it displays it in the instruments window
        public void instrument_searchColumn(string columnName, string value)
        {
            try
            {
                foreach(DataRow row in this.currentTable.Rows)
                    if(row[columnName].ToString().Equals(value)) //value found
                    {
                        this.f10_DatabaseTableTools.display_searchColumn(row,this.currentTable.Columns);
                    }
            }
            catch (Exception exception)
            {
                Utility.DisplayError("DataBaseTableEditor_cannot_search_column", exception, "DataBaseTableEditor: cannot search a value in the specified column: \n" + exception.ToString(), false);
            }
        }


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

                   //delete old values from the array to make sure that a missing attribute from the XML does not allow an old value to be
                   //written into memory again (in the next iteration)
                   for (i = 0; i < this.currentTable.Columns.Count; i++)
                        rowValues[i] = null;
                }


            }catch(Exception exception)
            {
                Utility.DisplayError("DataBaseTableEditor_failed_to_parse_local_file_tbl", exception, "DataBaseTableEditor: Failed to parse a locally stored .tbl file: " + exception.ToString(), false);
            }
        }

        //get the table values from the connected database
        private void getCurrentTable()
        {
            try
            {
                //clear variable
                this.currentTable.Clear();
                this.currentTable.Columns.Clear();

                SqlCommand refresh_cmd = Utility.getSqlCommand("SELECT * FROM " + this.databaseTable.ToString());

                SqlDataAdapter ad = new SqlDataAdapter(refresh_cmd);
                ad.Fill(this.currentTable);
                return;
                

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
                    Utility.logDiagnosticEntry("DatabaseTableEditor: failed to load database table; 'databaseTable' was null");
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
                XmlWriterSettings ws = new XmlWriterSettings();
                ws.IndentChars = "\t";
                ws.Indent = true;
                ws.NewLineChars = "\r\n";
                ws.CheckCharacters = true;

                //create file
                //StreamWriter file = File.CreateText(folderPath + "\\" + this.databaseTable + ".tbl");

                XmlWriter w = XmlWriter.Create(folderPath+"\\"+this.databaseTable+".tbl", ws);
                w.WriteStartDocument(true);
                w.WriteStartElement("table");
                //columns
                w.WriteStartElement("columns");

                int index = 1;
                foreach (DataColumn column in this.currentTable.Columns) 
                {

                    w.WriteAttributeString("column"+index.ToString(), column.ColumnName);
                    index++;
                }
                
                w.WriteEndElement();

                //rows
                w.WriteStartElement("rows");
                index = 1;
                foreach (DataRow row in this.currentTable.Rows)
                {
                    w.WriteStartElement("row");
                    index=1;
                    foreach (string cell in row.ItemArray)
                    {

                        w.WriteAttributeString("value" + index.ToString(), cell.ToString());
                        index++;

                    }
                    w.WriteEndElement();
                }

                w.WriteEndElement();


                w.WriteEndElement();
                w.WriteEndDocument();

                w.Close();
                //file.Close();
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
            Utility.setLanguage(this); //set language

            //form settings
            this.MinimumSize = Utility.mdiEditorMinimumSize;
            
            //refresh control appearance
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

                bool _string = false; //the current column type is numeric (_string=false) or not (_string=true)
                string values2;

                foreach (DataGridViewRow row in this.F5mdi3_dataGridView_databaseTableEditor.Rows)
                {
                    values = "";
                    values2 = "";

                    if (row.IsNewRow) break; //if this is the last, empty row used to add new rows, skip it
                    _string = false;
                    for (int i=0; i<row.Cells.Count;i++)
                    {
                        //construct a string containing the new values
                        values += this.stringColumns[i]+"=";

                        //prepend and append the value to '
                        
                        if(this.currentTable.Columns[i].DataType.Name.Equals("String"))
                            _string= true;


                        if (_string == true)
                        {
                            values += "'";
                            values2 += "'";
                        }
                        
                        values += row.Cells[i].Value;
                        values2 += row.Cells[i].Value;

                        if (_string == true)
                        {
                            values += "'";
                            values2 += "'";
                        }


                        if (i < row.Cells.Count - 1)
                        {
                            values += ",";
                            values2 += ",";
                        }

                    }

                    SqlCommand read = Utility.getSqlCommand("SELECT * FROM "+this.databaseTable);
                    SqlDataReader dr = read.ExecuteReader();
                    bool found = false;

                    while(dr.Read())
                    {
                        if(dr.GetInt32(0)== Convert.ToInt32(row.Cells[0].Value.ToString()))
                        {
                            found = true;
                            dr.Close();
                            break;
                        }
                    }

                    if (dr!=null) dr.Close();
                    string where = "";

                    if (found==true) //current _id found in the database table => UPDATE
                    {
                        
                        values = "UPDATE " + databaseTable.ToString() + " SET " + values;
                        where = " WHERE _id=" + row.Cells[0].Value.ToString();
                    }
                    else //current _id not found in the database table => INSERT
                    {
                        values = "INSERT INTO " + databaseTable.ToString() + " VALUES (" + values2+")";
                    }

                    //update the row
                    upload_cmd = Utility.getSqlCommand(values+where);
                    int result = upload_cmd.ExecuteNonQuery();

                    if (result == -1) //no rows updated
                        throw new Exception("Failed to update connected database table");
                }

                //delete rows that are no longer in the editor
                SqlCommand read2 = Utility.getSqlCommand("SELECT * FROM " + this.databaseTable);
                SqlDataReader dr2 = read2.ExecuteReader();
                bool found2 = false;
                List<int> IDs = new List<int>();

                while (dr2.Read())
                {
                    IDs.Add(dr2.GetInt32(0));
                }

                if (dr2 != null) dr2.Close();

                foreach (int _id in IDs)
                {
                    found2 = false;

                    foreach (DataGridViewRow row in this.F5mdi3_dataGridView_databaseTableEditor.Rows)
                    {
                        if (row.IsNewRow) break; //last, empty row (used to insert new rows)

                        if ( _id == Convert.ToInt32(row.Cells[0].Value.ToString()))
                        {
                            found2 = true;
                            break;
                        }
                    }

                    if (found2 == false) //delete row
                    {
                        upload_cmd = Utility.getSqlCommand("DELETE FROM "+this.databaseTable.ToString()+" WHERE _id=" + _id.ToString());
                        int result = upload_cmd.ExecuteNonQuery();

                        if (result == -1) //no rows updated
                            throw new Exception("Failed to update connected database table");
                    }
                }

                //if this is a new table, add it into the database files table
                if (this.newTable == true)
                {
                    SqlCommand addTable = Utility.getSqlCommand("INSERT INTO " + Utility.currentGroup.getName() + "_DatabaseFiles(filename, databaseTable) VALUES ('" + this.tableName + "', 1)");

                    addTable.ExecuteNonQuery();

                    addTable.Dispose();
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
                    
                    case DialogResult.Cancel: //cancel opening the file
                        break;
                    
                    case DialogResult.Abort: //cancel opening the file
                            break;
                    
                    default: 
                        throw new Exception("Invalid dialog option chosen: "+result.ToString());
                        //break;
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

                //refresh table
                this.refreshDatabaseData();

            }catch(Exception exception)
            {
                Utility.DisplayError("DataBaseTableEditor_failed_to_add_new_column_to_table", exception, "DataBaseTableEditor: Failed to add a new column to the table: \n" + exception.ToString(), false);
            }
        }

        private void F5mdi3_toolStripButton_tools_Click(object sender, EventArgs e)
        {
            try
            {
                f10_DatabaseTableTools = new F10_DatabaseTableTools(this);

                f10_DatabaseTableTools.Show();

               

            }catch (Exception exception)
            {
                Utility.DisplayError("DataBaseTableEditor_cannot_open_tools_form", exception, "DataBaseTableEditor: Could not open the database table editor tools form: \n" + exception.ToString(), false); ;
            }
        }
    }
    }
