
using BookStore.Data.Interfaces;
using BookStore.Data.ShoppingCart;
using BusinessObjects;
using BusinessObjects.Exceptions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;


namespace BookStore.Data.Repositories
{
    public class Store : IStore
    {

        private  static BusinessObjects.BookStore StoreStock = new BusinessObjects.BookStore();
               
        public double Buy(params string[] basketByNames)
        {
            double price = 0;

            if (StoreStock.Catalog != null && basketByNames.Any())
            {

                ShoppingCart.ShoppingCart shoppingCart = new ShoppingCart.ShoppingCart();

                foreach (string item in basketByNames)
                {
                    Book book = StoreStock.Catalog.FirstOrDefault(x => x.Name == item);

                    if (book == null)
                        continue;

                    Category category = StoreStock.Category.FirstOrDefault(x => x.Name == book.Category);

                    shoppingCart.Add(new Product() { Category = category, Book = book });
                }

                price = shoppingCart.GetTotalPrice();

            }

            return price;
        }

        public void Import(string catalogAsJson)
        {
            StoreStock = JsonConvert.DeserializeObject<BusinessObjects.BookStore>(catalogAsJson);
        }

        public int Quantity(string name)
        {
            int res = 0;
            if (StoreStock.Catalog !=null && StoreStock.Catalog.Any(x => x.Name == name))
                res = StoreStock.Catalog.FirstOrDefault(x => x.Name == name).Quantity;

            return res;
        }
  }
}
