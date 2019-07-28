using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogProject.Models
{
    public class Comment
    {
        //Primary key
        public int ID { get; set; }
        public int BlogPostID { get; set; }
        public string AuthorID { get; set; }
        [AllowHtml]
        public string Body { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Updated { get; set; }
        public string UpdateReason { get; set; }

        //Navigation 
        public virtual BlogPost BlogPost { get; set; }
        public virtual ApplicationUser Author { get; set; }
    }
}