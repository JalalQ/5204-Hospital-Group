using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace team2Geraldton.Models.ViewModels
{
    public class ListPosts
    {
        public bool isadmin { get; set; }
        public IEnumerable<PostDto> posts { get; set; }
    }
}