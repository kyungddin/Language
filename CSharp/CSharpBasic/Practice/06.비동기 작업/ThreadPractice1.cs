using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;


namespace Practice._06.비동기_작업
{
    class ThreadPractice1
    {
        static void Main()
        {
            Thread t1 = new Thread(DoSomething);
            t1.IsBackground = true;

            t1.Start();
            //t1.Join();
        }

        static void DoSomething()
        {
            for (int i=0; i<5; i++)
            {
                Console.WriteLine($"[실습] DoSomething : {i}");
                Trace.WriteLine($"[실습] DoSomething : {i}");
                Thread.Sleep(1000);

            }
            Thread.Sleep(3000);
            Console.WriteLine($"[실습] Finished DoSomething");
            Trace.WriteLine($"[실습] Finished DoSomething");
        }
    }
}
