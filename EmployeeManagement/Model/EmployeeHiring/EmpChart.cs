using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Model.EmployeeHiring
{
    [Keyless]
    public class EmpChart
    {

        public int chartid { get; set; }

        public int chartindex { get; set; }

        public string chartname { get; set; }

        public string charttype{ get; set; }

        public string labelquery { get; set; }

        public int did { get; set; }

        public int chartdid { get; set; }

        public string label { get; set; }

        public string dataquery { get; set; }

        public string bgcolor { get; set; }

        public string brdcolor { get; set; }

        public bool isfill { get; set; }

    }
}
