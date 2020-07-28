using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RMISDemo.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PostContent { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<BlogPostUrl> BlogPostUrl { get; set; }

    }
}