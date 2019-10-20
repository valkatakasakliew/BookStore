using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObjects
{
    /// <summary>
    /// category with its discount, 
    /// </summary>
    public class Category : BusinessObject
    {
        /// <summary>
        /// The unique name of the category, it is a functionnal key
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// the discount applies when buying multiple book of this category
        /// </summary>
        public double Discount { get; set; }
    }
}
