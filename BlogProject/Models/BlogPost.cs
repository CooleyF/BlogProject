using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogProject.Models
{
    public class BlogPost
    {
        //Parent child relationship. EVery comment has parent blogpost
        public BlogPost()
        {
            this.Comments = new HashSet<Comment>();
        }
        //Primary key is the ID.
        public int ID { get; set;  }
        public string AuthorID {  get; set;  }
        public DateTimeOffset Created { get; set; }

        public DateTimeOffset? Updated { get; set;  }
        [Display(Name = "Blog Title")]
        public string Title { get; set; }
        //slug is derived from title.
        public string Slug { get; set; }
        //this property will be used to display a short snippet to the user
        public string Abstract { get; set; }
        [AllowHtml]
        public string BlogBody { get; set; }

        public string MediaURL { get; set; }
        public bool Published { get; set;  }
        //virtual Nav
        public virtual ICollection<Comment> Comments { get; set;  }

        

        public virtual ApplicationUser Author { get; set; }

    }
}