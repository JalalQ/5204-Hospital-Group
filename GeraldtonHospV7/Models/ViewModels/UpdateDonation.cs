using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeraldtonHospV7.Models.ViewModels
{
    public class UpdateDonation
    {
        //base information about the donor
        public DonationDto Donation { get; set; }

        //display all donations that this donor has made
        public IEnumerable<DonorDto> Donors { get; set; }
        public IEnumerable<DonationDto> AllDonations { get; internal set; }
    }
}