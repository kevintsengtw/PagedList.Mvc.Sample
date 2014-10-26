using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace src.ViewModels
{
    public class ProductSearchModel
    {
        public string ProductName { get; set; }

        public string QuantityPerUnit { get; set; }

        public string Category { get; set; }

        public string Supplier { get; set; }
    }
}