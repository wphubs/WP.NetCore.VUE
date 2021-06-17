using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WP.NetCore.API.AuthHelper;
using WP.NetCore.Common;
using WP.NetCore.IServices;
using WP.NetCore.Model;
using WP.NetCore.Model.Dto;

namespace WP.NetCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public string Get() 
        {
            int a = 1;
            int b = 0;
            var c = a/b;
            return "";
        }


        [HttpPost]
        public async Task<ResponseResult> Post(LoginDto objLogin)
        {
            var objUserInfo = await userService.CheckUserAsync(objLogin.UserName,objLogin.PassWord);
            if (objUserInfo != null)
            {
                TokenModelJwt tokenModel = new TokenModelJwt
                {
                    Id = objUserInfo.Id,
                    Role = String.Join(',', objUserInfo.UserRoles.Select(x => x.RoleId).ToList()),
                    UserName = objUserInfo.UserName,
                    Name = objUserInfo.Name,
                    Avatar = "https://github.githubassets.com/pinned-octocat.svg"

                };
                var strJWT = JwtHelper.IssueJwt(tokenModel);
                return new ResponseResult().Success(new { token = strJWT.token,exp=strJWT.expTime });
            }
            else
            {
                return new ResponseResult().Error("用户信息不存在");
            }
        }
    }
}
