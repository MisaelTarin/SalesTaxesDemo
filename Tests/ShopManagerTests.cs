using Managers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Models.Enums;
using System;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class ShopManagerTests
    {
        private readonly ShopManager manager;
        public ShopManagerTests()
        {
            manager = new ShopManager();
        }

        [TestMethod]
        public void CheckBasket_ShouldReturnSumOfSameProduct()
        {
            int expected = 3;

            List<Item> basket = new List<Item> {
                new Item { Name = "Book", Price = 12.49M, IsImported = false, Category = ItemCategory.BOOK },
                new Item { Name = "Book", Price = 12.49M, IsImported = false, Category = ItemCategory.BOOK },
                new Item { Name = "Music CD", Price = 14.99M, IsImported = false, Category = ItemCategory.OTHER },
                new Item { Name = "Chocolate Bar", Price = 0.85M, IsImported = false, Category = ItemCategory.FOOD }
            };

            var result = manager.CheckBasket(basket);

            Assert.AreEqual(expected, result.Count);
        }

        [TestMethod]
        public void CalculateSaleTaxOnItems_ShouldReturnTaxedItem()
        {
            decimal expected = 0.6m;

            Item item = new Item { Name = "Book", Price = 5.60M, IsImported = false, Category = ItemCategory.OTHER };

            var result = manager.CalculateTaxOnItem (item);

            Assert.AreEqual(expected, result.TaxPrice);
        }

        [TestMethod]
        public void CalculateImportTaxOnItems_ShouldReturnTaxedItem()
        {
            decimal expected = 0.3m;

            Item item = new Item { Name = "Book", Price = 5.60M, IsImported = true, Category = ItemCategory.BOOK };

            var result = manager.CalculateTaxOnItem(item);

            Assert.AreEqual(expected, result.TaxPrice);
        }

        [TestMethod]
        public void CalculateBothTaxesOnItems_ShouldReturnTaxedItem()
        {
            decimal expected = 0.85m;

            Item item = new Item { Name = "Book", Price = 5.60M, IsImported = true, Category = ItemCategory.OTHER };

            var result = manager.CalculateTaxOnItem(item);

            Assert.AreEqual(expected, result.TaxPrice);
        }

        [TestMethod]
        public void CalculateSaleTest()
        {
            int expectedItems = 3;
            decimal expectedTaxes = 1.5m;
            decimal expectedTotal = 42.32m;

            List<Item> basket = new List<Item> {
                new Item { Name = "Book", Price = 12.49M, IsImported = false, Category = ItemCategory.BOOK },
                new Item { Name = "Book", Price = 12.49M, IsImported = false, Category = ItemCategory.BOOK },
                new Item { Name = "Music CD", Price = 14.99M, IsImported = false, Category = ItemCategory.OTHER },
                new Item { Name = "Chocolate Bar", Price = 0.85M, IsImported = false, Category = ItemCategory.FOOD }
            };

            var result = manager.CalculateSale(basket);

            Assert.AreEqual(expectedItems, result.Items.Count);
            Assert.AreEqual(expectedTaxes, result.Tax);
            Assert.AreEqual(expectedTotal, result.Total);
        }

        [TestMethod]
        public void PrintTicketTest_ShouldReturnSummary()
        {

            List<Item> basket = new List<Item> {
                new Item { Name = "Book", Price = 12.49M, IsImported = false, Category = ItemCategory.BOOK },
                new Item { Name = "Book", Price = 12.49M, IsImported = false, Category = ItemCategory.BOOK },
                new Item { Name = "Music CD", Price = 14.99M, IsImported = false, Category = ItemCategory.OTHER },
                new Item { Name = "Chocolate Bar", Price = 0.85M, IsImported = false, Category = ItemCategory.FOOD }
            };

            var sale = manager.CalculateSale(basket);
            var result = manager.PrintTicket(sale);

            Assert.IsFalse(string.IsNullOrWhiteSpace(result));
        }
    }
}
