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
    //[Authorize]
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

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResponseResult> Get(int pageIndex, int pageSize)
        {
            var listUser = await roleService.GetPageListAsync(pageIndex, pageSize);
            var res= new ResponseResult().Success(listUser);
            return res;
        }



        [HttpGet]
        [Route("GetAll")]
        public async Task<ResponseResult> GetAll()
        {
            var listUser = await roleService.GetAllAsync();
            return new ResponseResult().Success(listUser);
        }



        [HttpGet]
        [Route("GetPermission")]
        public async Task<ResponseResult> GetPermission(long roleId)
        {
            var listMenu = await menuService.GetMenuListAsync();
            var listRoleMenu = await roleService.GetRoleMenu(roleId);
            List<object> list = new List<object>();
            list.Add(listMenu);
            list.Add(listRoleMenu);
            return new ResponseResult().Success(list);
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
        [HttpPost("SetPermission")]
        public async Task<ResponseResult> SetPermission([FromBody] AddRoleMenuDto userDto)
        {
            userDto.CreateBy = GetToken().Id;
            await roleService.SetRoleMenu(userDto);
            return new ResponseResult().Success();
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
