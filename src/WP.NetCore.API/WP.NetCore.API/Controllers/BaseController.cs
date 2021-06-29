using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WP.NetCore.Extensions;

namespace WP.NetCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {

        [ApiExplorerSettings(IgnoreApi = true)]
        public TokenModelJwt GetToken()
        {
            return JwtHelper.TokenInfo(User);
        }
    }

}
