using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel; //for DisplayName Dtos

namespace GeraldtonHospV7.Models
{
    public class Donation
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
        public int DonorID { get; set; }
        public virtual Donor Donor { get; set; }
    }

    public class DonationDto
    {
        [DisplayName("ID")]
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

        //[DisplayName("Donor ID")]
        //public string DonorID { get; set; }
    }

}