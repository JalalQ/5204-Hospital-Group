using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace team2Geraldton.Models.ViewModels
{
    public class ShowDoctor
    {
        //Conditionally render the page depending on if the admin is logged in.
        public bool isadmin { get; set; }
        public doctorDto doctor { get; set; }
    }
}