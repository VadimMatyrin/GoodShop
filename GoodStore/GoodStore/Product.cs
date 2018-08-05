using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GoodStore
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string MeasureUnit { get; set; }
        public double UnitPrice { get; set; }

        List<Supply> Supplies { get; set; }

        public static Product GetFromUser()
        {
            var product = new Product();

            while (product.Name is null || product.Name.Length == 0)
            {
                Console.Write("Enter products name - ");
                product.Name = Console.ReadLine();
            }

            while (product.MeasureUnit is null || product.MeasureUnit.Length == 0)
            {
                Console.Write("Enter measure units - ");
                product.MeasureUnit = Console.ReadLine();
            }

            Console.Write("Enter units price - ");
            double price;
            while (!Double.TryParse(Console.ReadLine(), out price) || price == 0)
            {
                Console.Write("Incorrect price value or format ");
            }

            product.UnitPrice = price;

            return product;
        }
    }
}
