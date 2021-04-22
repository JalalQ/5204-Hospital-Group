using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace team2Geraldton.Models
{
    public class appointment
    {
        [Key]
        public int bookId { get; set; }
        public DateTime bookDate { get; set; }
        public string bookReason { get; set; }
     //  [ForeignKey("ApplicationUser")]
     //  public string Id { get; set; }
      // public string Healthnumber{ get; set; }
      // public string Name { get; set; }
       
       

    }
    public class appointmentDto
    {
       

        public int bookId { get; set; }
        public DateTime bookDate { get; set; }
        public string bookReason { get; set; }
        public int clientId { get; set; }
        public string drName { get; set; }
        public string department { get; set; }
       //public string Id { get; set; }
      //  public string Healthnumber { get; set; }
       // public string Name { get; set; }

    }
}
    
