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
    [Authorize]
    public class RoleController : BaseController
    {
        private readonly IRoleService roleService;
        private readonly IMapper mapper;

        public RoleController(IRoleService roleService,IMapper mapper) 
        {
            this.roleService = roleService;
            this.mapper = mapper;
        }

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPage")]
        public async Task<ResponseResult> GetPage(int pageIndex, int pageSize)
        {
            var listUser = await roleService.GetPageListAsync(pageIndex, pageSize);
            var res= new ResponseResult().Success(listUser);
            return res;
        }

        [HttpGet]
        public async Task<ResponseResult> Get()
        {
            var listUser = await roleService.GetAllAsync();
            return new ResponseResult().Success(listUser);
        }


        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseResult> Post([FromBody] AddRoleDto userDto)
        {
            var objUser = mapper.Map<Role>(userDto);
            objUser.CreateBy = GetToken().Id;
          
            await roleService.AddAsync(objUser);
            return new ResponseResult().Success();
        }

        /// <summary>
        /// 设置角色菜单
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPost("SetRoleMenu")]
        public async Task<ResponseResult> SetRoleMenu([FromBody] AddRoleMenuDto userDto)
        {
            userDto.CreateBy = GetToken().Id;
            await roleService.SetRoleMenu(userDto);
            return new ResponseResult().Success();
        }

        /// <summary>
        /// 获取角色菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet("GetRoleMenu")]
        public async Task<ResponseResult> GetRoleMenu(long roleId)
        {
            var listMenu = await roleService.GetRoleMenu(roleId);
            return new ResponseResult().Success(listMenu);
        }




        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResponseResult> Put([FromBody] UpdateRoleDto userDto)
        {
            var objUser = mapper.Map<Role>(userDto);
            objUser.ModifyBy = GetToken().Id;
            await roleService.UpdateAsync(objUser);
            return new ResponseResult().Success();
        }

        /// <summary>
        /// 删除角色
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
            await roleService.DeleteAsync(Id);
            return new ResponseResult().Success();
        }
    }
}
