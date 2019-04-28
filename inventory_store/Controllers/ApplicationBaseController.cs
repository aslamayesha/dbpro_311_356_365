using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using inventory_store.Models;
namespace inventory_store.Controllers
{
    public class ApplicationBaseController : Controller
    {
        // GET: ApplicationBase
        public ActionResult Index()
        {
            return View();
        }

        
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (LoginUser.Username != null)
            {
                /* var context = new PharmacyDBEntities4();
                 var userid = User.Identity.;
                 var username = context.AspNetUsers.Where(x => x.Id == userid).First().AccountUserName;
                 var currentdate = DateTime.Today.ToString("yyyy-MM-dd");
                 var MedicineAddedToday = context.Stocks.Where(x => x.AddedDate.ToString() == currentdate).Count();
                 var SaleToday = context.AllSales.Where(x => x.Date.ToString() == currentdate).Count();
                 if (!string.IsNullOrEmpty(username) || MedicineAddedToday >= 0)
                 {
                     ViewData.Add("FullName", username);
                     ViewData.Add("MedicineAddToday", MedicineAddedToday);
                     ViewData.Add("SaleToday", SaleToday);

                 }*/
                ViewData.Add("FullName", LoginUser.Username);
            }
            base.OnActionExecuted(filterContext);
        }
        public ApplicationBaseController()
        { }
    }
}