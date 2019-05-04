using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using inventory_store.Models;
using System.Net.Mail;
using System.Net;
namespace inventory_store.Controllers
{
    public class UserAccountController : Controller
    {
        // GET: UserAccount
        public UserAccountController()
        {
            DataBaseConnection.getInstance().conStr = "Data Source=UET\\NUMANSQL;Initial Catalog=DB1;Integrated Security=True";
            DataBaseConnection.getInstance().getConnection();
        }

        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                string authorizeQuery = string.Format("select count(Id) from Login where Username='{0}' and Email='{1}' and Password='{2}'", model.Username, model.Email, model.Password);
                int isExistCount = DataBaseConnection.getInstance().executeScalar(authorizeQuery);
                if (isExistCount > 0)
                {
                    string roleQuery = string.Format("select Role from Login where Username='{0}' and Email='{1}' and Password='{2}'", model.Username, model.Email, model.Password);
                    var obj = DataBaseConnection.getInstance().readData(roleQuery);
                    // string role = null;
                    obj.Read();

                    int userId = 0;
                    string queryUserId = string.Format("select Id from Login where Username='{0}' and Email='{1}'", model.Username, model.Email);
                    userId = DataBaseConnection.getInstance().executeScalar(queryUserId);
                    LoginUser.Username = model.Username;
                    LoginUser.userId = userId;
                    if (obj.GetValue(0).ToString() == "Admin")
                    {
                       
                        //  return RedirectToAction("Home", "Admin", new { name = "hello welcome" });
                        return RedirectToAction("Home", "Admin");
                    }
                    else
                    {
                        //return RedirectToAction("Home", "Staff");
                     
                        return RedirectToAction("Home", "Staff");
                        // return RedirectToAction("Home", "Staff", new { staffId = staffId });
                    }
                }
            }
            return View(model);

        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string maxId = "select max(Id) from Login";
                    int max = DataBaseConnection.getInstance().executeScalar(maxId) + 1;
                    string query = string.Format("insert into Login(Id,Username,Email,Password,Role) values('{0}','{1}','{2}','{3}','{4}')", max, model.Username, model.Email, model.Password, "Admin");
                    DataBaseConnection.getInstance().executeQuery(query);

                }
                catch
                {
                    string query = string.Format("insert into Login(Id,Username,Email,Password,Role) values('{0}','{1}','{2}','{3}','{4}')", 1, model.Username, model.Email, model.Password, "Admin");
                    DataBaseConnection.getInstance().executeQuery(query);
                }

                ViewBag.LoginUser = model.Username;
                return RedirectToAction("Home", "Admin");
            }
            return View(model);
        }




        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                string authorizeQuery = string.Format("select count(Id) from Login where  Email='{0}'", model.Email);
                int isExistCount = DataBaseConnection.getInstance().executeScalar(authorizeQuery);
                if (isExistCount > 0)
                {
                    try
                    {
                        string random = GetRandomString(5);
                        SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                        client.EnableSsl = true;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Credentials = new NetworkCredential("risingpearls16@gmail.com", "risingPearls471912");
                        MailMessage msg = new MailMessage();
                        msg.To.Add(model.Email);
                        msg.From = new MailAddress("risingpearls16@gmail.com");
                        msg.Subject = "Password Recovery";
                        msg.Body = "Your New password is : " + random;
                        client.Send(msg);

                        string queryResetPassword = string.Format("update Login set Password='{0}' where Email='{1}'", random, model.Email);
                        DataBaseConnection.getInstance().executeQuery(queryResetPassword);
                        return RedirectToAction("Login", "UserAccount");
                    }
                    catch
                    {
                        return View(model);
                    }
                }
            }
            return View(model);
        }

        public string GetRandomString(int seed)
        {
            //use the following string to control your set of alphabetic characters to choose from
            //for example, you could include uppercase too
            const string alphabet = "abcdefghijklmnopqrstuvwxyz";

            // Random is not truly random,
            // so we try to encourage better randomness by always changing the seed value
            Random rnd = new Random((seed + DateTime.Now.Millisecond));
            // basic 5 digit random number
            string result = rnd.Next(10000, 99999).ToString();
            // single random character in ascii range a-z
            string alphaChar = alphabet.Substring(rnd.Next(0, alphabet.Length - 1), 1);
            // random position to put the alpha character
            int replacementIndex = rnd.Next(0, (result.Length - 1));
            result = result.Remove(replacementIndex, 1).Insert(replacementIndex, alphaChar);
            return result;
        }

        private char RandomNumber(int v1, int v2)
        {
            throw new NotImplementedException();
        }

      //  [Authorize]
     //   [AllowAnonymous]
        public ActionResult ChangedPassword()
        {
            return View();
        }
        [HttpPost]
     //   [AllowAnonymous]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
           
                string Query = string.Format("update Login set Password='{0}' where Password='{1}'", model.NewPassword,model.OldPassword);
               DataBaseConnection.getInstance().executeQuery(Query);
                return RedirectToAction("Home", "Admin");
                

          
        }

    }
}