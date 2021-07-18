using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Sale
    {
        public List<Item> Items { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }

    }
}
