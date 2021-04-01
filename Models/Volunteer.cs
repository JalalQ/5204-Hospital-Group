using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Simran_hospital.Models
{
    public class Volunteer
    {
        [Key]
        public int VolunteerID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public DateTime DOB { get; set; }
        public int ContactNumber { get; set; }
        public string Language { get; set; }
        public string CurrentEmployer { get; set; }
        public string Position { get; set; }
        public string UniversitySchool { get; set; }
        public double Grades { get; set; }
        public string WorkExperience { get; set; }
        public string WhyInterested { get; set; }
        public string PastVolunteer { get; set; }
		//Jalal comments

        [ForeignKey("Opportunity")]
        public int OpportunityID { get; set; }
        public virtual Opportunity Opportunity { get; set; }

    }
    public class VolunteerDto
    {
        public int VolunteerID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public DateTime DOB { get; set; }
        public int ContactNumber { get; set; }
        public string Language { get; set; }
        public String CurrentEmployer { get; set; }
        public string Position { get; set; }
        public string UniversitySchool { get; set; }
        public double Grades { get; set; }
        public string WorkExperience { get; set; }
        public string WhyInterested { get; set; }
        public string PastVolunteer { get; set; }
        public int OpportunityID { get; set; }

    }
}