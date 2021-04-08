using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeraldtonHospital_v1.Models
{
    public class Donation
    //this class describes the donation entity
    
    {
        //specifying the primary key: 
        //[Key]
        public int DonationID { get; set; }

        public int CardNum { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public int Cvc { get; set; }

        public decimal Amount { get; set; }


        //FOREIGN KEY
        ///A donation is from ONE donor 
        //Many payments can be made by 1 donor 
        //[ForeignKey("Donor")]
        //public int DonorID { get; set; }
        //public virtual Donor Donor { get; set; }
    }

    //"Data Transfer Object": This class can be used to transfer information about a player.
    //this class is basically a 'Model' that was used in 5101.
    //It is simply a vessel of communication with the display name: 

    public class DonationDto
    {
        public int DonationID { get; set; }

        [DisplayName("Card Number")]
        public int CardNum { get; set; }

        [DisplayName("Month")]
        public int Month { get; set; }

        [DisplayName("Year")]
        public int Year { get; set; }

        [DisplayName("CVC")]
        public int Cvc { get; set; }

        [DisplayName("Amount")]
        public decimal Amount { get; set; }

    }
}