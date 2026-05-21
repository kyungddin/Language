using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice._05.클래스_고급.OtherWise
{
    class DelegateSample
    {

        static void FuncTest()
        {
            Func<string, string> convert = str => str.ToUpper();

            string name = "Dakota";
            Console.WriteLine(convert(name));
        }
        static void FindPointsTest()
        {
            Point[] points =
            {
                new Point(100, 200),
                new Point(50000, 120321),
                new Point(20000, 500000)
            };

            Predicate<Point> predicate = FindPoints;
            Point first = Array.Find(points, predicate);
            Console.WriteLine($"X={first.X}, Y={first.Y}");
        }

        private static bool FindPoints(Point obj)
        {
            return obj.X * obj.Y > 100000;
        }
        static void Main()
        {
            RunLambda(() => Console.WriteLine("매개변수로 람다 식"));
        }

        static void RunLambda(Action action) => action();
    }

    class Point
    {
        public Point(int x, int y)
        {
            X = x; Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }
}
