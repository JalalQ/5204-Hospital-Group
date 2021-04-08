using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace News_Events_Payments.Models
{
    public class Payments
    {
        [Key]
        public int Payment_id { get; set; }

        public string Patient_firstname { get; set; }

        public string Patient_lastname { get; set; }

        public string Patient_email { get; set; }
        public DateTime Payment_date { get; set; }
        public int Payment_amount { get; set; }
        public int Bill_number { get; set; }
        public int Card_number { get; set; }
        public int Card_csv { get; set; }

    }
}