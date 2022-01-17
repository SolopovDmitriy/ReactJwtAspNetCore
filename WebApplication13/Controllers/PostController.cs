using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication13.Entities;
using WebApp.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly ILogger<PostController> _logger;
        private static ClinicDBContext _dBContext;
        private CategoryModel _categoryModel;
        private PostModel _postModel;
        public PostController(ILogger<PostController> logger)
        {
            _logger = logger;
            _dBContext = new ClinicDBContext(new DbContextOptions<ClinicDBContext>());
            _categoryModel = new CategoryModel(_dBContext);
            _postModel = new PostModel(_dBContext);
        }

        [HttpGet]
        public IEnumerable<Post> Get()
        {
            return _dBContext.Posts.Include(p => p.Category).Where(p => p.PostedOn == true).OrderBy(p => p.DateOfPublished).ToArray();
        }

        [HttpPost]
        public async Task<ActionResult<Post>> Post([FromBody] string postSlug)
        {
            return await _dBContext.Posts.SingleOrDefaultAsync(p => p.UrlSlug == postSlug);
        }


        [HttpGet("{countposts}")]
        public int Get(string countposts)
        {
            return _dBContext.Posts.Count();
        }


            // https://localhost:44394/Post/3/2 
            // page=3 limit=2
        [HttpGet("{page}/{limit}")]
        public IEnumerable<Post> Get(int page, int limit)
        {
            return _dBContext.Posts
                  .Where(p => p.PostedOn == true)
                  .OrderBy(p => p.DateOfPublished)
                  .Skip((page - 1) * limit)
                  .Take(limit).ToArray();
        }
    }
}
