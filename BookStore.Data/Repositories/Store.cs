
using BookStore.Data.Interfaces;
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

                List<Book> storedInBasket = StoredInBasket(basketByNames);

                price = GetBasketItemPrice(storedInBasket);
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


        private List<Book> StoredInBasket(string[] basketByNames)
        {
            List<Book> basketItems = new List<Book>();
            List<Book> missing = new List<Book>();

            foreach (string name in basketByNames)
            {
                if (basketItems.Any(x => x.Name == name))
                {
                    if (basketItems.FirstOrDefault(x => x.Name == name).Quantity < Quantity(name))
                        basketItems.FirstOrDefault(x => x.Name == name).Quantity++;
                    else
                        missing.Add(basketItems.FirstOrDefault(x => x.Name == name));
                }
                else
                {
                    Book bookInStock = StoreStock.Catalog.FirstOrDefault(x => x.Name == name);
                    if (bookInStock != null)
                    {
                        basketItems.Add(new Book
                        {
                            Name = bookInStock.Name,
                            Category = bookInStock.Category,
                            Price = bookInStock.Price,
                            Quantity = 1
                        });
                    }
                    else
                    {
                        if(missing.Any(m=>m.Name == name))
                            missing.FirstOrDefault(x => x.Name == name).Quantity++;
                        else
                            missing.Add(new Book() { Name = name, Quantity = 1 });
                    }
                }
            }

            if (missing.Any())
                throw new NotEnoughInventoryException(missing);

            return basketItems;
        }

        private double GetBasketItemPrice(List<Book> itemsInBasket)
        {
            double price = 0;

            foreach (Book item in itemsInBasket)
            {
                if (itemsInBasket.Count(x => x.Category == item.Category) > 1 || item.Quantity > 1)
                    price += System.Math.Round((item.Quantity - StoreStock.Category.FirstOrDefault(x => x.Name == item.Category).Discount) * item.Price,2);
                else
                    price += item.Price * item.Quantity;

            }

            return price;
        }
    }
}
