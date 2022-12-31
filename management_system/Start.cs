using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace management_system
{
    internal static class Start
    {
        //VARIABLES
        //forms
        
        //main
        static public F0_Login f0_logIn;
        static public F1_MainForm f1_mainForm;
        //utility service
        static public U0_UtilityMainForm u0_utilityMainFrom;
        static public Thread utility_thread = null;

        [STAThread]

        //utility service
        public static void initializeUtilityService()
        {
            Start.utility_thread = new Thread(new ThreadStart(Start.startUtilityService));
            Start.utility_thread.SetApartmentState(ApartmentState.STA);
            Start.utility_thread.Start();
        }
        private static void startUtilityService()
        {
            u0_utilityMainFrom = new U0_UtilityMainForm();
            Application.Run(u0_utilityMainFrom);
        }
        //main
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //initialize the Utility class
            Utility.Initialize();
            

            if (Utility.ERR == false)
            {
                //main
                f0_logIn = new F0_Login();
                f1_mainForm = new F1_MainForm();

                Start.initializeUtilityService(); //DEV

                //start the main program
                Application.Run(f0_logIn);

                //stop the utility service
                try
                {
                    Start.utility_thread.Abort();
                }catch(Exception exception) 
                {
                    MessageBox.Show(Utility.displayError("Code_utility_thread_termination_error")+exception.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Utility.ERR = true;
                    Application.Exit();
                }
                finally { Application.Exit(); }
            }
        }


        
    }
}
