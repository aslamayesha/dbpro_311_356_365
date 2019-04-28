using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace inventory_store.Models
{
    public class Medicine
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Formula { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int MedicineId { get; set; }
        [Required]
        public int MedicinePerPack { get; set; }
        [Required]
        public int PurchasePricePack { get; set; }
        [Required]
        public int SellingPriceItem { get; set; }

    }
}