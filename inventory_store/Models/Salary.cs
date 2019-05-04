using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace inventory_store.Models
{
    public class Salary
    {
        
        
        public int med_id { get; set; }
            [Range(1, Int32.MaxValue)]
            public int SalaryAmount { get; set; }
            [Range(1, Int32.MaxValue)]
            public int bonus { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Month { get; set; }
    }
}