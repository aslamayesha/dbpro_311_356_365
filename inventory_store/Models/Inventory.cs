using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace inventory_store.Models
{
    public class Inventory
    {
        [Required]
        public int MedicineId { get; set; }
        [Required]
        public int NumberofPacks { get; set; }
        [Required]
        public int MedicinePerPack { get; set; }
        [Required]
        public int PurchasePrice { get; set; }
        [Required]
        public int SellingPrice { get; set; }
        [Required]
        public DateTime ManufactureDate { get; set; }
        [Required]
        public DateTime ExpiryDate { get; set; }
       
      
    }
}