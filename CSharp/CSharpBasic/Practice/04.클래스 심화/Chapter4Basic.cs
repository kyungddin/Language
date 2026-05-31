using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice._04.클래스_심화
{
    class Chapter4Basic
    {
        #region 4-1) 제네릭
        class Wanted<T>
        {
            public T Value;
            public Wanted(T value)
            {
                Value = value;
            }
        }
        #endregion

        #region 4-2) 인덱스 오버라이딩
        class SquareCalculator
        {
            public int this[int i]
            {
                get { return i * i; }
            } // 이런식으로 인덱서를 직접 구현 가능
        }

        static void useIndex()
        {
            SquareCalculator square = new SquareCalculator();
            Console.WriteLine(square[10]);
        }
        #endregion

        #region 4-3) out 키워드
        static void NextPosition(int x, int y, int vx, int vy, out int rx, out int ry)
        {
            rx = x + vx;
            ry = y + vy;
        }

        static void useOutKeyword()
        {
            int x = 0;
            int y = 0;
            int vx = 1;
            int vy = 1;

            Console.WriteLine($"현재좌표: ({x}, {y})");
            NextPosition(x, y, vx, vy, out x, out y);
            Console.WriteLine($"현재좌표: ({x}, {y})");
        }
        #endregion

        #region 4-4) 구조체 복사
        class PointClass
        {
            public int X;
            public int Y;

            public PointClass(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        struct PointStruct
        {
            public int X;
            public int Y;

            public PointStruct(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        static public void ClassCopy()
        {
            PointClass pointClassA = new PointClass(10, 20);
            PointClass pointClassB = pointClassA; // Copy Ref

            pointClassB.X = 100;
            pointClassB.Y = 200;

            Console.WriteLine($"pointClassA: {pointClassA.X}, {pointClassA.Y}");
            Console.WriteLine($"pointClassB: {pointClassB.X}, {pointClassB.Y}");
        }

        static public void StructCopy()
        {
            PointStruct pointStructA = new PointStruct(10, 20);
            PointStruct pointStructB = pointStructA; // just Copy Value

            pointStructB.X = 100;
            pointStructB.Y = 200;

            Console.WriteLine($"pointStructA: {pointStructA.X}, {pointStructA.Y}");
            Console.WriteLine($"pointStructB: {pointStructB.X}, {pointStructB.Y}");
        }
        #endregion

        static void Main()
        {
            //useIndex();
            //useOutKeyword();

            ClassCopy();
            StructCopy();
        }
    }
}
