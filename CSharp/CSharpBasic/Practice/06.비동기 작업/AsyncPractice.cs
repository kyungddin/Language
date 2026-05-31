using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Practice._06.비동기_작업
{
    class AsyncPractice
    {
        static int ThreadID => Thread.CurrentThread.ManagedThreadId;

        public static void Main()
        {
            Console.WriteLine($"[실습] Main start (ThreadID:{ThreadID})");

            TaskA();
            Task t1 = TaskB();
            Task<int> t2 = TaskC();

            for (int i=0; i<5; i++)
            {
                Console.WriteLine($"[실습] Main 실행 중 {i + 1} (ThreadID:{ThreadID})");
            }

            t1.Wait();
            int result = t2.Result;

            Console.WriteLine($"[실습] Main finished (ThreadID:{ThreadID})");

            Console.ReadLine();
        }

        static async void TaskA()
        {
            await Task.Run(() =>
            {
                Thread.Sleep(2000);
                Console.WriteLine($"[실습] TaskA, returns 10 (ThreadID:{ThreadID}");
            });

            Console.WriteLine($"[실습] TaskA finished (ThreadID:{ThreadID}");
        }

        static async Task TaskB()
        {
            await Task.Run(()=>
            {
                Console.WriteLine($"[실습] TaskB await Task.Run(()=>ThreadID:{ThreadID})");
                Thread.Sleep(2000);
            });

            Console.WriteLine($"[실습] TaskB Finished (ThreadID:{ThreadID})");
        }

        static async Task<int> TaskC()
        {
            int n = await Task.Run(()=>
            {
                Console.WriteLine($"[실습] TaskB await Task.Run(()=>ThreadID:{ThreadID})");
                Thread.Sleep(2000);

                return 10;
            });

            return n;
        }
    }
}
