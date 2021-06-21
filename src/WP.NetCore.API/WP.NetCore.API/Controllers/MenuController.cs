using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
using WP.NetCore.Model.ViewModel;

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
        [Authorize("Permission")]
        public async Task<ActionResult> Post([FromBody] AddMenuDto menuDto)
        {
            var objMenu = mapper.Map<Menu>(menuDto);
            objMenu.CreateBy = GetToken().Id;
            await menuService.AddAsync(objMenu);
            return Ok();
        }

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="menuDto"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize("Permission")]
        public async Task<ActionResult> Put([FromBody] UpdateMenuDto menuDto)
        {
            if (await menuService.FirstNoTrackingAsync(menuDto.Id) == null)
            {
                return NotFound("ID不存在");
            }
            var objMenu = mapper.Map<Menu>(menuDto);
            objMenu.ModifyBy = GetToken().Id;
            await menuService.UpdateAsync(objMenu);
            return NoContent();
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize("Permission")]
        public async Task<ActionResult> Delete([FromQuery] long Id)
        {
            if (default(long) == Id)
            {
                return BadRequest("ID不能为空");
            }
            if (await menuService.FirstNoTrackingAsync(Id) == null)
            {
                return NotFound("ID不存在");
            }
            await menuService.DeleteAsync(Id);
            return NoContent();
        }


        /// <summary>
        /// 获取所有页面菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize("Permission")]
        public async Task<ActionResult<List<PageMenuViewModel>>> Get()
        {
            var list = await menuService.GetPageMenuListAsync();
            return Ok(list); ;
        }


        /// <summary>
        /// 获取菜单信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetMenuTree")]
        [Authorize("Permission")]
        public async Task<ActionResult<List<MenuViewModel>>> GetMenuTree()
        {
            var list =await menuService.GetMenuListAsync();
            return Ok(list); ;
        }



        /// <summary>
        /// 根据角色获取菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetRoleRouter")]
        [Authorize]
        public async Task<ActionResult<List<RouterViewModel>>> GetRoleRouter()
        {
            var tokenInfo = JwtHelper.TokenInfo(User);
            var listMenu = await menuService.GetRoleMenuListAsync(tokenInfo.Role.Split(',').Select(x => Convert.ToInt64(x)).ToList());
            return Ok(listMenu);
        }


    }
}
