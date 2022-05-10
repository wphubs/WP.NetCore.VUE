using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WP.NetCore.Common;
using WP.NetCore.Extensions;
using WP.NetCore.IServices;
using WP.NetCore.Model;
using WP.NetCore.Model.Dto;
using WP.NetCore.Model.Dto.User;

namespace WP.NetCore.API.Controllers.v2
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

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="objLogin"></param>
        /// <returns></returns>
        [HttpPost]
        [VersionRoute(ApiVersions.v2)]
        public async Task<ActionResult<string>> Post([FromBody] LoginV2Dto objLogin)
        {
            var objUserInfo = await userService.CheckUserAsync(objLogin.username,objLogin.password);
            if (objUserInfo != null)
            {
                TokenModelJwt tokenModel = new TokenModelJwt
                {
                    Id = objUserInfo.Id,
                    Role = String.Join(',', objUserInfo.UserRoles.Select(x => x.RoleId).ToList()),
                    UserName = objUserInfo.UserName,
                    Name = objUserInfo.Name,
                    Avatar = ""
                };
                var strJWT = JwtHelper.IssueJwt(tokenModel);
                return Ok(new { token = strJWT.token });
            }
            else
            {
                return BadRequest("用户信息不存在");
            }
        }


       

    }
}
