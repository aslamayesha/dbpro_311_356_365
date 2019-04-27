using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace inventory_store.Models
{
    public class Salary
    {
        public int med_id { get; set; }
       public int SalaryAmount { get; set; }
      public  int bonus { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Month { get; set; }
    }
}