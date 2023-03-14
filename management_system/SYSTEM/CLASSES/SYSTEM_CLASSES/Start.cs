/*
    Management System
    
    Developer: Trif, Paul-Deian
 */

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

        //main thread
        static public Thread main_thread = null;

        
        
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
            //u0_utilityMainFrom.Hide();
            Application.Run(u0_utilityMainFrom);
            
        }

        //main thread
        public static void initializeMainThread()
        {
            Start.main_thread = new Thread(new ThreadStart(startMainThread));
            Start.main_thread.SetApartmentState(ApartmentState.STA);
            Start.main_thread.Start();
        }

        public static void startMainThread()
        {
            f0_logIn = new F0_Login();
            Application.Run(f0_logIn);
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

                Start.initializeUtilityService(); //start the utility service thread
                Start.initializeMainThread(); //start the main thread

                //DEV - DEBUG: to be deleted after development
                F5_FileEditorForm f5_debug = new F5_FileEditorForm();
                Application.Run(f5_debug);

                /* DEV - obsolete?
                //stop the main thread
                try
                {
                    Start.main_thread.Abort();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(Utility.displayError("Code_main_thread_termination_error") + exception.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Utility.ERR = true;
                    Application.Exit();
                }
                finally { Application.Exit(); }

                //stop the utility service
                try
                {
                    Start.utility_thread.Abort();
                }catch(Exception exception) 
                {
                    MessageBox.Show(Utility.displayError("Code_utility_thread_termination_error")+exception.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Utility.ERR = true;
                    Application.Exit();
                }
                finally { Application.Exit(); }
                */
            }
        }


        
    }
}
