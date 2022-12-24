using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Security.Cryptography;

namespace management_system
{
    public partial class Utility
    {

        //variables
        private Dictionary<string, string> errors = new Dictionary<string, string>(); //eror messages
        private Dictionary<string, string> notifications = new Dictionary<string, string>(); //active notifications

        string user = null;
        int key = 0;

        //XML documents
        string XML_errors = "..\\..\\..\\SYSTEM\\SETTINGS\\XML\\XML_errors.xml";

        //database connection
        static string DB_connString = "Data Source=DEIAN-PC\\WINCC;Initial Catalog = Management_system; Integrated Security = True"; //DEV
        public static SqlConnection conn = new SqlConnection(Utility.DB_connString);

        //functions / methods

        //initialization
        public int Initialize(string user, int key)
        {
            this.user = user;
            this.key = key;

            //reset stored values
            this.errors.Clear();
            this.notifications.Clear();

            //update values
            this.setErrors();
            this.setNotifications(this.user, this.key);
            this.setLanguage();
            this.setTheme();

            return 1;
        }

        #region encryption
        //encryption functions

        //Used to encrypt DataBase specific data (such as usernames and passwords)
        public string ENC_DB(string input, int key)
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
        public string ENC_GEN(string input, int key)
        {
            string output = "";
            RSA rsa = RSA.Create();


            input.ToCharArray();

            return output;
        }

        //decryption functions

        //Used to decrypt DataBase specific data such as usernames and passwords (complementary to ENC_DB() )
        public string DEC_DB(string input, int key)
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
        public string DEC_GEN(string input, int key)
        {
            string output = "";


            return output;
        }

        #endregion

        //getters & setters
        #region errors
        //error messages
        //load error messages from DataBase
        public void setErrors()
        {
            //DEV
            //DEV
            string str_cmd = "select * from BASE_Errors";
            List<string> errors = new List<string>();
            int i = 0;
            try
            {
                SqlCommand cmd = new SqlCommand(str_cmd, conn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read() && !dr["notification"].ToString().Equals("") && dr["notification"].ToString() != null)
                {
                    errors.Add(dr["notification"].ToString());
                }

                dr.Close();
                cmd.Dispose();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error loading errors from the DataBase table 'BASE_Errors'.");
            }


        }
        //get error messages
        public Dictionary<string, string> getErrors()
        {
            return this.errors;
        }
        #endregion //DEV
        #region notifications
        //update notifications list
        public void setNotifications(string user, int key)
        {
            //DEV
            /* General Table structure:
             ID | notification_text | read(0/1 / false/true)
             */

            string str_cmd = "select * from " + user + "_notifications";
            List<string> notif = new List<string>();
            int i = 0;
            SqlCommand cmd = new SqlCommand(str_cmd, conn);
            SqlDataReader dr = cmd.ExecuteReader();


            while (dr.Read() && !dr["notification"].ToString().Equals("") && dr["notification"].ToString() != null)
            {
                notif.Add(dr["notification"].ToString());
            }

            dr.Close();
            cmd.Dispose();
        }
        //get notifications
        public Dictionary<string, string> getNotifications()
        {
            return this.notifications;
        }
        #endregion //DEV

        #region languages
        public void setLanguage()
        {

        }
        #endregion
        #region themes
        public void setTheme()
        {

        }
        #endregion

    }
}
