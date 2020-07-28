using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RMISDemo.Models
{
    public class BlogPostUrl
    {
        public int Id { get; set; }
        public int BlogPostId { get; set; }
        public string UrlPath { get; set; }
        public string UrlTypeId { get; set; }

        public BlogPostUrlCategory UrlType { get; set; }
    }
}