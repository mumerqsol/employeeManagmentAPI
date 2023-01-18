using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Model;
using EmployeeManagement.Model.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {

        public readonly HCMContext HCMContext;
        public readonly IConfiguration config;

        public UserController(IConfiguration configuration, HCMContext _HCMContext)
        {

            HCMContext = _HCMContext;
            this.config = configuration;

        }

        [HttpGet]
        public JsonResult GetAllUsers()
        {

            var users = HCMContext.usermst.ToList();

            return new JsonResult(users);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> CreateUser(UserMaster userMaster) {

            if (ModelState.IsValid)
            {
                if (userMaster.id == 0)
                {
                    HCMContext.usermst.Add(userMaster);
                    await HCMContext.SaveChangesAsync();
                }
                else
                {
                    HCMContext.Entry(userMaster).State = EntityState.Modified;
                    await HCMContext.SaveChangesAsync();
                }
            }
            return new JsonResult(userMaster);

        }

        [HttpDelete("{id}")]
        public JsonResult DeleteUser(int id)
        {
            if (id > 0)
            {
                UserMaster userMaster = HCMContext.usermst.Find(id);
                HCMContext.usermst.Remove(userMaster);
                HCMContext.SaveChanges();
            }
            return new JsonResult("User has been deleted...!");
        }


        // User Login API
        
        [AllowAnonymous]
        [HttpPost]
        [Route("userlogin")]
        public JsonResult UserLogin(UserLogin userLoging)
        {

            if (userLoging.userid != "" && userLoging.password != "")
            {
                UserMaster _userMaster = HCMContext.usermst.Where(o => o.userid == userLoging.userid && o.password == userLoging.password).FirstOrDefault<UserMaster>();
                if (_userMaster != null)
                {
                    string Token = new JwtService(config).GenerateToken(

                        _userMaster.id.ToString(),
                        _userMaster.userid,
                        _userMaster.usertypeid.ToString(),
                        _userMaster.username,
                        _userMaster.password,
                        _userMaster.isactive.ToString()

                        ); ;
                    return new JsonResult(Token);
                }
                else
                {
                    return new JsonResult("Invalid User Id Or Password");
                }
            }
            return new JsonResult("User Id or Password is not Valid..!");
        }

    }
}