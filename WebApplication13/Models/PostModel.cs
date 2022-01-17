using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication13.Entities;

namespace WebApplication13.Entities
{
    public class PostModel
    {
        private readonly ClinicDBContext _dBContext;
        public PostModel(ClinicDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public IQueryable<Post> getAllPosts()
        {
            return _dBContext.Posts.Where(pst => pst.PostedOn == true).OrderByDescending(pst => pst.DateOfPublished);
        }

        public List<Post> GetAllPostsByCategory(Category currCategory)
        {
            return _dBContext.Posts.Where(pst => pst.PostedOn == true && pst.CategoryId == currCategory.Id).OrderByDescending(pst => pst.DateOfPublished).ToList();
        }

        public Post GetOnePost(string slug)
        {
            return _dBContext.Posts.SingleOrDefault(pst => pst.PostedOn == true && pst.UrlSlug == slug);
        }
    }
}
