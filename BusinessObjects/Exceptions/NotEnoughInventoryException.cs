using BusinessObjects.Interfaces;
using System;
using System.Collections.Generic;

namespace BusinessObjects.Exceptions
{
    public class NotEnoughInventoryException : Exception
    {
        public NotEnoughInventoryException()
        {

        }

        public NotEnoughInventoryException(IEnumerable<INameQuantity> missingBooks)
        {
            Missing = missingBooks;
        }

        public IEnumerable<INameQuantity> Missing { get; }
        
        public override string Message
        {
            get
            {
                string message = $"Unfortunately we don't have in stock:\n";
                if (Missing != null)
                {
                    int i = 1;
                    foreach (INameQuantity miss in Missing)
                    {
                        message += $"{i}.{miss.Quantity} copies from {miss.Name} book.";
                        i++;
                    }
                }
                return message;
            }
        }

    }
}
