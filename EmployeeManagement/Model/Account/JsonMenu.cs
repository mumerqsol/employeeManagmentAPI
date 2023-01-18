using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Model.Account
{
    public class JsonMenu
    {
       
        public int pid { get; set; }

        public int cid { get; set; }

        public int uid { get; set; }

        public string userid { get; set; }

        public string formname { get; set; }

        public string url { get; set; }

        public string faicon { get; set; }

        public string status { get; set; }

        public List<MenuMaster> data { get; set; }
    }
}
