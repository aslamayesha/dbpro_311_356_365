using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace inventory_store.Models
{
    public class Inventory
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public int MedicineId { get; set; }
        [Required]
        public int NumberofPacks { get; set; }
        [Required]
        public int Quantity { get; set; }

        [Required]
        public DateTime ManufactureDate { get; set; }
        [Required]
        public DateTime ExpiryDate { get; set; }


    }
}