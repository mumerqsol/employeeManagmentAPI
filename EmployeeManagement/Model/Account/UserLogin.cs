using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Model.Account
{
    public class UserLogin
    {
        [Required]
        public string userid { get; set; }

        [Required]
        public string password { get; set; }
    }
}
