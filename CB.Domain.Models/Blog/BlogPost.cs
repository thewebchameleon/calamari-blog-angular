using System;
using System.Collections.Generic;

namespace CB.Domain.Models
{
    public class BlogPost
    {
        public string ID { get; set; }

        public DateTime PublishedDate { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public BlogCategory Category { get; set; }

        public List<BlogPostTag> Tags { get; set; }

        public BlogPost()
        {
            Tags = new List<BlogPostTag>();
            Category = new BlogCategory();
        }
    }
}
