using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PagedList;
using src.Models;

namespace src.Controllers
{
    public class OrderController : Controller
    {
        private Northwind db = new Northwind();

        private int pageSize = 5;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PagedPartial(int page = 1)
        {
            int currentPage = page < 1 ? 1 : page;
            var customers = db.Orders.OrderByDescending(x => x.OrderID);
            var result = customers.ToPagedList(currentPage, pageSize);

            ViewData.Model = result;

            return PartialView("_PagedPartial");
        }

        public ActionResult Sample2()
        {
            return View();
        }

        public ActionResult PagedCheckboxPartial(int page = 1)
        {
            int currentPage = page < 1 ? 1 : page;
            var customers = db.Orders.OrderByDescending(x => x.OrderID);
            var result = customers.ToPagedList(currentPage, pageSize);

            ViewData.Model = result;

            return PartialView("_PagedCheckboxPartial");
        }

        public ActionResult PostCheckedValues(string checkedValues)
        {
            JObject result = new JObject();

            if (string.IsNullOrWhiteSpace(checkedValues))
            {
                result.Add("Result", false);
                result.Add("Msg", "Empty");
            }
            else
            {
                var orderIds = checkedValues.Split(',').ToList().Select(Int32.Parse);
                var query = db.Orders.Where(x => orderIds.Contains(x.OrderID));
                result.Add("Result", true);
                result.Add("Msg", query.Count());
            }

            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

    }
}