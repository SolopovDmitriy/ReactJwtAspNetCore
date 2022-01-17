using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using WebApplication13.Entities;

namespace WebApplication13.Auth
{

    //public class MyController
    //{
    //    private readonly ApplicationDbContext _context;

    //    public MyController(ApplicationDbContext context)
    //    {
    //        _context = context;
    //    }
    //}


    public class AccountService : IAcccountService
    {
        private AppSettings _appSettings;
        // private static ClinicDBContext _dBContext; // old
        private readonly ClinicDBContext _dBContext; // new

        // old
        //public AccountService(IOptions<AppSettings> appSettings)
        //{
        //    _dBContext = new ClinicDBContext(new DbContextOptions<ClinicDBContext>());
        //    _appSettings = appSettings.Value;
        //}
        //new
        public AccountService(ClinicDBContext dBContext, IOptions<AppSettings> appSettings)
        {
            _dBContext = dBContext;
            _appSettings = appSettings.Value;
        }


        public AuthenficateResponse Authentificate(AuthenficateRequest authenficateRequest)
        {
            User user = _dBContext.Users.SingleOrDefault(u => u.Email == authenficateRequest.Email && u.Password == authenficateRequest.Password);
            if (user == null) return null;
            return new AuthenficateResponse(user, generateJWTToken(user));
        }

        private string generateJWTToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = Encoding.UTF8.GetBytes(_appSettings.Secret);                   //секретный ключ на основании секретной строки 

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { 
                    new Claim ("Id", user.Id.ToString()),
                    new Claim ("Login", user.Login),
                    new Claim ("Email", user.Email),
                }),
                Expires = DateTime.Now.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature),
                Issuer = "Hello" //издатель для token для дополнительной информации
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


      


    }
}
