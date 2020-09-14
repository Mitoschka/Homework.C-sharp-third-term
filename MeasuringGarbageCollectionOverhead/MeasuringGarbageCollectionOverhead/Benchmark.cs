/// <summary>
/// Global namespace.
/// </summary>
namespace MeasuringGarbageCollectionOverhead
{
    /// <summary>
    /// Comparative analysis
    /// </summary>
    public class Benchmark
    {
        /// <summary>
        /// The method that calls the finalizer
        /// </summary>
        public void Result()
        {
            for (int i = 0; i < 50; i++)
            {
                new Finalizable(i);
            }
        }
    }
}
