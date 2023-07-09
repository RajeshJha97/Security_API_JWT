using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Security_Dem2.Utilities
{
    public class utility
    {
        public string CreateToken(List<Claim> claims, DateTime expiresAt,string secretKey)
        {
            //Below are the 2 nuget package required
            //Microsoft.AspNetCore.Authentication.JwtBearer
            //System.IdentityModel.Tokens.Jwt
            var key =Encoding.ASCII.GetBytes(secretKey);
            var jwt = new JwtSecurityToken(
                    claims: claims,
                    notBefore: DateTime.Now,
                    expires:expiresAt,
                    signingCredentials:new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature                        
                        )
                );
            return new JwtSecurityTokenHandler().WriteToken(jwt);                    
        }
    }
}
