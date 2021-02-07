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
    public class LoginController : ControllerBase
    {
        private readonly IUserService userService;

        public LoginController(IUserService userService)
        {
            this.userService = userService;
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
                    Role = "系统管理员",
                    UserName = objUserInfo.UserName,
                    Name = objUserInfo.Name,
                    Avatar = "https://github.githubassets.com/pinned-octocat.svg"

                };
                var strJWT = JwtHelper.IssueJwt(tokenModel);
                return new ResponseResult().Success(new { token = strJWT });
            }
            else
            {
                return new ResponseResult().Error("用户信息不存在");
            }
        }
    }
}
