using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace management_system
{
    /* Class used to contain tools for database table editing
     */
    public partial class F10_DatabaseTableTools : Form
    {
        //VARIABLES
        private F5mdi3_DatabaseTableEditor f5Mdi3_DatabaseTableEditor = null;

        //CONSTRUCTORS
        public F10_DatabaseTableTools(F5mdi3_DatabaseTableEditor f5Mdi3_DatabaseTableEditor)
        {
            InitializeComponent();

            //store table editor form
            this.f5Mdi3_DatabaseTableEditor = f5Mdi3_DatabaseTableEditor;

            //initialize instrument list
            this.F10_comboBox_databaseTableTools.Items.Clear();

            this.F10_comboBox_databaseTableTools.Items.Add(Utility.displayMessage("DataBaseTableEditor_instrument_searchColumn"));

            this.F10_comboBox_databaseTableTools.SelectedIndex = 0;
        }

        //OTHER METHODS

        //display instrument action results
        public void display_searchColumn(DataRow row, DataColumnCollection columns)
        {
            //add columns to the data grid results view
            

            foreach (DataColumn col in columns)
                this.F10_dataGridView_searchColumn_results.Columns.Add(col.ColumnName,col.ColumnName);

            //add row to the data grid results view
           
            this.F10_dataGridView_searchColumn_results.Rows.Add(row.ItemArray);
        }

        //EVENT HANDLERS
        private void F10_DatabaseTableTools_Load(object sender, EventArgs e)
        {
            //hide unused instruments
            this.F10_panel_searchColumn.Visible = false;

            //set language
            Utility.setLanguage(this);

            
        }

        //instrument: search value in a column

        //start search
        private void F10_button_searchColumn_startSearch_Click(object sender, EventArgs e)
        {
            //initialize data grid values
            this.F10_dataGridView_searchColumn_results.Columns.Clear();
            this.F10_dataGridView_searchColumn_results.Rows.Clear();

            //start column search
            this.f5Mdi3_DatabaseTableEditor.instrument_searchColumn(this.F10_textBox_searchColumn_columnName.Text, this.F10_textBox_searchColumn_valueToSearch.Text);
        }

        //change instrument
        private void F10_comboBox_databaseTableTools_SelectedIndexChanged(object sender, EventArgs e)
        {
            //hide instruments
            this.F10_panel_searchColumn.Visible = false;

            //display instruments
            switch (this.F10_comboBox_databaseTableTools.SelectedIndex)
            {
                case 0: //search value in column
                    this.F10_panel_searchColumn.Visible = true;
                    
                    break;


                default:
                    break;
            }

        }
    }
}
