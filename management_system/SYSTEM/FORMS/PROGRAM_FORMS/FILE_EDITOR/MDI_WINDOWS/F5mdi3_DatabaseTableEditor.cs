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
using System.Xml.Linq;

namespace management_system.SYSTEM.FORMS.PROGRAM_FORMS.FILE_EDITOR.MDI_WINDOWS
{
    public partial class F5mdi3_DatabaseTableEditor : Form
    {
        //VARIABLES
        //database table
        private string databaseTable = null; //GroupName + _ + TableName

        private F5_FileEditorForm f5_containerForm = null;

        //control appearance
        private int AbsoluteEditorWidthSubtract = 40; //points
        private int AbsoluteEditorHeightSubtract = 100; //points

        //current database table
        private DataTable currentTable = null;
        private List<string> stringColumns = new List<string>();

        //CONSTRUCTORS
        public F5mdi3_DatabaseTableEditor(F5_FileEditorForm f5_containerForm, string databaseTable)
        {
            InitializeComponent();

            //link the specified database table with the data grid view control
            this.databaseTable = databaseTable;
            this.currentTable = new DataTable(this.databaseTable);
            //DEBUG - TO BE DELETED AFTER DEVELOPMENT
            this.databaseTable = "DevGroup1_Table1";

            this.refreshDatabaseData();

            //container form
            this.MdiParent = f5_containerForm;
            this.f5_containerForm = f5_containerForm;
        }

        //UTILITY FUNCTIONS

        //get the table values
        private void getCurrentTable()
        {
            try
            {
                SqlCommand refresh_cmd = Utility.getSqlCommand("SELECT * FROM " + databaseTable.ToString());

                //get columns
                SqlDataReader dr = refresh_cmd.ExecuteReader();
                for(int i = 0;i<dr.FieldCount;i++)
                {
                    //construct a string containing column names to be used when updating the database table
                    this.stringColumns.Add(dr.GetName(i));

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
                    Utility.logDiagnsoticEntry("DatabaseTableEditor: failed to load database table; 'databasTable' was null");
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
                foreach (DataGridViewRow row in this.F5mdi3_dataGridView_databaseTableEditor.Rows)
                {
                    values = "UPDATE "+databaseTable.ToString()+" SET ";
                    for(int i=0; i<row.Cells.Count;i++)
                    {
                        //construct a string containing the new values
                        values += this.stringColumns[i]+"="+row.Cells[i].Value;

                        if (i < row.Cells.Count - 1) values += ",";
                    }

                    //update the row
                    upload_cmd = Utility.getSqlCommand(values);
                    int result = upload_cmd.ExecuteNonQuery();

                    if (result == -1) //no rows updated
                        throw new Exception("Failed to update database table");
                }
            }
            catch (Exception exception)
            {
                Utility.DisplayError("DataBaseTableEditor_failed_to_upload_database_table",exception,"DataBaseTableEditor: failed to upload the database table into the database: "+exception.ToString(),false);
            }
        }
    }
}
