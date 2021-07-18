using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Managers.Interfaces
{
    public interface IShopManager
    {
        string PrintTicket(Sale sale);
        string SubmitSale(List<Item> items);
        List<Item> CheckBasket(List<Item> basket);
        Sale CalculateSale(List<Item> items);
        Item CalculateTaxOnItem(Item item);
    }
}
