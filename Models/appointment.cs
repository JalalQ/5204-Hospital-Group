using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hospitalPrj.Models
{
    public class appointment
    {
        [Key]
        public int bookId { get; set; }
        public DateTime bookDate { get; set; }
        public DateTime bookTime { get; set; }
        public string bookReason { get; set; }
    }
}