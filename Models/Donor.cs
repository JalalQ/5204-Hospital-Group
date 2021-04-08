using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeraldtonHospital_v1.Models
{
    public class Donor
    { 
    //[Key]
    public int DonorID { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Province { get; set; }
    public string PostalCode { get; set; }
    public string DonorMessage { get; set; }

    //A donor can make many donations
    public ICollection<Donation> Donations { get; set; }
}


    public class DonorDto
    {
        public int DonorID { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Address")]
        public string Address { get; set; }
        [DisplayName("City")]
        public string City { get; set; }
        [DisplayName("Province")]
        public string Province { get; set; }
        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }
        [DisplayName("Message")]
        public string DonorMessage { get; set; }

        //    ////A donor can make many donations
        //    //public int DonationID { get; set; }
        }
    }