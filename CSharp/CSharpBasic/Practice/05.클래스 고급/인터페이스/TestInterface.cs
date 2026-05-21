using System;

namespace Practice._05.클래스_고급
{
    internal class TestInterface : IBasic
    {
        public int TestProperty { get; set; }
        

        public int TestInstanceMethod()
        {
            Console.WriteLine("Test Instance Method!");

            return 0;
        }
    }
}
