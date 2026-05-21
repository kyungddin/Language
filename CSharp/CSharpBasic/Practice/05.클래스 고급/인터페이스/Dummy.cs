using System;

namespace Practice._05.클래스_고급
{
    class Dummy : IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine("Dispose 메서드를 호출합니다");
        }
    }
}
