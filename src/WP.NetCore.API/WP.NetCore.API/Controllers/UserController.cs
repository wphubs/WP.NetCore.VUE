using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WP.NetCore.API.AuthHelper;
using WP.NetCore.API.SwaggerHelper;
using WP.NetCore.IServices;
using WP.NetCore.Model;
using WP.NetCore.Model.Dto;
using WP.NetCore.Model.Dto.User;
using WP.NetCore.Model.EntityModel;
using WP.NetCore.Model.ViewModel;

namespace WP.NetCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : BaseController
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public UserController(IUserService userService, IMapper mapper) 
        {
            this.userService = userService;
            this.mapper = mapper;
        }


        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [Route("GetUserInfo")]
        [HttpGet]
        [Authorize("Permission")]
        public ResponseResult GetUserInfo() 
        {
            var tokenInfo = JwtHelper.TokenInfo(User);
            return new ResponseResult().Success(tokenInfo);
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize("Permission")]
        public async Task<ResponseResult> Get(int pageIndex,int pageSize) 
        {
            //var listUser = await userService.GetPageListAsync(pageIndex,pageSize);
            var listUser = await userService.GetUserListAsync(pageIndex, pageSize);
            return new ResponseResult().Success(listUser);
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseResult> Post([FromBody]AddUserDto userDto)
        {
            var objUser = mapper.Map<User>(userDto);
            objUser.CreateBy = GetToken().Id;
            await userService.AddUserAsync(objUser,userDto.Roles);
            return new ResponseResult().Success();
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResponseResult> Put([FromBody] UpdateUserDto userDto)
        {
            var objUser = mapper.Map<User>(userDto);
  
            objUser.ModifyBy = GetToken().Id;
            await userService.EditUserAsync(objUser, userDto.Roles);
            return new ResponseResult().Success();
        }
        
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ResponseResult> Delete(long Id)
        {
            if (default(long)==Id)
            {
                return new ResponseResult().Error("ID不能为空");
            }
            await userService.DeleteAsync(Id);
            return new ResponseResult().Success();
        }

        //[HttpPost]
        //[Route("SetUserRole")]
        //public async Task<ResponseResult> SetUserRole(SetUserRoleDto userRoleDto)
        //{
        //    await userService.SetUserRoleAsync(userRoleDto.UserId, userRoleDto.RoleId);
        //    return new ResponseResult().Success();
        //}

    }
}
