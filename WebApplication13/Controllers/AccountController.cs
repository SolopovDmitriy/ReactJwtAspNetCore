using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using WebApplication13.Auth;
using WebApplication13.Entities;

namespace WebApplication13.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private IAcccountService _acccountService;
        private static ClinicDBContext _dBContext;
        public AccountController(IAcccountService acccountService)
        {
            _acccountService = acccountService;
            _dBContext = new ClinicDBContext(new DbContextOptions<ClinicDBContext>());
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenficateRequest authenficateRequest)
        {
            var response = _acccountService.Authentificate(authenficateRequest);
            if(response == null)
            {
                return BadRequest(new { message = "Email or password incorrect"});
            }
            else
            {
                return Ok(response); // попадает ответ назад к клиенту (postman) от сервера
            }
        }

        [Authorize]
        [HttpGet]
        public IEnumerable<User> GetAll()
        {
            string currentUser = HttpContext.User.Identity.Name; //  context.Items["User"] = user;
            return _dBContext.Users.ToArray();
        }


//        {
//    "email": "admin@domain.com",
//    "password": "qwerty"
//}





}
}
