using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace team2Geraldton.Models
{
    public class JobApplication
    {
        public int JobApplicationId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Dob { get; set; }
        public int Email { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public string Qualification { get; set; }
        public string Experience { get; set; }
    }
}