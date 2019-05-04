using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;

namespace inventory_store.Models
{
    public class Addstaff
    {
       
       
           
       public int? Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Name length can't be more than 50.")]
        public  string Username { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Email length can't be more than 50.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "incorrect Email")]
        public string Email { get; set; }
        [Required]
        [StringLength(11, ErrorMessage = "contact length can't be more than 11.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "UPRN must be numeric")]
        public  string Contact { get; set; }
        [StringLength(50, ErrorMessage = "Address length can't be more than 50.")]
        [Required]
        public  string Address { get; set; }
    }
}