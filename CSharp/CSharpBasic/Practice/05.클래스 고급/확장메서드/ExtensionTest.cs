using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice._05.클래스_고급.확장메서드
{
    public static class ExtensionMethod
    {
        public static bool IsEven(this int a)
        {
            return a % 2 == 0;
        }

        public static string DashAppend(this string s, string text)
        {
            return s + "-" + text;
        }

        public static int Modulo(this Calculator calc, int a, int b)
        {
            return a % b;
        }
    }

    public sealed class Calculator
    {
        public int Add(int a, int b) { return a + b; }
        public int Subtract(int a, int b) { return a - b; }
        public int Multiply(int a, int b) { return a * b; }
        public int Divide(int a, int b) { return a / b; }
    }
    class ExtensionTest
    {
        static public void ExtensionTest1()
        {

            int a = 124;
            bool b = a.IsEven();
            b = 100.IsEven();
        }

        static public void ExtensionTest2()
        {
            string s = "ABC";
            string s2 = s.DashAppend("DEF");
            Console.WriteLine(s2);
        }

        static public void ExtensionTest3()
        {
            Calculator c = new Calculator();
            int result = c.Modulo(5, 3);

            Console.WriteLine(result);
        }


        static void Main()
        {
            //ExtensionTest1();
            //ExtensionTest2();
            ExtensionTest3();
        }
    }
}
