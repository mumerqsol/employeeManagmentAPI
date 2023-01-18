using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.DAL;
using EmployeeManagement.Model;
using EmployeeManagement.Model.EmployeeHiring;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json.Linq;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpChartController : ControllerBase
    {

        public readonly HCMContext hCMContext;
        public readonly IConfiguration configuration;

        public EmpChartController(HCMContext hCMContext,IConfiguration configuration)
        {
            this.hCMContext = hCMContext;
            this.configuration = configuration;
        }

        [HttpGet]
        public async Task<JsonResult> GetAllEmpChart()
        {
            List<EmpChart> listChart = await this.hCMContext.empv_empchart.ToListAsync();
            return new JsonResult(listChart);
        }

        [HttpGet]
        [Route("chartlist")]
        public JsonResult GetAllEmpChartList()
        {
                                                             
            List<EmpChart> listChart =  this.hCMContext.empv_empchart.ToList();
            var dt = listChart.Select( i => new { 
                                                 i.chartid
                                                ,i.chartname
                                                ,i.charttype
                                                ,i.chartindex
                                                ,i.labelquery }).Distinct().ToList();

            List<string> chartformate = new List<string>();
            string charform = "";

            foreach (var item in dt)
            {
                var labels = this.hCMContext.ExecuteQuery<Chartlabels<string>>(item.labelquery).ToList();
                string strlabel = "";
                charform = charform +  " {chartname: '"+item.chartname+"', type: '" + item.charttype + "', " +
                                    " data: {  ";
                //" labels:['a','b','c','d','e','f','g','h'], " +

                foreach (var label in labels)
                {
                    strlabel = strlabel + "'" + label.Data + "',";

                }
                strlabel = strlabel.Remove(strlabel.Length - 1);
                charform = charform + " labels:[" + strlabel + "], ";

                charform = charform + " datasets:[ ";

                var dtlchart = listChart.Where(o => o.chartdid == item.chartid).Select(t => t).ToList();

                foreach (var itemdtl in dtlchart)
                {

                    charform = charform + " {   " +
                                    "    label:'" + itemdtl.label + "', ";
                    var data = this.hCMContext.ExecuteQuery<Chartlabels<float>>(itemdtl.dataquery).ToList();
                    string value = "";
                    foreach (var val in data)
                    {
                        value = value + ""+ val.Data.ToString() + ",";

                    }
                    value = value.Remove(value.Length - 1);
                    charform = charform + "   data:["+ value + "], " +
                                    "  backgroundColor: '"+itemdtl.bgcolor+"'," +
                                    "  borderColor: '"+itemdtl.brdcolor+"' " +
                                    " },";


                }

                charform = charform + "]}}";
            }

            JObject json = JObject.Parse(charform);
            return new JsonResult(json);
        }
    }
}