using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using inventory_store.Models;
using System.Data.SqlClient;

namespace inventory_store.Controllers
{
    public class AdminController : Controller
    {
        public string constr = "Data Source=FINE\\AYESHASLAM;Initial Catalog=DB1;Integrated Security=True";
        // GET: Admin
        public ActionResult Home()
        {
            return View();
        }
       
        
        [HttpGet]
        public ActionResult Addstaff()
        {
            list_staff f = new list_staff();
            SqlConnection con = new SqlConnection("Data Source=FINE\\AYESHASLAM;Initial Catalog=DB1;Integrated Security=True");
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                SqlDataAdapter sda1 = new SqlDataAdapter("Select * From Staff", con);
                DataTable TT = new DataTable();
                sda1.Fill(TT);

                foreach (DataRow dr in TT.Rows)  // dt is a DataTable
                {
                    f.list.Add(new Addstaff {Id= Convert.ToInt32( dr["Id"]), Username = dr["Username"].ToString(), Email = dr["Email"].ToString(), Contact = dr["Contact"].ToString(), Address = dr["Address"].ToString() });


                }
                return View(f);
            }
            else
            {
                return View();
            }
           
        }
        [HttpPost]
        public ActionResult Addstaff(list_staff s)
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                if (s.staff.Id > 0)
                {
                    string q = "UPDATE [Staff] SET  Staff.Username='" + s.staff.Username.ToString() + "',Staff.Email='" + s.staff.Email.ToString() + "',Staff.Contact='" + s.staff.Contact.ToString() + "',Staff.Address='" + s.staff.Address.ToString() + "' where Staff.Id='" + Convert.ToInt32(s.staff.Id) + "'";
                    SqlCommand cmd = new SqlCommand(q, con);
                    cmd.ExecuteNonQuery();
                    return RedirectToAction("Addstaff");

                }
                else
                {
                    string q = "Insert INTO [Staff] VALUES('" + s.staff.Username.ToString() + "','" + s.staff.Email.ToString() + "','" + s.staff.Contact.ToString() + "','" + s.staff.Address.ToString() + "')";
                    SqlCommand cmd = new SqlCommand(q, con);
                    cmd.ExecuteNonQuery();
                    return RedirectToAction("Addstaff");
                 
                  
                }

               

            }

            return View();
        }
        public ActionResult Delete_staff(int ?id)
        {
            list_staff f = new list_staff();
            SqlConnection con = new SqlConnection("Data Source=FINE\\AYESHASLAM;Initial Catalog=DB1;Integrated Security=True");
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                SqlDataAdapter sda1 = new SqlDataAdapter("Select * From Staff", con);
                DataTable TT = new DataTable();
                sda1.Fill(TT);
                foreach (DataRow dr in TT.Rows)  // dt is a DataTable
                {
                    if (id == Convert.ToInt32(dr["Id"]))
                    {

                        string q = "Delete From Staff where Staff.Id='" + id + "'";
                        SqlCommand cmd = new SqlCommand(q, con);
                        cmd.ExecuteNonQuery();
                    }


                }
                SqlDataAdapter sda11 = new SqlDataAdapter("Select * From Staff", con);
                DataTable TT1 = new DataTable();
                sda11.Fill(TT1);
                foreach (DataRow dr in TT1.Rows)  // dt is a DataTable
                {

                    f.list.Add(new Addstaff { Id = Convert.ToInt32(dr["Id"]), Username = dr["Username"].ToString(), Email = dr["Email"].ToString(), Contact = dr["Contact"].ToString(), Address = dr["Address"].ToString() });

                }
            }
            return View("Addstaff", f);

        }
        public ActionResult Edit_staff(int? id)
        {
            list_staff f = new list_staff();
            Addstaff Staff = new Addstaff();
            SqlConnection con = new SqlConnection("Data Source=FINE\\AYESHASLAM;Initial Catalog=DB1;Integrated Security=True");
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                SqlDataAdapter sda1 = new SqlDataAdapter("Select * From Staff", con);
                DataTable TT = new DataTable();
                sda1.Fill(TT);
                foreach (DataRow dr in TT.Rows)  // dt is a DataTable
                {
                    f.list.Add(new Addstaff { Id = Convert.ToInt32(dr["Id"]), Username = dr["Username"].ToString(), Email = dr["Email"].ToString(), Contact = dr["Contact"].ToString(), Address = dr["Address"].ToString() });
                    if (id == Convert.ToInt32(dr["Id"]))
                    {
                        Staff.Id = id;
                        Staff.Email  = dr["Email"].ToString();
                        Staff.Contact = dr["Contact"].ToString();
                        Staff.Address = dr["Address"].ToString();
                        Staff.Username = dr["Username"].ToString();

                    }


                }
                f.staff = Staff;

            }
            return View("Addstaff", f);
            

        }

        public ActionResult Addsalary()
        {
            return View();
        }
    }
}