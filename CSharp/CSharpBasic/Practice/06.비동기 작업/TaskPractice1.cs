using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice._06.비동기_작업
{
    class TaskPractice1
    {
        static void Main()
        {
            Task t1 = new Task(PrintSomething);
            t1.Start();
            t1.Wait(); // Task Blocking

            Task t2 = new Task(PrintCircleArea, 3.5);
            t2.Start();
            t2.Wait();

            Task<int> t3 = new Task<int>(GetSomething);
            t3.Start();
            t3.Wait();
            var t3Result = t3.Result;
            Console.WriteLine(t3Result);

            Task<double> t4 = new Task<double>(GetCircleArea, 3.5);
            t4.Start();

            var t4Result = t4.Result;
            Console.WriteLine(t4Result);
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
