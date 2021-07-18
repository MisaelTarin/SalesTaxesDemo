using Managers.Interfaces;
using Models;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Managers
{
    public class ShopManager : IShopManager
    {
        /// <summary>
        /// A facade method that process the entire process.
        /// </summary>
        /// <param name="items"></param>
        /// <returns>A summary of the Sale as an string with the list of items bought, taxes applied and total.</returns>
        public string SubmitSale(List<Item> items) {
            var sale = CalculateSale(items);
            return PrintTicket(sale);
        }

        /// <summary>
        /// Process the sale.
        /// </summary>
        /// <param name="items"></param>
        /// <returns>A summary of the Sale as a Sale Object with the list of items bought, taxes applied and total.</returns>
        public Sale CalculateSale(List<Item> items)
        {
            Sale sale = new Sale();
            //sum up the list of items.
            sale.Items = CheckBasket(items);
            
            foreach (var item in sale.Items)
            {
                //sum up the tax on the items and the total.
                sale.Tax += item.TaxPrice;
                sale.Total += item.Price;
            }

            return sale;
        }

        /// <summary>
        /// Calculates the imported and sale taxes of the item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>the taxes applied to the item price.</returns>
        public Item CalculateTaxOnItem(Item item)
        {
            //First we get the exception taxes, we could get this from a table, for simplicity of this case it was made like this.
            List<ItemCategory> taxExceptions = new List<ItemCategory> { ItemCategory.BOOK, ItemCategory.FOOD, ItemCategory.MEDICAL_PRODUCT, };
            //we get the tax configuration, we can get this from a services or configuration file.
            decimal importedTax = 5/100M;
            decimal saleTax = 10/100M;

            //First we check if the item has been imported and if the item doesnt exclude the sale tax.
            if (item.IsImported && !taxExceptions.Exists(c=> c == item.Category))
            {
                item.TaxPrice = RoundTax(Math.Round(item.Price * (importedTax + saleTax), 2));
            }
            else if (item.IsImported)
            {
                //if the item is imported but excluded from the sale tax, we calculate the imported tax on the price.
                item.TaxPrice = RoundTax(Math.Round(item.Price * importedTax,2));
            }
            else if (!taxExceptions.Exists(c => c == item.Category))
            {
                //if the item is not imported and is not excluded from the sale tax, we calculate the sale tax on the price.
                item.TaxPrice = RoundTax(Math.Round(item.Price * saleTax, 2));
            }
            item.Price += item.TaxPrice;

            return item;
        }

        /// <summary>
        /// Iterates throug the list of items sums items with the same name.
        /// </summary>
        /// <param name="basket"></param>
        /// <returns>a new list of items with price sum up if there are multiple items of the same name.</returns>
        public List<Item> CheckBasket(List<Item> basket)
        {
            List<Item> groceries = new List<Item>();
            //Iterates through  the list of items
            foreach (var item in basket)
            {
                //check if the item has already been added to the new list.
                var itemCharged = groceries.FirstOrDefault(x => x.Name == item.Name);

                
                //If the item already exists in the new list, the price will be sum up. but won't be added to the list.
                if (itemCharged != null)
                {
                    itemCharged.Price += item.Price;
                    //Calculates the taxes on the item.
                    CalculateTaxOnItem(itemCharged);
                }
                else
                {
                    CalculateTaxOnItem(item);
                    //if the items doesnt exists it will be added to the list.
                    groceries.Add(item);
                }
            }

            return groceries;
        }
        /// <summary>
        /// Prints a summary of the sale. Listing all the items bought, taxes applied and total.
        /// </summary>
        /// <param name="sale"></param>
        /// <returns>a formated string with the summary of the sale.</returns>
        public string PrintTicket(Sale sale) {
            string ticket="";
            foreach (var item in sale.Items)
            {
                ticket += $"{item.Name}: {item.Price}{Environment.NewLine}";
          
            }
            ticket += $"Sales Taxes: {sale.Tax:0.00}{Environment.NewLine}Total:{sale.Total:0.00}"; 

            return ticket;
        }

        /// <summary>
        /// Simple helper method to round to next 5 cents of the tax.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private  decimal RoundTax(decimal value)
        {
            
            var val = Math.Ceiling(value * 20);

            return val == 0 ? 0 : val / 20;

        }
    }
}
