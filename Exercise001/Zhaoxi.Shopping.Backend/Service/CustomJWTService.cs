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
        private readonly JWTTokenOptions _JWTTokenOptions;

        public CustomJWTService(IOptionsMonitor<JWTTokenOptions> options)
        {
            _JWTTokenOptions = options.CurrentValue;
        }

        public async Task<string> GetToken(UserRes user)
        {
            var result = await Task.Run(() =>
            {
                Claim[] claims = new[]
                {
                    new Claim("ID",user.ID),
                    new Claim("Name",user.Name),
                    new Claim("NickName",user.NickName),
                    new Claim("UserType",user.UserType.ToString()),
                    new Claim("Image",user.Image==null?"":user.Image)
                };
                // 需要加密
                // NuGet 包： Microsoft.AspNetCore.Authentication.JwtBearer
                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JWTTokenOptions.SecurityKey));
                SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                // NuGet 包： Microsoft.IdentityModel.Tokens
                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: _JWTTokenOptions.Issuer,
                    audience: _JWTTokenOptions.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(3),
                    // notBefore: null,
                    signingCredentials: credentials);
                string res = new JwtSecurityTokenHandler().WriteToken(token);
                return res;
            });
            return result;
        }
    }
}
