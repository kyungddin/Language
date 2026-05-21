using System;

namespace Practice._05.클래스_고급
{
    class Product:IComparable
    {
        public string Name { get; set; }
        public int Price { get; set; }

        public int CompareTo(object obj)
        {
            return Price.CompareTo((obj as Product).Price);
        }

        public override string ToString()
        {
            return $"{Name} : {Price} 원";
        }
    }
}
