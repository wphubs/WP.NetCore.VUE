using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WP.NetCore.API.AuthHelper;
using WP.NetCore.IServices;
using WP.NetCore.Model;
using WP.NetCore.Model.Dto.Menu;
using WP.NetCore.Model.EntityModel;

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
        /// 新增菜单
        /// </summary>
        /// <param name="menuDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseResult> Post([FromBody] AddMenuDto menuDto)
        {
            var objMenu = mapper.Map<Menu>(menuDto);
            objMenu.CreateBy = GetToken().Id;

            await menuService.AddAsync(objMenu);
            return new ResponseResult().Success();
        }

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="menuDto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResponseResult> Put([FromBody] UpdateMenuDto menuDto)
        {
            var objMenu = mapper.Map<Menu>(menuDto);
            objMenu.ModifyBy = GetToken().Id;

            await menuService.UpdateAsync(objMenu);
            return new ResponseResult().Success();
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ResponseResult> Delete(long Id)
        {
            if (default(long) == Id)
            {
                return new ResponseResult().Error("ID不能为空");
            }
            await menuService.DeleteAsync(Id);
            return new ResponseResult().Success();
        }


        /// <summary>
        /// 获取所有页面菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetPageMenuList")]
        public async Task<ResponseResult> GetPageMenuList()
        {
            var list = await menuService.GetPageMenuListAsync();
            return new ResponseResult().Success(list); ;
        }


        /// <summary>
        /// 获取菜单信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResponseResult> Get()
        {
            var list =await menuService.GetMenuListAsync();
            return new ResponseResult().Success(list); ;
        }



        /// <summary>
        /// 根据角色获取菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetRoleRouter")]
        public async Task<ResponseResult> GetRoleRouter()
        {
            var tokenInfo = JwtHelper.TokenInfo(User);
            var listMenu = await menuService.GetRoleMenuListAsync();
            return new ResponseResult().Success(listMenu);
        }


    }
}
