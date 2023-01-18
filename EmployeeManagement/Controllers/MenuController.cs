using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Model;
using EmployeeManagement.Model.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        public readonly HCMContext hCMContext;
        public readonly IConfiguration configuration;

        public MenuController(  HCMContext hCMContext,IConfiguration configuration)
        {
            this.configuration = configuration;
            this.hCMContext = hCMContext;
        }


        [HttpGet]
        public async Task<JsonResult> GetAllMenus() {

            List<MenuMaster> MenuList = await hCMContext.UserMenu.ToListAsync();

            return new JsonResult(MenuList);

        }

        [HttpGet]
        [Route("menujson")]
        public async Task<JsonResult> GetJsonMenu()
        {

            List<MenuMaster> MenuList = await hCMContext.UserMenu.ToListAsync();
            List<JsonMenu> jsonMenu = null;

            if (MenuList != null)
            {
                jsonMenu = new List<JsonMenu>();

                var menuGroupList = MenuList.Where(o => Convert.ToInt32(o.cid) == 0).ToList();


                foreach (MenuMaster item in menuGroupList)
                {
                    JsonMenu menu = new JsonMenu();
                    menu.pid = item.pid;
                    menu.cid = item.cid;
                    menu.uid = item.uid;
                    menu.userid = item.userid;
                    menu.formname = item.formname;
                    menu.url = item.url;
                    menu.faicon = item.faicon;
                    menu.status = item.status;
                    menu.data = new List<MenuMaster>();

                    foreach (MenuMaster itemDetail in MenuList)
                    {
                        if (menu.pid == itemDetail.cid)
                        {
                            menu.data.Add(itemDetail);
                        }
                    }

                    jsonMenu.Add(menu);

                }
            }

            return new JsonResult(jsonMenu);
        }
    }
}