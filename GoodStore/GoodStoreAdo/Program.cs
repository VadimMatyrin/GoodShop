using System;

namespace GoodStoreAdo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var repository = new Repository())
            {
                var products = repository.Products;

                foreach(var product in products)
                {
                    Console.WriteLine($"Product id - {product.ProductId}, name - {product.Name}, measure units - {product.MeasureUnit}, price - {product.UnitPrice}");
                }
                Console.WriteLine();
                var supplies = repository.Supplies;

                foreach (var supply in supplies)
                {
                    Console.WriteLine($"Supply id - {supply.SupplyId}, product id - {supply.ProductId}, amount - {supply.Amount}, time - {supply.Time}");
                }
                repository.ShowRemainingProducts();
                //repository.AddProduct(Product.GetFromUser());
                //products = repository.Products;

                //foreach (var product in products)
                //{
                //    Console.WriteLine($"Product id - {product.ProductId}, name - {product.Name}, measure units - {product.MeasureUnit}, price - {product.UnitPrice}");
                //}

                //Console.WriteLine();
                //supplies = repository.Supplies;

                //foreach (var supply in supplies)
                //{
                //    Console.WriteLine($"Supply id - {supply.SupplyId}, product id - {supply.ProductId}, amount - {supply.Amount}, time - {supply.Time}");
                //}
            }
        }
    }
}
