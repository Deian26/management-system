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
using System.Xml.Linq;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Diagnostics.Contracts;

namespace management_system
{
    //Notification class
    public class Notification
    {
        //FIELDS
        private int id, //unique notification ID
                    importance, //importance; 1=important, 0=not important
                    read; //1=read, 0=unread
        private string text, // notification text
                       sender, //the username of the sender
                       date; //a string representing the date when the notification was sent

        //CONSTRUCTORS
        public Notification(int id, string text, string sender, string date, int importance, int read)
        {
            this.id = id;
            this.text = text;
            this.sender = sender;
            this.date = date;
            this.importance = importance;
            this.read = read;
        }

        //SETTERS
        public void setRead(int read)
        {
            this.read = read;
        }

        //GETTERS
        public int getId()
        {
            return this.id;
        }

        public string getText()
        {
            return this.text;
        }

        public string getSender()
        {
            return this.sender;
        }

        public string getDate()
        {
            return this.date;
        }

        public int getImportance()
        {
            return this.importance;
        }

        public int getRead()
        {
            return this.read;
        }


        //other methods
        //checks if this instance is equal to another one (equal fields)
        public bool equal(Notification notification)
        {
            return this.id.Equals(notification.id) && 
                this.text.Equals(notification.text) && 
                this.sender.Equals(notification.sender) && 
                this.date.Equals(notification.date) && 
                this.importance.Equals(notification.importance) && 
                this.read.Equals(notification.read);
        }
        
        public bool equalId(int id)
        {
            return this.id == id;
        }
    }

    //Utility class
    public static class Utility
    {
        //VARIABLES

        //misc
        public static int oldNotificationsLifespanDays = 14; //days
        public static int genKey = 23; //generic encryption key, used to encrypt general data in the database table (such as each group's cryptographic key)
        public static string enc_allowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 !?.,;:#@$%^&*()_+-[]/\\<>`~|="; //allowed characters for encryption/decryption
        public static string enc_allowedCharacters_TXT = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 !?.,;:#@$%^&*()_+-[]/\\<>`~|='\""; //allowed characters for encryption/decryption of text files
        public static int LOGO_interval = 7500;//ms; logo form (F9) display time
        //reversed enc_ strings (used for decryption)
        public static string rev_enc_allowedCharacters = "=|~`><\\/][-+_)(*&^%$@#:;,.?! 9876543210ZYXWVUTSRQPONMLKJIHGFEDCBAzyxwvutsrqponmlkjihgfedcba";
        public static string rev_enc_allowedCharacters_TXT = "\"'=|~`><\\/][-+_)(*&^%$@#:;,.?! 9876543210ZYXWVUTSRQPONMLKJIHGFEDCBAzyxwvutsrqponmlkjihgfedcba";

        public static string filename_allowedSpecialCharacters = "_# *"; //special characters allowed in file names
        //diagnostic
        public static int maxDiagnosticLogCharacters = int.MaxValue-1; //max number of characters to add into the diagnostic log before clearing the textbox

        //forms
        public static bool openUtilityService = false;
        public static Size utilityServiceMainFormSize = new Size(1170, 856);
        public static Size fileEditorFormMinimumSize = new Size(1000, 700);
        public static Size mdiEditorMinimumSize = new Size((int)(Utility.fileEditorFormMinimumSize.Width*0.15), (int)(Utility.fileEditorFormMinimumSize.Height*0.30));
        public static Size minimumInfoFormSize = new Size(1000,500);
        
        //error flag
        public static bool ERR = false;
        //warning flag
        public static bool WARNING = false;
        //error loading messages
        public static bool ERR_MESSAGES = false;

        //dictionaries
        public static Dictionary<string, string> errors = new Dictionary<string, string>(); //error messages
        public static Dictionary<string, string> messages = new Dictionary<string, string>(); //non-error messages
        public static Dictionary<int, string> language_list = new Dictionary<int, string>(); //language list
        public static Dictionary<int, string> theme_list = new Dictionary<int, string>(); //theme list
        public static List<string> databases_list = new List<string>(); //databases list
        public static List<Notification> notifications = new List<Notification>(); //active notifications
        //public static List<Notification> XML_notifications = new List<Notification>(); //active notifications logged in the user's XML notifications file
        private static Dictionary<string, string> f1_greetings = new Dictionary<string, string>(); //greetings for F1_MainForm
        private static List<string> error_log = new List<string>(); //errors and warnings reported (and not yet written into the diagnostic log); keys for error messages
        //private static List<string> error_log_messages = new List<string>(); //errors and warnings reported; actual error messages

        public static string username = null;
        public static string admin = null;
        public static int key = 0;
        public static int language = 1; //default language: Romanian (RO)
        public static int theme = 0; //default theme: Lite (LITE)
        
        //paths
        //image files
        public static string[] IMG_notifications_icons = { "..\\..\\SYSTEM\\RESOURCES\\IMG_notifications_icon_unimportant.bmp",
                                                           "..\\..\\SYSTEM\\RESOURCES\\IMG_notifications_icon_important.bmp",
                                                           "..\\..\\SYSTEM\\RESOURCES\\IMG_notifications_icon_important_notificationWindow.bmp"
                                                           };
        public static string IMG_defaultIconFilePath = "..\\..\\SYSTEM\\RESOURCES\\IMG_default_group_icon.bmp";
        public static string imgName_defaultGroupIcon = "IMG_defaultGroupIcon";
        //XML documents
        private static string XML_errors = "..\\..\\SYSTEM\\SETTINGS\\XML_errors.xml";
        private static string XML_messages = "..\\..\\SYSTEM\\SETTINGS\\XML_messages.xml";
        private static string XML_languages = "..\\..\\SYSTEM\\SETTINGS\\XML_languagePack.xml";
        private static string XML_databases = "..\\..\\SYSTEM\\SETTINGS\\XML_databases.xml";
        private static string XML_preferences = "..\\..\\SYSTEM\\SETTINGS\\XML_preferences.xml";
        private static string XML_themes = "..\\..\\SYSTEM\\SETTINGS\\XML_themePack.xml";
        public static string XML_notifications_userFolder = "..\\..\\DATA\\";

        //folders
        public static string dirPathDATA = "..\\..\\DATA"; //filePath to the DATA folder
        public static string dirPathSETTINGS = "..\\..\\SYSTEM\\SETTINGS"; //filePath to the SETTINGS folder

        public static string[] pathXmlSettingFiles = {
            Utility.dirPathSETTINGS + "\\XML_databases.xml", //databases
            Utility.dirPathSETTINGS + "\\XML_errors.xml", //errors
            Utility.dirPathSETTINGS + "\\XML_languagePack.xml", //languages
            Utility.dirPathSETTINGS + "\\XML_messages.xml", //messages
            Utility.dirPathSETTINGS + "\\XML_preferences.xml", //preferences
            Utility.dirPathSETTINGS + "\\XML_themePack.xml" //themes
            };

        //database connection
        private static SqlConnection conn = null;
        
        public static string DB_name = "";
        public static string DB_connString = "";

        //timer interval (ms)
        public static int clearErrTimeInterval = 100; //ms
        public static int clearWarningTimeInterval = 5000; //ms; timer started when the error timer is stopped
        public static int tooltipDuration = 5000; //ms
        public static int intervalUpdateTimer = 10; //ms
        public static int updateDBNotificationsInterval = 86400000;// 24h
        public static int shutdownTimerInterval = 3600000;// 1h
        public static int updateTimerInterval = 1000;// 1s



        //groups
        public static List<Group> userGroups = new List<Group>();
        public static string currentGroupPath = null; //the path to the local group path
        public const int maxGroupNameLength = 20; //the maximum length of a group name
        public const int maxUsernameLength = 20; //the maximum length of a username
        public static Group currentGroup = null; //current group   

        public static string groupIconFileFilterString = "JPEG|*.jpeg|JPG|*.jpg|PNG|*.png";//the filter to be applied for the files shown in the new group icon file dialog 

        //local folder names
        //public static string TempfolderName = "TBL"; //Temp folder
        //public static string localRtfFolderName = "RTF"; //RTF folder
        //public static string localXmlFolderName = "XML"; //XML folder
        //public static string localTblFolderName = "TBL"; //TBL folder
        //public static string localGrphFolderName = "GRPH"; //GRPH folder
        


        //file editing
        public const float textPointSizeIncrement = 1.5f; //size (in points) with which to increment the a selection from a text
        public const float textPointSizeDecrement = 1.5f; //size (in points) with which to decrement the a selection from a text
        public const float minPointTextSize = 5.0f; //minimum text size in points
        public static float maxPointTextSize = 50f; //maximum text size in points
        public static float defaultPointTextSize = 12.0f; //default tetx size in points

        public static string defaultFontFamily = "Consolas"; //default font family

        public static string recognizedWhitespaces = " \t\n\r";

        //XML editor
        public static TreeNode rootXmlNode = new TreeNode("root"); //default (mandatory for the program) XML root node (to be placed in a tree view control)

        public static string dbFilePrefix = "@DB\\"; //the string prefixed to a file filePath to mark that the file is stored in the connected database 
        //functions / methods

        //initialization
        public static int Initialize()
        {
            if (Utility.ERR == true) return 0; //exit the function if errors were detected
            
            //reset stored values
            Utility.errors.Clear();
            Utility.notifications.Clear();
            Utility.f1_greetings.Clear();
            Utility.error_log.Clear();

            //load values from stored files / the DataBase
            Utility.getLanguages(); //get the language names
            Utility.setPreferences(); //set the preferences saved in the XML file
            Utility.getErrors(); //get error messages
            Utility.getMessages(); //get non-error messages
            Utility.getErrors(); //get error messages in the selected language
            Utility.getDataBases(); //get the listed databases
            Utility.getThemes(); //get theme names
            


            if (Utility.ERR == true) return 0; //exit the function if errors were detected

            return 1;
        }

        #region utility
        //utility functions

        //converts a given input into a byte array

        //check the username
        public static bool validUsername(string username)
        {
            //minimum character length: 5
            //valid characters: [a-zA-Z0-9_]

            if (username == null || username.Equals("") || username.Length < 5) return false;

            foreach (char c in username)
                if ((c < 'a' || c > 'z') && (c < 'A' || c > 'Z') && (c < '0' || c > '9') && c != '_') return false;

            return true;
        }

        //check the password (before registering a new user)
        public static bool validPassword(string password)
        {
            /*
            the password must:
             * be have least 10 characters
             
            the password must contain:
             * at least one lower case latin alphabet letter [a-z]
             * at least one upper case latin alphabet letter [A-Z]
             * at least one digit [0-9]
             * at least one special character from the list: [~!@#$%^*_]
             */

            //flags
            bool lowercase = false,
                 uppercase = false,
                 digit = false,
                 special_char = false;

            if (password == null || password.Equals("") || password.Length < 10) return false;

            foreach (char c in password)
            {
                if (c >= 'a' && c <= 'z') lowercase = true; //lower case letter found
                if (c >= 'A' && c <= 'Z') uppercase = true; //upper case letter found
                if ("0123456789".Contains(c)) digit = true; //digit found
                if ("~!@#$%^*_".Contains(c)) special_char = true; //special character found
            }

            if (lowercase == true && uppercase == true && digit == true && special_char == true) return true;

            return false;

        }


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
                    output[i] = (byte)c;
                    i++;
                }catch (Exception exception)
                {
                    Utility.DisplayError("Code_invalid_byte_digit",exception, "CODE: Invalid byte digit given to Utility.toByteArray(string); digit given: "+c.ToString()+"; details: \n"+exception.ToString(),false);
                }

            }

            return output;
        }

        //extracts a single byte from the given string (2 characters)
        public static byte parseHexStringToByte(string input)
        {
            byte output = 0x00;
            string hex_digits = "0123456789ABCDEF";
            string hex_nr="0123456789",hex_letters = "ABCDEF";
            int i0=0, i1=0;

            try
            {
                if (input.Length!=2) throw new Exception("Invalid hex string");
            

            
                if (!hex_digits.Contains(input[0]) || !hex_digits.Contains(input[1]))
                {
                    throw new Exception("Invalid hex string");
                }
                else
                {
                    //input[0]
                    if (hex_nr.Contains(input[0])) //0->9
                        i0 = input[0] - '0';
                    else if (hex_letters.Contains(input[0])) //A->F
                        i0 = input[0] - 'A' + 10;

                    //input[1]
                    if (hex_nr.Contains(input[1])) //0->9
                        i1 = input[1] - '0';
                    else if (hex_letters.Contains(input[1])) //A->F
                        i1 = input[1] - 'A' + 10;


                    output = (byte)(0x0F & i0);
                    output = (byte)((output << 4) | i1);
                }
                

            }
            catch (Exception exception)
            {
                Utility.DisplayError("Code_invalid_byte_digit", exception, "CODE: Invalid byte digit given to Utility.toByteArray(string); details: \n" + exception.ToString(), false);
            }


            return output;
        }

        //converts HEX string to a corresponding byte array
        public static byte[] hexStringToByteArray(string input)
        {
            byte[] output = new byte[input.Length/2];

            for(int i =0, j=0;i<input.Length;i=i+2,j++)
            {
                output[j] = Utility.parseHexStringToByte(input[i].ToString() + input[i+1].ToString());

            }

            return output;
        }

        //converts an int string to the corresponding byte
        public static string parseIntToHexString(int input)
        {
            char[] output = new char[2];
            int aux;
            string str_output ="";

            aux = (0xF0 & input) >> 4;
            if (aux >= 0x0A && aux <= 0x0F) output[1] = (char)(aux -10 + 'A');
            else output[1] = (char)('0' + aux);

            aux = input & 0x0F;
            if (aux >= 0x0A && aux <= 0x0F) output[0] = (char)(aux - 10 + 'A');
            else output[0] = (char)('0' + aux);


            str_output = output[1].ToString() + output[0].ToString();
            return str_output;
        }

        //converts a byte array to a string
        public static string toString(byte[] input)
        {
            string output = "";
            
            foreach (byte b in input)
            {

                output += Utility.parseIntToHexString(b);
            }
            
            return output;

        }

        //checks the format and signature of the specified notifications XML file
        public static bool checkNotificationsXML(string XML_path)
        {
            
            XmlDocument xml = null;
            XmlNode root = null;
            string XML_string = "";
            bool checkSignature = false;

            try
            {
                //check if the document can be opened and the root node selected
                xml = new XmlDocument();
                xml.Load(XML_path);
                root = xml.DocumentElement;

            }
            catch (Exception exception)
            {
                Utility.DisplayWarning("XML_format_error",exception,"SYSTEM: Notifications XML file format error: \n"+exception.ToString(),false);
                return false;
            }

            //check the data signature
            try
            {
                if (root!=null && root.ChildNodes.Count > 0)
                {
                    foreach (XmlNode node in root.ChildNodes)
                    {
                        if (node.Name.Equals("Signature") && node.Attributes!=null && node.Attributes[0].Name.Equals("signature")) //check signature
                        {
                            //checkSignature = Utility.rsa.VerifyData(Utility.toByteArray(XML_string), Utility.hexStringToByteArray(node.Attributes[0].Value), HashAlgorithmName.SHA512, RSASignaturePadding.Pkcs1);
                            checkSignature = Utility.SignatureHash(XML_string).Equals(Utility.DEC_GEN(node.Attributes[0].Value,Utility.key));
                            
                            break;
                        }
                        else //store notification details into the 'XML_string' variable
                        {
                            if(node.Name.Equals("notification") && node.Attributes!=null && node.Attributes.Count == 6 
                                && node.Attributes[0]!= null && node.Attributes[0].Name.Equals("id")
                                && node.Attributes[1] != null && node.Attributes[1].Name.Equals("text")
                                && node.Attributes[2] != null && node.Attributes[2].Name.Equals("sender")
                                && node.Attributes[3] != null && node.Attributes[3].Name.Equals("date")
                                && node.Attributes[4] != null && node.Attributes[4].Name.Equals("importance")
                                && node.Attributes[5] != null && node.Attributes[5].Name.Equals("read")
                                )
                            {
                                XML_string += Utility.DEC_GEN(node.Attributes[0].Value,Utility.key) + Utility.DEC_GEN(node.Attributes[1].Value, Utility.key) 
                                    + Utility.DEC_GEN(node.Attributes[2].Value,Utility.key) + Utility.DEC_GEN(node.Attributes[3].Value, Utility.key)
                                    + Utility.DEC_GEN(node.Attributes[4].Value, Utility.key) + Utility.DEC_GEN(node.Attributes[5].Value,Utility.key);
                            }
                        }
                    }
                }
                if(checkSignature == false)
                {
                    //MessageBox.Show(Utility.displayError("XML_file_invalid_signature"), "SECURITY ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); //display an error message
                    Utility.logDiagnosticEntry("EN: Invalid signature for the XML file: " + XML_path.ToString());
                    Utility.WARNING = true;
                    //Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                    //Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared
                    //Application.Exit(); //trigger an application exit
                    return false;
                }
            }
            catch (Exception exception)
            {
                Utility.DisplayWarning("XML_format_error",exception,"SYSTEM: Notifications XML file format error: \n"+exception.ToString(),false);
                return false;
            }

            return true;
        }

        //gets attribute values and inner text strings from all of the XML nodes of the root element
        //DEV - to be deleted ?
        private static string getXmlNodeSubStrings(XmlNode root)
        {
            string attributes_string = "";

            if(root.HasChildNodes)
                foreach(XmlNode node in root.ChildNodes)
                {
                    if(node.Attributes.Count>0)
                        foreach(XmlAttribute attribute in node.Attributes)
                            attributes_string += attribute.Value;

                    return attributes_string + getXmlNodeSubStrings(node); ;
                }

            return root.InnerText;

        }

        //checks the specified XML file with the standard format: the root element with the first attribute = 'signature'
        public static bool checkXML(string path)
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(path);

                XmlNode root = xml.DocumentElement;

                string signature = null;

                if(root!=null && root.Attributes.Count>0 && root.Attributes[0].Name.Equals("signature")) signature = root.Attributes[0].Value;

                //compute signature
                //use all attributes + inner text in the computation of the signature
                

                if (signature.Equals(Utility.DB_HASH(root.InnerXml)) == false) //incorrect signature
                {
                    if (Utility.pathXmlSettingFiles[0].Equals(path)) //database XML file
                        {
                            //Application.Exit();//trigger application exit
                        throw new Exception(path + ": Database file corrupted. Application shutdown triggered.");  
                        }
                    throw new Exception(path);
                } //correct signature 
            

            }catch (Exception exception)
            {
                Utility.DisplayError("XML_file_invalid_signature", exception, "Invalid XML file signature: " + path.ToString(), false);

            }

            return true;
        }

        //create a new directory (folder)
        public static bool createDirectory(string path)
        {
            try
            {
                Directory.CreateDirectory(path);
            }catch (Exception exception)
            {
                Utility.DisplayError("Data_wrong_folder_path",exception,"SYSTEM: Failed to create the 'DATA' directory: \n"+exception.ToString(),true);
            }
            return true;
        }

        #endregion

        #region database
        //serialize the file at the specified path then upload it into the corresponding table of the currently connected database; the function returns 'true' if the upload was successful and 'false' otherwise
        public static bool uploadFileIntoDB(string filePath)
        {
            try
            {
                //DEV
                //serialize file
            }catch(Exception exception)
            {
                Utility.DisplayError("DB_failed_to_upload_group_file", exception, "DataBase: Failed to upload a file to the database, file:" + filePath.ToString() + " ; details: " + exception.ToString(),false);
                return false; //upload unsuccessful
            }

            return true; //upload successful
        }

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
                Utility.DisplayError("DB_load_databases_failed",exception,"SYSTEM: Failed to load databases from the XML file: \n"+exception.ToString(),true);
                return false;
            }

            //save the database paths
            try
            {
                foreach (XmlNode database in root.ChildNodes)
                    Utility.databases_list.Add(database.InnerText);
            }catch (Exception exception)
            {
                Utility.DisplayError("XML_format_error",exception,"SYSTEM: Databases XML file format error: \n"+exception.ToString(),true);
                return false;
            }
            return true;
        }

        //save the current credentials
        public static void setCredentials(string username, int key, string admin, string password)
        {
            if (Utility.ERR == true) return; //exit the function if errors were detected

            Utility.username = username;
            Utility.key = key;
            string auxAdminString = Utility.DB_HASH(Utility.ENC_GEN(password, Utility.key));

            if (auxAdminString.Equals(admin)) Utility.admin = auxAdminString;
            else Utility.admin = null;


        }

        //connect to the database
        public static bool DB_connect(string connString)
        {
            Utility.DB_name = connString.Split('=')[1].Split(';')[0];
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
                Utility.DB_name = "#ERR#";
                Utility.DisplayError("DB_conn_failed",exception,"SYSTEM: Failed to connect to the database: \n"+exception.ToString(),true);
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
                Utility.DisplayError("DB_disconnect_failed",exception,"SYSTEM: Failed to disconnect from the database: \n"+exception.ToString(),false);
            }
        }

        //log into the system
        public static string logIn(string username, string password)
        {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            string hashed_username = Utility.DB_HASH(username);
            string hashed_password = Utility.DB_HASH(password);
            string adminString = "";

            try
            {
                cmd = new SqlCommand("SELECT * FROM Users WHERE username='" + hashed_username + "' AND password='" + hashed_password + "'", Utility.conn);
                dr = cmd.ExecuteReader();
            }catch (Exception exception)
            {
                if (dr != null) dr.Close();
                Utility.DisplayError("SQL_statement_error",exception,"SYSTEM: SQL statement error: \n"+exception.ToString(),true);
                return null;
            }
                
            int rows = 0;

            try
            {
                while (dr.Read())
                {
                    if (hashed_username.Equals(dr.GetString(0)) && hashed_password.Equals(dr.GetString(1))) //username and password
                    {
                        adminString = dr.GetString(3);
                        dr.Close();
                        return adminString; //account found
                    }

                    rows++;
                    if (rows >= 2)
                    {
                        if (dr != null) dr.Close();
                        Utility.DisplayError("Invalid_credentials",new Exception(""),"SYSTEM: Invalid credentials given",true);
                        return null; //multiple accounts found
                    }
                }

                dr.Close();
                return null; //no account found
            }catch (Exception exception)
            {
                if (dr != null) dr.Close();
                Utility.DisplayError("SQL_table_format_error", exception,"SYSTEM: SQL table format error: \n"+exception.ToString(), true) ;
                return null; //multiple accounts found
            }
        }

        //log out of the system
        public static void logOut(F1_MainForm f1)
        {
            Utility.username = "";
            Utility.key = 0;
            Utility.notifications.Clear();
            Utility.userGroups.Clear();
            Utility.currentGroup = null;

            Utility.ERR = false;
            Utility.WARNING = false;
            //f1.Close();
            if(f1!=null) f1.Hide();
            Start.f0_logIn.Show();
            Start.f0_logIn.ClearCredentials();

        }

        //get data key
        public static int getDataKey(string username)
        {
            try
            {
                SqlCommand sql = new SqlCommand("SELECT * FROM Users WHERE username='" + Utility.DB_HASH(username) + "'", Utility.conn);
                SqlDataReader dr = sql.ExecuteReader();
                int key = -1;
                int i = 0;

                while (dr.Read())
                {
                    key = dr.GetInt32(2);
                    i++;
                }
                dr.Close();
                sql.Dispose();

                if (i != 1) //duplicate usernames
                {
                    Utility.DisplayError("Invalid_username_duplicate", new Exception(""), "Error: duplicate usernames found in the Users table: " + username.ToString(), false);
                    return -1;
                }
                if (i == 1)
                    return key + username.Length;
                else
                    return -1;
            }catch (Exception exception)
            {
                Utility.DisplayError("DB_error_getting_key", exception, "Error getting the cryptographic key: " + exception.ToString(), true);
            }

            return -1;
        }

        //get the last database the application was connected to
        public static string getLastDataBaseConnString()
        {
            return Utility.DB_connString;
        }

        //create an SQL command that does not contain 'DROP TABLE'
        public static SqlCommand getSqlCommand(string SQL_command)
        {
            SqlCommand cmd = null;

            try
            {
                if (SQL_command.ToUpper().Contains("DROP TABLE")) //refuse to construct the specified SQL command
                    return new SqlCommand();

                cmd = new SqlCommand(SQL_command, Utility.conn);
            }catch (Exception exception)
            {
                Utility.DisplayError("DB_conn_failed", exception, "SYSTEM: Database connection failed: " + exception.ToString(), true);
            }

            return cmd;
        }

        //return a command that is used to delete ('DROP') an entire table specified by name
        public static SqlCommand getDropTableSqlCommand(string table_name)
        {
            SqlCommand cmd = null;

            try
            {
                if (table_name.ToUpper().Contains("DROP TABLE") || table_name.ToUpper().Contains("DELETE")) //refuse to construct the specified SQL command
                    return new SqlCommand();

                cmd = new SqlCommand("DROP TABLE "+table_name, Utility.conn);
            }
            catch (Exception exception)
            {
                Utility.DisplayError("DB_conn_failed", exception, "SYSTEM: Database connection failed: " + exception.ToString(), true);
            }

            return cmd;
        }

        //create a table in the database; if the table already exists, an exception will be thrown but handled without a warning
        //ATTENTION: FOR COMPATIBILITY, FIELDS CREATED USING THIS FUNCTION SHOULD ALWAYS CONTAIN '_' AT THE BEGINNING OF THEIR NAME
        //Dictionary layout: fields[field_name] = field_type (ex.: fields["id"]="INT")
        public static bool setCreateTable(string table, Dictionary<string,string> fields)
        {
            SqlCommand cmd = null;
            string aux_fields = "";
            int i;
            try
            {
                i = 0;
                foreach (string field_name in fields.Keys)
                {
                    aux_fields+= field_name+" "+fields[field_name]; //field_name field_type
                    i++;
                    if (i<fields.Count) aux_fields+=", ";
                }
                try
                {
                    cmd = new SqlCommand("CREATE TABLE " + table + "(" + aux_fields + ")", Utility.conn);
                    cmd.ExecuteNonQuery();
                }catch (Exception exception) 
                {
                    Utility.logDiagnosticEntry("Notifications table already exists in the database: "+exception.ToString());
                }
                    
                cmd.Dispose();
            }
            catch (Exception exception)
            {
                Utility.DisplayError("DB_create_table_failed", exception, "SYSTEM: Error creating table. " + exception.ToString(), false);
                return false;
            }

            return true;
        }

        //delete the specified notifications from the corresponding table
        public static void deleteOldDbNotifications(int lifespan_days)
        {
            try
            {
                if(lifespan_days<=0) return;

                SqlCommand cmd = new SqlCommand("SELECT * FROM Notifications",Utility.conn);
                SqlDataReader dr = cmd.ExecuteReader();
                List<int> IDs = new List<int>();

                //store the IDs of the notifications older than the specified lifespan (days)
                while(dr.Read())
                {
                    DateTime sent_date = DateTime.Parse(dr.GetString(3));

                    if(DateTime.Now.Subtract(sent_date).Days>lifespan_days)
                    {
                        IDs.Add(dr.GetInt32(0));
                    }


                }
                dr.Close();

                //delete old notifications
                SqlCommand cmd_delete = null;

                foreach (int id in IDs)
                {
                    cmd_delete = new SqlCommand("DELETE FROM Notifications WHERE _id='" + id.ToString() + "'", Utility.conn);
                    cmd_delete.ExecuteNonQuery();
                    
                }

                if(cmd_delete != null) cmd_delete.Dispose();


            }
            catch (Exception exception)
            {
                Utility.DisplayWarning("DB_delete_old_notifications_failed", exception, "SYSTEM: Error deleting old notifications from the database. " + exception.ToString(), false);
            }
        }

        #endregion

        #region encryption
        //encryption functions

        //hash - signatures
        public static string SignatureHash(string input)
        {
            SHA512 sha512 = SHA512Managed.Create();
            string output = "";
            byte[] byte_input = new byte[0x00];

            sha512.Initialize();
            byte_input = Utility.toByteArray(input);

            output = Convert.ToBase64String(sha512.ComputeHash(byte_input));

            sha512.Dispose();
            
            return output;
        }

        //hash - usernames and passwords
        public static string DB_HASH(string input)
        {
            SHA512 sha = SHA512Managed.Create();
            string output="";
            byte[] byte_input = new byte[0x00];

            sha.Initialize();
            byte_input = Utility.toByteArray(input);

            output = Convert.ToBase64String(sha.ComputeHash(byte_input));

            sha.Dispose();

            return output;
        }

        //Used to encrypt DataBase specific data (except usernames and passwords)
        public static string DB_ENC(string input, int key)
        {
            return input; //DEV

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
            int sgn = 1;
            char[] txt = input.ToCharArray();
            
            
            for (int i = 0; i < txt.Length; i++)
            {
                //txt[i] = (char)(txt[i] + (char)(sgn * key));
                if (txt[i]!='\n' && txt[i]!='\t' && txt[i]!='\r')
                    txt[i] = Utility.enc_allowedCharacters[(Utility.enc_allowedCharacters.IndexOf(txt[i]) + (sgn * key)) % Utility.enc_allowedCharacters.Length];
                //sgn *= -1;
            }

            foreach (char c in txt)
                output += c;


            return output;
        }

        //ENC_GEN() version for text files
        public static string ENC_TXT(string input, int key)
        {
            string output = "";
            int sgn = 1;
            char[] txt = input.ToCharArray();


            for (int i = 0; i < txt.Length; i++)
            {
                //txt[i] = (char)(txt[i] + (char)(sgn * key));
                if (txt[i] != '\n' && txt[i] != '\t' && txt[i] != '\r')
                    txt[i] = Utility.enc_allowedCharacters_TXT[(Utility.enc_allowedCharacters_TXT.IndexOf(txt[i]) + (sgn * key)) % Utility.enc_allowedCharacters_TXT.Length];
                //sgn *= -1;
            }

            foreach (char c in txt)
                output += c;


            return output;
        }
        
        //ENC_GEN() overload for integers
        public static int ENC_GEN(int input, int key)
        {
            return input + key;
        }

        //ENC_GEN() overload for floating point numbers
        public static float ENC_GEN(float input, int key)
        {
            return input + key;
        }

        //decryption functions

        //Used to decrypt DataBase specific data such as usernames and passwords (complementary to DB_ENC() )
        public static string DB_DEC(string input, int key)
        {
            return input; //DEV

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
            int sgn = 1;
            char[] txt = input.ToCharArray();

            for (int i = 0; i < txt.Length; i++)
            {
                //txt[i] = (char)(txt[i] + (char)(sgn * key));
                if (txt[i] != '\n' && txt[i] != '\t' && txt[i] != '\r')
                    txt[i] = Utility.rev_enc_allowedCharacters[(Utility.rev_enc_allowedCharacters.IndexOf(txt[i]) + (sgn * key)) % Utility.rev_enc_allowedCharacters.Length];
                //sgn *= -1;
            }

            foreach (char c in txt)
                output += c;


            return output;
            /*
            string output = "";
            int sgn = -1;
            char[] txt = input.ToCharArray();

            for (int i = 0; i < txt.Length; i++)
            {
                //txt[i] = (char)(txt[i] + (char)(sgn * key));
                if (txt[i]!='\n' && txt[i]!='\t' && txt[i]!='\r')
                    if (Utility.enc_allowedCharacters.IndexOf(txt[i])-key<0) //wrap around from the end of the Utility.enc_allowedCharacters string
                    {
                        int index = key - Utility.enc_allowedCharacters.IndexOf(txt[i]);

                        txt[i] = Utility.enc_allowedCharacters[Utility.enc_allowedCharacters.Length - index];
                    }
                    else
                    {
                        txt[i] = Utility.enc_allowedCharacters[Utility.enc_allowedCharacters.IndexOf(txt[i]) - key];
                    }
                
                //sgn *= -1;
            }

            foreach (char c in txt)
                output += c;

            return output;
            */
        }

        //DEC_GEN() version for text files
        public static string DEC_TXT(string input, int key)
        {
            string output = "";
            int sgn = 1;
            char[] txt = input.ToCharArray();

            for (int i = 0; i < txt.Length; i++)
            {
                //txt[i] = (char)(txt[i] + (char)(sgn * key));
                if (txt[i] != '\n' && txt[i] != '\t' && txt[i] != '\r')
                    txt[i] = Utility.rev_enc_allowedCharacters_TXT[(Utility.rev_enc_allowedCharacters_TXT.IndexOf(txt[i]) + (sgn * key)) % Utility.rev_enc_allowedCharacters_TXT.Length];
                //sgn *= -1;
            }

            foreach (char c in txt)
                output += c;


            return output;
        }

        //DEC_GEN() overload for integers
        public static int DEC_GEN(int input, int key)
        {

            return input - key;

        }

        //DEC_GEN() overload for floating point numbers
        public static float DEC_GEN(float input, int key)
        {

            return input - key;

        }
        #endregion

        //getters & setters
        #region messages and errors
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
                MessageBox.Show("EN: ERROR LOADING ERROR MESSAGES; CHECK THE XML FILE UNDER management_system\\SYSTEM\\SETTINGS\\XML_errors.xml; Details: "+exception.Message,"ENGLISH",
                    MessageBoxButtons.OK,MessageBoxIcon.Error); //display a generic, english error message for an error loading error messages
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
                MessageBox.Show("EN: ERROR WITH THE XML FORMAT FOR THE ERROR MESSAGES; CHECK THE XML FILE UNDER management_system\\SYSTEM\\SETTINGS\\XML_errors.xml; Details: " + exception.Message, "ENGLISH", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error); //display a generic, english error message for an error loading error messages
                Utility.ERR = true;
                //Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                //Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared
                Application.Exit(); //trigger an application exit
                return;
            }

            if (Utility.errors.Count == 0) //translations not found
            {
                MessageBox.Show("EN: ERORR LOADING ERROR MESSAGES PLEASE SWITCH TO ANOTHER LANGUAGE", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utility.error_log.Add("ERROR_LOADING_ERRORS");
                //Utility.ERR = true;
                Utility.WARNING = true;
            }
        }
        
        //get error log
        public static List<string> getErrorLog()
        {
            List<string> aux_err_log = new List<string>();
            
            foreach(string error in Utility.error_log)
            {
                aux_err_log.Add(error);
            }
            
            return aux_err_log;
        }

        //erase error log
        public static void clearErrorLog()
        {
            Utility.error_log.Clear();
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
                Utility.DisplayError("XML_format_error", exception, "SYSTEM: XML file format error: " + exception.ToString(), true);
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
                MessageBox.Show("EN: ERROR WITH THE XML FORMAT FOR THE MESSAGES; CHECK THE XML FILE UNDER management_system\\SYSTEM\\SETTINGS\\XML_errors.xml; Details: " + exception.Message, "ENGLISH", MessageBoxButtons.OK, MessageBoxIcon.Error); //display a generic, english error message for an error loading error messages
                Utility.ERR = true;
                Utility.ERR_MESSAGES = true;
                //Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                //Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared
                Application.Exit(); //trigger an application exit
                return;
            }



            if (Utility.messages.Count == 0) //translations not found
            {
                MessageBox.Show("EN: ERORR LOADING MESSAGES PLEASE SWITCH TO ANOTHER LANGUAGE", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utility.error_log.Add("ERROR_LOADING_MESSAGES");
                //Utility.ERR = true;
                Utility.WARNING = true;
            }
        }

        //display the error message associated with the given key
        public static string displayError(string key)
        {
            if (key.Equals("") || key == null) return "EN: ERROR MESSAGE NOT FOUND; KEY: "+key.ToString();

            try
            {
                if(Utility.errors.ContainsKey(key)) Utility.error_log.Add(key); //add error into the error log
                return Utility.errors[key]; //get the error message from the dictionary
            }catch(Exception exception) 
            {
                MessageBox.Show("EN: ERROR MESSAGE NOT FOUND; KEY:"+key.ToString() + " : "+ exception.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); //display a generic, english error message for an error loading error messages
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
                //if (Utility.messages.ContainsKey(key)) Utility.error_log.Add(key);
                return Utility.messages[key]; //get the error message from the dictionary
            }
            catch (Exception exception)
            {
                MessageBox.Show("EN: MESSAGE NOT FOUND; KEY:" + key.ToString() + " : " + exception.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); //display a generic, english error message for an error loading error messages
                Utility.ERR = true;
                Utility.WARNING = true;
                Utility.ERR_MESSAGES = true;
                Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared
                //Application.Exit(); //trigger an application exit
                return "#ERR#";
            }
        }
        #endregion

        #region notifications
        //update notifications list from the database
        public static void getNotificationsFromDB(string username)
        {
            if (Utility.ERR == true) return; //exit the function if errors were detected

            if (conn!=null && conn.State.Equals(System.Data.ConnectionState.Open))
            {
                SqlCommand cmd = null;
                SqlDataReader dr = null;

                Utility.notifications.Clear(); //clear notification list

                string str_cmd = "SELECT * FROM Notifications"; //standard table name

                try
                {
                    cmd = new SqlCommand(str_cmd, conn);
                    dr = cmd.ExecuteReader();
                }catch (Exception exception)
                {
                    Utility.DisplayError("SQL_statement_error",exception,"SYSTEM: SQL statement error: "+exception.ToString(), true);
                    return;
                }

                try
                {
                    //clear the notifications list
                    Utility.notifications.Clear();

                    //load new notifications
                    while (dr.Read())
                    {
                            Utility.notifications.Add(new Notification(dr.GetInt32(0), Utility.DB_DEC(dr.GetString(1), Utility.genKey), Utility.DB_DEC(dr.GetString(2), Utility.genKey), Utility.DB_DEC(dr.GetString(3), Utility.genKey).ToString(), dr.GetInt32(4), 0));
                    }

                    dr.Close();
                    cmd.Dispose();
                }catch (Exception exception)
                {
                    Utility.DisplayError("SQL_table_format_error",exception,"SYSTEM: Database table format error: \n"+exception.ToString(), true);
                    return;
                }
            }
            else
            {
                Utility.DisplayError("DB_conn_failed", new Exception(""), "SYSTEM: Database connection failed.", true);
                return;
            }
        }

        //get the notifications from the local XML file
        public static List<Notification> getNotificationsFromXML(string path)
        {
            List<Notification> notif = new List<Notification>();

            try
            {
                XmlDocument xml_notif = new XmlDocument();

                xml_notif.Load(path);
                XmlNode root = xml_notif.DocumentElement;

                foreach (XmlNode notification in root.ChildNodes)
                {
                    //save notification details
                    if (notification.Name.Equals("notification") && notification.Attributes != null && notification.Attributes.Count == 6 &&
                        notification.Attributes[0] != null && notification.Attributes[0].Name.Equals("id") &&
                        notification.Attributes[1] != null && notification.Attributes[1].Name.Equals("text") &&
                        notification.Attributes[2] != null && notification.Attributes[2].Name.Equals("sender") &&
                        notification.Attributes[3] != null && notification.Attributes[3].Name.Equals("date") &&
                        notification.Attributes[4] != null && notification.Attributes[4].Name.Equals("importance") &&
                        notification.Attributes[5] != null && notification.Attributes[5].Name.Equals("read")
                        )
                    {
                        notif.Add(new Notification(Convert.ToInt32(Utility.DEC_GEN(notification.Attributes[0].Value, Utility.genKey)),
                                                   Utility.DEC_GEN(notification.Attributes[1].Value, Utility.genKey),
                                                   Utility.DEC_GEN(notification.Attributes[2].Value, Utility.genKey),
                                                   Utility.DEC_GEN(notification.Attributes[3].Value, Utility.genKey),
                                                   Convert.ToInt32(Utility.DEC_GEN(notification.Attributes[4].Value, Utility.genKey)),
                                                   Convert.ToInt32(Utility.DEC_GEN(notification.Attributes[5].Value, Utility.genKey))
                                                  ));
                    }
                }
            }catch (Exception exception)
            {
                Utility.DisplayError("XML_updating_notifications_failed", exception, "SYSTEM: Updating notifications to the XML file failed: \n" + exception.ToString(), false);
            }

            return notif;
        }

        //create a new file at the specified filePath
        public static bool createNotificationFile(string path, string type, string username)
        {
            try
            {
                XmlWriterSettings ws = new XmlWriterSettings();
                ws.IndentChars = "\t";
                ws.Indent = true;
                ws.NewLineChars = "\r\n";
                ws.CheckCharacters = true;

                XmlWriter w = XmlWriter.Create(path, ws);
                string XML_string = "";
                string signature = "";

                w.WriteStartDocument(true); //start XML document
                w.WriteStartElement("Notifications"); //start root element        
                                                      //write the ID node
                                                      //w.WriteStartElement("ID");
                w.WriteAttributeString("Username", username);
                w.WriteAttributeString("DateTime", DateTime.Now.ToString());
                //w.WriteEndElement();

                //get notifications form the data base
                Utility.getNotificationsFromDB(username);

                //write notifications into the XML file
                foreach (Notification notification in Utility.notifications)
                {
                    w.WriteStartElement("notification");
                    w.WriteAttributeString("id", Utility.ENC_GEN(notification.getId().ToString(), Utility.key));
                    w.WriteAttributeString("text", Utility.ENC_GEN(notification.getText(), Utility.key));
                    w.WriteAttributeString("sender", Utility.ENC_GEN(notification.getSender(), Utility.key));
                    w.WriteAttributeString("date", Utility.ENC_GEN(notification.getDate().ToString(), Utility.key));
                    w.WriteAttributeString("importance", Utility.ENC_GEN(notification.getImportance().ToString(), Utility.key));
                    w.WriteAttributeString("read", Utility.ENC_GEN(notification.getRead().ToString(), Utility.key));
                    w.WriteEndElement();
                    XML_string += notification.getId().ToString() + notification.getText() + notification.getSender()
                                + notification.getDate().ToString() + notification.getImportance().ToString() + notification.getRead().ToString();
                }

                //compute the signature
                //signature = Utility.toString(Utility.rsa.SignData(Utility.toByteArray(XML_string), HashAlgorithmName.SHA512, RSASignaturePadding.Pkcs1));
                signature = Utility.ENC_GEN(Utility.SignatureHash(XML_string), Utility.key);


                w.WriteStartElement("Signature");
                w.WriteAttributeString("signature", signature);
                w.WriteEndElement();

                w.WriteEndDocument(); //end the XML document
                w.Close();

            }
            catch (Exception exception)
            {
                Utility.DisplayError("Data_wrong_file_path_or_type",exception, "SYSTEM: Wrong path to the notifications XML file or invalid file format: \n"+exception.ToString(),true);
            }

            return true;
        }

        //write notifications from memory to the XML file (overwrites the existing text)
        public static void writeNotificationsToXmlFile(string path)
        {
            XmlDocument xml_notif = new XmlDocument();
            XmlNode root;
            string signature, XML_string = "";

            try
            {
                //open the file
                xml_notif.Load(path);
                root = xml_notif.DocumentElement;

                //overwrite the notifications in the file
                XmlWriterSettings ws = new XmlWriterSettings();
                ws.IndentChars = "\t";
                ws.Indent = true;
                ws.NewLineChars = "\r\n";
                ws.CheckCharacters = true;
                XmlWriter w = XmlWriter.Create(path, ws);

                w.WriteStartDocument(); //start the XML document

                w.WriteStartElement("Notifications");//start root element
                                                     //write the ID node
                                                     //w.WriteStartElement("ID");
                w.WriteAttributeString("Username", Utility.username);
                w.WriteAttributeString("DateTime", DateTime.Now.ToString());
                //w.WriteEndElement();

                //write notification nodes
                foreach (Notification notification in Utility.notifications)
                {
                    w.WriteStartElement("notification");
                    w.WriteAttributeString("id", Utility.ENC_GEN(notification.getId().ToString(), Utility.key));
                    w.WriteAttributeString("text", Utility.ENC_GEN(notification.getText(), Utility.key));
                    w.WriteAttributeString("sender", Utility.ENC_GEN(notification.getSender(), Utility.key));
                    w.WriteAttributeString("date", Utility.ENC_GEN(notification.getDate(), Utility.key));
                    w.WriteAttributeString("importance", Utility.ENC_GEN(notification.getImportance().ToString(), Utility.key));
                    w.WriteAttributeString("read", Utility.ENC_GEN(notification.getRead().ToString(), Utility.key));
                    w.WriteEndElement();

                    XML_string += notification.getId().ToString() + notification.getText() + notification.getSender()
                                + notification.getDate().ToString() + notification.getImportance().ToString() + notification.getRead().ToString();
                }

                //write signature
                //signature = Utility.toString(Utility.rsa.SignData(Utility.toByteArray(XML_string), HashAlgorithmName.SHA512, RSASignaturePadding.Pkcs1));

                signature = Utility.ENC_GEN(Utility.SignatureHash(XML_string), Utility.key);

                w.WriteStartElement("Signature");
                w.WriteAttributeString("signature", signature);
                w.WriteEndElement();

                w.WriteEndElement();//end root element

                w.WriteEndDocument(); //end the XML document
                w.Close(); //close stream

            }
            catch (Exception exception)
            {
                Utility.DisplayError("XML_updating_notifications_failed",exception,"SYSTEM: Failed to update the XML notifications file: \n"+exception.ToString(),false);
            }
        }

        //load notifications from the local XML file and the database into memory
        public static void updateXMLNotificationsFile(string path)
        {
            List<Notification> notif = new List<Notification>();
            XmlDocument xml_notif = new XmlDocument();
            string XML_string = "";

            try
            {
                //create the local XML file if not present
                if (!File.Exists(path) || !Utility.checkNotificationsXML(path))
                {
                    Utility.createNotificationFile(path, "Notifications", Utility.username);
                    //return;
                }


                //get notifications from the database
                Utility.getNotificationsFromDB(Utility.username);

                //load notifications from the file into memory and store the details used in computing the signature
                xml_notif.Load(path);
                XmlNode root = xml_notif.DocumentElement;

                foreach (XmlNode notification in root.ChildNodes)
                {
                    //save notification details
                    if (notification.Name.Equals("notification") && notification.Attributes != null && notification.Attributes.Count == 6 &&
                        notification.Attributes[0] != null && notification.Attributes[0].Name.Equals("id") &&
                        notification.Attributes[1] != null && notification.Attributes[1].Name.Equals("text") &&
                        notification.Attributes[2] != null && notification.Attributes[2].Name.Equals("sender") &&
                        notification.Attributes[3] != null && notification.Attributes[3].Name.Equals("date") &&
                        notification.Attributes[4] != null && notification.Attributes[4].Name.Equals("importance") &&
                        notification.Attributes[5] != null && notification.Attributes[5].Name.Equals("read")

                        )
                    {
                        notif.Add(new Notification(Convert.ToInt32(Utility.DEC_GEN(notification.Attributes[0].Value, Utility.key)),
                                                   Utility.DEC_GEN(notification.Attributes[1].Value, Utility.key),
                                                   Utility.DEC_GEN(notification.Attributes[2].Value, Utility.key),
                                                   Utility.DEC_GEN(notification.Attributes[3].Value, Utility.key),
                                                   Convert.ToInt32(Utility.DEC_GEN(notification.Attributes[4].Value, Utility.key)),
                                                   Convert.ToInt32(Utility.DEC_GEN(notification.Attributes[5].Value, Utility.key))
                                                  ));
                        XML_string += Utility.DEC_GEN(notification.Attributes[0].Value, Utility.key) + Utility.DEC_GEN(notification.Attributes[1].Value, Utility.key)
                                    + Utility.DEC_GEN(notification.Attributes[2].Value, Utility.key) + Utility.DEC_GEN(notification.Attributes[3].Value, Utility.key)
                                    + Utility.DEC_GEN(notification.Attributes[4].Value, Utility.key) + Utility.DEC_GEN(notification.Attributes[5].Value, Utility.key);
                    }
                }

                List<Notification> updateNotif = new List<Notification>();
                bool found = false;

                //only keep in memory notifications that appear in the database too
                //and only load notifications from the database that are not already present in memory and that are not yet read
                foreach (Notification notification in Utility.notifications)
                {
                    found = false;
                    foreach (Notification notification1 in notif)
                        if (notification.equalId(notification1.getId()) && notification.getRead() == 0) { updateNotif.Add(notification); found = true; break; }

                    if (found == false) updateNotif.Add(notification);
                }


                //use the Utility.notifications variable to point to the updated notification list
                Utility.notifications.Clear(); //clear the list
                Utility.notifications = updateNotif;

                //update the XML file
                Utility.writeNotificationsToXmlFile(path);


            }
            catch (Exception exception)
            {
                Utility.DisplayError("XML_updating_notifications_failed", exception, "SYSTEM: Failed to update the XML notifications file: \n" + exception.ToString(), false);
            }

        }


        //mark the notification with the given ID as 'read'
        public static void markNotificationAsRead(int id)
        {
            foreach (Notification notification in Utility.notifications)
                if (notification.getId() == id)
                {
                    notification.setRead(1);
                    break;
                }

            //delete the notification from the local XML file
            Utility.writeNotificationsToXmlFile(Utility.XML_notifications_userFolder + Utility.username+"\\"+Utility.username+"_notifications.xml");
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
                Utility.DisplayError("Load_languages_failed",exception,"SYSTEM: Faield to load langauges: \n"+exception.ToString(),true);
                return;
            }

            //save the language names into the dictionary
            try
            {
                foreach (XmlNode language in root.ChildNodes)
                {
                    Utility.language_list.Add(index, language.Name);
                    index++;
                }
            }catch (Exception exception)
            {
                Utility.DisplayError("XML_format_error", exception, "SYSTEM: Languages XML file format error: \n" + exception.ToString(), true);
                return;
            }



            if (Utility.language_list.Count == 0) //translations not found
            {
                MessageBox.Show("EN: ERORR LOADING TRANSLATION PLEASE SWITCH TO ANOTHER LANGUAGE", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utility.error_log.Add("ERROR_LOADING_TRANSLATION");
               //Utility.ERR = true;
                Utility.WARNING = true;
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
                Utility.DisplayError("Load_languages_failed",exception,"SYSTEM: Failed to load the languages from the language XML file: \n"+exception.ToString(), true);
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
                                                /*SUPPORTED VARIABLES (AS OF 25-DEC-2022)
                                                 * #USERNAME# - username
                                                 * #NUMBER_NOTIFICATIONS# - the number of currently unread notifications
                                                 */

                                                variable_text = variable_text.Replace("#USERNAME#", Utility.username);
                                                variable_text = variable_text.Replace("#NUMBER_NOTIFICATIONS#", Utility.notifications.Count.ToString());
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
                Utility.DisplayError("XML_format_error",exception,"SYSTEM: Languages XML file format error; \n"+exception.ToString(), true);
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

            //load messages
            Utility.getMessages();

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
                Utility.DisplayError("Load_themes_failed", exception,"SYSTEM: Failed to load languages XML file: \n"+exception.ToString(),true);
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
                Utility.DisplayError("XML_format_error", exception, "SYSTEM: Themes XML file format error: \n" + exception.ToString(), true);
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

                Utility.DisplayError("XML_format_error", exception, "SYSTEM: Themes XML file format error: \n" + exception.ToString(), true);
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
                                    else if (ctrl_list.Length == 0) MessageBox.Show(Utility.displayError("XML_theme_control_not_found") + control.Name, "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);//display info message
                                                                                                                                                                                                                  //about a control not being found
                                    else
                                    { //error
                                        Utility.DisplayError("Form_duplicate_controls",new Exception(""),"SYSTEM: A form contains duplicate controls in the themes XML file: "+control.Name.ToString(),true);
                                        return "#ERR#";
                                    }
                                }
                            }


            }catch (Exception exception)
            {
                Utility.DisplayError("XML_format_error", exception, "SYSTEM: Themes XML file format error: \n" + exception.ToString(), true);
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
                Utility.DisplayError("Code_wrong_function_call",new Exception(""), "CODE: Wrong function call (check parameters and returned values); function called: Utility.savePreference()", false);
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
                Utility.DisplayError("XML_format_error",exception,"SYSTEM: Preferences XML file format error: \n"+exception.ToString(),true);
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
                Utility.DisplayError("XML_format_error", exception, "SYSTEM: Preferences XML file format error: \n" + exception.ToString(), true);
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
                Utility.DisplayError("XML_format_error", exception, "SYSTEM: Preferences XML file format error: \n" + exception.ToString(), true);
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
                Utility.DisplayError("XML_format_error", exception, "SYSTEM: Preferences XML file format error: \n" + exception.ToString(), true);
            }
            

        }


        //set the correct language and theme on the buttons in the login form
        public static string[] setLoginButtonsText()
        {
            string[] preferences = new string[2];
            preferences[0] = Utility.language_list[Utility.language];
            preferences[1] = Utility.theme_list[Utility.theme];

            return preferences;
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
                if (Utility.conn == null) return null; //no database connected

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
                Utility.DisplayError("DB_conn_failed", exception, "SYSTEM: Themes XML file format error: \n" + exception.ToString(), true);
            }

            return connection_details;
        }

        //add a new entry into the diagnostic log (Utility.error_log); this function is used when the displayError() function is not to be used
        public static void logDiagnosticEntry(string message)
        {
            Utility.error_log.Add(message);
        }

        //display an error message and log a diagnostic entry and restarts the clear error timer; exit the application if requested
        public static void DisplayError(string errorKey, Exception exception, string diagnosticEntry, bool exitApplication)
        {
            try
            {
                MessageBox.Show(Utility.displayError(errorKey) + exception.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); //display an error message
                Utility.logDiagnosticEntry(diagnosticEntry + "; " + exception.ToString()); //add a new entry into the diagnostic log
                Utility.ERR = true;
                Utility.WARNING = true;
                Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared

                if(exitApplication==true) //exist application
                {
                    Application.Exit();
                }

            }catch (Exception ex)
            {
                MessageBox.Show("Cannot display error; error key: "+errorKey.ToString()+"; details: "+ex.Message,"SYSTEM ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utility.logDiagnosticEntry("System: cannot display error. Error key: " + errorKey.ToString() + "; details: " + ex.ToString());
                Utility.ERR = true;
                Utility.WARNING = true;
                Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared

                if (exitApplication == true) //exist application
                {
                    Application.Exit();
                }
            }
        }
            //overload for the above function - allows the specification of custom message box buttons and icon
        public static void DisplayError(string errorKey, Exception exception, string diagnosticEntry, bool exitApplication, MessageBoxButtons messageBoxButton, MessageBoxIcon messageBoxIcon)
        {
            try
            {
                MessageBox.Show(Utility.displayError(errorKey) + exception.Message, "ERROR", messageBoxButton, messageBoxIcon); //display an error message
                Utility.logDiagnosticEntry(diagnosticEntry + "; " + exception.ToString()); //add a new entry into the diagnostic log
                Utility.ERR = true;
                Utility.WARNING = true;
                Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared

                if (exitApplication == true) //exist application
                {
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot display error; error key: " + errorKey.ToString() + "; details: " + ex.Message, "SYSTEM ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utility.logDiagnosticEntry("System: cannot display error. Error key: " + errorKey.ToString() + "; details: " + ex.ToString());
                Utility.ERR = true;
                Utility.WARNING = true;
                Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared

                if (exitApplication == true) //exist application
                {
                    Application.Exit();
                }
            }
        }

        //display warning error / message and add a corresponding entry into the diagnostic log and restart the clear error timer
        public static void DisplayWarning(string errorKey, Exception exception, string diagnosticEntry, bool exitApplication)
        {
            try
            {
                MessageBox.Show(Utility.displayError(errorKey) + exception.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); //display an error message
                if(diagnosticEntry!=null) Utility.logDiagnosticEntry(diagnosticEntry + "; " + exception.ToString()); //add a new entry into the diagnostic log
                Utility.WARNING = true;
                Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared

                if (exitApplication == true) //exist application
                {
                    Application.Exit();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot display error; error key: " + errorKey.ToString() + "; details: " + ex.Message, "SYSTEM ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utility.logDiagnosticEntry("System: cannot display error. Error key: " + errorKey.ToString() + "; details: " + ex.ToString());
                Utility.WARNING = true;
                Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared

                if (exitApplication == true) //exist application
                {
                    Application.Exit();
                }
            }
        }
        //overload for the above function - allows the specification of custom message box buttons and icon
        public static void DisplayWarning(string errorKey, Exception exception, string diagnosticEntry, bool exitApplication, MessageBoxButtons messageBoxButton, MessageBoxIcon messageBoxIcon)
        {
            try
            {
                MessageBox.Show(Utility.displayError(errorKey) + exception.Message, "ERROR", messageBoxButton, messageBoxIcon); //display an error message
                if(diagnosticEntry!=null) Utility.logDiagnosticEntry(diagnosticEntry + "; " + exception.ToString()); //add a new entry into the diagnostic log
                Utility.WARNING = true;
                Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared

                if (exitApplication == true) //exist application
                {
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot display error; error key: " + errorKey.ToString() + "; details: " + ex.Message, "SYSTEM ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utility.logDiagnosticEntry("System: cannot display error. Error key: " + errorKey.ToString() + "; details: " + ex.ToString());
                Utility.WARNING = true;
                Start.f0_logIn.F0_timer_errorClear.Stop(); //stop the timer for the error flags to be cleared
                Start.f0_logIn.F0_timer_errorClear.Start(); //start the timer for the error flags to be cleared

                if (exitApplication == true) //exist application
                {
                    Application.Exit();
                }
            }
        }

        #endregion


        #region groups

        //check if te file at the given path is in the under the main 'management_system' folder of the program (returns 'true' if so)
        public static bool managementSystemDataFile(string filePath)
        {
            try
            {
                //get the path to the folder 'management_system'
                string[] path = Directory.GetCurrentDirectory().Split('\\');
                string mainFolderPath = "";
                for (int i = 0; i < path.Length - 3; i++)
                {
                    mainFolderPath += path[i] + "\\";

                }
                mainFolderPath += path[path.Length - 3];

                if (Directory.GetParent(filePath).Parent.Parent.Parent.FullName.ToLower().Equals(mainFolderPath.ToLower()) &&
                Directory.GetParent(filePath).Parent.Parent.Name.ToLower().Equals("data"))
                {
                    return true; //management system DATA file
                }
            }
            catch (Exception exception)
            {
                Utility.DisplayError("Groups_failed_to_verify_if_file_is_management_system_data_file", exception, "Group: Failed to verify if the file at the given path is a Management System DATA file: " + exception.ToString(), false);
                return false; //error
            }

            return false; //not a management system DATA file
        }

        //check the name of the group
        private static bool validGroupNameFromDB(string groupName)
        {
            if(groupName.Length > Utility.maxGroupNameLength) //name length check
            {
                Utility.DisplayError("Groups_failed_to_load_group_NameTooLongInDB",new Exception(), "Error loading group: " + groupName + "; the name from the database was too long",false);
                return false;
            }

            //check characters
            foreach(char c in groupName)
            {
                if((c<'A' || c>'Z') && (c<'a' || c>'z') && (c<'0' || c>'9') && c!='_') //invalid character
                {
                    Utility.DisplayError("Groups_failed_to_load_group_InvalidCharacter", new Exception(""),"Error loading group: " + groupName + "; invalid character found: " + c.ToString(),false);
                    return false;
                }
            }

            //no errors
            return true;

        }

        //loads a .bmp image from the database
        private static Image getSqlImage(byte[] byteBuffer)
        {
            Image image = null;
            try
            {
                
                Stream db_icon;

                db_icon = new MemoryStream(byteBuffer);

                image = Image.FromStream(db_icon);

            }
            catch (Exception exception)
            {
                Utility.DisplayError("Groups_failed_to_load_group_InvalidCharacter", exception, "Error loading group: " + exception.Message + "; invalid image ", false);
                return null;
            }

            return image;
        }

        //load into memory the groups this user has access to
        public static void loadGroups()
        {
            try
            {
                SqlCommand cmd_getGroups = Utility.getSqlCommand("SELECT * FROM GroupIndex"); //select all groups from the database
                SqlDataReader dr_groups = cmd_getGroups.ExecuteReader();

                string name, author;
                bool adminGroup;
                DateTime dateCreated; 
                Image icon;
                byte[] imageBuffer = new byte[4096];
                int imageBufferLength = 4096; //bytes

                while (dr_groups.Read())
                {
                    //table layout
                    //group name | author | date created | admin group | icon (.bmp)

                    //store details
                    name = dr_groups.GetString(0);
                    author = dr_groups.GetString(1);
                    try
                    {
                        dateCreated = DateTime.Parse(dr_groups.GetString(2));
                    }catch(Exception exception)
                    {
                        dateCreated = DateTime.Parse("1.1.1970"); //default value - Epoch
                        Utility.DisplayWarning("Groups_failed_to_load_groups_invalid_date", exception, "Error loading group (invalid creation date):" + exception.Message, true);

                    }
                    adminGroup = true ? dr_groups.GetInt32(3) == 1 : false;
                    dr_groups.GetBytes(4, (long)0, imageBuffer, 0, imageBufferLength); //loads the byte array into memory
                    icon = Utility.getSqlImage(imageBuffer); //get the group icon from the database
                        
                    //check details
                    if(Utility.validGroupNameFromDB(name)==true) //valid name
                    {
                        if(author.Length<=Utility.maxUsernameLength) //valid username length
                        {
                            Utility.userGroups.Add(new Group(name, //group name
                                             author, //author
                                             dateCreated, //date created
                                             Utility.DB_name, //database name
                                             adminGroup, //admin group (true/false)
                                             icon//group icon
                                             ));
                        }
                    }

                    
                }


                dr_groups.Close();
                cmd_getGroups.Dispose();


            }
            catch (Exception exception)
            {
                Utility.DisplayError("Groups_failed_to_load_groups",exception, "Error loading groups: \n" + exception.ToString(),true);
            }
        }

        //find and return a group specified by name
        public static Group getGroupByName(string name)
        {
            try
            {
                foreach(Group group in Utility.userGroups)
                {
                    if(group.getName().Equals(name))
                        return group; //group found
                }


            }catch(Exception exception)
            {
                Utility.DisplayError("Groups_failed_to_find_group_by_name", exception, "Group: Failed to find the group: " + name.ToString()+"; details: " + exception.ToString(), false);
                return null; //group not found
            }

            return null; //group not found
        }

        //delete a group given by Group object
        public static bool deleteGroup(Group group)
        {
            try
            {
                string name = group.getName();

                //delete database tables associated with the group
                SqlCommand deleteGroupTable = Utility.getSqlCommand("SELECT * FROM "+name+"_DatabaseFiles");
                SqlDataReader dr = deleteGroupTable.ExecuteReader();
                List<string> tables = new List<string>(); //list of database tables in the group

                while(dr.Read())
                {
                    if(dr.GetInt32(2)==1) //if the current file is a database table, delete the table
                    {
                        tables.Add(dr.GetString(0)); //add table name in the 'tables' list
                    }
                }

                dr.Close();

                foreach(string table in tables)
                {
                    deleteGroupTable = Utility.getDropTableSqlCommand(table); //delete the table
                    deleteGroupTable.ExecuteNonQuery();
                }

                //delete the _DatabaseFiles table
                deleteGroupTable = Utility.getDropTableSqlCommand(name+"_DatabaseFiles");
                deleteGroupTable.ExecuteNonQuery();

                //delete the entry associated with this group from the GroupIndex table
                deleteGroupTable = Utility.getSqlCommand("DELETE FROM GroupIndex WHERE name='"+Utility.ENC_GEN(name,Utility.genKey) +"'");
                deleteGroupTable.ExecuteNonQuery();

                //delete the GroupName_Users table
                deleteGroupTable = Utility.getDropTableSqlCommand(name + "_Users");
                deleteGroupTable.ExecuteNonQuery();

                //delete local folder of the group
                Directory.Delete(group.getLocalFilesPath(),true);

                //delete the group from the userGroups list
                Utility.userGroups.Remove(group);

                return true; //group deleted
            }
            catch (Exception exception)
            {
                Utility.DisplayError("Groups_failed_to_delete_a_group", exception, "Group: Failed to delete group: " + group.getName().ToString()+"; details: " + exception.ToString(), false) ;
                return false; //group was not deleted
            }

            //return false; //group was not deleted
        }

        //create a new group
        public static int createNewGroup(string groupName, string author, DateTime dateCreated, bool adminGroup, Image icon)
        {

            try
            {
                //if the group name already exists in the group table, do not create it
                SqlCommand checkDuplicateGroup = Utility.getSqlCommand("SELECT * FROM GroupIndex");
                SqlDataReader dr = checkDuplicateGroup.ExecuteReader();

                while (dr.Read())
                {
                    if(dr.GetString(0).Equals(Utility.ENC_GEN(groupName,Utility.genKey))==true) //group name already exists in the GroupIndex database table
                    {
                        Utility.logDiagnosticEntry("Group: the group name already exists in the GroupIndex database table and cannot be used again: " + groupName);
                        dr.Close();
                        return -1; //group not created - duplicate names
                    }
                }
                
                dr.Close();
                checkDuplicateGroup.Dispose();

                //if the name already exists in memory, do not create the group again
                foreach (Group group in Utility.userGroups)
                    if (group.getName().Equals(groupName))
                    {
                        Utility.logDiagnosticEntry("Group: the group name already exists in memory and cannot be used again: " + groupName);

                        return -1; //group not created - duplicate names
                    }
                
                //add the details of the new group into memory
                Utility.userGroups.Add(new Group(groupName,
                                                 author,
                                                 dateCreated,
                                                 Utility.DB_name,
                                                 adminGroup,
                                                 icon
                                                ));
            }catch(Exception exception) 
            {
                Utility.DisplayError("Groups_failed_to_load_group_into_memory", exception, "Group: Failed to load group: " + groupName + " into memory: " + exception.ToString(), false); ;
                return -2; //group not created - error
            }

            return 0; //group created
        }

        //load groups from the database into memory
        public static void loadGroupsIntoMemory()
        {
            SqlDataReader dr = null;
            try
            {
                SqlCommand readGroups = Utility.getSqlCommand("SELECT * FROM GroupIndex");
                dr = readGroups.ExecuteReader();
                List<tempGroup> tempList = new List<tempGroup>(); //temporary memory struct
                bool userHasAccess = false;

                //add group details into temporary memory storage
                while(dr.Read())
                {
                    tempList.Add(new tempGroup(
                            Utility.DEC_GEN(dr.GetString(0), Utility.genKey), //group name
                            Utility.DEC_GEN(dr.GetString(1), Utility.genKey), //group author
                            Utility.DEC_GEN(dr.GetString(2), Utility.genKey), //creation date of the group
                            Utility.DEC_GEN(dr.GetInt32(3), Utility.genKey), //adminGroup (1=true, 0 = false)
                            null,                                       //image, //image - DEV
                            Utility.DEC_GEN(dr.GetInt32(5), Utility.genKey), //cryptographic key

                            Utility.getLastDataBaseConnString()//currently connected database - from which the details are read
                            )
                            );
                }
                dr.Close();

                //check which groups the current user has access to by checking the GroupName_Users table of each group
                for(int i = 0; i<tempList.Count; i++) 
                {
                    readGroups = Utility.getSqlCommand("SELECT * FROM " + tempList[i].getName()+"_Users");
                    dr = readGroups.ExecuteReader();

                    userHasAccess = false;

                    while (dr.Read())
                    {
                        if(dr.GetString(0).Equals(Utility.DB_HASH(Utility.username))==true) //the current user has access to the current group
                        {
                            userHasAccess = true;
                            break;
                        }
                    }

                    if(userHasAccess==false) //delete the group from temporary memory
                    {
                        tempList.Remove(tempList[i]);
                        i--;
                    }

                    dr.Close();
                }

                //move details from temporary storage to the userGroups list
                foreach(tempGroup group in tempList)
                {
                    Utility.userGroups.Add(new Group(//already defined group constructor
                        group.getName(),
                        group.getAuthor(),
                        group.getDateCreated(),
                        group.getAdminGroup(),
                        //group.getIcon(), //DEV
                        group.getKey(),
                        group.getDatabase()
                        )
                        ) ;
                }

                /*
                while (dr.Read())
                {
                    //DEV
                    //byte[] image = new byte[256];
                    //dr.GetBytes(4, 0, image, 0, 256);

                    
                    Utility.userGroups.Add(new Group( //already defined group
                        Utility.DEC_GEN(dr.GetString(0),Utility.genKey), //group name
                        Utility.DEC_GEN(dr.GetString(1),Utility.genKey), //group author
                        Utility.DEC_GEN(dr.GetString(2), Utility.genKey), //creation date of the group
                        Utility.DEC_GEN(dr.GetInt32(3),Utility.genKey), //adminGroup (1=true, 0 = false)
                        //image, //image - DEV
                        Utility.DEC_GEN(dr.GetInt32(5),Utility.genKey) //cryptographic key
                        )
                        );
                    


                }
                */


                //dr.Close();
                readGroups.Dispose();


            }catch(Exception exception)
            {
                if (dr != null) dr.Close();
                Utility.DisplayError("Groups_failed_to_load_groups_form_database_into_memory", exception, "Group: Failed to load groups from the database indexing table into memory: " + exception.ToString(), false);
                
            }
        }

        #endregion
    }
}
