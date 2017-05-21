using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Coding4Fun.Phone.Controls.Data;
using System.IO.IsolatedStorage;
using System.Diagnostics;

namespace SC2BuildOrder
{
    public partial class App : Application
    {
         IsolatedStorageSettings _IsolatedStorageSettings = IsolatedStorageSettings.ApplicationSettings;

        private static MainViewModel viewModel = null;

        /// <summary>
        /// A static ViewModel used by the views to bind against.
        /// </summary>
        /// <returns>The MainViewModel object.</returns>
        public static MainViewModel ViewModel
        {
            get
            {
                // Delay creation of the view model until necessary
                if (viewModel == null)
                    viewModel = new MainViewModel();

                return viewModel;
            }
        }

        /// <summary>
        /// Provides easy access to the root frame of the Phone Application.
        /// </summary>
        /// <returns>The root frame of the Phone Application.</returns>
        public PhoneApplicationFrame RootFrame { get; private set; }

        /// <summary>
        /// Constructor for the Application object.
        /// </summary>
        public App()
        {
            // Global handler for uncaught exceptions. 
            UnhandledException += Application_UnhandledException;

            // Standard Silverlight initialization
            InitializeComponent();

            // Phone-specific initialization
            InitializePhoneApplication();

            // Show graphics profiling information while debugging.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // Display the current frame rate counters.
                Application.Current.Host.Settings.EnableFrameRateCounter = true;

                // Show the areas of the app that are being redrawn in each frame.
                //Application.Current.Host.Settings.EnableRedrawRegions = true;

                // Enable non-production analysis visualization mode, 
                // which shows areas of a page that are handed off to GPU with a colored overlay.
                //Application.Current.Host.Settings.EnableCacheVisualization = true;

                // Disable the application idle detection by setting the UserIdleDetectionMode property of the
                // application's PhoneApplicationService object to Disabled.
                // Caution:- Use this under debug mode only. Application that disables user idle detection will continue to run
                // and consume battery power when the user is not using the phone.
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }



            // Check version of app
            bool IsVersionChanged = false;
            if (_IsolatedStorageSettings.Contains("AppCurrentVersion"))
            {
                if (_IsolatedStorageSettings["AppCurrentVersion"].Equals(PhoneHelper.GetAppAttribute("Version")))
                {
                    Debug.WriteLine("Version hasn't changed"); // do nothing
                }
                else
                {
                    // App Version has changed
                    _IsolatedStorageSettings.Remove("AppCurrentVersion");
                    _IsolatedStorageSettings.Add("AppCurrentVersion", PhoneHelper.GetAppAttribute("Version"));
                    Debug.WriteLine("New Version");
                    IsVersionChanged = true;
                    Debug.WriteLine("App Current Version = " + _IsolatedStorageSettings["AppCurrentVersion"]);
                }
            }
            else
            {
                // add the Version setting
                _IsolatedStorageSettings.Add("AppCurrentVersion", PhoneHelper.GetAppAttribute("Version"));
                Debug.WriteLine("App Current Version = " + _IsolatedStorageSettings["AppCurrentVersion"]);
            }
             _IsolatedStorageSettings.Save(); 
           

            // Create the database if it does not exist.
            using (Index.DataContext_Index DB_Index = new Index.DataContext_Index(Index.DataContext_Index.DBConnectionString))
            {

#if(DEBUG)
                Debug.WriteLine("Deleting database DB_Index");
                DB_Index.DeleteDatabase();
#endif
                
                if (DB_Index.DatabaseExists() == false)
                {
                    //Create the database
                    DB_Index.CreateDatabase();
                }
                else if (DB_Index.DatabaseExists() == true && IsVersionChanged == true)
                {
                    // rebuild the database for new version of app
                    DB_Index.DeleteDatabase();
                    DB_Index.CreateDatabase();
                }

            }

            using (Build_Order.DataContext_Build_Order DB_Build_Order = new Build_Order.DataContext_Build_Order(Build_Order.DataContext_Build_Order.DBConnectionString))
            {

#if(DEBUG)
                Debug.WriteLine("Deleting database DB_Build_Order");
                DB_Build_Order.DeleteDatabase();
#endif

                if (DB_Build_Order.DatabaseExists() == false)
                {
                    //Create the database
                    DB_Build_Order.CreateDatabase();
                }
                else if (DB_Build_Order.DatabaseExists() == true && IsVersionChanged == true)
                {
                    // rebuild the database for new version of app
                    DB_Build_Order.DeleteDatabase();
                    DB_Build_Order.CreateDatabase();
                }
            }

            using (Object_SC.DataContext_Object_SC DB_Object_SC = new Object_SC.DataContext_Object_SC(Object_SC.DataContext_Object_SC.DBConnectionString))
            {
#if(DEBUG)
                Debug.WriteLine("Deleting database DB_Object_SC");
                DB_Object_SC.DeleteDatabase();
#endif
                if (DB_Object_SC.DatabaseExists() == false)
                {
                    //Create the database
                    DB_Object_SC.CreateDatabase();
                    DB_Helper.enterObjectSC_Information();
                    DB_Helper.enterBuilt_In_BuildOrder();
                }
                else if (DB_Object_SC.DatabaseExists() == true && IsVersionChanged == true)
                {
                    // rebuild the database for new version of app
                    DB_Object_SC.DeleteDatabase();
                    DB_Object_SC.CreateDatabase();
                    DB_Helper.enterObjectSC_Information();
                    DB_Helper.enterBuilt_In_BuildOrder();
                }
            }            
        }

        // Code to execute when the application is launching (eg, from Start)
        // This code will not execute when the application is reactivated
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
        }

        // Code to execute when the application is activated (brought to foreground)
        // This code will not execute when the application is first launched
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
            //// Ensure that application state is restored appropriately
            //if (!App.ViewModel.IsDataLoaded)
            //{
            //    App.ViewModel.LoadData();
            //}
        }

        // Code to execute when the application is deactivated (sent to background)
        // This code will not execute when the application is closing
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
            // Ensure that required application state is persisted here.
        }

        // Code to execute when the application is closing (eg, user hit Back)
        // This code will not execute when the application is deactivated
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
        }

        // Code to execute if a navigation fails
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // A navigation has failed; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        // Code to execute on Unhandled Exceptions
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        #region Phone application initialization

        // Avoid double-initialization
        private bool phoneApplicationInitialized = false;

        // Do not add any additional code to this method
        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            // Create the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            RootFrame = new TransitionFrame();
            //RootFrame = new PhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            // Handle navigation failures
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // Ensure we don't initialize again
            phoneApplicationInitialized = true;
        }

        // Do not add any additional code to this method
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Set the root visual to allow the application to render
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            // Remove this handler since it is no longer needed
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        #endregion
    }
}