using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Model;
using EmployeeManagement.Model.EmployeeHiring;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpDesignationController : ControllerBase
    {
        public readonly IConfiguration configuration;
        public readonly HCMContext hCMContext;

        public EmpDesignationController(IConfiguration configuration,HCMContext hCMContext ) 
        {
            this.configuration = configuration;
            this.hCMContext = hCMContext;
        }


        [HttpPost]
        public async Task<JsonResult> AddDesignation(EmpDesignation designation )
        {

            if (ModelState.IsValid)
            {
                if (designation.id == 0)
                {
                    hCMContext.empdesignation.Add(designation);
                    await hCMContext.SaveChangesAsync();
                }
                else
                {
                    hCMContext.Entry(designation).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    await hCMContext.SaveChangesAsync();
                }
            } 

            return new JsonResult(designation);
        }

        [HttpGet]
        public async Task<JsonResult> GetAllEmpDesig()
        {

            List<EmpDesignation> empDesigList = await hCMContext.empdesignation.ToListAsync();

            return new JsonResult(empDesigList);
        }

        [HttpDelete("/id")]
        public async Task<JsonResult> DeleteEmpDesig(int id)
        {

            if (id > 0)
            {
                EmpDesignation empDesig = await hCMContext.empdesignation.FindAsync(id);
                hCMContext.empdesignation.Remove(empDesig);

                return new JsonResult("Your record has been deleted.");
            }

            return new JsonResult("Something went wrong.");
        
        }

    }
}