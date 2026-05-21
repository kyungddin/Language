using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice._05.클래스_고급.OtherWise
{
    class DelegateTest
    {
        static void GoForward() => Console.WriteLine("직진");
        static void GoFast()
        {
            Console.WriteLine("가속");
        }

        static void Main()
        {
            Action goHome = GoForward;
            goHome += GoFast;
            goHome += delegate () { Console.WriteLine("우회전"); };
            goHome += () => Console.WriteLine("좌회전");
            for(int i=0; i<100; i++) goHome();

            Action go = () => Console.WriteLine("운전");
            go();
        }
    }
}
