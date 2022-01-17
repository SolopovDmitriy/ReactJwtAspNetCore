using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication13.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Slogan { get; set; }

        public string Content { get; set; }

        public string ImgSrc { get; set; }
        
        public string ImgAlt { get; set; }

        public int Order { get; set; }

        public string UrlSlug { get; set; }

        [ForeignKey("Id")]
        public int? ParentId { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
