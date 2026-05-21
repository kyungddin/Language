using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice._01.기본요소
{
    class Chapter1Basic // internal class: 같은 플젝 내 접근 가능
    {
        // Enum element
        public enum OrderStatus
        {
            Open,
            InDelivery,
            CancleByCustomer
        }

        // public 상수와 public 읽기 전용 정적 변수
        public class Order
        {
            public const int MaxOrders = 30;
            public static readonly int Tag;
        }

        // Escape String
        string path = @"C:/Merong/zz.txt";

        // 문자열 합치기
        public static void StringConcat()
        {
            int s1 = 1;
            int s2 = 2;
            int s3 = s1 + s2;
            var res1 = string.Format("{0} + {1} = {2}", s1, s2, s3);
            var res2 = $"{s1} + {s2} = {s3}";

            Console.WriteLine(res1);
            Console.WriteLine(res2);
        }

        // 자료형 췍
        public static void DataTypeCheck()
        {
            // 부동소수점은 기본 자료형이 double이라 보통 float 쓸 때는 F를 명시해준다
            Console.WriteLine(int.Parse("52"));
            Console.WriteLine(int.Parse("52").GetType());
            Console.WriteLine((52).ToString());
        }

        // 대문자
        public static void UpperCheck()
        {
            string input = "Potato Tomato";
            Console.WriteLine(input.ToUpper());
        }

        // cin
        public static void ReadCheck()
        {
            string tmp = Console.ReadLine();
            Console.WriteLine(tmp);
        }

        // split 연구소
        public static void SplitTest()
        {
            string input = "감자 고구마 도마도";
            string[] inputs = input.Split(new char[] {' '});
            
            foreach (var item in inputs)
            {
                Console.WriteLine(item);
            }
        }

        // 트림 연구소
        public static void TrimTest()
        {
            string input = "      test        \n";
            Console.WriteLine("::" + input.Trim() + "::");
        }

        // 조인 연구소
        public static void JoinTest()
        {
            string[] array = { "감자", "고구마", "토마도", "가지" };
            Console.WriteLine(String.Join(",", array));
        }


        static void Main()
        {
            StringConcat();
            DataTypeCheck();
            UpperCheck();
            ReadCheck();
            SplitTest();
        }
    }
}
