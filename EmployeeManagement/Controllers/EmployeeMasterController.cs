using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Model;
using EmployeeManagement.Model.EmployeeHiring;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
/*    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]*/
    public class EmployeeMasterController : ControllerBase
    {
        public readonly HCMContext hCMContext;
        public readonly IConfiguration configuration;

        public EmployeeMasterController(HCMContext hCMContext, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.hCMContext = hCMContext;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> CreateEmployee(EmployeeMaster employee)
        {

            if (ModelState.IsValid)
            {
                if (employee.empid == 0)
                {
                    employee = await hCMContext.InserEmployeeMaster(employee);
                }
                else
                {
                    employee = await hCMContext.InserEmployeeMaster(employee);
                    // await hCMContext.SaveChangesAsync();
                }
            }

            return new JsonResult(employee);
        }


        [HttpGet]
        public async Task<JsonResult> GetEmployeeList()
        {
            //List<Employee> empList = await Task.Run(() => hCMContext.empmaster.ToList());
            List<Employee> empList = await hCMContext.empmaster.ToListAsync();

            return new JsonResult(empList);
        }

        [HttpDelete("{id}")]
        public async Task<JsonResult> DeleteEmployee(int id)
        {

            if (id > 0)
            {
                Employee employee = await hCMContext.empmaster.FindAsync(id);
                hCMContext.empmaster.Remove(employee);
            }

            return new JsonResult("Employee has been deleted ...");
        }

        [HttpGet("{id:int}/details")]
        [Route("getemployee")]
        [AllowAnonymous]
        public async Task<JsonResult> GetEmployee(int id)
        {

            if (id > 0)
            {
                EmployeeMaster employee = await hCMContext.GetEmployeeMaster(id);
                if (employee != null)
                {
                    return new JsonResult(employee);

                }
            }

            return new JsonResult("Record not found!");


        }

    }
}