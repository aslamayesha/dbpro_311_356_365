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
        [StringLength(50, ErrorMessage = "Name length can't be more than 50.")]
        public string Name { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Formula length can't be more than 50.")]
        public string Formula { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "Category length can't be more than 10.")]
        public string Category { get; set; }
        [Required]
        [Range(1, Int32.MaxValue)]
        public int Price { get; set; }
        [Required]
        public int MedicineId { get; set; }
        [Required]
        [Range(1, Int32.MaxValue)]
        public int MedicinePerPack { get; set; }
        [Required]
        [Range(1, Int32.MaxValue)]
        public int PurchasePricePack { get; set; }
        [Required]
        [Range(1, Int32.MaxValue)]
        public int SellingPriceItem { get; set; }
        [Required]
        [Range(1, Int32.MaxValue)]
        public int ThresholdQuantity { get; set; }


    }
}