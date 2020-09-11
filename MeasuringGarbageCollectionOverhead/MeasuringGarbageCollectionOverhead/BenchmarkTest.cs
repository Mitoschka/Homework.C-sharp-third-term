using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace MeasuringGarbageCollectionOverhead
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class BenchmarkTest
    {
        [Benchmark]
        public void Result()
        {
            for (int i = 0; i < 10; i++)
            {
                new Finalizable(i);
            }
        }
    }
}
