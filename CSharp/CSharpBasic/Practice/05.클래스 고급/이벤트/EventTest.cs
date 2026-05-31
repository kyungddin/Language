using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice._05.클래스_고급.이벤트
{
    class EventTest
    {
        static void Main()
        {
            Counter c = new Counter();
            c.ThresholdReached += c_ThresholdReached;

            Console.WriteLine("press 'a' key to increase total");
            while(Console.ReadKey(true).KeyChar == 'a')
            {
                Console.WriteLine("adding one");
                c.Add(1);
            }
        }

        static void c_ThresholdReached()
        {
            Console.WriteLine("The threshold was reached.");
        }
    }

    class Counter
    {
        private int threshold;
        private int total;

        public event Action ThresholdReached;

        public Counter()
        {
            threshold = 5;
        }

        public void Add(int x)
        {
            total += x;
            if (total >= threshold)
            {
                ThresholdReached?.Invoke();
            }
        }
    }
}
