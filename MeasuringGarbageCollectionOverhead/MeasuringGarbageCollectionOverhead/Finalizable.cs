using System;
using BenchmarkDotNet.Attributes;

namespace MeasuringGarbageCollectionOverhead
{
    class Finalizable
    {
        int number;
        byte[] memory = new byte[100000000];

        static object lockObject = new object();

        public Finalizable(int number)
        {
            this.number = number;
            lock (lockObject)
            {
                Console.WriteLine("Object {0,2} is created", number);
            }
        }

        ~Finalizable()
        {
            lock (lockObject)
            {
                Console.WriteLine("Object {0,2} is disposed", number);
            }
        }
    }
}
