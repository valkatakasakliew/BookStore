using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.BusinessObjects
{
   public class BookStore
    {
        /// <summary>
        /// The Catalog of the store
        /// </summary>
        public Book[] Catalog { get; set; }

        /// <summary>
        /// List of existing category with associated discount
        /// </summary>
        public Category[] Category { get; set; }

    }
}
