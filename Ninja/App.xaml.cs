﻿

namespace Ninja
{
    using System.Collections.Generic;
    using System.Windows.Media;
    using Localization;
    using OfficeOpenXml;
    using Profiles;
    using Settings;
    using Syncfusion.Licensing;
    using Syncfusion.SfSkinManager;
    using Syncfusion.Themes.FluentDark.WPF;
    using Utilities;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Windows;
    using System.Windows.Threading;
    using log4net;
    using Ninja.Localization.Resources;

    /*
     * Class: App
     * 1) Get command line args
     * 2) Detect current configuration
     * 3) Get assembly info
     * 4) Load settings
     * 5) Load localization / language
     *
     * Class: MainWindow
     * 6) Load appearance
     * 7) Load profiles
     */

    public partial class App
    {
        // Single instance identifier
        private const string Guid = "6A3F34B2-161F-4F70-A8BC-A19C40F79CFB";
        private static readonly ILog Log = LogManager.GetLogger(typeof(App));
        private DispatcherTimer _dispatcherTimer;
        private Mutex _mutex;

        private bool _singleInstanceClose;

        /// <summary>
        /// The controls
        /// </summary>
        public static string[] Controls =
        {
            "ComboBoxAdv",
            "MetroComboBox",
            "MetroDatagrid",
            "SfDataGrid",
            "ToolBarAdv",
            "ToolStrip",
            "MetroCalendar",
            "CalendarEdit",
            "PivotGridControl",
            "MetroPivotGrid",
            "SfChart",
            "SfChart3D",
            "SfHeatMap",
            "SfMap",
            "MetroMap",
            "EditControl",
            "CheckListBox",
            "MetroEditor",
            "DropDownButtonAdv",
            "MetroDropDown",
            "TextBoxExt",
            "SfCircularProgressBar",
            "SfLinearProgressBar",
            "GridControl",
            "MetroGridControl",
            "TabControlExt",
            "MetroTabControl",
            "SfTextInputLayout",
            "MetroTextInput",
            "SfSpreadsheet",
            "SfSpreadsheetRibbon",
            "MenuItemAdv",
            "ButtonAdv",
            "Carousel",
            "ColorEdit",
            "SfCalculator",
            "SfMultiColumnDropDownControl"
        };

        /// <summary>
        /// Gets or sets the windows.
        /// </summary>
        /// <value>
        /// The windows.
        /// </value>
        public static IDictionary<string, Window> ActiveWindows { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            ShutdownMode = ShutdownMode.OnMainWindowClose;
            AppDomain.CurrentDomain.UnhandledException += App.OnUnhandledException;
            var _key = System.Configuration.ConfigurationManager.AppSettings["UI"];
            SyncfusionLicenseProvider.RegisterLicense(_key);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            App.ActiveWindows = new Dictionary<string, Window>();
            RegisterTheme( );
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var startLog = $@"
  _   _ _____ _____                    _    __  __                                   
 | \ | | ____|_   _|_      _____  _ __| | _|  \/  | __ _ _ __   __ _  __ _  ___ _ __ 
 |  \| |  _|   | | \ \ /\ / / _ \| '__| |/ / |\/| |/ _` | '_ \ / _` |/ _` |/ _ \ '__|
 | |\  | |___  | |  \ V  V / (_) | |  |   <| |  | | (_| | | | | (_| | (_| |  __/ |   
 |_| \_|_____| |_|   \_/\_/ \___/|_|  |_|\_\_|  |_|\__,_|_| |_|\__,_|\__, |\___|_|   
                                                                     |___/        
            
                                               by Terry Eppler
                                               GitHub.com/is-leeroy-jenkins/ Ninja

                                               Version: {AssemblyManager.Current.Version}
";
            Log.Info(startLog);

            // Catch unhandled exception globally
            AppDomain.CurrentDomain.UnhandledException += (_, args) =>
            {
                Log.Fatal("Unhandled exception occured!");

                Log.Fatal($"Exception raised by: {args.ExceptionObject}");
            };

            // Wait until the previous instance has been closed (restart from ui)
            if (CommandLineManager.Current.RestartPid != -1)
            {
                Log.Info(
                    $"Waiting for another Ninjaprocess with Pid {CommandLineManager.Current.RestartPid} to exit...");

                var processList = Process.GetProcesses();
                var process = processList.FirstOrDefault(x => x.Id == CommandLineManager.Current.RestartPid);
                process?.WaitForExit();

                Log.Info($"Ninja process with Pid {CommandLineManager.Current.RestartPid} has been exited.");
            }

            // Load (or initialize) settings
            try
            {
                Log.Info("Application settings are being loaded...");

                if (CommandLineManager.Current.ResetSettings)
                    SettingsManager.Initialize();
                else
                    SettingsManager.Load();
            }
            catch (InvalidOperationException ex)
            {
                Log.Error("Could not load application settings!");
                Log.Error(ex.Message + "-" + ex.StackTrace);

                // Create backup of corrupted file
                var destinationFile =
                    $"{TimestampHelper.GetTimestamp()}_corrupted_" + SettingsManager.GetSettingsFileName();
                File.Copy(SettingsManager.GetSettingsFilePath(),
                    Path.Combine(SettingsManager.GetSettingsFolderLocation(), destinationFile));
                Log.Info($"A backup of the corrupted settings file has been saved under {destinationFile}");

                // Initialize default application settings
                Log.Info("Initialize default application settings...");

                SettingsManager.Initialize();
                ConfigurationManager.Current.ShowSettingsResetNoteOnStartup = true;
            }

            // Upgrade settings if necessary
            var settingsVersion = Version.Parse(SettingsManager.Current.Version);

            if (settingsVersion < AssemblyManager.Current.Version)
            {
                Log.Info(
                    $"Application settings are on version {settingsVersion} and will be upgraded to {AssemblyManager.Current.Version}");

                SettingsManager.Upgrade(settingsVersion, AssemblyManager.Current.Version);

                Log.Info($"Application settings upgraded to version {AssemblyManager.Current.Version}");
            }
            else
            {
                Log.Info($"Application settings are already on version {AssemblyManager.Current.Version}.");
            }

            // Initialize localization
            var localizationManager = LocalizationManager.GetInstance(SettingsManager.Current.Localization_CultureCode);
            Strings.Culture = localizationManager.Culture;

            Log.Info(
                $"Application localization culture has been set to {localizationManager.Current.Code} (Settings value is \"{SettingsManager.Current.Localization_CultureCode}\").");

            // Show (localized) help window
            if (CommandLineManager.Current.Help)
            {
                Log.Info("Set StartupUri to CommandLineWindow.xaml...");
                StartupUri = new Uri("CommandLineWindow.xaml", UriKind.Relative);

                return;
            }

            // Create mutex (to detect single instance)
            Log.Info($"Try to acquire mutex with GUID {Guid} for single instance detection...");

            _mutex = new Mutex(true, "{" + Guid + "}");
            var mutexIsAcquired = _mutex.WaitOne(TimeSpan.Zero, true);

            Log.Info($"Mutex value for {Guid} is {mutexIsAcquired}");

            // Release mutex
            if (mutexIsAcquired)
                _mutex.ReleaseMutex();

            if (mutexIsAcquired || SettingsManager.Current.Window_MultipleInstances)
            {
                // Setup background job
                if (SettingsManager.Current.General_BackgroundJobInterval != 0)
                {
                    Log.Info(
                        $"Setup background job with interval {SettingsManager.Current.General_BackgroundJobInterval} minute(s)...");

                    _dispatcherTimer = new DispatcherTimer
                    {
                        Interval = TimeSpan.FromMinutes(SettingsManager.Current.General_BackgroundJobInterval)
                    };
                    _dispatcherTimer.Tick += DispatcherTimer_Tick;
                    _dispatcherTimer.Start();
                }
                else
                {
                    Log.Info("Background job is disabled.");
                }

                // Setup ThreadPool for the application
                ThreadPool.GetMaxThreads(out var workerThreadsMax, out var completionPortThreadsMax);
                ThreadPool.GetMinThreads(out var workerThreadsMin, out var completionPortThreadsMin);

                var workerThreadsMinNew = workerThreadsMin + SettingsManager.Current.General_ThreadPoolAdditionalMinThreads;
                var completionPortThreadsMinNew = completionPortThreadsMin +
                    SettingsManager.Current.General_ThreadPoolAdditionalMinThreads;

                if (workerThreadsMinNew > workerThreadsMax)
                    workerThreadsMinNew = workerThreadsMax;

                if (completionPortThreadsMinNew > completionPortThreadsMax)
                    completionPortThreadsMinNew = completionPortThreadsMax;

                if (ThreadPool.SetMinThreads(workerThreadsMinNew, completionPortThreadsMinNew))
                    Log.Info(
                        $"ThreadPool min threads set to: workerThreads: {workerThreadsMinNew}, completionPortThreads: {completionPortThreadsMinNew}");
                else
                    Log.Warn(
                        $"ThreadPool min threads could not be set to workerThreads: {workerThreadsMinNew}, completionPortThreads: {completionPortThreadsMinNew}");

                // Show splash screen
                if (SettingsManager.Current.SplashScreen_Enabled)
                {
                    Log.Info("Show SplashScreen while application is loading...");
                    new SplashScreen(@"SplashScreen.png").Show(true, true);
                }

                // Show main window
                Log.Info("Set StartupUri to MainWindow.xaml...");
                StartupUri = new Uri("MainWindow.xaml", UriKind.Relative);
            }
            else
            {
                // Bring the already running application into the foreground
                Log.Info(
                    "Another Ninja process is already running. Trying to bring the window to the foreground...");
                SingleInstance.PostMessage(SingleInstance.HWND_BROADCAST, SingleInstance.WM_SHOWME, IntPtr.Zero,
                    IntPtr.Zero);

                // Close the application                
                _singleInstanceClose = true;
                Shutdown();
            }
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            Log.Info("Run background job...");

            Save();
        }

        protected override void OnSessionEnding(SessionEndingCancelEventArgs e)
        {
            base.OnSessionEnding(e);

            e.Cancel = true;

            Shutdown();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Log.Info("Exiting Ninja...");

            // Save settings, when the application is normally closed
            if (_singleInstanceClose || CommandLineManager.Current.Help)
                return;

            Log.Info("Stop background job (if it exists)...");
            _dispatcherTimer?.Stop();

            Save();

            Log.Info("Bye!");
        }

        private void Save()
        {
            if (SettingsManager.Current.SettingsChanged)
            {
                Log.Info("Save application settings...");
                SettingsManager.Save();
            }

            if (ProfileManager.ProfilesChanged)
            {
                Log.Info("Save current profiles...");
                ProfileManager.Save();
            }
        }

        /// <summary>
        /// Registers the theme.
        /// </summary>
        private void RegisterTheme( )
        {
            var _theme = new FluentDarkThemeSettings
            {
                PrimaryBackground = new SolidColorBrush(Color.FromRgb(20, 20, 20)),
                PrimaryColorForeground = new SolidColorBrush(Color.FromRgb(0, 120, 212)),
                PrimaryForeground = new SolidColorBrush(Color.FromRgb(222, 222, 222)),
                BodyFontSize = 12,
                HeaderFontSize = 16,
                SubHeaderFontSize = 14,
                TitleFontSize = 14,
                SubTitleFontSize = 126,
                BodyAltFontSize = 10,
                FontFamily = new FontFamily("Segoe UI")
            };

            SfSkinManager.RegisterThemeSettings("FluentDark", _theme);
            SfSkinManager.ApplyStylesOnApplication = true;
        }

        /// <summary>
        /// Handles the UnhandledException event of the CurrentDomain control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">The
        /// <see cref="UnhandledExceptionEventArgs"/>
        /// instance containing the event data.</param>
        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var _ex = e.ExceptionObject as Exception;
            Fail(_ex);
        }

        /// <summary>
        /// Fails the specified _ex.
        /// </summary>
        /// <param name="_ex">The _ex.</param>
        private static void Fail(Exception _ex)
        {
            var _error = new ErrorWindow(_ex);
            _error?.SetText();
            _error?.ShowDialog();
        }
    }
}