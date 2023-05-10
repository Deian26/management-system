using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace management_system
{   
    /*struct to temporarily store data about a group (until a Group object can be created - mainly used to avoid multiple data readers having to be opened
    at once or having functions called outside the constructors) */
    public struct tempGroup
    {
        //fields matching the details about the group found in the indexing database table (connected database) and the database itself
        
        //VARIABLES
        private string name;
        private string author;
        private string dateCreated;
        private int adminGroup;
        private Image icon;
        private int key;

        public string database;

        //CONSTRUCTORS
        public tempGroup(string name, string author, string dateCreated, int adminGroup, Image icon, int key, string database)
        {
            try
            {
                this.name = name;
                this.author = author;
                this.dateCreated = dateCreated;
                this.adminGroup = adminGroup;
                this.icon = icon;
                this.key = key;

                this.database = database;
            }catch (Exception exception)
            {
                this.key = 0;
                this.name = null;
                this.author = null;
                this.database = null;
                this.adminGroup = 0;
                this.dateCreated = null;
                this.icon=null;
                
                Utility.DisplayError("Groups_failed_to_add_to_temporary_memory", exception, "Group: Failed to add group details from the database into the temporary memory struct tempGroup: " + exception.ToString(), false);
            }
        }

        //OTHER METHODS
        
        //getters
        public string getName()
        {
            return this.name;
        }

        public string getAuthor()
        {
            return this.author;
        }

        public string getDatabase()
        {
            return this.database;
        }

        public int getKey()
        {
            return this.key;
        }

        public Image getIcon()
        {
            return this.icon;
        }

        public int getAdminGroup()
        {
            return this.adminGroup;
        }

        public string getDateCreated()
        {
            return this.dateCreated;
        }


        //clear current structure
        public void clear()
        {
            try
            {
                this.key = 0;
                this.name = null;
                this.author = null;
                this.database = null;
                this.adminGroup= 0;
                this.dateCreated = null;
                this.icon.Dispose();
                
            }catch(Exception exception)
            {
                this.icon = null; //backup
                Utility.DisplayError("Groups_failed_to_clear_temporary_memory",exception, "Group: Failed to clear the temporary memory structure tempGroup: "+exception.ToString(),false);
            }
        }

    }

    //class to handle groups
    public class Group
    {
        //fields
        private string name, //group name
                       author, //author
                       database; //the database where this group can be found
        private DateTime creationDate; //the date and time this group was created
        private int key; //cryptographic key
        private bool adminGroup; //true if the group was created by an administrator, false otherwise
        private Image icon; //group icon

        //files
        private string localFilesPath; //filePath to the folder containing local files
        private List<string> localFileNames = new List<string>(); //local file names
        private List<string> databaseFileNames = new List<string>(); //names of the files stored in the database

        //constructors

        //new group
        public Group(string name, string author, DateTime creationDate, string database, bool adminGroup, Image icon)
        {
            //create a local folder for the group if it does not exist already (the group would not be new)
            if (!Directory.Exists(localFilesPath))
            {
                //store group details
                this.name = name;
                this.author = author;
                this.creationDate = creationDate;
                this.database = database;
                this.adminGroup = adminGroup;
                this.icon = icon;

                //store the filePath to the local files folder
                this.localFilesPath = Utility.dirPathDATA + "\\" + Utility.username + "\\Group_" + this.name;
                this.localFilesPath = Path.GetFullPath(this.localFilesPath);

                Directory.CreateDirectory(localFilesPath);

                //compute key
                this.computeKey(this.creationDate);

                //store the group's details into the indexing database table (currently connected database)
                this.storeGroupDetailsIntoIndexingTable();

                //create a GroupName_DatabaseFiles table to store group files in the database
                this.createGroupDatabaseFilesTable();
            }
            else
            {
                Utility.DisplayError("Groups_group_exists_locally", new Exception(""), "Group: The group " + name + " already has a local folder path and thus cannot be created.", false);
            }

        }

        //already created group (load details into memory only)
        public Group(string name, string author, string dateCreated, int adminGroup, /*object icon,*/ int key, string database) //DEV
        {
            //store details into memory
            //this.icon = icon; //DEV - change this
            this.name = name;
            this.adminGroup = adminGroup == 1;
            this.author = author;
            this.creationDate = DateTime.Parse(dateCreated);
            this.key = key;
            this.database = database;

            //store the filePath to the local files folder
            this.localFilesPath = Utility.dirPathDATA +"\\"+Utility.username+ "\\Group_" + this.name;
            this.localFilesPath = Path.GetFullPath(this.localFilesPath);

            //load the names of the locally stored files
            this.loadLocalFileNames(this.localFilesPath);

            //load the names of the files in this group which are stored in the connected database
            this.loadDbFileNames();
        }

        //getters
        public string getName()
        {
            return this.name;
        }

        public string getLocalFilesPath()
        {
            return this.localFilesPath;
        }

        public DateTime getCreationDate()
        {
            return this.creationDate;

        }

        public string getAuthor()
        {
            return this.author;
        }

        public string getDatabase()
        {
            return this.database;
        }

        public bool getAdminGroup()
        {
            return this.adminGroup;
        }

        public int getKey()
        {
            return this.key;
        }

        public Image getIcon()
        {
            return this.icon;
        }

        //other methods

        //creates a table in the connected database (naming: Group_DatabaseFiles) which can store the group files
        private void createGroupDatabaseFilesTable()
        {
            try
            {
                SqlCommand createTable = Utility.getSqlCommand("CREATE TABLE " + this.name + "_DatabaseFiles (filename nvarchar(50), _file varbinary(MAX) )");

                //create table
                createTable.ExecuteNonQuery();
            }catch(Exception exception) 
            {
                Utility.DisplayError("Groups_failed_to_create_databaseFiles_table", exception, "Group: Failed to create the DatabaseFiles table in the connected database: " + exception.ToString(), false);
            }
        }

        //store the current group's details into the indexing table (in the connected database)
        private void storeGroupDetailsIntoIndexingTable()
        {
            try
            {
                int aux_adminGroup = 0;
                aux_adminGroup = this.adminGroup ? 1 : 0; //1=admin group, 0=not an admin group

                //DEV - chage the storing of the group's icon here
                SqlCommand addDetails = Utility.getSqlCommand("INSERT INTO GroupIndex (name,author,dateCreated,adminGroup,cryptographicKey) VALUES('" + Utility.ENC_GEN(this.name,Utility.genKey) + "','" + Utility.ENC_GEN(this.author,Utility.genKey) + "','" + Utility.ENC_GEN(this.creationDate.ToString(),Utility.genKey) + "',"+(aux_adminGroup+Utility.genKey).ToString()/* + Utility.ENC_GEN(this.icon.ToString(),Utility.genKey) + ","*/ + ","+ Utility.ENC_GEN(this.key,Utility.genKey).ToString() + ")");
                
                addDetails.ExecuteNonQuery(); //add values into the database indexing table

                addDetails.Dispose();
                
            }catch(Exception exception)
            {
                Utility.DisplayError("Groups_failed_to_store_details_into_indexing_table",exception,"Group: Failed to store the group details into the indexing database table (currently connected database): "+exception.ToString(),false);
            }
        }

        //load the file names of the locally stored files
        private void loadLocalFileNames(string localFilesFolder)
        {
            try
            {
                if (Directory.Exists(localFilesFolder)) //the local folder exists
                {
                    IEnumerable<string> localFileNames = Directory.EnumerateFiles(localFilesFolder);

                    this.localFileNames.Clear();

                    //load the file names into the localFileNames list
                    foreach (string localFileName in localFileNames)
                    {
                        this.localFileNames.Add(localFileName);
                    }
                }

            }catch (Exception exception)
            {
                Utility.DisplayError("Groups_failed_to_load_local_fileNames", exception, "Group: Failed to load local database files", false);
            }
        }

        //load the file names of the files stored in the database
        private void loadDbFileNames()
        {
            try
            {
                SqlCommand cmd = Utility.getSqlCommand("SELECT * FROM " + this.name + "_DatabaseFiles");
                SqlDataReader dr = cmd.ExecuteReader();

                this.databaseFileNames.Clear();
                
                //load file names
                while (dr.Read())
                {
                    //table layout
                    //fileName | fileExtension | date created | author

                    this.databaseFileNames.Add(dr.GetString(0));
                }
                dr.Close();
                cmd.Dispose();
            }catch (Exception exception)
            {
                MessageBox.Show(Utility.displayError("Groups_failed_to_load_database_fileNames") + exception.Message, "Groups", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utility.logDiagnsoticEntry("Failed to load database file names");
                Utility.ERR = true;
                Utility.WARNING = true;

                Start.f0_logIn.F0_timer_errorClear.Stop();
                Start.f0_logIn.F0_timer_errorClear.Start();
            }
        }
        
        //compute the group's cryptographic key
        private void computeKey(DateTime creationDate)
        {
            try
            {
                this.key = (int)(creationDate.Ticks) % 100;
            }
            catch (Exception exception)
            {
                Utility.DisplayError("Groups_failed_to_compute_key", exception, "Group: Failed to compute key: " + exception.ToString(), false);
            }
        }
        
    }
}
