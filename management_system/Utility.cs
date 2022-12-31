using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Xml.Schema;
using System.Windows.Forms.VisualStyles;
using System.Drawing.Text;
using System.IO;

namespace management_system
{
    public static class Utility
    {
        //VARIABLES

        //error flag
        public static bool ERR = false;
        //warning flag
        public static bool WARNING = false;

        //dictionaries
        public static Dictionary<string, string> errors = new Dictionary<string, string>(); //error messages
        public static Dictionary<string, string> messages = new Dictionary<string, string>(); //non-error messages
        public static Dictionary<int, string> language_list = new Dictionary<int, string>(); //language list
        public static Dictionary<int, string> theme_list = new Dictionary<int, string>(); //theme list
        public static List<string> databases_list = new List<string>(); //databases list
        private static Dictionary<string, string> notifications = new Dictionary<string, string>(); //active notifications
        private static Dictionary<string, string> f1_greetings = new Dictionary<string, string>(); //greetings for F1_MainForm
        
        public static string username= null;
        public static bool admin = false;
        public static int key = 0;
        public static int language = 0; //default language: English (EN)
        public static int theme = 0; //default theme: Lite (LITE)

        //XML documents
        private static string XML_errors = "..\\..\\SYSTEM\\SETTINGS\\XML_errors.xml";
        private static string XML_messages = "..\\..\\SYSTEM\\SETTINGS\\XML_messages.xml";
        private static string XML_languages = "..\\..\\SYSTEM\\SETTINGS\\XML_languagePack.xml";
        private static string XML_databases = "..\\..\\SYSTEM\\SETTINGS\\XML_databases.xml";
        private static string XML_preferences = "..\\..\\SYSTEM\\SETTINGS\\XML_preferences.xml";
        private static string XML_themes = "..\\..\\SYSTEM\\SETTINGS\\XML_themePack.xml";

        //database connection
        // "Data Source=DEIAN-PC\\WINCC;Initial Catalog = Management_system; Integrated Security = True"; //DEV
        private static SqlConnection conn = null;
        
        public static string DB_name = "";
        public static string DB_connString = "";

        //timer interval (ms)
        public static int clearErrTimeInterval = 100; //ms
        public static int clearWarningTimeInterval = 5000; //ms; timer started when the error timer is stopped

        //functions / methods

        //initialization
        public static int Initialize()
        {
            if (Utility.ERR == true) return 0; //exit the function if errors were detected

            //reset stored values
            Utility.errors.Clear();
            Utility.notifications.Clear();
            Utility.f1_greetings.Clear();

            //load values from stored files / the DataBase
            Utility.getLanguages(); //get the language names
            Utility.getErrors(); //get error messages
            Utility.getMessages(); //get non-error messages
            Utility.setPreferences(); //set the preferences saved in the XML file
            Utility.getErrors(); //get error messages in the selected language
            Utility.getThemes(); //get theme names
            Utility.getDataBases(); //get the listed databases
            


            if (Utility.ERR == true) return 0; //exit the function if errors were detected

            return 1;
        }

        #region utility
        //utility functions

        //converts a givne input into a byte array
        public static byte[] toByteArray(string input)
        {
            byte[] output = new byte[input.Length];
            int i = 0;

            if (input.Equals("") && input.Length == 0)
                return new byte[0x00]; //error

            foreach (char c in input)
            {
                try
                {
                    output[i] = Convert.ToByte((int)c);
                    i++;
                }catch (Exception exception)
                {
                    MessageBox.Show(Utility.displayError("Code_invalid_byte_digit") + exception.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error); //display an error message
                    Utility.ERR = true;
                    Utility.WARNING = true;
                    Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                    Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared
                }

            }

            return output;
        }


        //checks the format and signature of the specified XML file
        public static bool checkXML(string XML_path)
        {
            
            XmlDocument xml = null;
            XmlNode root = null;
            RSA rsa = RSA.Create();
            string XML_string = null;
            bool checkSignature = false;
            
            try
            {
                //check if the document can be opened and the root node selected
                xml = new XmlDocument();
                 root = xml.DocumentElement;
                //open the XML file as a text file and read the content
                XML_string = File.ReadAllText(XML_path); 

            }
            catch (Exception exception)
            {
                MessageBox.Show(Utility.displayError("XML_format_error") + exception.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error); //display an error message
                Utility.ERR = true;
                //Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                //Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared
                Application.Exit(); //trigger an application exit
                return false;
            }

            //check the data signature
            try
            {
                checkSignature = rsa.VerifyData(Utility.toByteArray(XML_string), Utility.toByteArray(root.Attributes[0].Value), HashAlgorithmName.SHA512, RSASignaturePadding.Pss);
                
                if(checkSignature == false)
                {
                    MessageBox.Show(Utility.displayError("XML_file_invalid_signature"), "SECURITY ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); //display an error message
                    Utility.ERR = true;
                    //Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                    //Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared
                    Application.Exit(); //trigger an application exit
                    return false;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(Utility.displayError("XML_format_error") + exception.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); //display an error message
                Utility.ERR = true;
                //Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                //Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared
                Application.Exit(); //trigger an application exit
                return false;
            }
            return true;
        }
        #endregion

        #region database
        //get the databases from the XML file
        public static bool getDataBases()
        {
            XmlDocument xml = null;
            XmlNode root = null;

            try
            {
                xml = new XmlDocument();
                xml.Load(Utility.XML_databases);
                root = xml.DocumentElement;

            }catch (Exception exception)
            {
                MessageBox.Show(Utility.displayError("DB_load_databases_failed")+exception.ToString(),"",MessageBoxButtons.OK,MessageBoxIcon.Error);
                Utility.ERR = true;
                //Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                //Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared
                Application.Exit();
                return false;
            }

            //save the database paths
            try
            {
                foreach (XmlNode database in root.ChildNodes)
                    Utility.databases_list.Add(database.InnerText);
            }catch (Exception exception)
            {
                MessageBox.Show(Utility.displayError("XML_format_error") + exception.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utility.ERR = true;
                //Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                //Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared
                Application.Exit();
                return false;
            }
            return true;
        }

        //save the current credentials
        public static void setCredentials(string username, int key, bool admin)
        {
            if (Utility.ERR == true) return; //exit the function if errors were detected

            Utility.username = username;
            Utility.admin= admin;
            Utility.key = key;


        }
        //connect to the database
        public static bool DB_connect(string connString)
        {
            connString += "; Integrated Security = True"; //add connection options

            Utility.DB_connString = connString;

            if (Utility.ERR == true) return false; //exit the function if errors were detected

            try
            {
                if (connString.Equals("") || connString == null || connString.Trim().Equals("; Integrated Security = True")) throw new Exception("Connection strin = null");

                Utility.conn = new SqlConnection(connString); //connect to the given database
                Utility.conn.Open();
                
                return true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(Utility.displayError("DB_conn_failed")+exception.ToString(),"",MessageBoxButtons.OK,MessageBoxIcon.Error); //display an error message
                Utility.ERR = true;
                //Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                //Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared
                Application.Exit(); //trigger an application exit
                return false;
            }
        }

        //disconnect from the database
        public static void DB_disconnect()
        {
            try
            {
                if (Utility.conn != null) Utility.conn.Close();
                

            }catch (Exception exception)
            {
                MessageBox.Show(Utility.displayError("DB_disconnect_failed")+exception.ToString(),"",MessageBoxButtons.OK,MessageBoxIcon.Error); //display error
                Utility.ERR = true;
                Utility.WARNING = true;

                Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared
            }
        }

        //log into the system
        public static int logIn(string username, string password)
        {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            string hashed_username = Utility.DB_HASH(username);
            string hashed_password = Utility.DB_HASH(password);
            try
            {
                cmd = new SqlCommand("SELECT * FROM Users WHERE username='" + hashed_username + "' AND password='" + hashed_password + "'", Utility.conn);
                dr = cmd.ExecuteReader();
            }catch (Exception exception)
            {
                MessageBox.Show(Utility.displayError("SQL_statement_error")+exception.ToString(),"",MessageBoxButtons.OK,MessageBoxIcon.Error);
                Utility.ERR = true;
                //Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                //Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared
                dr.Close();
                Application.Exit();
                return -1;
            }
                
            int rows = 0;

            try
            {
                while (dr.Read())
                {
                    if (hashed_username.Equals(dr.GetString(0)) && hashed_password.Equals(dr.GetString(1))) //username and password
                    {
                        if (dr.GetString(0).ElementAt(dr.GetString(0).Length - 2).Equals('#') && dr.GetString(0).ElementAt(dr.GetString(0).Length - 1).Equals('A')) //access; admin: username#A
                        {
                            dr.Close();
                            return 0; //admin
                        }
                        if (dr.GetString(0).Contains('#')) //error
                        {
                            dr.Close();
                            return -1;
                        }

                        dr.Close();
                        return 1; //user
                    }

                    rows++;
                    if (rows >= 2)
                    {
                        MessageBox.Show(Utility.displayError("Invalid_credentials"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Utility.ERR = true;
                        Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                        Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared
                        dr.Close();
                        Application.Exit();
                        return -1; //multiple accounts found
                    }
                }

                dr.Close();
                return -1; //no account found
            }catch (Exception exception)
            {
                MessageBox.Show(Utility.displayError("SQL_table_format_error")+exception.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utility.ERR = true;
                //Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                //Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared
                dr.Close();
                Application.Exit();
                return -1; //multiple accounts found
            }
        }


        //get tha last database the application was connected to
        public static string getLastDataBaseConnString()
        {
            return Utility.DB_connString;
        }

        //create an SQL command
        public static SqlCommand getSqlStatement(string SQL_command)
        {
            SqlCommand cmd = null;

            try
            {
                cmd = new SqlCommand(SQL_command, Utility.conn);
            }catch (Exception exception)
            {
                Utility.ERR = true;
                Start.f0_logIn.F0_timer_errorClear.Stop();
                Start.f0_logIn.F0_timer_errorClear.Start();
                MessageBox.Show(Utility.displayError("DB_conn_failed") +exception.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            return cmd;
        }
        #endregion

        #region encryption
        //encryption functions

        //hash (usernames and passwords)
        public static string DB_HASH(string input)
        {
            SHA512 sha = SHA512Managed.Create();
            string output="";
            byte[] byte_input = new byte[0x00];

            sha.Initialize();
            byte_input = Utility.toByteArray(input);

            output = Convert.ToBase64String(sha.ComputeHash(byte_input));

            return output;
        }

        //Used to encrypt DataBase specific data (except usernames and passwords)
        public static string DB_ENC(string input, int key)
        {
            string output = "";
            int sgn = 1;
            char[] txt = input.ToCharArray();

            for (int i = 0; i < txt.Length; i++)
            {
                txt[i] = (char)(txt[i] + (char)(sgn * key));
                sgn *= -1;
            }

            foreach (char c in txt)
                output += c;

            return output;
        }

        //Used to encrypt general data (such as XML files, notifications from the DataBase etc.)
        public static string ENC_GEN(string input, int key)
        {
            string output = "";
            RSA rsa = RSA.Create();


            input.ToCharArray();

            return output;
        }

        //decryption functions

        //Used to decrypt DataBase specific data such as usernames and passwords (complementary to DB_ENC() )
        public static string DB_DEC(string input, int key)
        {
            string output = "";
            int sgn = -1;
            char[] txt = input.ToCharArray();

            for (int i = 0; i < txt.Length; i++)
            {
                txt[i] = (char)(txt[i] + (char)(sgn * key));
                sgn *= -1;
            }

            foreach (char c in txt)
                output += c;

            return output;
        }

        //Used to decrypt general data such as XML files, notifications from the DataBase etc. (complementary to ENC_GEN)
        public static string DEC_GEN(string input, int key)
        {
            string output = "";


            return output;
        }

        #endregion

        //getters & setters
        #region messages
        //error messages
        //load error messages from the XML_errors document
        public static void getErrors()
        {
            if (Utility.ERR == true) return; //exit the function if errors were detected

            //clear the current dictionary
            Utility.errors.Clear();

            XmlDocument xml = null;
            XmlNode root = null;

            try
            {
                xml = new XmlDocument(); //open the XML document and load into the 'xml' variable
                xml.Load(XML_errors);
                root = xml.DocumentElement;

                if (root == null) throw new Exception("EN: NO ROOT ELEMENT FOR THE ERROR MESSAGE XML FILE");

            }catch (Exception exception)
            {
                MessageBox.Show("EN: ERROR LOADING ERROR MESSAGES; CHECK THE XML FILE UNDER management_system\\SYSTEM\\SETTINGS\\XML_errors.xml; Details: "+exception.ToString(),"ENGLISH",MessageBoxButtons.OK,MessageBoxIcon.Error); //display a generic, english error message for an error loading error messages
                Utility.ERR = true;
                Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared
                Application.Exit(); //trigger an application exit
                return;
            }

            try
            {
                foreach (XmlNode node_language in root.ChildNodes) //search through the listed languages
                    if (node_language.Name.Equals(Utility.language_list[Utility.language])) //select the language set by the user
                        foreach (XmlNode translation in node_language.ChildNodes)
                        {
                            Utility.errors.Add(translation.Name, translation.InnerText); //save the error message in the 'errors' dictionary
                        }

            }catch (Exception exception) 
            {
                MessageBox.Show("EN: ERROR WITH THE XML FORMAT FOR THE ERROR MESSAGES; CHECK THE XML FILE UNDER management_system\\SYSTEM\\SETTINGS\\XML_errors.xml; Details: " + exception.ToString(), "ENGLISH", MessageBoxButtons.OK, MessageBoxIcon.Error); //display a generic, english error message for an error loading error messages
                Utility.ERR = true;
                Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared
                Application.Exit(); //trigger an application exit
                return;
            }
        }
        
        //non-error messages
        //load non-error messaged
        public static void getMessages()
        {
            if (Utility.ERR == true) return; //exit the function if errors were detected

            //clear the current dictionary
            Utility.messages.Clear();

            XmlDocument xml = null;
            XmlNode root = null;

            try
            {
                xml = new XmlDocument(); //open the XML document and load into the 'xml' variable
                xml.Load(XML_messages);
                root = xml.DocumentElement;

                if (root == null) throw new Exception("root=null");

            }
            catch (Exception exception)
            {
                MessageBox.Show(Utility.displayError("XML_format_error")+exception.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error); //display a generic, english error message for an error loading error messages
                Utility.ERR = true;
                if (Start.f0_logIn != null)
                {
                    //Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                    //Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared
                }
                Application.Exit(); //trigger an application exit
                return;
            }

            try
            {
                foreach (XmlNode node_language in root.ChildNodes) //search through the listed languages
                    if (node_language.Name.Equals(Utility.language_list[Utility.language])) //select the language set by the user
                        foreach (XmlNode translation in node_language.ChildNodes)
                        {
                            Utility.messages.Add(translation.Name, translation.InnerText); //save the error message in the 'errors' dictionary
                        }

            }
            catch (Exception exception)
            {
                MessageBox.Show("EN: ERROR WITH THE XML FORMAT FOR THE MESSAGES; CHECK THE XML FILE UNDER management_system\\SYSTEM\\SETTINGS\\XML_errors.xml; Details: " + exception.ToString(), "ENGLISH", MessageBoxButtons.OK, MessageBoxIcon.Error); //display a generic, english error message for an error loading error messages
                Utility.ERR = true;
                //Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                //Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared
                Application.Exit(); //trigger an application exit
                return;
            }
        }

        //display the error message associated with the given key
        public static string displayError(string key)
        {
            if (key.Equals("") || key == null) return "EN: ERROR MESSAGE NOT FOUND; KEY: "+key.ToString();

            try
            {
                return Utility.errors[key]; //get the error message from the dictionary
            }catch(Exception exception) 
            {
                MessageBox.Show("EN: ERROR MESSAGE NOT FOUND; KEY:"+key.ToString() + " : "+ exception.ToString(), "ENGLISH", MessageBoxButtons.OK, MessageBoxIcon.Error); //display a generic, english error message for an error loading error messages
                Utility.ERR = true;
                Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared
                //Application.Exit(); //trigger an application exit
                return "#ERR#";
            }
        }

        //display the non-error message associated with the given key
        public static string displayMessage(string key)
        {
            if (key.Equals("") || key == null) return "EN: MESSAGE NOT FOUND; KEY: " + key.ToString();

            try
            {
                return Utility.messages[key]; //get the error message from the dictionary
            }
            catch (Exception exception)
            {
                MessageBox.Show("EN: MESSAGE NOT FOUND; KEY:" + key.ToString() + " : " + exception.ToString(), "ENGLISH", MessageBoxButtons.OK, MessageBoxIcon.Error); //display a generic, english error message for an error loading error messages
                Utility.ERR = true;
                Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared
                //Application.Exit(); //trigger an application exit
                return "#ERR#";
            }
        }
        #endregion

        #region notifications
        //update notifications list
        public static void getNotifications(string user, int key)
        {
            if (Utility.ERR == true) return; //exit the function if errors were detected

            //DEV
            /* General Table structure:
             ID | notification_text | read(0/1 / false/true)
             */
            if (conn!=null && conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand cmd = null;
                SqlDataReader dr = null;

                string str_cmd = "select * from " + user + "_notifications";
                List<string> notif = new List<string>();

                try
                {
                    cmd = new SqlCommand(str_cmd, conn);
                    dr = cmd.ExecuteReader();
                }catch (Exception exception)
                {
                    
                    MessageBox.Show(Utility.displayError("SQL_statement_error")+exception.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Utility.ERR = true;
                    //Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                    //Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared
                    Application.Exit();
                    return;
                }

                try
                {
                    while (dr.Read() && !dr["notification"].ToString().Equals("") && dr["notification"].ToString() != null)
                    {
                        notif.Add(dr["notification"].ToString());
                    }

                    dr.Close();
                    cmd.Dispose();
                }catch (Exception exception)
                {
                    Utility.ERR = true;
                    //Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                    //Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared
                    MessageBox.Show(Utility.displayError("SQL_table_format_error") + exception.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                    return;
                }
            }
            else
            {
                Utility.ERR = true;
                //Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                //Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared
                MessageBox.Show(Utility.displayError("DB_conn_failed"),"",MessageBoxButtons.OK,MessageBoxIcon.Error);
                Application.Exit();
                return;
            }
        }
        #endregion //DEV

        #region language
        //get languages and save them into a dictionary
        public static void getLanguages()
        {
            XmlDocument xml = null;
            XmlNode root = null;
            int index = 0;

            //clear the dictionary
            Utility.language_list.Clear();

            try
            {
                xml = new XmlDocument(); //open the XML document and load into the 'xml' variable
                xml.Load(XML_languages);
                root = xml.DocumentElement;
            }
            catch (Exception exception)
            {

                MessageBox.Show(Utility.displayError("Load_languages_failed") + exception.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error); //display an error message
                Utility.ERR = true;
                //Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                //Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared
                Application.Exit(); //trigger an application exit
                return;
            }

            //save the langage names into the dictionary
            try
            {
                foreach (XmlNode language in root.ChildNodes)
                {
                    Utility.language_list.Add(index, language.Name);
                    index++;
                }
            }catch (Exception exception)
            {
                MessageBox.Show(Utility.displayError("XML_format_error")+exception.ToString(),"",MessageBoxButtons.OK,MessageBoxIcon.Error);
                Utility.ERR = true;
                //Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                //Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared
                Application.Exit(); //trigger an application exit
                return;
            }
        }
        //set the language for the current form
        public static void setLanguage(Form currentForm)
        {
            if (Utility.ERR == true) return; //exit the function if errors were detected
            if (currentForm == null) return; //error

            XmlDocument xml = null;
            XmlNode root = null;
            Control[] controls = null; //auxiliary variables to store the controls found when searching by name
            //used in the pseudo-random selection of a text from multiple options; A CONTROL WITH MULTIPLE TEXT OPTIONS WILL HAVE ONE CHOSEN AT PSEUDO-RANDOM; '#' MARKS A VARIABLE IN THE TEXT (Variables supported: #USERNAME#, #NUMBER_NOTIFICATIONS# )
            Random rnd = new Random(); 
            List<string> text_options = new List<string>();
            string variable_text = "";

            try
            {
                xml = new XmlDocument(); //open the XML document and load into the 'xml' variable
                xml.Load(Utility.XML_languages);
                root = xml.DocumentElement;
            }
            catch (Exception exception)
            {

                MessageBox.Show(Utility.displayError("Load_languages_failed") + exception.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error); //display an error message
                Utility.ERR = true;
                //Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                //Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared
                Application.Exit(); //trigger an application exit
                return;
            }

            try
            {
                foreach (XmlNode node_language in root.ChildNodes) //search through the listed languages
                    if (node_language.Name.Equals(Utility.language_list[Utility.language])) //select the language set by the user
                        foreach (XmlNode form in node_language.ChildNodes) //search through the listed forms
                            if (form.Name.Equals(currentForm.Name)) //select the current form
                                foreach (XmlNode translation in form.ChildNodes) //load the translations for the controls in the current form
                                {
                                    if (translation.ChildNodes.Count > 1) //the control with multiple texts given for the selected language
                                    {
                                        foreach (XmlNode alternative_text in translation.ChildNodes) //search through the given text options and choose one at pseudo-random
                                        {
                                            text_options.Add(alternative_text.InnerText); //save the text options in a list
                                        }

                                        //choose an option from the list and set the control text to it
                                        controls = currentForm.Controls.Find(translation.Name, true); //store the found controls in the variable 'controls'

                                        foreach (Control ctrl in controls) //rename each control according to the translation
                                        {
                                            variable_text = text_options[rnd.Next()%text_options.Count];

                                            if (variable_text.Contains('#')) //replace any potential variables with their corresponding values
                                            {
                                                //UPDATE

                                                /*SUPPORTED VARIABLES (AS OF 25-DEC-2022)
                                                 * #USERNAME# - username
                                                 * #NUMBER_NOTIFICATIONS# - the number of currently unread notifications
                                                 */

                                                variable_text.Replace("#USERNAME#", Utility.username);
                                                variable_text.Replace("#NUMBER_NOTIFICATIONS#", Utility.notifications.Count.ToString());
                                            }

                                            ctrl.Text = variable_text;
                                        }
                                        
                                        if(controls.Length == 0 ) //no controls found
                                        {
                                            MessageBox.Show(Utility.displayError("XML_language_control_not_found") + translation.Name, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                    }
                                    else //the control only has one text for the selected language
                                    {
                                        controls = currentForm.Controls.Find(translation.Name, true); //store the found controls in the variable 'controls'

                                        foreach (Control ctrl in controls) //rename each control according to the translation
                                            ctrl.Text = translation.InnerText;

                                        if (controls.Length == 0) //no controls found
                                        {
                                            MessageBox.Show(Utility.displayError("XML_language_control_not_found") + translation.Name, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }

                                    }
                                }
            }catch (Exception exception)
            {
                MessageBox.Show(Utility.displayError("XML_format_error") + exception.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error); //display an error message
                Utility.ERR = true;
                //Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                //Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared
                Application.Exit(); //trigger an application exit
                return;
            }
                                                    
        }

        //get the next language in the list (after the currently selected one)
        public static void nextLanguage(Form form)
        {
            //select the next language
            if (Utility.language < Utility.language_list.Count - 1) Utility.language++;
            else Utility.language = 0;

            //load errors in the new language
            Utility.getErrors();

            //save the language in the XML file (XML_preferences)
            Utility.savePreference("language", Utility.language);

            //set the language for the current form
            Utility.setLanguage(form);
        }

        #endregion

        #region theme
        //get themes from the XML file XML_themePack
        public static void getThemes()
        {
            XmlDocument xml = null;
            XmlNode root = null;
            int index = 0;

            //clear the dictionary
            Utility.theme_list.Clear();

            try
            {
                xml = new XmlDocument(); //open the XML document and load into the 'xml' variable
                xml.Load(Utility.XML_themes);
                root = xml.DocumentElement;
            }
            catch (Exception exception)
            {

                MessageBox.Show(Utility.displayError("Load_themes_failed") + exception.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error); //display an error message
                Utility.ERR = true;
                //Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                //Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared
                Application.Exit(); //trigger an application exit
                return;
            }

            //save the theme names into the dictionary
            try
            {
                foreach(XmlNode theme in root.ChildNodes)
                    {
                        Utility.theme_list.Add(index, theme.Name);
                        index++;
                    }
            }
            catch (Exception exception)
            {
                MessageBox.Show(Utility.displayError("XML_format_error") + exception.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utility.ERR = true;
                //Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                //Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared
                Application.Exit(); //trigger an application exit
                return;
            }
        }

        //set the theme for the current form
        public static string setTheme(Form currentForm)
        {
            if (Utility.ERR == true) return "#ERR#"; //exit the function if errors were detected
            if (currentForm == null) return "#ERR#"; //error

            XmlDocument xml = null;
            XmlNode root = null;
            Control[] ctrl_list = null;

            try //open the XML_themePack file
            {
                xml = new XmlDocument();
                xml.Load(Utility.XML_themes);

                root = xml.DocumentElement;

            }catch (Exception exception)
            {

                MessageBox.Show(Utility.displayError("XML_format_error") + exception.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utility.ERR = true;
                //Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                //Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared
                Application.Exit(); //trigger an application exit
                return "#ERR#";

            }

            //get the theme from the XML file
            try
            {
                foreach(XmlNode theme in root.ChildNodes) //search the currently selected theme in the XML file
                    if (theme.Name.Equals(Utility.theme_list[Utility.theme]))
                        foreach(XmlNode form in theme.ChildNodes) //search the current form in the XML file
                            if(form.Name.Equals(currentForm.Name))
                            {   //change form properties
                                if(form.Attributes!=null)
                                {
                                    if (form.Attributes[0].Name.Equals("backcolor")) currentForm.BackColor = Color.FromName(form.Attributes[0].Value);
                                }

                                foreach (XmlNode control in form.ChildNodes) //set the theme for all controls in the current form
                                {
                                    ctrl_list = currentForm.Controls.Find(control.Name, true); //find all the controls matching the name of the current xml node

                                    if (ctrl_list.Length == 1) //set the theme for the found control
                                    {
                                        if (control.Attributes != null)
                                        {
                                            //DEV: add border styles (flatstyles) to theme customization
                                            if (control.Attributes[0].Name.Equals("backcolor")) ctrl_list[0].BackColor = Color.FromName(control.Attributes[0].Value);
                                            if (control.Attributes[1].Name.Equals("forecolor")) ctrl_list[0].ForeColor = Color.FromName(control.Attributes[1].Value);
                                            if (control.Attributes[2].Name.Equals("forecolor")) ctrl_list[0].ForeColor = Color.FromName(control.Attributes[2].Value);
                                            
                                            /* DEV
                                            if(control.Attributes!=null && control.Attributes[1].Name.Equals("font")) ctrl_list[0].Font = (control.Attributes[2].Value);
                                            */
                                        }
                                    }
                                    else if (ctrl_list.Length == 0) MessageBox.Show(Utility.displayError("XML_theme_control_not_found") + control.Name, "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);//display info message about a control not being found
                                    else
                                    { //error
                                        MessageBox.Show(Utility.displayError("Form_duplicate_controls") + control.Name + "; " + ctrl_list.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        Utility.ERR = true;
                                        //Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                                        //Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared
                                        Application.Exit(); //trigger an application exit
                                        return "#ERR#";
                                    }
                                }
                            }


            }catch (Exception exception)
            {
                MessageBox.Show(Utility.displayError("XML_format_error") + exception.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utility.ERR = true;
                //Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                //Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared
                Application.Exit(); //trigger an application exit
                return "#ERR#";
            }



            return Utility.theme_list[Utility.theme];
        }

        //get the next theme in the list (after the currently selected one)
        public static void nextTheme(Form form)
        {
            //select the next theme
            if (Utility.theme < Utility.theme_list.Count - 1) Utility.theme++;
            else Utility.theme = 0;

            //save the theme in the XML file (XML_preferences)
            Utility.savePreference("theme", Utility.theme);

            //set the theme for the current form
            Start.f0_logIn.F0_button_theme.Text = Utility.setTheme(form);
        }
        #endregion

        #region customization
        //save preferences in the XML file XML_preferences
        private static void savePreference(string preference, int value)
        {
            if(preference.Equals("") || preference==null || value<0 || value>255) // 0 <= value <= 255
            {
                Utility.ERR = true;
                Utility.WARNING = true;
                Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared
                MessageBox.Show(Utility.displayError("Code_wrong_function_call")+ "Utility.savePreference()","", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            XmlDocument xml = null;
            XmlNode root = null;

            try
            {
                xml = new XmlDocument();
                xml.Load(Utility.XML_preferences);

                root = xml.DocumentElement;

            }
            catch (Exception exception)
            {
                MessageBox.Show(Utility.displayError("XML_format_error") + exception.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utility.ERR = true;
                //Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                //Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared
                Application.Exit();

            }

            try
            {
                if (root != null && root.HasChildNodes && root.ChildNodes[0].Name.Equals("language") && root.ChildNodes[0].Attributes["value"] != null)
                {
                    root.ChildNodes[0].Attributes["value"].Value = value.ToString(); //save the value in the XML file; 
                    xml.Save(XML_preferences);
                }
                else throw new Exception();
            }
            catch (Exception exception)
            {
                MessageBox.Show(Utility.displayError("XML_format_error") + exception.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utility.ERR = true;
                //Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                //Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared
                Application.Exit();
            }
        }

        //sets the preferences saved in the XML file
        private static void setPreferences()
        {
            XmlDocument xml = null;
            XmlNode root = null;

            try
            {
                xml = new XmlDocument();
                xml.Load(Utility.XML_preferences);

                root = xml.DocumentElement;

            }
            catch (Exception exception)
            {
                MessageBox.Show(Utility.displayError("XML_format_error") + exception.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utility.ERR = true;
                //Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                //Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared
                Application.Exit();

            }

            try
            {
                //set the variables corresponding to the preference settings to the values in the XML file, if the format of the XML file is correct
                if(root != null && root.HasChildNodes && 
                root.ChildNodes[0].Name.Equals("language") && root.ChildNodes[0].Attributes.Count==1 && root.ChildNodes[0].Attributes["value"]!=null &&
                root.ChildNodes[1].Name.Equals("theme") && root.ChildNodes[1].Attributes.Count==1 && root.ChildNodes[1].Attributes["value"]!=null
                  )
                {
                    Utility.language = Convert.ToInt32(root.ChildNodes[0].Attributes["value"].Value); //set language
                    Utility.theme = Convert.ToInt32(root.ChildNodes[1].Attributes["value"].Value); //set theme
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(Utility.displayError("XML_format_error") + exception.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utility.ERR = true;
                //Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                //Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared
                Application.Exit();
            }


        }

        #endregion

        #region diagnostics
        //gets details about the database connection
        public static Dictionary<string,string> getConnDetails()
        {
            Dictionary<string,string> connection_details = new Dictionary<string,string>();
            //initialize list
            connection_details.Add("STATE","NULL"); //connection state
            connection_details.Add("DATABASE_NAME","NULL"); //database connected or database to connect to
            connection_details.Add("CONNECTION_STRING","NULL"); //connection string
            connection_details.Add("LAST_CONNECTION_ID","NULL"); //ID of the last connection (successful or unsuccessful)
            connection_details.Add("WORKSTATION_ID","NULL"); //Workstation ID
            //connection_details.Add("SQL_CREDENTIALS","NULL"); //the SQL credentials of the connection

            //get details about the current connection, if it exists
            try
            {
                if (Utility.conn == null) return connection_details; //no database connected

                connection_details.Clear();
                connection_details.Add("STATE",Utility.conn.State.ToString()); //connection_details[1] = connection state
                connection_details.Add("DATABASE_NAME",Utility.conn.Database.ToString()); //connection_details[2] = connected database or database to connect to
                connection_details.Add("CONNECTION_STRING",Utility.conn.ConnectionString.ToString()); //connection_details[3] = connection string
                connection_details.Add("WORKSTATION_ID",Utility.conn.WorkstationId.ToString()); //connection_details[4] = workstation ID
                connection_details.Add("LAST_CONNECTION_ID", Utility.conn.ClientConnectionId.ToString()); //connection_details[5] = the ID of the last connection (successful or unsuccessful)
                //connection_details.Add(Utility.conn.Credential.ToString()); //connection_details[6] = the SQL credentials of the connection
            }
            catch (Exception exception) 
            {
                MessageBox.Show(Utility.displayError("DB_conn_failed") + exception.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); //display an error message
                Utility.ERR = true;
                Utility.WARNING = true;
                Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared
            }

            return connection_details;
        }
        #endregion

    }
}
