using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace inventory_store.Controllers
{
    public class StaffController : Controller
    {
      
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult Addmedicine()
        {
            return View();
        }
        public ActionResult AddSales()
        {
            return View();
        }
        public ActionResult Inventory()
        {
            return View();
        }


    }
}