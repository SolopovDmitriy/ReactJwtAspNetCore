using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApplication13.Entities;

namespace WebApplication13.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private static ClinicDBContext _dBContext = new Entities.ClinicDBContext(new DbContextOptions<ClinicDBContext>());

        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(ILogger<EmployeesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return _dBContext.Employees.ToArray();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> Get(int id)
        {
            return await _dBContext.Employees.FindAsync(id);
        }
    }
}
