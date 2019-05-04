using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace inventory_store.Models
{
    public class Sales
    {
        public class UniqueEmail : ValidationAttribute
        {
           // public string constr = "Data Source=FINE\\AYESHASLAM;Initial Catalog=DB1;Integrated Security=True";
            DataBaseConnection conD = DataBaseConnection.getInstance();
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                SqlConnection con = conD.getConnection();
                if (con.State == System.Data.ConnectionState.Open)
                {
                    var model = (Sales)validationContext.ObjectInstance;
                    SqlDataAdapter sda1 = new SqlDataAdapter("Select * From Medicine", con);
                    DataTable TT = new DataTable();
                    sda1.Fill(TT);
                    foreach (DataRow dr in TT.Rows)  // dt is a DataTable
                    {
                        if (dr["Name"].ToString() == model.Name)
                        {
                            return new ValidationResult("Email already Exists");
                        }
                    }
                }
                return ValidationResult.Success;
            }

        }
        [Required]
        public int SaleId { get; set; }
        [Required]
        public int InventoryId { get; set; }
        //[Required]
        //public int StaffId { get; set; }
        [Required]
        
        //  [UniqueEmail(ErrorMessage = "email already exists")]
        public string Name { get; set; }
      //  [Required]
     //   public string Formula { get; set; }
        [Required]
      //  [Remote("CheckOrderExists", "Staff", ErrorMessage = "Order Already has been added", AdditionalFields = "Name")]
        public string Category { get; set; }
        [Required]
       
        public int Quantity { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public float Discount { get; set; }
        [Required]
        public float Subtotal { get; set; }

        //public List<Sales> listSale = null;
        //public Sales()
        //{
        //    listSale = new List<Sales>();
        //}
    }

}