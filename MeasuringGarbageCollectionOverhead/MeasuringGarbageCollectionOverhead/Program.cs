using System;

/// <summary>
/// Global namespace.
/// </summary>
namespace MeasuringGarbageCollectionOverhead
{
    /// <summary>
    /// Program launch
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Launches programs
        /// </summary>
        static void Main()
        {
            Benchmark benchmark = new Benchmark();
            benchmark.Result();
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
