using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace Practice._06.비동기_작업
{
    class ThreadPractice2
    {
        static void Main()
        {
            Thread t1 = new Thread(() => DoSomething());
            t1.Start();

            Thread t2 = new Thread(new ParameterizedThreadStart(Calc));
            t2.Start(3.5); // ParameterizedThreadStart() 이므로..

            Thread t3 = new Thread(() => Sum(10, 20, 30));
            t3.Start();

            Console.WriteLine($"[실습] Finished Main");
            Trace.WriteLine($"[실습] Finished Main");
        }

        static void DoSomething()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"[실습] DoSomething : {i}");
                Trace.WriteLine($"[실습] DoSomething : {i}");
                Thread.Sleep(1000);

            }
            Thread.Sleep(3000);
            Console.WriteLine($"[실습] Finished DoSomething");
            Trace.WriteLine($"[실습] Finished DoSomething");
        }

        static void Calc(object radius)
        {
            double r = (double)radius;
            double area = r * r * 3.14;
            Console.WriteLine("r = {0}, area={1}", r, area);
        }

        static void Sum(int d1, int d2, int d3)
        {
            int sum = d1 + d2 + d3;
            Console.WriteLine(sum);
        }
    }
}
