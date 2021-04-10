using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeraldtonHospital_v1.Models.ViewModels
{
    public class ListDonors
    {
        //Pass this flag to conditionally render update/new links

        //setting admin 
        //public bool isadmin { get; set; }
        public IEnumerable<DonorDto> Donors { get; set; }
    }
}