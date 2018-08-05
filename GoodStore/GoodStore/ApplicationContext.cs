using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GoodStore
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Supply> Supplies { get; set; }
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            IConfigurationRoot configuration = builder.Build();
            string conStr = configuration["ConnectionStrings:DefaultConnection"];

            optionsBuilder.UseSqlServer(conStr);
            base.OnConfiguring(optionsBuilder);
        }
        public void ShowRemainingProducts()
        {
            var remaining = Supplies.GroupBy(s => s.Product, p => p.Amount).Select(s => new { Id = s.Key.ProductId, s.Key.Name, Amount = s.Sum() }).ToList();
            
            foreach(var product in remaining)
            {
                Console.WriteLine($"Id - {product.Id}, Product - {product.Name}, Amount - {product.Amount}");
            }
        }
    }

}
