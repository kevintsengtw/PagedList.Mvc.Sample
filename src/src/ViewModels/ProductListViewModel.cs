using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using src.Models;

namespace src.ViewModels
{
    public class ProductListViewModel
    {
        public ProductSearchModel SearchParameter { get; set; }

        public IPagedList<Product> Products { get; set; }

        public SelectList CategoryItems { get; set; }

        public SelectList Suppliers { get; set; }

        public int PageIndex { get; set; }

    }

}