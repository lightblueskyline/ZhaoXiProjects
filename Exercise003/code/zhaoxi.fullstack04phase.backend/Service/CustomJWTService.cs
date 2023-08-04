using Interface;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Model.DTO.User;
using Model.Other;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service
{
    public class CustomJWTService : ICustomJWTService
    {
        private readonly JwtTokenOption _JwtTokenOption;

        public CustomJWTService(IOptionsMonitor<JwtTokenOption> optionsMonitor)
        {
            _JwtTokenOption = optionsMonitor.CurrentValue;
        }

        /// <summary>
        /// 获取 Token
        /// </summary>
        /// <param name="userResponse"></param>
        /// <returns></returns>
        public async Task<string> GetToken(UserResponse userResponse)
        {
            var result = await Task.Run(() =>
            {
                // 有效载荷，避免敏感信息
                var claims = new[]
                {
                    new Claim("Id",userResponse.Id),
                    new Claim("NickName",userResponse.NickName),
                    new Claim("Name",userResponse.Name),
                    new Claim("UserType",userResponse.UserType.ToString()),
                    new Claim("Image",userResponse.Image==null?"":userResponse.Image),
                };
                // 需要加密： 以加密 KEY
                // Microsoft.IdentityModel.Tokens
                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JwtTokenOption.SecurityKey));
                //
                SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                // System.IdentityModel.Tokens.Jwt
                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: _JwtTokenOption.Issuer,
                    audience: _JwtTokenOption.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(5), // Token 有效期
                    notBefore: null,
                    signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            });

            return result;
        }
    }
}
