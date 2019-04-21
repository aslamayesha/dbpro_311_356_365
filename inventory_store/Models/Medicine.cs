using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace inventory_store.Models
{
    public class Medicine
    {[Required]
        public string Name { get; set; }
        [Required]
        public string Formula { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public int  Price { get; set; }
    }
}