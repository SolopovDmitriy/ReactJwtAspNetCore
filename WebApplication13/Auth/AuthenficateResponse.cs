using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication13.Entities;

namespace WebApplication13.Auth
{
    public class AuthenficateResponse
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }


        public AuthenficateResponse(User user, string token)
        {
            Id = user.Id;

            Login = user.Login;

            Email = user.Email;

            Token = token;
        }
    }
}
