using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice._05.클래스_고급
{
    class HighClass
    {
        delegate void TestDelegate();
        static void Main()
        {
            TestDelegate testDelegate = method;
        }

        public static void method()
        {

        }
    }
}
