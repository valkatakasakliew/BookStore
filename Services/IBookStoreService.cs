using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IBookStoreService
    {
        string ImportStock(string catalogAsJson);

        int BookQuantity(string name);

        double BooksPrice(string[] booksNames);
    }
}
