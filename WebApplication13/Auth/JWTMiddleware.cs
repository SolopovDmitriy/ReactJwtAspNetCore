using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication13.Entities;

namespace WebApplication13.Auth
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;
        // private static ClinicDBContext _dBContext; // old
        //private readonly ClinicDBContext _dBContext; // new


        // old
        //public JWTMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        //{
        //    _next = next;
        //    _appSettings = appSettings.Value;
        //    _dBContext = new ClinicDBContext(new DbContextOptions<ClinicDBContext>());
        //}

        public JWTMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {           
            _next = next;
            _appSettings = appSettings.Value;
        }


        // old
        public async Task Invoke(HttpContext context, ClinicDBContext dBContext,  IAcccountService acccountService)
        {
           
            string requestToken = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();  // "Bearer eyJhbGciOiJI...." Bearer - предъявитель---------
            if (requestToken != null) //пользователь типа авторизирован
            {
                attachUserToContext(dBContext, context, acccountService, requestToken);
            }
            await _next(context);
        }


        private void attachUserToContext(ClinicDBContext _dBContext, HttpContext context, 
            IAcccountService acccountService, string requestToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = Encoding.UTF8.GetBytes(_appSettings.Secret);

            tokenHandler.ValidateToken(requestToken, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                ValidateIssuer = true,
                // ValidIssuer = configuration["JwtAuthentication:Issuer"],
                ValidIssuer = "Hello", // --------------------------------------------------
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            JwtSecurityToken jwtToken = (JwtSecurityToken)validatedToken;           //тот токен который пришел от пользователя
            
            var userId = jwtToken.Claims.FirstOrDefault(u => u.Type == "Id").Value;
            var userLogin = jwtToken.Claims.FirstOrDefault(u => u.Type == "Login").Value;
            var userEmail = jwtToken.Claims.FirstOrDefault(u => u.Type == "Email").Value;

            var user = _dBContext.Users.SingleOrDefault(
                            u => u.Email == userEmail && u.Login == userLogin && u.Id == int.Parse(userId)
                        );
            context.Items["User"] = user; // string currentUser = HttpContext.User.Identity.Name;
        }
    }
}
