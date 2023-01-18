using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Model.EmployeeHiring
{
    public class EmpDesignation
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage ="empid is required")]
        public int empid { get; set; }

        [Required(ErrorMessage ="Designation is Required")]
        public int desigid { get; set; }

        [Required(ErrorMessage ="Valid From Is Required")]
        public DateTime validfrom { get; set; }

        [Required(ErrorMessage ="Valid To Is Required")]
        public DateTime validto { get; set; }

        [Required(ErrorMessage ="Isactive is Required")]
        public bool isactive { get; set; }
    }
}
