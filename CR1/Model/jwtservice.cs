using Microsoft.CodeAnalysis.Classification;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace CR1.Model
{
    public class jwtservice
    {
        

        public string SecretKey { get; set; }       
        public int TokenDuration { get; set; }
        // private readonly IConfiguration _config;

        public jwtservice() { }
           // config = _config;
           // this.SecretKey = config.GetSection("jwtconfig").GetSection("key").Value;
           // this.TokenDuration = Int32.Parse(config.GetSection("jwtconfig").GetSection("Duration").Value);

        

        public string GenerateToken( string email, string password,string role) {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MNU66iBl3T5rh6H52iabsar"));
            var signature = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("email",email),
                new Claim("password",password),
                new Claim("role",role)
        };
            var jwtToken = new JwtSecurityToken(
                issuer: "http://localhost:15996",
                audience: "http://localhost:15996",
                claims,
               // expires:DateTime.Now.AddMinutes(60),
                signingCredentials:signature
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
          
                }
    }
}
