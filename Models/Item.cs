using Models.Enums;
using System;

namespace Models
{
    public class Item
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool HasSaleTax { get; set; }
        public bool IsImported { get; set; }
        public decimal TaxPrice { get; set; }
        public ItemCategory Category { get; set; }
    }
}
