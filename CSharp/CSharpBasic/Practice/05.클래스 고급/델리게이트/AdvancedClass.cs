using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice._05.클래스_고급
{
    class AdvancedClass
    {
        public delegate void TestDelegate();

        static void Main()
        {
            TestDelegate delegateA = TestMethod;
            TestDelegate delegateB = delegate () { };
            TestDelegate delegateC = () => { };
        }

        static void TestMethod()
        {

        }
    }
}
