using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeraldtonHospV7.Models.ViewModels
{
    public class ShowDonor
    {
        //Information about specific donor and their donations 
        public DonorDto Donor { get; set; }

        //Information about all donations by donor
        public IEnumerable<DonationDto> DonorDonations { get; set; }

    }
}