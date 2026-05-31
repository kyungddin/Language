using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice._06.비동기_작업
{
    class TaskPractice2
    {
        static void Main()
        {
            Task t1 = Task.Run(() => PrintSomething());
            t1.Wait();

            Task t2 = Task.Run(() => PrintCircleArea(3.0));
            t2.Wait();

            Task<int> t3 = Task.Run(() => GetSomething());
            var t3Result = t3.Result;

            Task<double> t4 = Task.Run(() => GetCircleArea(5.0));
            var t4Result = t4.Result;

            Console.ReadKey();
        }

        static void PrintSomething()
        {
            Console.WriteLine("This Prints Something!");
        }

        static void PrintCircleArea(object radius)
        {
            double r = (double)radius;
            double area = r * r * 3.14;
            Console.WriteLine(area);
        }

        static int GetSomething()
        {
            return 1;
        }

        static double GetCircleArea(object radius)
        {
            double r = (double)radius;
            double area = r * r * 3.14;
            return area;
        }
    }
}
