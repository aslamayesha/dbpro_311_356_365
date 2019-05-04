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
       
        public class UniqueEmail : ValidationAttribute
        {
            public string constr = "Data Source=FINE\\AYESHASLAM;Initial Catalog=DB1;Integrated Security=True";
            DataBaseConnection conD = DataBaseConnection.getInstance();
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                SqlConnection con = conD.getConnection();
                if (con.State == System.Data.ConnectionState.Open)
                {
                    var model = (Addstaff)validationContext.ObjectInstance;
                    SqlDataAdapter sda1 = new SqlDataAdapter("Select * From Staff", con);
                    DataTable TT = new DataTable();
                    sda1.Fill(TT);
                    foreach (DataRow dr in TT.Rows)  // dt is a DataTable
                    {
                       if(dr["Email"].ToString()==model.Email)
                        {
                            return new ValidationResult("Email already Exists");
                        }
                    }
                }
                    return ValidationResult.Success;
            }

        }
       public int? Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Name length can't be more than 50.")]
        public  string Username { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Email length can't be more than 50.")]
        [UniqueEmail(ErrorMessage ="email already exists")]
        [DataType(DataType.EmailAddress, ErrorMessage = "incorrect Email")]
        public string Email { get; set; }
        [Required]
        [StringLength(11, ErrorMessage = "Name length can't be more than 50.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "UPRN must be numeric")]
        public  string Contact { get; set; }
        [StringLength(50, ErrorMessage = "Address length can't be more than 50.")]
        [Required]
        public  string Address { get; set; }
    }
}