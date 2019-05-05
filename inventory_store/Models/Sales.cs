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
  
        [Required]
        public int SaleId { get; set; }
        [Required]
        public int InventoryId { get; set; }
        
        [Required]
        
        public string Name { get; set; }
  
        [Required]
     
        public string Category { get; set; }
        [Required]
        [Range(typeof(int), "1", "1000", ErrorMessage = "Quantity must be greater than 0")]   
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