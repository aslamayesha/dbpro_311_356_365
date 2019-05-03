﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using inventory_store.Models;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;

namespace inventory_store.Controllers
{
    public class AdminController : ApplicationBaseController //Controller
    {
        public string constr = "Data Source=UET\\NUMANSQL;Initial Catalog=DB1;Integrated Security=True";
        DataBaseConnection conD = DataBaseConnection.getInstance();
        // GET: Admin
        public ActionResult Home()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Addstaff()
        {
            list_staff f = new list_staff();
            SqlConnection con = conD.getConnection();
            if (con.State == System.Data.ConnectionState.Open)
            {
                SqlDataAdapter sda1 = new SqlDataAdapter("Select * From Staff", con);
                DataTable TT = new DataTable();
                sda1.Fill(TT);

                foreach (DataRow dr in TT.Rows)  // dt is a DataTable
                {
                    f.list.Add(new Addstaff { Id = Convert.ToInt32(dr["Id"]), Username = dr["Username"].ToString(), Email = dr["Email"].ToString(), Contact = dr["Contact"].ToString(), Address = dr["Address"].ToString() });


                }
                con.Close();
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
            SqlConnection con = conD.getConnection();
            if (con.State == System.Data.ConnectionState.Open)
            {
                try {
                    string q = "Insert INTO [Staff] VALUES('" + s.staff.Username.ToString() + "','" + s.staff.Email.ToString() + "','" + s.staff.Contact.ToString() + "','" + s.staff.Address.ToString() + "')";
                    SqlCommand cmd = new SqlCommand(q, con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    ///////////////////Staff Login Account Functionality/////////////////
                    string random = GetRandomString(5);
                    SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                    client.EnableSsl = true;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential("risingpearls16@gmail.com", "risingPearls471912");
                    MailMessage msg = new MailMessage();
                    msg.To.Add(s.staff.Email);
                    msg.From = new MailAddress("risingpearls16@gmail.com");
                    msg.Subject = "Staff Added by Admin";
                    msg.Body = "Your Username is :" + s.staff.Username + "password is : " + random;
                    client.Send(msg);

                    string loginQuery = "select max(Id) from Staff";
                    int staffId = DataBaseConnection.getInstance().executeScalar(loginQuery);

                    string query = string.Format("insert into Login(Id,Username,Email,Password,Role) values('{0}','{1}','{2}','{3}','{4}')", staffId, s.staff.Username, s.staff.Email, random, "Staff");
                    DataBaseConnection.getInstance().executeQuery(query);
                    ////////////////////////////////////////////////////////////////////////
                    return RedirectToAction("Addstaff");
                }
                catch
                {
                    return View(s);
                }

 }

            return View();
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


        [HttpPost]
        //httppost for edit
        public ActionResult Edit_staff(int? id, list_staff s)
        {
            SqlConnection con = conD.getConnection();
            if (con.State == System.Data.ConnectionState.Open)
            {

                string q = "UPDATE [Staff] SET  Staff.Username='" + s.staff.Username.ToString() + "',Staff.Email='" + s.staff.Email.ToString() + "',Staff.Contact='" + s.staff.Contact.ToString() + "',Staff.Address='" + s.staff.Address.ToString() + "' where Staff.Id='" + Convert.ToInt32(s.staff.Id) + "'";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.ExecuteNonQuery();
                con.Close();
                return RedirectToAction("Addstaff");



            }
            return View();
        }
        public ActionResult Delete_staff(int? id)
        {
            list_staff f = new list_staff();
            SqlConnection con = conD.getConnection();
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
                con.Close();
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
            SqlConnection con = conD.getConnection();
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
                        Staff.Email = dr["Email"].ToString();
                        Staff.Contact = dr["Contact"].ToString();
                        Staff.Address = dr["Address"].ToString();
                        Staff.Username = dr["Username"].ToString();

                    }


                }
                f.staff = Staff;

            }
            return View("Addstaff", f);


        }
        [HttpGet]
        public ActionResult customer_detail()
        {
            list_staff f = new list_staff();
            SqlConnection con = conD.getConnection();
            if (con.State == System.Data.ConnectionState.Open)
            {
                SqlDataAdapter sda1 = new SqlDataAdapter("Select * From Staff", con);
                DataTable TT = new DataTable();
                sda1.Fill(TT);

                foreach (DataRow dr in TT.Rows)  // dt is a DataTable
                {
                    f.list.Add(new Addstaff { Id = Convert.ToInt32(dr["Id"]), Username = dr["Username"].ToString(), Email = dr["Email"].ToString(), Contact = dr["Contact"].ToString(), Address = dr["Address"].ToString() });


                }
                con.Close();
                return View(f);
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult Addsalary(int? id)
        {

            List_salary f = new List_salary();

            SqlConnection con = conD.getConnection();
            if (con.State == System.Data.ConnectionState.Open)
            {
                SqlDataAdapter sda1 = new SqlDataAdapter("Select * From Salary Where StaffId='" + id + "'", con);
                DataTable TT = new DataTable();
                sda1.Fill(TT);

                foreach (DataRow dr in TT.Rows)  // dt is a DataTable
                {

                    f.list.Add(new Salary { med_id = Convert.ToInt32(dr["StaffId"]), SalaryAmount = Convert.ToInt32(dr["SalaryAmount"]), bonus = Convert.ToInt32(dr["Bonus"]), Month = Convert.ToDateTime(dr["Month"]) });


                }
                con.Close();
                return View(f);
            }
            else
            {
                return View();
            }

        }

        [HttpPost]
        public ActionResult Addsalary(int? id, List_salary k)
        {
            SqlConnection con = conD.getConnection();
            if (con.State == System.Data.ConnectionState.Open)
            {

                string q = "Insert INTO [Salary] VALUES('" + Convert.ToInt32(id) + "','" + Convert.ToInt32(k.s.SalaryAmount) + "','" + Convert.ToInt32(k.s.bonus) + "','" + Convert.ToDateTime(k.s.Month) + "')";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.ExecuteNonQuery();
                con.Close();
                return RedirectToAction("Addsalary");








            }
            return View();


        }
    }
}