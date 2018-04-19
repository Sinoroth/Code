using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace Homework3Producator
{
    class Assembly
    {
        private static AssemblyLine al;

        public static object removeFromList = new object();
        public static object addToList = new object();


        public Assembly()
        {
            al = new AssemblyLine(10);
        }

        public static void consumer(int consumerNumber)
        {
            while (true)
            {
                lock (addToList)
                {

                    if (al.isEmpty())
                    {
                        Monitor.Wait(addToList);
                    }

                   Thread.Sleep(1000);

                    al.consume(consumerNumber);

                    Monitor.Pulse(addToList);
                }
            }
        }

        public static void producer(int producerNumber)
        {
            while (true)
            {
                lock (addToList)
                {
                    if (al.isThereRoomLeft() == false)
                    {
                        Monitor.Wait(addToList);
                    }

                    Thread.Sleep(1000);

                    al.addOne(producerNumber);

                    Monitor.Pulse(addToList);
                }
            }
        }

        public void run()
        {
            Thread[] threads = new Thread[10];

            for (int i = 0; i < 5; ++i)
            {
                threads[i] = new Thread(() => producer(i + 1));

            }

            for (int i = 0; i < 5; ++i)
            {
                threads[i + 5] = new Thread(() => consumer(i + 1));
            }

            for (int i = 0; i < 10; ++i)
            {
                threads[i].Start();
            }

            for (int i = 0; i < 10; ++i)
            {
                threads[i].Join();
            }

        }

        class AssemblyLine
        {
            int currentNumberOfObjects, totalNumberOfObjects;

            public AssemblyLine(int totalObjects)
            {
                totalNumberOfObjects = totalObjects;
                currentNumberOfObjects = 0;
            }

            public void addOne(int producerN)
            {
                currentNumberOfObjects++;
                Console.WriteLine("Producer " + producerN + " produced item " + currentNumberOfObjects);
            }

            public void consume(int consumerN)
            {
                Console.WriteLine("Consumer " + consumerN + " consumed item " + currentNumberOfObjects);
                currentNumberOfObjects--;
            }

            public bool isThereRoomLeft()
            {
                return currentNumberOfObjects < totalNumberOfObjects;
            }

            public bool isEmpty()
            {
                if (currentNumberOfObjects == 0) return true;
                return false;
            }


        }
    }
}
