using System;
using System.Globalization;

/// <summary>
/// Global namespace.
/// </summary>
namespace MeasuringGarbageCollectionOverhead
{
    /// <summary>
    /// Class for calling the garbage collector
    /// </summary>
    public class Finalizable
    {
        private int number;
        private byte[] memory = new byte[2100000000];
        private static object lockObject = new object();

        /// <summary>
        /// Method that is called to invoke the garbage collector
        /// </summary>
        /// <param name="number">Serial number of the object</param>
        public Finalizable(int number)
        {
            lock (lockObject)
            {
                this.number = number;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Object {0} is created", number);
            }
        }

        /// <summary>
        /// Method that is called during garbage collection
        /// </summary>
        ~Finalizable()
        {
            lock (lockObject)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.ffff", CultureInfo.InvariantCulture));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Object {0} is disposed", number);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.ffff", CultureInfo.InvariantCulture) + "\n");
            }
        }
    }
}
