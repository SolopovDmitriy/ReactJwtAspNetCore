using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication13.Entities;
using WebApp.Models;
using Microsoft.Extensions.Logging;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private static ClinicDBContext _dBContext;
        public CategoryController(ILogger<CategoryController> logger)
        {
            _dBContext = new ClinicDBContext(new DbContextOptions<ClinicDBContext>());
        }
        [HttpGet]
        public IEnumerable<Category> Get()
        {
            return _dBContext.Categories.ToArray();
        }

        [HttpGet("{id}")]
        public Category Get(string id)
        {
            return _dBContext.Categories.SingleOrDefault(c => c.UrlSlug.Equals(id));
        }
    }
}
