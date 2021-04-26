using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace team2Geraldton.Models.ViewModels
{
    public class ShowReview
    {

        //Conditionally render the page depending on if the admin is logged in.
        public bool isadmin { get; set; }

        //information about the doctor for which the user has given review
        public doctorDto doctor { get; set; }

        //information about the review
        public reviewDto review { get; set; }
    }
}