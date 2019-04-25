using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace inventory_store.Models
{
    public class list_staff
    {
        public List<Addstaff> list = new List<Addstaff>();
        public Addstaff staff { get; set; }
    }
}