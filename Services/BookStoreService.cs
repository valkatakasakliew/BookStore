using BookStore.Data.Factories;
using BookStore.Data.Interfaces;
using System;

namespace Services
{
    public class BookStoreService : IBookStoreService
    {
        static readonly IStore _storeRepo = new RepositoryFactory().StoreRepository;

        public int BookQuantity(string name)
        {
            return _storeRepo.Quantity(name);
        }

        public double BooksPrice(string[] booksNames)
        {
            return _storeRepo.Buy(booksNames);
        }

        public string ImportStock(string catalogAsJson)
        {
            string result = string.Empty;
            try
            {
                _storeRepo.Import(catalogAsJson);
                result = "sucessfuly imported";
            }
            catch (Exception ex)
            {
                result = $"Error while importing catalog.{ex.Message}";
            }
            return result;
        }
    }
}
