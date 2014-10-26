using PagedList;
using src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace src.Controllers
{
    public class CustomerController : Controller
    {
        private Northwind db = new Northwind();

        private int pageSize = 5;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Sample1(int page = 1)
        {
            int currentPage = page < 1 ? 1 : page;

            var customers = db.Customers.OrderBy(x => x.CustomerID);

            var result = customers.ToPagedList(currentPage, pageSize);

            return View(result);
        }

        public ActionResult Sample2(int page = 1)
        {
            int currentPage = page < 1 ? 1 : page;

            var customers = db.Customers.OrderBy(x => x.CustomerID);

            var result = customers.ToPagedList(currentPage, pageSize);

            return View(result);
        }

        public ActionResult Sample3(int page = 1)
        {
            var currentPage = page < 1 ? 1 : page;

            var customers = db.Customers.OrderBy(x => x.CustomerID);
            var result = customers.ToPagedList(currentPage, pageSize);

            var items = new List<SelectListItem>();
            for (var i = 1; i <= result.PageCount; i++)
            {
                items.Add(new SelectListItem()
                {
                    Text = i.ToString(),
                    Value = i.ToString(),
                    Selected = i.Equals(result.PageNumber)
                });
            }
            ViewBag.PageItems = items;

            ViewBag.PageCountAndCurrentLocation = string.Format(
                "第 {0} 頁 / 共 {1} 頁",
                result.PageNumber, result.PageCount);

            ViewBag.ItemSliceAndTotalFormat = string.Format(
                "顯示項目： {0} ~ {1} , 共 {2} 項",
                result.FirstItemOnPage,
                result.LastItemOnPage,
                result.TotalItemCount);

            return View(result);            
        }

    }
}