using Managers;
using Models;
using Models.Enums;
using System;
using System.Collections.Generic;

namespace SalesTaxesProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            ShopManager shopManager = new ShopManager();
            string ticket;
            List<Item> basket = new List<Item> {
                new Item { Name = "Book", Price = 12.49M, IsImported = false, Category = ItemCategory.BOOK },

                new Item { Name = "Book", Price = 12.49M, IsImported = false, Category = ItemCategory.BOOK },

                new Item { Name = "Music CD", Price = 14.99M, IsImported = false, Category = ItemCategory.OTHER },

                new Item { Name = "Chocolate Bar", Price = 0.85M, IsImported = false, Category = ItemCategory.FOOD }
            };
            ticket = shopManager.SubmitSale(basket);
            Console.WriteLine("Output 1");
            Console.WriteLine(ticket);

            basket = new List<Item> {
                new Item { Name = "Imported Box of Chocolates", Price = 10.00m, IsImported = true, Category = ItemCategory.FOOD },

                new Item { Name = "Imported bottle of perfume", Price = 47.50m, IsImported = true, Category = ItemCategory.OTHER }
            };
            ticket = shopManager.SubmitSale(basket);
            Console.WriteLine($"{Environment.NewLine}Output 2");
            Console.WriteLine(ticket);

            basket = new List<Item> {
                new Item { Name = "Imported bottle of perfume", Price = 27.99m, IsImported = true, Category = ItemCategory.OTHER},
                new Item { Name = "Bottle of perfume", Price = 18.99m, IsImported = false, Category = ItemCategory.OTHER },
                new Item { Name = "Packet of headache pill", Price = 9.75m, IsImported = false, Category = ItemCategory.MEDICAL_PRODUCT },
                new Item { Name = "Imported box of chocolates", Price = 11.25m, IsImported = true, Category = ItemCategory.FOOD },
                new Item { Name = "Imported box of chocolates", Price = 11.25m, IsImported = true, Category = ItemCategory.FOOD }
            };
            ticket =  shopManager.SubmitSale(basket);
            Console.WriteLine($"{Environment.NewLine}Output 3");
            Console.WriteLine(ticket);
        }
    }
}
