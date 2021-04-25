using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeraldtonHospV7.Models.ViewModels
{
    public class ShowDonation
    {
        //Information about specific donation and their donor
        public DonationDto Donation { get; set; }

        //Information about all donors by donation
        public DonorDto Donor { get; set; }

    }
}