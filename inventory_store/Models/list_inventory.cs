using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace inventory_store.Models
{
    public class list_inventory
    {
        public List<Medicine> l = new List<Medicine>();
        public List<Inventory> llistinv = new List<Inventory>();
        public List<Inventory> list = new List<Inventory>();
        public System.Web.Mvc.SelectList CategoryList { get; set; }
        public Inventory inv { get; set; }
        public Medicine med { get; set; }

    }
}