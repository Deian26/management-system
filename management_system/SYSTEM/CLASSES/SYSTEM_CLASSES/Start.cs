/*
    Management System / „Sistem de administrare a fisierelor”
    
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
        
        static public F9_Logo f9_logo;
        
        //utility service
        static public U0_UtilityMainForm u0_utilityMainForm;
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
            u0_utilityMainForm = new U0_UtilityMainForm();
            //u0_utilityMainForm.Hide();
            Application.Run(u0_utilityMainForm);
            
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
            //Application.Run(f0_logIn);

            f9_logo = new F9_Logo();
            Application.Run(f9_logo);
        
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

            }
        }


        
    }
}
