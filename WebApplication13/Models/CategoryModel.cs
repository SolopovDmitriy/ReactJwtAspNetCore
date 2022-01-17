using System.Linq;
using WebApplication13.Entities;

namespace WebApp.Models
{
    public class CategoryModel
    {
        private readonly ClinicDBContext _dBContext;
        public CategoryModel(ClinicDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public IQueryable<Category> GetAllCategories()
        {
            return _dBContext.Categories.OrderBy(emp => emp.Order);
        }
        public Category GetCategory(string urlSlug)
        {
           return _dBContext.Categories.SingleOrDefault(cat => cat.UrlSlug == urlSlug);  //либо null либо Category
        }
    }
}
