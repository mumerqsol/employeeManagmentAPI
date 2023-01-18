using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Model
{
    public class JwtService
    {
        public string SecretKey { get; set; }
        public int TokenDuration { get; set; }

        public readonly IConfiguration config;

        public JwtService(IConfiguration configuration)
        {
            this.config = configuration;
            this.SecretKey = config.GetSection("JwtConfig").GetSection("Key").Value;
            this.TokenDuration = Convert.ToInt32(config.GetSection("JwtConfig").GetSection("Duration").Value);
        }


        public string GenerateToken(string id, string userid, string usertype, string username, string password,  string isactive)
        {
            //this.id = id;
            //this.userid = userid;
            //this.usertypeid = usertypeid;
            //this.username = username;
            //this.password = password;
            //this.cnfpassword = cnfpassword;
            //this.isactive = isactive;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.SecretKey));
            var signature = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var payload = new[]
            {
                new Claim("id",id),
                new Claim("userid",userid),
                new Claim("usertype",usertype),
                new Claim("username",username),
                new Claim("password",password),
                new Claim("isactive",isactive),
        };

            var jwtToken = new JwtSecurityToken(

                issuer: "localhost",
                audience: "",
                claims:payload,
                expires:DateTime.Now.AddMinutes(TokenDuration),
                signingCredentials:signature
            
                );

            string Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return Token;
        }
    }
}
