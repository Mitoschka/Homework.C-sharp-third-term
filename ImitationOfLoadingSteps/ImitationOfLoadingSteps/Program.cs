using System;
using System.Threading.Tasks;

/// <summary>
/// Global namespace.
/// </summary>
namespace ImitationOfLoadingSteps
{
    /// <summary>
    /// Program launch
    /// </summary>
    class Program
    {
        /// <summary>
        /// Launches programs
        /// </summary>
        static void Main()
        {
            try
            {
                var splash = Task.Run(() => ShowSplash());
                var errorOfShowSplash = splash.ContinueWith(ant => Console.WriteLine(ant.Exception), TaskContinuationOptions.OnlyOnFaulted);
                var license = splash.ContinueWith(ant => RequestLicense(), TaskContinuationOptions.NotOnCanceled);
                var errorOfRequestLicense = license.ContinueWith(ant => Console.WriteLine(ant.Exception), TaskContinuationOptions.OnlyOnFaulted);
                var setupMenu = license.ContinueWith(ant => SetupMenus(), TaskContinuationOptions.NotOnCanceled);
                var errorOfSetupMenu = setupMenu.ContinueWith(ant => Console.WriteLine(ant.Exception), TaskContinuationOptions.OnlyOnFaulted);
                var update = splash.ContinueWith(ant => CheckUpdate(), TaskContinuationOptions.NotOnCanceled);
                var errorOfCheckUpdate = update.ContinueWith(ant => Console.WriteLine(ant.Exception), TaskContinuationOptions.OnlyOnFaulted);
                var download = update.ContinueWith(ant => DownloadUpdate(), TaskContinuationOptions.NotOnCanceled);
                var errorOfDownloadUpdate = download.ContinueWith(ant => Console.WriteLine(ant.Exception), TaskContinuationOptions.OnlyOnFaulted);
                var display = Task.Factory.ContinueWhenAll(new[] { setupMenu, download }, tasks => DisplayScreen())
                    .ContinueWith(ant => HideSplash());
                display.Wait();
                Console.WriteLine(display.IsCompleted);
            }
            catch(MyException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Show splash
        /// </summary>
        private static void ShowSplash()
        {
            Console.WriteLine("Show Splash");
        }

        /// <summary>
        /// Request license
        /// </summary>
        private static void RequestLicense()
        {
            Console.WriteLine("Request License");
        }

        /// <summary>
        /// Check update
        /// </summary>
        private static void CheckUpdate()
        {
            Console.WriteLine("Check for Update");
        }

        /// <summary>
        /// Download update
        /// </summary>
        private static void DownloadUpdate()
        {
            Console.WriteLine("Download Update");
        }

        /// <summary>
        /// Show display screen
        /// </summary>
        private static void DisplayScreen()
        {
            Console.WriteLine("Display Welcome Screen");
        }

        /// <summary>
        /// Show hide splash
        /// </summary>
        private static void HideSplash()
        {
            Console.WriteLine("Hide Splash");
        }

        /// <summary>
        /// Show setup menus
        /// </summary>
        private static void SetupMenus()
        {
            Console.WriteLine("Setup Menus");
        }
    }
}
