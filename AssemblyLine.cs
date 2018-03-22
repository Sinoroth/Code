using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1ProducerConsumer
{
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
        }

        public bool isThereRoomLeft() {
            return currentNumberOfObjects < totalNumberOfObjects;
        }

        public bool isEmpty()
        {
            return currentNumberOfObjects == 0;
        }


    }
}
