using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice._05.클래스_고급
{
    class TestComparison
    {
        static void Main()
        {
            List<Product> Products = new List<Product>
            {
                new Product() {Name = "감자",  Price = 500},
                new Product() {Name = "TW350HT",  Price = 5000}
            };

            // Sort
            Products.Sort(SortWithPrice);

            // Sort with NoName

            Products.Sort(delegate (Product a, Product b)
            {
                return a.Price.CompareTo(b.Price);
            });


            // Sort with Lambda
            Products.Sort((a, b) => a.Price.CompareTo(b.Price));

            foreach (var item in Products)
            {
                Console.WriteLine($"{item.Name}:{item.Price}");
            }
        }

        static int SortWithPrice(Product a, Product b)
        {
            return a.Price.CompareTo(b.Price);
        }
    }
}
