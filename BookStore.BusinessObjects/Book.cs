using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.BusinessObjects
{
    /// <summary>
    /// Book in the catalog
    /// </summary>
    public class Book : BusinessObject
    {
        /// <summary>
        /// The unique Name of the book, it is a functionnal key
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The name of category
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// The price of an copy of the book
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// The Quantity of copy of the book in the catalog
        /// </summary>
        public int Quantity { get; set; }

    }
}
