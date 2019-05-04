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
        [Range(1, Int32.MaxValue)]
        public int NumberofPacks { get; set; }
        [Required]
        [Range(1, Int32.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ManufactureDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ExpiryDate { get; set; }


    }
}