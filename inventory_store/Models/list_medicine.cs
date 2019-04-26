using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace inventory_store.Models
{
    public class list_medicine
    {
        public List<Medicine> l = new List<Medicine>();
        public Medicine med { get; set; }
    }
}