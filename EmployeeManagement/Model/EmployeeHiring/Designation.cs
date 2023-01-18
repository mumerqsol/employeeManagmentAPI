using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Model.EmployeeHiring
{
    public class Designation
    {
        [Key]
        public int desigid { get; set; }

        [Required]
        public string designame { get; set; }

        [Required]
        public bool isactive { get; set; }

    }
}
