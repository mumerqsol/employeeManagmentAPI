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
    public class EmployeeController : ControllerBase
    {
        public readonly HCMContext hCMContext;
        public readonly IConfiguration configuration;

        public EmployeeController(HCMContext hCMContext , IConfiguration configuration )
        {
            this.configuration = configuration;
            this.hCMContext = hCMContext;
        }

        [HttpPost]
        public async Task<JsonResult> CreateEmployee(Employee employee) {

            if (ModelState.IsValid)
            {
                if (employee.empid == 0)
                {

                    hCMContext.empmaster.Add(employee);
                    await hCMContext.SaveChangesAsync();
                }
                else
                {
                    hCMContext.Entry(employee).State = EntityState.Modified;
                    await hCMContext.SaveChangesAsync();
                }
            }

            return new JsonResult(employee);
        }


        [HttpGet]
        public async Task<JsonResult> GetEmployeeList()
        {
            List<Employee> empList = await Task.Run(() => hCMContext.empmaster.ToList());
          
                hCMContext.empmaster.ToListAsync();

            
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
                Employee employee = await hCMContext.empmaster.FindAsync(id);
                if (employee != null)
                {
                    return new JsonResult(employee);

                }
            }

            return new JsonResult("Record not found!");

           
        }



    }
}