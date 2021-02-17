using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WP.NetCore.IServices;
using WP.NetCore.Model;

namespace WP.NetCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : BaseController
    {
        private readonly IMapper mapper;
        private readonly IMenuService menuService;

        public MenuController(IMapper mapper, IMenuService menuService)
        {
            this.mapper = mapper;
            this.menuService = menuService;
        }

        /// <summary>
        /// 根据角色获取菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResponseResult> Get()
        {
            var listMenu = await menuService.GetRoleMenuList();
            return new ResponseResult().Success(listMenu);
        }


    }
}
