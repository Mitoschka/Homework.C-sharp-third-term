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
        private byte[] memory = new byte[2000000000];
        private static object lockObject = new object();
        private static bool isFinalizable;

        private static int startHour;
        private static int startMinute;
        private static int startSecond;
        private static int startMillisecond;
        private static DateTime start;

        private int endHour;
        private int endMinute;
        private int endSecond;
        private int endMillisecond;
        private static DateTime end;

        /// <summary>
        /// Method that is called to invoke the garbage collector
        /// </summary>
        /// <param name="number">Serial number of the object</param>
        public Finalizable(int number)
        {
            if (isFinalizable == true)
            {
                lock (lockObject)
                {
                    this.number = number;

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Object {0} is created", number);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    endHour = DateTime.Now.Hour;
                    endMinute = DateTime.Now.Minute;
                    endSecond = DateTime.Now.Second;
                    endMillisecond = DateTime.Now.Millisecond;
                    end = new DateTime(2020, 9, 14, endHour, endMinute, endSecond, endMillisecond);
                    TimeSpan interval = end - start;
                    Console.WriteLine(interval + "\n");
                    isFinalizable = false;
                }
            }
        }

        /// <summary>
        /// Method that is called during garbage collection
        /// </summary>
        ~Finalizable()
        {
            isFinalizable = true;
            startHour = DateTime.Now.Hour;
            startMinute = DateTime.Now.Minute;
            startSecond = DateTime.Now.Second;
            startMillisecond = DateTime.Now.Millisecond;
            start = new DateTime(2020, 9, 14, startHour, startMinute, startSecond, startMillisecond);
        }
    }
}
