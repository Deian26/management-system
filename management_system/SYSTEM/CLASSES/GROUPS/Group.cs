using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace management_system
{
    //class to handle groups
    public class Group
    {
        //fields
        private string name, //group name
                       author, //author
                       database; //the database where this group can be found
        private DateTime creationDate; //the date and time this group was created
        private int key; //cryptography key
        private bool adminGroup; //true if the group was created by an administrator, false otherwise
        private Image icon; //group icon

        //files
        private string localFilesPath; //filePath to the folder containing local files
        private List<string> localFileNames = new List<string>(); //local file names
        private List<string> databaseFileNames = new List<string>(); //names of the files stored in the database

        //constructors
        public Group(string name, string author, DateTime creationDate, string database, bool adminGroup, Image icon)
        {
            //store group details
            this.name = name;
            this.author = author;
            this.creationDate = creationDate;
            this.database = database;
            this.adminGroup = adminGroup;
            this.icon= icon;

            //compute key
            this.computeKey(this.creationDate);

            //store the filePath to the local files folder
            this.localFilesPath = Utility.dirPathDATA + "\\Group_"+this.name;

            //load local file names into memory
            this.loadLocalFileNames(this.localFilesPath);

            //load database file names into memory
            this.loadDbFileNames();
        }

        //getters
        public string getName()
        {
            return this.name;
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

        //load the file names of the locally stored files
        private void loadLocalFileNames(string localFilesFolder)
        {
            try
            {

                IEnumerable<string> localFileNames = Directory.EnumerateFiles(localFilesFolder);

                this.localFileNames.Clear();

                //load the file names into the localFileNames list
                foreach (string localFileName in localFileNames)
                {
                    this.localFileNames.Add(localFileName);
                }

            }catch (Exception exception)
            {
                MessageBox.Show(Utility.displayError("Groups_failed_to_load_local_fileNames")+exception.Message,"Groups",MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utility.logDiagnsoticEntry("Failed to load local file names");
                Utility.ERR = true;
                Utility.WARNING = true;

                Start.f0_logIn.F0_timer_errorClear.Stop();
                Start.f0_logIn.F0_timer_errorClear.Start();
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
                MessageBox.Show(Utility.displayError("Groups_failed_to_compute_key") + exception.Message, "Groups", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utility.logDiagnsoticEntry("Failed to compute the group's cryptographic key");
                Utility.ERR = true;
                Utility.WARNING = true;

                Start.f0_logIn.F0_timer_errorClear.Stop();
                Start.f0_logIn.F0_timer_errorClear.Start();
            }
        }
        
    }
}
