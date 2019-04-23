﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace inventory_store.Models
{
    public class Sales
    {
        [Required]
        public int MedicineId { get; set; }
        [Required]
        public int StaffId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Formula { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Quantity { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string Discount { get; set; }
        [Required]
        public string Subtotal { get; set; }
    }

}