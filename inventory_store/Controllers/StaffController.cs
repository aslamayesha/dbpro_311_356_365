using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using inventory_store.Models;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


namespace inventory_store.Controllers
{
    public class StaffController : Controller
    {
        public string constr = "Data Source=DESKTOP-16KVTNK;Initial Catalog=DB1;User ID=sa;Password=123";


        public ActionResult Home()
        {
            return View();
        }
        [HttpGet]

        public ActionResult Addmedicine()
        {

            list_medicine k = new list_medicine();
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-16KVTNK;Initial Catalog=DB1;User ID=sa;Password=123");

            conn.Open();
            DataTable ds = new DataTable();
            if (conn.State == System.Data.ConnectionState.Open)
            {


                SqlCommand cmd = new SqlCommand("  Select * FROM Medicine INNER JOIN MedicineInventory ON Medicine.Id=MedicineInventory.MedicineId", conn);
                SqlDataAdapter j = new SqlDataAdapter();
                j.SelectCommand = cmd;
                j.Fill(ds);
                foreach (DataRow t in ds.Rows)
                {
                    k.l.Add(new Medicine { Id = Convert.ToInt32(t["Id"]), Name = t["Name"].ToString(), Formula = t["Formula"].ToString(), Category = t["Category"].ToString(), Price = Convert.ToInt32(t["Price"]), MedicinePerPack = Convert.ToInt32(t["MedicinePerPack"]), PurchasePricePack = Convert.ToInt32(t["PurchasePricePack"]), SellingPriceItem = Convert.ToInt32(t["SellingPriceItem"]) });
                }
                return View(k);
            }



            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Addmedicine(list_medicine s)
        {
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                string query1 = "Insert into [Medicine] Values('" + s.med.Name.ToString() + "','" + s.med.Formula.ToString() + "','" + s.med.Category.ToString() + "','" + Convert.ToInt32(s.med.Price) + "' )";

                SqlCommand cmd3 = new SqlCommand(query1, conn);
                cmd3.ExecuteNonQuery();
                string qeury = "Insert into [MedicineInventory] Values ((Select Max(Id) from [Medicine]) ,'" + Convert.ToInt32(s.med.MedicinePerPack) + "','" + Convert.ToInt32(s.med.PurchasePricePack) + "','" + Convert.ToInt32(s.med.SellingPriceItem) + "')";
                SqlCommand ss = new SqlCommand(qeury, conn);
                ss.ExecuteNonQuery();
                return RedirectToAction("addmedicine");
            }
            return View();

        }
        public ActionResult delete_medicine(int? id)
        {
            list_medicine k = new list_medicine();
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-16KVTNK;Initial Catalog=DB1;User ID=sa;Password=123");

            conn.Open();
            DataTable ds = new DataTable();
            if (conn.State == System.Data.ConnectionState.Open)
            {

                SqlDataAdapter sda1 = new SqlDataAdapter(" Select * FROM Medicine INNER JOIN MedicineInventory ON Medicine.Id=MedicineInventory.MedicineId", conn);
                DataTable TT = new DataTable();

                sda1.Fill(TT); //filling the table
                foreach (DataRow dr in TT.Rows)  // dt is a DataTable
                {
                    if (id == Convert.ToInt32(dr["Id"]))
                    {

                        string q = "Delete From Medicine where Medicine .Id='" + id + "'";

                        SqlCommand cmd = new SqlCommand(q, conn);
                        cmd.ExecuteNonQuery();
                        string qq = "Delete From MedicineInventory where MedicineInventory.MedicineId='" + id + "'";

                        SqlCommand cmd2 = new SqlCommand(qq, conn);
                        cmd2.ExecuteNonQuery();


                    }


                }
                SqlDataAdapter sda11 = new SqlDataAdapter(" Select * FROM Medicine INNER JOIN MedicineInventory ON Medicine.Id=MedicineInventory.MedicineId ", conn);
                DataTable TT1 = new DataTable();
                sda11.Fill(TT1);
                foreach (DataRow dr in TT1.Rows)  // dt is a DataTable
                {
                    k.l.Add(new Medicine { Id = Convert.ToInt32(dr["Id"]), Name = dr["Name"].ToString(), Formula = dr["Formula"].ToString(), Category = dr["Category"].ToString(), Price = Convert.ToInt32(dr["Price"]), MedicinePerPack = Convert.ToInt32(dr["MedicinePerPack"]), PurchasePricePack = Convert.ToInt32(dr["PurchasePricePack"]), SellingPriceItem = Convert.ToInt32(dr["SellingPriceItem"]) });
                }


            }

            return View("addmedicine", k);

        }
        public ActionResult Edit_medicne(int? id)
        {
            list_medicine k = new list_medicine();
            Medicine m = new Medicine();
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-16KVTNK;Initial Catalog=DB1;User ID=sa;Password=123");

            conn.Open();
            DataTable ds = new DataTable();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlDataAdapter sda1 = new SqlDataAdapter("Select * FROM Medicine INNER JOIN MedicineInventory ON Medicine.Id=MedicineInventory.MedicineId", conn);
                DataTable TT = new DataTable();
                sda1.Fill(TT);
                foreach (DataRow dr in TT.Rows)  // dt is a DataTable
                {

                    k.l.Add(new Medicine { Id = Convert.ToInt32(dr["Id"]), Name = dr["Name"].ToString(), Formula = dr["Formula"].ToString(), Category = dr["Category"].ToString(), Price = Convert.ToInt32(dr["Price"]), MedicinePerPack = Convert.ToInt32(dr["MedicinePerPack"]), PurchasePricePack = Convert.ToInt32(dr["PurchasePricePack"]), SellingPriceItem = Convert.ToInt32(dr["SellingPriceItem"]) });

                    if (id == Convert.ToInt32(dr["Id"]))
                    {
                        m.Id = id;

                        m.Name = dr["Name"].ToString();
                        m.Formula = dr["Formula"].ToString();
                        m.Category = dr["Category"].ToString();
                        m.Price = Convert.ToInt32(dr["Price"]);
                        m.MedicinePerPack = Convert.ToInt32(dr["MedicinePerPack"]);
                        m.PurchasePricePack = Convert.ToInt32(dr["PurchasePricePack"]);
                        m.SellingPriceItem = Convert.ToInt32(dr["SellingPriceItem"]);


                    }


                }
                k.med = m;



            }
            return View("addmedicine", k);


        }
        [HttpPost]
        public ActionResult Edit_medicne(list_medicine s, int? id)
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {

                string q = "UPDATE [Medicine] SET  Medicine.Name='" + s.med.Name.ToString() + "',Medicine.Formula='" + s.med.Formula.ToString() + "',Medicine.Category='" + s.med.Category.ToString() + "',Medicine.Price='" + Convert.ToInt32(s.med.Price) + "' where Medicine.Id='" + id + "'";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.ExecuteNonQuery();
                string qq = "UPDATE [MedicineInventory] SET  MedicineInventory.MedicinePerPack='" + Convert.ToInt32(s.med.MedicinePerPack) + "',MedicineInventory.PurchasePricePack='" + Convert.ToInt32(s.med.PurchasePricePack) + "',MedicineInventory.SellingPriceItem='" + Convert.ToInt32(s.med.SellingPriceItem) + "' where MedicineInventory.MedicineId ='" + id + "'";
                SqlCommand cmd2 = new SqlCommand(qq, con);
                cmd2.ExecuteNonQuery();

                return RedirectToAction("addmedicine");

            }


            return View();
        }



        public ActionResult AddSales()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Inventory()
        {
            List<string> name = new List<string>();
            list_inventory k = new list_inventory();
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-16KVTNK;Initial Catalog=DB1;User ID=sa;Password=123");

            conn.Open();
            DataTable ds = new DataTable();
            DataTable dr = new DataTable();

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand cmd1 = new SqlCommand("  Select * from Medicine", conn);
                SqlDataAdapter jj = new SqlDataAdapter();
                jj.SelectCommand = cmd1;
                jj.Fill(dr);
                SqlCommand cmd = new SqlCommand("  Select Inventory.Id,Medicine.Name,Medicine.Category,Inventory.NumberofPacks,Inventory.Quantity,Inventory.ManufactureDate,Inventory.ExpiryDate FROM Medicine INNER JOIN Inventory ON Medicine.Id=Inventory.MedicineId", conn);
                SqlDataAdapter j = new SqlDataAdapter();
                j.SelectCommand = cmd;
                j.Fill(ds);
                var list_inventory = new list_inventory();
                List<SelectListItem> item8 = new List<SelectListItem>();
                List<SelectListItem> item9 = new List<SelectListItem>();


                foreach (DataRow t in ds.Rows)
                {


                    k.llistinv.Add(new Inventory { Id = Convert.ToInt32(t["Id"]), Name = t["Name"].ToString(), Category = t["Category"].ToString(), MedicineId = Convert.ToInt32(t["Id"]), NumberofPacks = Convert.ToInt32(t["NumberofPacks"]), Quantity = Convert.ToInt32(t["Quantity"]), ManufactureDate = Convert.ToDateTime(t["ManufactureDate"]), ExpiryDate = Convert.ToDateTime(t["ExpiryDate"]) });
                }
                foreach (DataRow t in dr.Rows)
                {
                    k.l.Add(new Medicine { Id = Convert.ToInt32(t["Id"]), Name = t["Name"].ToString(), Category = t["Category"].ToString() });
                }
                foreach (var c in k.l)
                {
                    item8.Add(new SelectListItem
                    {
                        Text = c.Category,
                        // Value = c.Id.ToString()
                        Value = c.Category.ToString()
                    });
                    item9.Add(new SelectListItem { Text = c.Name, Value = c.Name.ToString() });
                }
                ViewBag.Category = item8;

                ViewBag.Name = item9;
                list_inventory.CategoryList = new SelectList(k.llistinv, "Category", "Category");
                list_inventory.CategoryList = new SelectList(k.llistinv, "Name", "Name");



                return View(k);
            }



            else
            {
                return View();
            }
        }


        [HttpPost]
        public ActionResult Inventory(list_inventory s)
        {
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                // string query1 = "Insert into [Inventory] Values('" + s.med.Category.ToString() + "','" + s.med.Name.ToString() + "','" + s.med.Category.ToString() + "','" + Convert.ToInt32(s.med.Price) + "' )";

                //SqlCommand cmd3 = new SqlCommand(query1, conn);
                //cmd3.ExecuteNonQuery();
                string qeury = "Insert into [Inventory] Values ((Select ID from [Medicine] where Medicine.Name='" + s.med.Name.ToString() + "' AND Medicine.Category='" + s.med.Category.ToString() + "') ,'" + Convert.ToInt32(s.inv.NumberofPacks) + "','" + Convert.ToInt32(s.inv.Quantity) + "','" + Convert.ToDateTime(s.inv.ManufactureDate) + "','" + Convert.ToDateTime(s.inv.ExpiryDate) + "')";
                SqlCommand ss = new SqlCommand(qeury, conn);
                ss.ExecuteNonQuery();
                return RedirectToAction("Inventory");
            }
            return View();
        }

        public ActionResult Delete_inventory(int? id)
        {
            list_inventory k = new list_inventory();
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-16KVTNK;Initial Catalog=DB1;User ID=sa;Password=123");

            conn.Open();
            DataTable ds = new DataTable();
            if (conn.State == System.Data.ConnectionState.Open)
            {

                //SqlDataAdapter sda1 = new SqlDataAdapter(" Select * FROM Medicine INNER JOIN MedicineInventory ON Medicine.Id=MedicineInventory.MedicineId", conn);
                SqlDataAdapter sda1 = new SqlDataAdapter("  Select Inventory.Id,Medicine.Name,Medicine.Category,Inventory.NumberofPacks,Inventory.Quantity,Inventory.ManufactureDate,Inventory.ExpiryDate FROM Medicine INNER JOIN Inventory ON Medicine.Id=Inventory.MedicineId", conn);


                DataTable TT = new DataTable();

                sda1.Fill(TT); //filling the table
                foreach (DataRow dr in TT.Rows)  // dt is a DataTable
                {
                    if (id == Convert.ToInt32(dr["Id"]))
                    {


                        string qq = "Delete From Inventory where Inventory.Id='" + id + "'";

                        SqlCommand cmd2 = new SqlCommand(qq, conn);
                        cmd2.ExecuteNonQuery();


                    }


                }
                SqlDataAdapter sda11 = new SqlDataAdapter("  Select Inventory.Id, Inventory.MedicineId,Medicine.Name,Medicine.Category,Inventory.NumberofPacks,Inventory.Quantity,Inventory.ManufactureDate,Inventory.ExpiryDate FROM Medicine INNER JOIN Inventory ON Medicine.Id=Inventory.MedicineId", conn);

                DataTable TT1 = new DataTable();
                sda11.Fill(TT1);
                foreach (DataRow dr in TT1.Rows)  // dt is a DataTable
                {
                    k.llistinv.Add(new Inventory { Id = Convert.ToInt32(dr["Id"]), Name = dr["Name"].ToString(), Category = dr["Category"].ToString(), MedicineId = Convert.ToInt32(dr["MedicineId"]), NumberofPacks = Convert.ToInt32(dr["NumberofPacks"]), Quantity = Convert.ToInt32(dr["Quantity"]), ManufactureDate = Convert.ToDateTime(dr["ManufactureDate"]), ExpiryDate = Convert.ToDateTime(dr["ExpiryDate"]) });
                }


            }

            return RedirectToAction("Inventory");

        }




        [HttpGet]

        public ActionResult Edit_inventory(int? id)
        {
            list_inventory k = new list_inventory();
            Inventory m = new Inventory();
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-16KVTNK;Initial Catalog=DB1;User ID=sa;Password=123");

            conn.Open();
            DataTable ds = new DataTable();
            DataTable dt = new DataTable();

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SqlCommand cmd1 = new SqlCommand("  Select * from Medicine", conn);
                SqlDataAdapter jj = new SqlDataAdapter();
                jj.SelectCommand = cmd1;
                jj.Fill(dt);
                SqlDataAdapter sda1 = new SqlDataAdapter("  Select Inventory.Id, Inventory.MedicineId,Medicine.Name,Medicine.Category,Inventory.NumberofPacks,Inventory.Quantity,Inventory.ManufactureDate,Inventory.ExpiryDate FROM Medicine INNER JOIN Inventory ON Medicine.Id=Inventory.MedicineId", conn);

                // SqlDataAdapter sda1 = new SqlDataAdapter("Select * FROM Medicine INNER JOIN MedicineInventory ON Medicine.Id=MedicineInventory.MedicineId", conn);
                DataTable TT = new DataTable();
                sda1.Fill(TT);
                foreach (DataRow dr in TT.Rows)  // dt is a DataTable
                {

                    k.llistinv.Add(new Inventory { Id = Convert.ToInt32(dr["Id"]), Name = dr["Name"].ToString(), Category = dr["Category"].ToString(), MedicineId = Convert.ToInt32(dr["MedicineId"]), NumberofPacks = Convert.ToInt32(dr["NumberofPacks"]), Quantity = Convert.ToInt32(dr["Quantity"]), ManufactureDate = Convert.ToDateTime(dr["ManufactureDate"]), ExpiryDate = Convert.ToDateTime(dr["ExpiryDate"]) });

                    if (id == Convert.ToInt32(dr["Id"]))
                    {
                        m.Id = id;

                        m.Name = dr["Name"].ToString();

                        m.Category = dr["Category"].ToString();
                        m.MedicineId = Convert.ToInt32(dr["MedicineId"]);
                        m.NumberofPacks = Convert.ToInt32(dr["NumberofPacks"]);
                        m.Quantity = Convert.ToInt32(dr["Quantity"]);
                        m.ManufactureDate = Convert.ToDateTime(dr["ManufactureDate"]);
                        m.ExpiryDate = Convert.ToDateTime(dr["ExpiryDate"]);



                    }


                }
                foreach (DataRow t in dt.Rows)
                {
                    k.l.Add(new Medicine { Id = Convert.ToInt32(t["Id"]), Name = t["Name"].ToString(), Category = t["Category"].ToString() });
                }

                var list_inventory = new list_inventory();
                List<SelectListItem> item8 = new List<SelectListItem>();
                List<SelectListItem> item9 = new List<SelectListItem>();
                foreach (var c in k.l)
                {
                    item8.Add(new SelectListItem
                    {
                        Text = c.Category,
                        // Value = c.Id.ToString()
                        Value = c.Category.ToString()
                    });
                    item9.Add(new SelectListItem { Text = c.Name, Value = c.Name.ToString() });
                }
                ViewBag.Category = item8;

                ViewBag.Name = item9;
                list_inventory.CategoryList = new SelectList(k.llistinv, "Category", "Category");
                list_inventory.CategoryList = new SelectList(k.llistinv, "Name", "Name");
                k.inv = m;




            }
            return View("Inventory", k);


        }

    }
}

