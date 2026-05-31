using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice._05.클래스_고급.델리게이트연산
{
    class TestDelegateCalc
    {
        static void GoForward() => Console.WriteLine("직진");
        static void GoFast() => Console.WriteLine("가속");

        delegate void CarDrive();

        static void Main()
        {
            CarDrive goHome = GoForward;
            goHome += GoFast;
            goHome += delegate ()
            {
                Console.WriteLine("기본과 원칙을 지킨다~");
            };
            goHome += () => { Console.WriteLine("HAHA"); };

            goHome.Invoke();
            //goHome();
        }
    }
}
