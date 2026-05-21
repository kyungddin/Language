using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice._02.클래스
{
    #region Class
    class Student
    {
        public string name;
        public int score;

        public Student()
        {
            Console.WriteLine("생성자 호출!");
        }
    }

    class ClassMember
    {
        public static double Pi = 3.141592;
        public static double getCircleArea(int radius)
        {
            return (Pi * radius * radius);
        }
    }
    partial class Example
    {
        public int a;
    }

    partial class Example
    {
        public int b;
    }

    partial class GetterSetter
    {
        private int width;
        public int Width
        {
            get { return width; }
            set
            {
                if (value > 0) { width = value; }
                else { throw new Exception("너비는 자연수를 입력해주세요"); }
            }
        }
    }

    partial class GetterSetter
    {
        private int height;
        public int Height
        {
            get { return height; }
            set
            {
                if (value > 0) { height = value; }
                else { throw new Exception("높인 자연수를 입력하세용"); }
            }
        }
    }

    partial class GetterSetter
    {
        public GetterSetter(int w, int h)
        {
            width = w;
            height = h;
        }
    }

    class GetterSetter2
    {
        public int MyProperty { get; set; }
    }

    #endregion

    class Chapter2Basic
    {
        #region 2-1) Random 클래스 테스트
        static public void RandomTest()
        {
            Random random = new Random();

            Console.WriteLine(random.Next(10));
            Console.WriteLine(random.Next(10));

            Console.WriteLine(random.Next(10, 100));
            Console.WriteLine(random.Next(10, 100));

            Console.WriteLine(random.NextDouble());
            Console.WriteLine(random.NextDouble());

            Console.WriteLine(random.NextDouble() * 10);
            Console.WriteLine(random.NextDouble() * 10);
        }
        #endregion

        #region 2-2) 리스트에 요소 추가
        static public void AddList()
        {
            List<int> list = new List<int>();

            list.Add(52);
            list.Add(273);
            list.Add(32);
            list.Add(64);

            foreach(var item in list)
            {
                Console.WriteLine($"Count: {list.Count} \t item: {item}");
            }
        }
        #endregion

        #region 2-3) 리스트 인스턴스 생성과 동시에 요소 추가
        static public void CreateAddList()
        {
            List<int> list = new List<int>() { 52, 273, 32, 64 };

            foreach(var item in list)
            {
                Console.WriteLine($"Count: {list.Count} \t item:{item}");
            }
        }
        #endregion

        #region 2-4) 리스트 요소제거
        static void DeleteList()
        {
            List<int> list = new List<int>() { 52, 273, 32, 64 };

            list.Remove(52);

            foreach(var item in list)
            {
                Console.WriteLine($"Count: {list.Count} \t item: {item}");
            }
        }
        #endregion

        #region 2-5) 리스트와 모델 클래스 초기화
        static public void InitList()
        {
            List<Student> list = new List<Student>();
            list.Add(new Student() { name = "김경민", score = 101323210 });
            list.Add(new Student() { name = "조현우", score = -1 });
            list.Add(new Student() { name = "김설희", score = 0 });

            foreach(var item in list)
            {
                Console.WriteLine($"Name: {item.name} \t Score: {item.score}");
            }
        }
        #endregion

        #region 2-5-1) 리스트와 모델 클래스 초기화 한 번 더
        static public void InitListAgain()
        {
            List<Student> list = new List<Student>();
            new Student() { name = "김경민", score = 1000 };
        }
        #endregion

        #region 2-6) Math 클래스
        static public void MathTest()
        {
            Console.WriteLine(Math.Abs(-1234));
            Console.WriteLine(Math.Ceiling(52.273));
            Console.WriteLine(Math.Floor(52.273));
            Console.WriteLine(Math.Max(52, 273));
            Console.WriteLine(Math.Min(52, 273));
            Console.WriteLine(Math.Round(52.273));
        }
        #endregion

        #region 2-7) 클래스 멤버/메서드
        static public void ClassTest()
        {
            Console.WriteLine(ClassMember.Pi);
            Console.WriteLine(ClassMember.getCircleArea(5));
        }
        #endregion

        #region 2-8) 패트와 매트
        
        static public void GetterSetterTest()
        {
            GetterSetter rect = new GetterSetter(10, 20);

            int width = rect.Width;
            int height = rect.Height;
            Console.WriteLine(width);
            Console.WriteLine(height);

            rect.Width = 30;
            rect.Height = 40;
            width = rect.Width;
            height = rect.Height;
            Console.WriteLine(width);
            Console.WriteLine(height);
        }
        #endregion

        static void Main()
        {
            //RandomTest();
            //AddList();
            //CreateAddList();
            //DeleteList();
            //InitList();
            //InitListAgain();
            //MathTest();
            //ClassTest();
            //GetterSetterTest();
        }
    }
}
