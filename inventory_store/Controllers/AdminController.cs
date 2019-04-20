using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace inventory_store.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult Addstaff()
        {
            return View();
        }

        public ActionResult Addsalary()
        {
            return View();
        }
    }
}