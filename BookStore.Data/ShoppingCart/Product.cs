using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Data.ShoppingCart
{
    public class Product
    {
        public Book Book { get; set; }
        public Category Category { get; set; }
        public int Quantity { get; set; }
    }
}
