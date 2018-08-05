using System;
using System.Collections.Generic;
using System.Text;

namespace GoodStore
{
    public class Supply
    {
        public int SupplyId { get; set; }
        public int Amount { get; set; }
        public DateTime Time { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public static List<Supply> GetFromUser()
        {
            var Supplies = new List<Supply>();
            while (true)
            {
                var supply = new Supply();

                Console.Write("Enter product id - ");

                int productId;

                while (!Int32.TryParse(Console.ReadLine(), out productId) || productId == 0)
                {
                    Console.WriteLine("Incorrect product id format");
                }

                supply.ProductId = productId;

                Console.Write("Enter product amount - ");

                int amount;

                while (!Int32.TryParse(Console.ReadLine(), out amount) || productId == 0)
                {
                    Console.WriteLine("Incorrect amount format or amount value ");
                }

                supply.Amount = amount;

                Console.WriteLine("Enter date in this format - yyyy-MM-dd HH:mm:SS, or enter CURR to automatically insert current time");

                DateTime supplyTime;

                var timeFromUser = Console.ReadLine();

                while (!DateTime.TryParse(timeFromUser, out supplyTime) && timeFromUser != "CURR")
                {
                    Console.WriteLine("Incorrect data format");
                    timeFromUser = Console.ReadLine();
                }

                supply.Time = timeFromUser == "CURR" ? DateTime.Now : supplyTime;

                Supplies.Add(supply);

                Console.WriteLine("One more - y/n");
                var key = Console.ReadKey().Key;
                while (key != ConsoleKey.Y && key != ConsoleKey.N)
                {
                    Console.WriteLine("One more try");
                    key = Console.ReadKey().Key;
                }

                if (key == ConsoleKey.Y)
                {
                    continue;
                }

                if (key == ConsoleKey.N)
                {
                    break;
                }

            }
            return Supplies;

        }
    }
}
