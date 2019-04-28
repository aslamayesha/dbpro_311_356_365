using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace inventory_store.Models
{
    public class CreatePOS
    {
        public Sales sale {get; set; }
        public List<Sales> listSale { get; set; }
        public CreatePOS()
        {
            sale = new Sales();
            listSale = new List<Sales>();
        }

    }
}