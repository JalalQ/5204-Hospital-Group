using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace team2Geraldton.Models.ViewModels
{
    public class ListReviews
    {
        public bool isadmin { get; set; }
        public IEnumerable<reviewDto> reviews { get; set; }
    }
}