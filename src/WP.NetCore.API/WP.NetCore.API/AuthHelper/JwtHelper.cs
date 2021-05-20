using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WP.NetCore.Common;

namespace WP.NetCore.API.AuthHelper
{
    public class JwtHelper
    {
        /// <summary>
        /// 颁发JWT字符串
        /// </summary>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        public static string IssueJwt(TokenModelJwt tokenModel)
        {
            string iss = Appsettings.app(new string[] { "Audience", "Issuer" });
            string aud = Appsettings.app(new string[] { "Audience", "Audience" });
            string secret = AppSecretConfig.Audience_Secret_String;
            //var claims = new Claim[] //old
            var claims = new List<Claim>
            {
             new Claim(JwtRegisteredClaimNames.Jti, tokenModel.Id.ToString()),
             new Claim(JwtRegisteredClaimNames.Iat, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
             new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
             //过期时间，目前是过期1000秒，可自定义
             new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddHours(24)).ToUnixTimeSeconds()}"),
             new Claim(ClaimTypes.Expiration, DateTime.Now.AddHours(24).ToString()),
             new Claim(ClaimTypes.Role,tokenModel.Role),
             new Claim(JwtRegisteredClaimNames.Iss,iss),
             new Claim(JwtRegisteredClaimNames.Aud,aud),
            };
            //claims.AddRange(tokenModel.Role.Split(',').Select(s => new Claim(ClaimTypes.Role, s)));
            PropertyInfo[] infos = tokenModel.GetType().GetProperties();
            foreach (var info in infos)
            {
                claims.Add(new Claim(info.Name, info.GetValue(tokenModel) + ""));
            }
            //秘钥 (SymmetricSecurityKey 对安全性的要求，密钥的长度太短会报出异常)
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: iss,
                claims: claims,
                
                signingCredentials: creds);

            var jwtHandler = new JwtSecurityTokenHandler();
            var encodedJwt = jwtHandler.WriteToken(jwt);

            return encodedJwt;
        }




        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="jwtStr"></param>
        /// <returns></returns>
        public static TokenModelJwt SerializeJwt(string jwtStr)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);
            object role;
            try
            {
                jwtToken.Payload.TryGetValue(ClaimTypes.Role, out role);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            var tm = new TokenModelJwt
            {
                //Id = jwtToken.Id,
                //Role = role != null ? role.ObjToString() : "",
             
            };
            return tm;
        }


        public static TokenModelJwt TokenInfo(ClaimsPrincipal user)
        {
            TokenModelJwt userBasicInfo = new TokenModelJwt();
            IEnumerable<Claim> claims = user.Claims;
            if (claims != null)
            {
                PropertyInfo[] infos = userBasicInfo.GetType().GetProperties();
                foreach (var info in infos)
                {
                    Claim claim = user.Claims.FirstOrDefault(it => it.Type == info.Name);

                    if (claim != null)
                    {
                        if (info.PropertyType == typeof(int))
                        {
                            int _val = int.Parse(claim.Value == "" ? "0" : claim.Value);
                            info.SetValue(userBasicInfo, _val);
                        }
                        else if (info.PropertyType == typeof(long))
                        {
                            var _val = Convert.ToInt64(claim.Value == "" ? "0" : claim.Value);
                            info.SetValue(userBasicInfo, _val);
                        }
     
                        else
                        {
                            info.SetValue(userBasicInfo, claim.Value);
                        }
                    }
                }
            }
            return userBasicInfo;
        }



    }
    /// <summary>
    /// 令牌
    /// </summary>
    public class TokenModelJwt
    {
        public long Id { get; set; }

        public string  Name { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// 职能
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

    }
}
