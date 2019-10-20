using BusinessObjects.Interfaces;

namespace BusinessObjects
{
    /// <summary>
    /// Book in the catalog
    /// </summary>
    public class Book : BusinessObject,INameQuantity
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
        public double Price { get; set; }

        /// <summary>
        /// The Quantity of copy of the book in the catalog
        /// </summary>
        public int Quantity { get; set; }

    }
}
