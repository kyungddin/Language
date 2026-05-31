using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Practice._06.비동기_작업
{
    class AsyncBlock
    {
        static void Main()  // A
        {
            Task<int> task = Task.Run(() =>  // B 함수
            {
                Thread.Sleep(3000);
                return 42;
            });

            // 콜백 등록 - B 끝나면 실행
            task.ContinueWith(t =>
            {
                Console.WriteLine($"콜백 실행: {t.Result}");  // 콜백 함수
            });

            task.Wait();  // Blocking - 제어권 넘기고 대기
            Console.WriteLine("test");
            Console.ReadLine();
        }
    }
}
