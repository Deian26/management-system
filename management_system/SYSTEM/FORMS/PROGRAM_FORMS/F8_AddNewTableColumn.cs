using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace management_system
{
    public partial class F8_AddNewTableColumn : Form
    {
        //VARIABLES
        private string tableName = "";

        //CONSTRUCTORS
        public F8_AddNewTableColumn(string tableName)
        {
            InitializeComponent();

            //store the name of the currently selected database table
            this.tableName = tableName;
        }

        //UTILITY METHODS
        //check column name validity
        private bool columnNameValid(string columnName)
        {
            if (columnName == null || columnName.Equals(""))
                return false; //invalid name

            foreach(char c in columnName) 
            {
                if ((c < 'a' || c > 'z') &&
                   (c < 'A' || c > 'Z') &&
                   (c < '0' || c > '9') &&
                   Utility.filename_allowedSpecialCharacters.Contains(c) == false
                    )
                    return false; //invalid name
            }

            return true; //valid name
        }

        //check the validity of the specified data type
        private bool validColumnDataType(string columnDataType)
        {
            if (columnDataType == null || columnDataType.Equals(""))
                return false; //invalid data type



            return true; //possibly valid data type
        }

        //EVENT HANDLERS
        private void F8_button_addNewColumn_Click(object sender, EventArgs e)
        {
            try
            {
                //check new column name validity
                string newColumnName = this.F8_textBox_newColumnName.Text;
                string newColumnDataType = this.F8_textBox_newColumnDataType.Text;

                if (this.columnNameValid(newColumnName) == true)
                {
                    if (this.validColumnDataType(newColumnDataType) == true)
                    {
                        SqlCommand addColumn = Utility.getSqlCommand("ALTER TABLE " + this.tableName + " ADD " + newColumnName + " " + newColumnDataType + " NULL");
                        addColumn.ExecuteNonQuery();

                        addColumn.Dispose();
                    }
                }

            }catch(Exception exception)
            {
                Utility.DisplayError("DataBaseTableEditor_failed_to_add_new_column_to_table_invalid_name", exception, "DataBaseTableEditor: Failed to add a new column: Invalid new column name or Database error; details: \n" + exception.ToString(), false);
            }
        }
    }
}
