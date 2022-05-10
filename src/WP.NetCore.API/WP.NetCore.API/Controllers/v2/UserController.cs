using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WP.NetCore.Extensions;
using WP.NetCore.IServices;
using WP.NetCore.Model;
using WP.NetCore.Model.Dto;
using WP.NetCore.Model.Dto.User;
using WP.NetCore.Model.EntityModel;
using WP.NetCore.Model.ViewModel;

namespace WP.NetCore.API.Controllers.v2
{
    [Route("api/[controller]")]
    [ApiController]
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
        [HttpGet]
        [Authorize]
        [VersionRoute(ApiVersions.v2, "GetUserInfo")]
        public ActionResult<TokenModelJwt> GetUserInfo() 
        {
            var tokenInfo = JwtHelper.TokenInfo(User);
            return Ok(new { userId =tokenInfo.Id, roles =new List<string>(), username =tokenInfo.UserName, realName =tokenInfo.Name, avatar ="",desc=""});
        }

       

    }
}
