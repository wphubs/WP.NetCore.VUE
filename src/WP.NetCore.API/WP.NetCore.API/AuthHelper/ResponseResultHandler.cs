using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using WP.NetCore.Model;

namespace WP.NetCore.API.AuthHelper
{
    public class ResponseResultHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public ResponseResultHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            throw new NotImplementedException();
        }
        protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.ContentType = "application/json";
            Response.StatusCode = 200;
            await Response.WriteAsync(JsonConvert.SerializeObject(new ResponseResult().Error("很抱歉，您没有操作权限")));
        }

        protected override async Task HandleForbiddenAsync(AuthenticationProperties properties)
        {
            Response.ContentType = "application/json";
            Response.StatusCode = 200;
            await Response.WriteAsync(JsonConvert.SerializeObject(new ResponseResult().Error("很抱歉，您没有操作权限")));
        }
    }
}
