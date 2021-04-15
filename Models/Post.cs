using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace team2Geraldton.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

    }
    public class PostDto
    {
        public int PostId { get; set; }
        [DisplayName("Job Title")]
        public string Title { get; set; }
        [DisplayName("Job Type")]
        public string Type { get; set; }
        [DisplayName("Job Description")]
        public string Description { get; set; }

    }
}
