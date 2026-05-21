using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice._05.클래스_고급
{
    class Chapter5Basic
    {
        static public void SortInterface()
        {
            List<Product> list = new List<Product>()
            {
                new Product() {Name = "고구마", Price = 1500},
                new Product() {Name = "사과", Price = 2400}
            };

            list.Sort();

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }

        static public void DisposeInterface()
        {
            using (Dummy dummy = new Dummy())
            {

            }
        }

        static public void TestInterface()
        {
            int num;
            IBasic testInterface = new TestInterface();
            num = testInterface.TestInstanceMethod();
            if (num == 0)
            {
                Console.WriteLine("No Problem at all!");
            }
        }

        static void Main()
        {
            //SortInterface();
            //DisposeInterface();
            //TestInterface();
        }
    }
}
