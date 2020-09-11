using BenchmarkDotNet.Running;

namespace MeasuringGarbageCollectionOverhead
{
    public class Program
    {
        static void Main()
        {
            BenchmarkRunner.Run<BenchmarkTest>();
        }
    }
}
