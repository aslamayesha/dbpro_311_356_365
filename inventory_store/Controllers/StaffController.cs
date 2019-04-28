﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using inventory_store.Models;
using System.Data.SqlClient;
using System.Data;


namespace inventory_store.Controllers
{
    public class StaffController : ApplicationBaseController //Controller
    {
        public string constr = "Data Source=UET\\NUMANSQL;Initial Catalog=DB1;Integrated Security=True";
        List<string> allsearch;
        List<string> StateList;
        int prevSellQuantity= 0;
        CreatePOS pos;
   //   int staffid = 0;
        public StaffController()
        {

            allsearch = new List<string>();
            StateList = new List<string>();
          
           pos = new CreatePOS();
        }
      //  CreatePOS pos;
        public ActionResult Home()
        {
         
            return View();
        }
        [HttpGet]

        public ActionResult Addmedicine()
        {

            list_medicine k = new list_medicine();
            SqlConnection conn = new SqlConnection("Data Source=UET\\NUMANSQL;Initial Catalog=DB1;Integrated Security=True");

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
            SqlConnection conn = new SqlConnection("Data Source=UET\\NUMANSQL;Initial Catalog=DB1;Integrated Security=True");

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
            SqlConnection conn = new SqlConnection("Data Source=UET\\NUMANSQL;Initial Catalog=DB1;Integrated Security=True");

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



        [HttpGet]
        public ActionResult Inventory()
        {
            List<string> name = new List<string>();
            list_inventory k = new list_inventory();
            SqlConnection conn = new SqlConnection("Data Source=UET\\NUMANSQL;Initial Catalog=DB1;Integrated Security=True");

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
            SqlConnection conn = new SqlConnection("Data Source=UET\\NUMANSQL;Initial Catalog=DB1;Integrated Security=True");

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
            SqlConnection conn = new SqlConnection("Data Source=UET\\NUMANSQL;Initial Catalog=DB1;Integrated Security=True");

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


        public ActionResult AddSales()
        {
            CreatePOS pos = new CreatePOS();
            string query = "select Sells.InventoryId,Medicine.Name,Medicine.Category,Medicine.Price,Sells.Quantity,Sells.Discount,Sells.Subtotal from Sells join Inventory on Inventory.Id=Sells.InventoryId join Medicine on Medicine.Id=Inventory.MedicineId";
            var data = DataBaseConnection.getInstance().readData(query);
            while (data.Read())
            {
                Sales sale = new Sales();
                sale.InventoryId = (int)data.GetValue(0);
                sale.Name = data.GetValue(1).ToString();
                sale.Category = data.GetValue(2).ToString();
                sale.Price = (int)data.GetValue(3);
                sale.Quantity = (int)data.GetValue(4);
                sale.Discount = float.Parse(data.GetValue(5).ToString());
                sale.Subtotal = float.Parse(data.GetValue(6).ToString());
                pos.listSale.Add(sale);
            }
         return View(pos);
        }

        [HttpPost]
        public ActionResult AddSales(CreatePOS orderItem)
        { int maxBillId = 1;
            string queryinventory = string.Format("select top 1 Inventory.Id,Medicine.Price from Inventory join Medicine on Medicine.Id=Inventory.MedicineId and  Medicine.Name='{0}' and Medicine.Category='{1}'", orderItem.sale.Name, orderItem.sale.Category);
            var inventory = DataBaseConnection.getInstance().readData(queryinventory);
            inventory.Read();
            int inventoryId = (int)inventory.GetValue(0);
            int price= (int)inventory.GetValue(1);
            try
            {
                string query = string.Format("select max(OrderId) from Bill");
                maxBillId = DataBaseConnection.getInstance().executeScalar(query)+1;

            }
            catch
            {
              

            }
            float subtotal = float.Parse((orderItem.sale.Quantity*price).ToString());
            string queryInsert = string.Format("insert into Sells values('{0}','{1}','{2}','{3}','{4}')", maxBillId,inventoryId, orderItem.sale.Quantity, orderItem.sale.Discount, subtotal);
            DataBaseConnection.getInstance().executeQuery(queryInsert);

            string queryUpdateInventory = string.Format("update Inventory set Quantity=(select Quantity from Inventory where Id='{1}')-'{0}' where Id='{1}'", orderItem.sale.Quantity,inventoryId);
            DataBaseConnection.getInstance().executeQuery(queryUpdateInventory);
            //   string updateInventory=string.Format("update Inventory set Quantity='{0}'",)
            // string query=string.Format("insert into ")


            return RedirectToAction("AddSales");
        //     return View(pos);
        }

        public ActionResult DeleteSale(int id)
        {
            string queryUndoStock = string.Format("select InventoryId,Quantity from Sells where InventoryId='{0}'", id);
            var data = DataBaseConnection.getInstance().readData(queryUndoStock);
            int InventoryId = 0, Quantity = 0;
            data.Read();
            InventoryId = (int)data.GetValue(0);
            Quantity = (int)data.GetValue(1);
            string deleteQuery = string.Format("delete Sells where InventoryId='{0}'", id);
            DataBaseConnection.getInstance().executeQuery(deleteQuery);
            string updateStockQuery = string.Format("update Inventory set Quantity+='{0}' where Id='{1}'", Quantity, InventoryId);
            DataBaseConnection.getInstance().executeQuery(updateStockQuery);
            return RedirectToAction("AddSales");
            //return View();
        }
        public ActionResult EditSale(int id)
        {

            CreatePOS pos = new CreatePOS();
            string queryObject = "select Medicine.Name,Medicine.Category,Sells.Quantity from Sells join Inventory on Inventory.Id=Sells.InventoryId join Medicine on Medicine.Id=Inventory.MedicineId and Sells.InventoryId='"+id+"'";
            var editObject = DataBaseConnection.getInstance().readData(queryObject);
            editObject.Read();
            pos.sale.Name = editObject.GetValue(0).ToString();
            pos.sale.Category = editObject.GetValue(1).ToString();
            pos.sale.Quantity = (int)editObject.GetValue(2);
            prevSellQuantity= (int)editObject.GetValue(2);
            string query = "select Sells.InventoryId,Medicine.Name,Medicine.Category,Medicine.Price,Sells.Quantity,Sells.Discount,Sells.Subtotal from Sells join Inventory on Inventory.Id=Sells.InventoryId join Medicine on Medicine.Id=Inventory.MedicineId";
            var data = DataBaseConnection.getInstance().readData(query);
            while (data.Read())
            {
                Sales sale = new Sales();
                sale.InventoryId = (int)data.GetValue(0);
                sale.Name = data.GetValue(1).ToString();
                sale.Category = data.GetValue(2).ToString();
                sale.Price = (int)data.GetValue(3);
                sale.Quantity = (int)data.GetValue(4);
                sale.Discount = float.Parse(data.GetValue(5).ToString());
                sale.Subtotal = float.Parse(data.GetValue(6).ToString());
                pos.listSale.Add(sale);
            }
   
            ViewBag.EditMode = "EditMode";
         //   ViewBag.CategoryList = new SelectList(MedicineCategories, "Category", "Category");
            return View("AddSales",pos);
            //return View();
        }
        [HttpPost]
        public ActionResult EditSale(int id,CreatePOS POS)
        {
            int quantity = prevSellQuantity - POS.sale.Quantity;
            string query = string.Format("update Inventory set Quantity+='{0}' where Id='{1}'", quantity, id);
            DataBaseConnection.getInstance().executeQuery(query);
            string querySell = string.Format("update Sells set Quantity='{0}' where InventoryId='{1}'", POS.sale.Quantity, id);
            DataBaseConnection.getInstance().executeQuery(querySell);
            return RedirectToAction("AddSales");
        }
        //autocomplete
        public JsonResult GetSearchValue(string search)
        {
            //   List<Stock> allsearch = _db.Stocks.Where(x => x.Name.StartsWith(search)).ToList();
            string fetchName = //string.Format("select Category from Medicine where Name Like ''{0}'%' ", search);
                " select Name from Medicine where Name like '"+search+"%'";//     Insert into [Medicine] Valu'" + s.med.Name.ToString() + "','" + s.med.Formula.ToString() + "','" + s.med.Category.ToString() + "','" + Convert.ToInt32(s.med.Price) + "' )";
            var data = DataBaseConnection.getInstance().readData(fetchName);

            string Name = null;
            while (data.Read())
            {
                Name = data.GetValue(0).ToString();
                if (!allsearch.Contains(Name))
                {
                    allsearch.Add(Name);
                }
            }
            return new JsonResult { Data = allsearch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetStateList(string CountryId)
        {
            string fetchName = string.Format("select * from Medicine where Name='{0}'", CountryId);
            var data = DataBaseConnection.getInstance().readData(fetchName);
            //   List<string> StateList = null;
            string Categ = "";
            while (data.Read())
            {
                Categ = data.GetValue(3).ToString();
                StateList.Add(Categ);
            }

  

            //   _db.Configuration.ProxyCreationEnabled = false;
            //   List<string> StateList = _db.Stocks.Where(x => x.Name == CountryId).ToList();
            return Json(StateList, JsonRequestBehavior.AllowGet);

        }
        
          


    }
}