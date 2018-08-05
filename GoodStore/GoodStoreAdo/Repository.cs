using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace GoodStoreAdo
{
    public class Repository : IDisposable
    {

        SqlConnection _con;
        public List<Product> Products { get; set; }
        public List<Supply> Supplies { get; set; }

        public Repository()
        {
            var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json");
            IConfigurationRoot configuration = builder.Build();
            string conStr = configuration["ConnectionStrings:DefaultConnection"];

            _con = new SqlConnection(conStr);

            UpdateProducts();
            UpdateSupplies();
        }

        public void Dispose()
        {
            _con.Dispose();
        }

        protected void UpdateProducts()
        {
            string commandString = "SELECT * FROM [Products]";

            SqlCommand command = new SqlCommand(commandString, _con);

            var result = new List<Product>();

            _con.Open();

            using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
            {
                while (reader.Read())
                {
                    result.Add(
                    new Product
                    {
                        ProductId = (int)reader["ProductId"],
                        Name = (string)reader["Name"],
                        MeasureUnit = (string)reader["MeasureUnit"],
                        UnitPrice = (double)reader["UnitPrice"]
                    });
                }

            }

            _con.Close();

            Products =  result;
        }
        protected void UpdateSupplies()
        {
            string commandString = "SELECT * FROM [Supplies]";

            SqlCommand command = new SqlCommand(commandString, _con);

            var result = new List<Supply>();

            _con.Open();

            using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
            {
                while (reader.Read())
                {
                    result.Add(
                    new Supply
                    {
                        SupplyId = (int)reader["SupplyId"],
                        ProductId = (int)reader["ProductId"],
                        Time = (DateTime)reader["Time"],
                        Amount = (int)reader["Amount"],
                        Product = Products.Where(p => p.ProductId == (int)reader["ProductId"]).First()
                    });

                }

            }

            _con.Close();

            Supplies = result;
        }
        
        public void AddProduct(Product product)
        {
            if (Products.Where(p => p.ProductId == product.ProductId).Count() != 0)
                return;

            SqlCommand cmd = _con.CreateCommand();
            cmd.CommandText = "INSERT INTO [Products] (Name, MeasureUnit, UnitPrice) VALUES (@Name, @MeasureUnit, @UnitPrice)";  
            cmd.Parameters.AddWithValue("@Name", product.Name);
            cmd.Parameters.AddWithValue("@MeasureUnit", product.MeasureUnit);
            cmd.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);

            _con.Open();

            cmd.ExecuteNonQuery();

            _con.Close();

            UpdateProducts();
        }
        public void UpdateProduct(Product product)
        {
            if (Products.Where(p => p.ProductId == product.ProductId).Count() == 0)
                return;

            SqlCommand cmd = _con.CreateCommand();
            cmd.CommandText = "UPDATE [Products] SET Name = @Name, MeasureUnit = @MeasureUnit, UnitPrice = @UnitPrice WHERE ProductId = @ProductId";
            cmd.Parameters.AddWithValue("@ProductId", product.ProductId);
            cmd.Parameters.AddWithValue("@Name", product.Name);
            cmd.Parameters.AddWithValue("@MeasureUnit", product.MeasureUnit);
            cmd.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);

            _con.Open();

            cmd.ExecuteNonQuery();

            _con.Close();

            UpdateProducts();
        }
        public void DeleteProduct(Product product)
        {
            if (Products.Where(p => p.ProductId == product.ProductId).Count() == 0)
                return;

            SqlCommand cmd = _con.CreateCommand();
            cmd.CommandText = "DELETE FROM [Products] WHERE ProductId = @ProductId";
            cmd.Parameters.AddWithValue("@ProductId", product.ProductId);

            _con.Open();

            cmd.ExecuteNonQuery();

            _con.Close();

            UpdateProducts();
        }

        public void AddSupply(Supply supply)
        {
            if (Products.Where(p => p.ProductId == supply.ProductId).Count() == 0 || Supplies.Where(s => s.SupplyId == supply.ProductId).Count() != 0)
                return;

            SqlCommand cmd = _con.CreateCommand();
            cmd.CommandText = "INSERT INTO [Supplies] (Amount, ProductId, Time) VALUES (@Amount, @ProductId, @Time)";
            cmd.Parameters.AddWithValue("@Amount", supply.Amount);
            cmd.Parameters.AddWithValue("@ProductId", supply.ProductId);
            cmd.Parameters.AddWithValue("@Time", supply.Time);

            _con.Open();

            cmd.ExecuteNonQuery();

            _con.Close();

            UpdateSupplies();
        }
        public void UpdateSupply(Supply supply)
        {
            if (Products.Where(p => p.ProductId == supply.ProductId).Count() == 0 || Supplies.Where(s => s.SupplyId == supply.ProductId).Count() == 0) 
                return;

            SqlCommand cmd = _con.CreateCommand();
            cmd.CommandText = "UPDATE [Supplies] SET Amount = @Amount, ProductId = @ProductId, Time = @Time WHERE SupplyId = @SupplyId";
            cmd.Parameters.AddWithValue("@SupplyId", supply.SupplyId);
            cmd.Parameters.AddWithValue("@Amount", supply.Amount);
            cmd.Parameters.AddWithValue("@ProductId", supply.ProductId);
            cmd.Parameters.AddWithValue("@Time", supply.Time);

            _con.Open();

            cmd.ExecuteNonQuery();

            _con.Close();

            UpdateSupplies();
        }
        public void DeleteSupply(Supply supply)
        {
            if (Supplies.Where(s => s.SupplyId == supply.ProductId).Count() == 0)
                return;

            SqlCommand cmd = _con.CreateCommand();
            cmd.CommandText = "DELERE FROM [Supplies] WHERE SupplyId = @SupplyId";
            cmd.Parameters.AddWithValue("@SupplyId", supply.SupplyId);

            _con.Open();

            cmd.ExecuteNonQuery();

            _con.Close();

            UpdateSupplies();

        }

        public void ShowRemainingProducts()
        {
            var remaining = Supplies.GroupBy(s => s.Product, p => p.Amount).Select(s => new { Id = s.Key.ProductId, s.Key.Name, Amount = s.Sum() }).ToList();

            foreach (var product in remaining)
            {
                Console.WriteLine($"Id - {product.Id}, Product - {product.Name}, Amount - {product.Amount}");
            }
        }

    }
}