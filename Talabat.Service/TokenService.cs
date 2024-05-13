using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services;

namespace Talabat.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;

        public TokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<string> CreateToken(AppUser User, UserManager<AppUser> userManager)
        {
            //1-Private Calims [User-Defined]
            var AuthClaims = new List<Claim>()
           {
               new Claim(ClaimTypes.GivenName , User.DisplayName),
               new Claim(ClaimTypes.Email , User.Email),
           };

            // Add Role Claims
            var UserRole = await userManager.GetRolesAsync(User);
            foreach (var role in UserRole)
            {
                AuthClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            //Key
            var AuthKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));

            // Token Object
            var Token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidateIssure"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(double.Parse(configuration["JWT:DurationInDays"])),
                claims: AuthClaims,
                signingCredentials: new SigningCredentials(AuthKey, SecurityAlgorithms.HmacSha256Signature)
                );

            // Write token 

            return new JwtSecurityTokenHandler().WriteToken(Token);

        }
    }
}
