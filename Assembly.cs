using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Homework1ProducerConsumer
{
    class Assembly
    {
        private static AssemblyLine al;
        

        private static Mutex mutex = new Mutex(true,"Test");

        private  static object lockForApplication = new object();

        private static Object lockObject = new object();

        private static bool flag = false;

        private static bool isMutexInUse()
        {
            if(flag == true)
            {
                return true;
            }
            return false;
        }

        private static void consume()
        {
            while(true)
                {
                lock (lockObject)
                {

                    if (al.isEmpty() == true)
                    {
                        continue;
                    }

                    al.consume();

                }
            }
        }

        private static void produce()
        {
            while (true)
            {
                lock (lockObject)
                {

                    if (al.isThereRoomLeft() == false)
                    {
                        continue;
                    }


                    al.addOne();
                }

            }
        }

        public Assembly()
        {
            al = new AssemblyLine(5);
        }

        public void run() {
            Thread producer = new Thread(new ThreadStart(produce));
            Thread consumer = new Thread(new ThreadStart(consume));

            producer.Start();
            consumer.Start();



            producer.Join();
            consumer.Join();

        }

        class AssemblyLine
        {
            int currentNumberOfObjects, totalNumberOfObjects;

            public AssemblyLine(int totalObjects)
            {
                totalNumberOfObjects = totalObjects;
                currentNumberOfObjects = 0;
            }

            public void addOne()
            {
                currentNumberOfObjects++;
                Console.WriteLine("Producer produced item " + currentNumberOfObjects);
            }

            public void consume()
            {
                Console.WriteLine("Consumer consumed item " + currentNumberOfObjects);
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
