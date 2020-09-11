using System;

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
            Console.WriteLine("Object {0,2} is created", number);
        }

        ~Finalizable()
        {
            Console.WriteLine("Object {0,2} is disposed", number);
        }
    }
}
