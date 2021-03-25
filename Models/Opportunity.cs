using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Simran_hospital.Models
{
    public class Opportunity
    {
        [Key]
        public int OpportunityID { get; set; }
        public string OpportunityName { get; set; }
        public ICollection<Volunteer> Volunteers { get; set; }
    }
    public class OpportunityDto
    {
        public int OpportunityID { get; set; }
        public string OpportunityName { get; set; }
    }
}