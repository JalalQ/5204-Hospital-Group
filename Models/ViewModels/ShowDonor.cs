using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeraldtonHospital_v1.Models.ViewModels
{
    public class ShowDonor
    {
        //Information about donor
        public DonorDto Donor { get; set; }

        //Information about all donations by donor
        public IEnumerable<DonationDto> DonorDonations { get; set; }

        
    }
}