using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace Practice._03.상속과_다형성
{
    #region Class
    #region 동물 상속과 오버라이드
    class Animal
    {
        public int Age { get; set; }
        public virtual void eat() 
        { 
            Console.WriteLine("냠냠"); 
        }
    }

    class Dog:Animal
    {
        public string color { get; set; }
        public void bark() 
        { 
            Console.WriteLine("멍멍"); 
        }

        public override void eat()
        {
            Console.WriteLine("강아지가 사료를 먹네요");
        }
    }

    class Cat : Animal
    {
        public void meow() 
        { 
            Console.WriteLine("야옹"); 
        }
        public override void eat()
        {
            Console.WriteLine("고양이가 사료를 먹네요");
        }

    }
    #endregion

    #region 부모 생성자 명시적 호출하기
    class NewAnimal
    {
        public NewAnimal(int age) { Age = age; }

        public int Age { get; set; }
        public virtual void eat() { Console.WriteLine("냠냠"); }
    }

    class NewDog:NewAnimal
    {
        public NewDog(): base(5) {}

        public string Color { get; set; }
        public void bark() { Console.WriteLine("왈왈~");  }
    }

    class NewCat:NewAnimal
    {
        public NewCat() : base(3) { }

        public void mewow() { Console.WriteLine("야옹");  }
    }
    #endregion
    
    // 변수 섀도잉
    class InheritanceAndPolymorhphism
    {
        public static int number = 10;
    }

    #region 자식의 메서드 하이딩
    class Parent
    {
        public int variable = 273;

        public void Method()
        {
            Console.WriteLine("부모의 메서드");
        }
    }

    class Child:Parent
    {
        public string variable = "hiding";

        public new void Method()    // 명시적 메서드 하이딩~
        {
            Console.WriteLine("자식의 메서드");
        }
    }
    #endregion

    #region Sealed!
    sealed class NewParent
    {
        public void Method()
        {
            Console.WriteLine("부모의 메서드");
        }
    }
    /*
    class NewChild : NewParent
    {
        public void Method()
        {
            Console.WriteLine(안돼용~);
        }
    } 클래스 sealed
    */

    class NewNewParent
    {
        public virtual void Method() { }
    }

    class NewNewChild : NewNewParent
    {
        sealed public override void Method() { }
    } // 여기까지만 오버라이드 가능!

    /*  
    class NewNewGrandChild : NewNewChild
    {
        public override void Method() { }
    }
    오버라이딩 sealed
    */
    #endregion

    #region Abstract
    partial class Program
    {
        abstract class Parent
        {
            public void Test() { }
            public abstract void NewTest();
        }

        class Child : Parent
        {
            public void Test() { }
            public override void NewTest() { } // 반드시 구현까지!!

        }

        static public void AbstractTest()
        {
            Parent parent = new Child(); // Parent() 객체는 생성 불가

        }
    }
    #endregion
    #endregion Class

    class Chapter3Basic
    {
        #region 3-1) 상속과 **is** 키워드
        static public void AnimalTest()
        {
            List<Animal> animals = new List<Animal>()
            {
                new Dog(), new Cat(), new Cat(), new Dog(),
                new Dog(), new Cat(), new Dog(), new Dog()
            };

            foreach (var item in animals)
            {
                item.eat();

                if (item is Dog)    // is
                {
                    ((Dog)item).bark();
                }

                if (item is Cat)
                {
                    ((Cat)item).meow();
                }
            }
        }

        static public void AnimalTest2()
        {
            List<Animal> animals = new List<Animal>()
            {
                new Dog(), new Cat(), new Cat(), new Dog(),
                new Dog(), new Cat(), new Dog(), new Dog()
            };

            foreach (var item in animals)
            {
                item.eat();

                var dog = item as Dog;  // as: 반환실패시 null
                dog?.bark();            // ?; if(dog != null)

                var cat = item as Cat;
                cat?.meow();
            }
        }
        #endregion

        #region 3-2) 섀도잉 테스트
        static public void ShadowingTest()
        {
            int number = 20;
            Console.WriteLine(number);
        }
        #endregion

        #region 3-3) 하이딩 테스트
        static public void HidingTest()
        {
            Child child = new Child();
            Console.WriteLine(child.variable);
            child.Method();
        }
        #endregion

        #region 3-4) 사료를 먹이자
        static public void FeedFood()
        {
            List<Animal> animals = new List<Animal>()
            {
                new Dog(), new Cat(), new Cat(), new Dog(),
                new Dog(), new Cat(), new Dog(), new Dog()
            };

            foreach (var item in animals)
            {
                item.eat();
            }
        }
        #endregion

        static void Main()
        {
            //AnimalTest();
            //AnimalTest2();
            //ShadowingTest();
            //HidingTest();
            //FeedFood();
        }
    }
}
