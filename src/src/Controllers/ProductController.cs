using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using src.Models;
using src.ViewModels;

namespace src.Controllers
{
    public class ProductController : Controller
    {
        private readonly Northwind _db = new Northwind();

        private const int PageSize = 10;

        private IEnumerable<Category> Categories
        {
            get { return _db.Categories.OrderBy(x => x.CategoryID); }
        }

        private IEnumerable<Supplier> Suppliers
        {
            get { return _db.Suppliers.OrderBy(x => x.CompanyName); }
        }

        public ActionResult Index(int page = 1)
        {
            var query = _db.Products.OrderBy(x => x.ProductID);

            int pageIndex = page < 1 ? 1 : page;

            var model = new ProductListViewModel
            {
                SearchParameter = new ProductSearchModel(),
                PageIndex = pageIndex,
                CategoryItems = new SelectList(this.Categories, "CategoryID", "CategoryName"),
                Suppliers = new SelectList(this.Suppliers, "SupplierID", "CompanyName"),
                Products = query.ToPagedList(pageIndex, PageSize)
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(ProductListViewModel model)
        {
            var query = _db.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(model.SearchParameter.ProductName))
            {
                query = query.Where(
                    x => x.ProductName.Contains(model.SearchParameter.ProductName));
            }
            if (!string.IsNullOrWhiteSpace(model.SearchParameter.QuantityPerUnit))
            {
                query = query.Where(
                    x => x.QuantityPerUnit.Contains(model.SearchParameter.QuantityPerUnit));
            }

            int supplierId;
            if (!string.IsNullOrWhiteSpace(model.SearchParameter.Supplier)
                &&
                int.TryParse(model.SearchParameter.Supplier, out supplierId))
            {
                query = query.Where(x => x.SupplierID == supplierId);
            }

            int categoryId;
            if (!string.IsNullOrWhiteSpace(model.SearchParameter.Category)
                &&
                int.TryParse(model.SearchParameter.Category, out categoryId))
            {
                query = query.Where(x => x.CategoryID == categoryId);
            }

            query = query.OrderBy(x => x.ProductID);

            int pageIndex = model.PageIndex < 1 ? 1 : model.PageIndex;

            var result = new ProductListViewModel
            {
                SearchParameter = model.SearchParameter,
                PageIndex = model.PageIndex < 1 ? 1 : model.PageIndex,
                CategoryItems = new SelectList(
                    items: this.Categories, dataValueField: "CategoryID",
                    dataTextField: "CategoryName",
                    selectedValue: model.SearchParameter.Category),
                Suppliers = new SelectList(
                    items: this.Suppliers,
                    dataValueField: "SupplierID",
                    dataTextField: "CompanyName",
                    selectedValue: model.SearchParameter.Supplier),
                Products = query.ToPagedList(pageIndex, PageSize)
            };

            return View(result);

        }
    }
}