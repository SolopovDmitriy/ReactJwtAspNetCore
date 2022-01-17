using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication13.Entities
{
    public class Post
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Slogan { get; set; }

        public string Content { get; set; }

        public string ImgSrc { get; set; }

        public string ImgAlt { get; set; }

        public string UrlSlug { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; } 

        [DefaultValue(false)]
        public bool PostedOn { get; set; }

        public DateTime DateOfPublished { get; set; }

        public Category Category { get; set; }
    }
}
