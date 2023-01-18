using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Model.EmployeeHiring
{
    public class Employee
    {
        [Key]
        public int empid { get; set; }

        [Required(ErrorMessage = "Employee Name is Required")]
        public string empname { get; set; }

        [Required(ErrorMessage = "Date of Birth  is Required")]
        public DateTime dob { get; set; }

        [Required(ErrorMessage = "Joining Date  is Required")]
        public DateTime jod { get; set; }

        [Required(ErrorMessage ="Confirm is Required")]
        public DateTime cnfmdate { get; set; }  

        [Required(ErrorMessage ="NIC No. cannot be empty")]
        public string nicno { get; set; }

        [Required(ErrorMessage = "Required")]
        public bool isactive { get; set; }

        public string imagepath { get; set; }

        //[NotMapped]
        //public string designation { get; set; }
    }
}
