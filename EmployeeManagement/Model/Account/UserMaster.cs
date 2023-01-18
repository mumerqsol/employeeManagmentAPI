using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Model.Account
{
    public class UserMaster
    {
        

        [Key]
        public int id { get; set; }

        [Required(ErrorMessage ="User Id cannot be empty")]
        public string userid { get; set; }

        [Required(ErrorMessage = "Please define User Type")]
        public int usertypeid { get; set; }

        [Required(ErrorMessage = "User Name cannot be empty")]
        public string username { get; set; }

        [Required(ErrorMessage = "Password cannot be empty")]
        public string password { get; set; }

        [NotMapped]
        public string cnfpassword { get; set; }

        [Required(ErrorMessage = "Required")]
        public bool isactive { get; set; }

    }
}
