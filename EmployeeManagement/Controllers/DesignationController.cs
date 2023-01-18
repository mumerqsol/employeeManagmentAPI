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
    public class DesignationController : ControllerBase
    {

        public readonly HCMContext hCMContext;
        public readonly IConfiguration configuration;

        public DesignationController(HCMContext hCMContext, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.hCMContext = hCMContext;
        }

        [HttpGet]
        public async Task<JsonResult> GetAllDesignation()
        {
            List<Designation> desigList  =await this.hCMContext.designation.ToListAsync();

            return new JsonResult(desigList);
        }

    }
}