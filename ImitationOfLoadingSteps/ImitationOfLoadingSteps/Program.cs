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
        private static String result = "Program completed successfully";

        /// <summary>
        /// Launches programs
        /// </summary>
        private static void Main()
        {
            try
            {
                var splash = Task.Run(() => ShowSplash());
                splash.Wait();
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
                Console.WriteLine($"\n\n\n*{result}*");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Show splash
        /// </summary>
        private static void ShowSplash()
        {
            String name = "Show splash";
            Console.Write(name);
            Console.Write(" (OK)");
        }

        /// <summary>
        /// Request license
        /// </summary>
        private static void RequestLicense()
        {
            String name = "Request License";
            Console.Write($"\n\n{name}");
            RandomException(name);
        }

        /// <summary>
        /// Check update
        /// </summary>
        private static void CheckUpdate()
        {
            String name = "Check for Update";
            Console.Write($"\n\n{name}");
            RandomException(name);
        }

        /// <summary>
        /// Download update
        /// </summary>
        private static void DownloadUpdate()
        {
            String name = "Download Update";
            Console.Write($"\n\n{name}");
            RandomException(name);
        }

        /// <summary>
        /// Show display screen
        /// </summary>
        private static void DisplayScreen()
        {
            String name = "Display Welcome Screen";
            Console.Write($"\n\n{name}");
            Console.Write(" (OK)");
        }

        /// <summary>
        /// Show hide splash
        /// </summary>
        private static void HideSplash()
        {
            String name = "Hide Splash";
            Console.Write($"\n\n{name}");
            Console.Write(" (OK)");
        }

        /// <summary>
        /// Show setup menus
        /// </summary>
        private static void SetupMenus()
        {
            String name = "Setup Menus";
            Console.Write($"\n\n{name}");
            RandomException(name);
        }

        /// <summary>
        /// Create random exception
        /// </summary>
        /// <param name="name"> The name of the method in which the error occurred </param>
        private static void RandomException(String name)
        {
            try
            {
                Random rnd = new Random();
                int value = rnd.Next(0, 10);
                if ((value % 5) == 0)
                {
                    throw new MyException(name);
                }
                Console.Write(" (OK)");
            }
            catch (MyException e)
            {
                Console.Write($" (Failure --> Error with : {e.Message})");
                result = "Program not completed";
            }
        }
    }
}
