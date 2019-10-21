using BusinessObjects;
using BusinessObjects.Exceptions;
using BusinessObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookStore.Data.ShoppingCart
{
    public class ShoppingCart
    {
        List<Product> _items = new List<Product>();
        List<Book> _missing = new List<Book>();

        public void Add(Product product)
        {

            if (_items.Any(x => x.Book.Name == product.Book.Name))
            {
                product = _items.FirstOrDefault(x => x.Book.Name == product.Book.Name);
                CheckProductQuantity(product);
            }
            else
            {
                product.Quantity = 1;
                _items.Add(product);
            }
        }

        public double GetTotalPrice()
        {
            CheckMissings();

            double price = 0;
            foreach (var item in _items)
            {
                if (_items.Count(x => x.Category.Name == item.Category.Name) > 1 || item.Quantity > 1)
                    price += System.Math.Round((item.Quantity - item.Category.Discount) * item.Book.Price, 2);
                else
                    price += item.Book.Price * item.Quantity;
            }
            return price;
        }

        private void CheckProductQuantity(Product product)
        {
            if (product.Book.Quantity > product.Quantity)
                _items.FirstOrDefault(x => x.Book.Name == product.Book.Name).Quantity++;
            else
                UpdateMissings(product);
        }

        private void UpdateMissings(Product product)
        {
            if (_missing.Any(x => x.Name == product.Book.Name))
                _missing.FirstOrDefault(x => x.Name == product.Book.Name).Quantity++;
            else
                _missing.Add(new Book() { Name = product.Book.Name, Quantity = _items.FirstOrDefault(x => x.Book.Name == product.Book.Name).Quantity +1});
        }

        private void CheckMissings()
        {
            if (_missing.Any())
                throw new NotEnoughInventoryException(_missing);
        }
    }
}
