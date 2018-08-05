using System;
using System.Linq;

namespace GoodStore
{
    class Program
    {

        static void Main(string[] args)
        {
            using (var context = new ApplicationContext())
            {
                var products = context.Products.ToList();
                foreach (var product in products)
                {
                    Console.WriteLine($"Id - {product.ProductId}, Name - {product.Name}, Measure unit - {product.MeasureUnit}, Unit price - {product.UnitPrice}");
                }

                //context.AddProductFromUser();

                //products = context.Products.ToList();
                //foreach (var product in products)
                //{
                //    Console.WriteLine($"Id - {product.ProductId}, Name - {product.Name}, Measure unit - {product.MeasureUnit}, Unit price - {product.UnitPrice}");
                //}

                var supplies = context.Supplies.ToList();
                foreach (var supply in supplies)
                {
                    Console.WriteLine($"Product id  - {supply.ProductId}, Amount - {supply.Amount}, Time - {supply.Time}");
                }
                context.ShowRemainingProducts();
                //context.AddSuppliesFromUser();

                //foreach (var supply in supplies)
                //{
                //    Console.WriteLine($"Product id  - {supply.ProductId}, Amount - {supply.Amount}, Time - {supply.Time}");
                //}

                
            }

        }
    }
}
