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
        // a change
        [Key]
        public int bookId { get; set; }
        public DateTime bookDate { get; set; }
        public string bookReason { get; set; }
        public string drName { get; set; }
        public string department { get; set; }

        [ForeignKey("client")]
        public int clientId { get; set; }
        public virtual client client { get; set; }
       }
    public class appointmentDto
    {
        internal string clientName;

        public int bookId { get; set; }
        public DateTime bookDate { get; set; }
        public string bookReason { get; set; }
        public int clientId { get; set; }
        public string drName { get; set; }
        public string department { get; set; }
    }
}