using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace team2Geraldton.Models.ViewModels
{
    public class UpdateReview
    {

        //information about the review itself
        public reviewDto review { get; set; }

        //information about possible doctors for which a review can be given for.
        public IEnumerable<doctorDto> doctors { get; set; }
    }
}