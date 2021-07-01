using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WP.NetCore.IServices;
using WP.NetCore.Model;
using WP.NetCore.Model.Dto.Menu;
using WP.NetCore.Model.Dto.Role;
using WP.NetCore.Model.EntityModel;

namespace WP.NetCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Permission")]
    public class RoleController : BaseController
    {
        private readonly IRoleService roleService;
        private readonly IMapper mapper;
        private readonly IMenuService menuService;

        public RoleController(IRoleService roleService,IMapper mapper, IMenuService menuService) 
        {
            this.roleService = roleService;
            this.mapper = mapper;
            this.menuService = menuService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> Get()
        {
            var listUser = await roleService.GetAllAsync();
            return Ok(listUser);
        }



        /// <summary>
        /// 获取角色权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPermission")]
        public async Task<ActionResult<List<object>>> GetPermission([FromQuery] long roleId)
        {
            var listMenu = await menuService.GetMenuListAsync();
            var listRoleMenu = await roleService.GetRoleMenu(roleId);
            List<object> list = new List<object>();
            list.Add(listMenu);
            list.Add(listRoleMenu);
            return Ok(list);
        }


        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AddRoleDto userDto)
        {
            var objUser = mapper.Map<Role>(userDto);
            objUser.CreateBy = GetToken().Id;
            await roleService.AddAsync(objUser);
            return Ok();
        }

        /// <summary>
        /// 设置角色权限
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPost("SetPermission")]
        public async Task<ActionResult> SetPermission([FromBody] AddRoleMenuDto userDto)
        {
            userDto.CreateBy = GetToken().Id;
            await roleService.SetRoleMenu(userDto);
            return NoContent();
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UpdateRoleDto userDto)
        {
            if (await roleService.FirstNoTrackingAsync(userDto.Id) == null)
            {
                return NotFound("ID不存在");
            }
            var objUser = mapper.Map<Role>(userDto);
            objUser.ModifyBy = GetToken().Id;
            await roleService.UpdateAsync(objUser);
            return NoContent();
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] long Id)
        {
            if (default(long) == Id)
            {
                return BadRequest("ID不能为空");
            }
            if (await roleService.FirstNoTrackingAsync(Id) == null)
            {
                return NotFound("ID不存在");
            }
            await roleService.DeleteAsync(Id);
            return NoContent(); ;
        }
    }
}
