using System;
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
        public string departName { get; set; }
        [ForeignKey("doctor")]
        public int doctorID {get;set; }
        public virtual doctor doctor{ get; set; }
        [ForeignKey("ApplicationUser")]
        public string Id{ get; set; }

        public virtual ApplicationUser ApplicationUser { get; set;}
       
       
      



    }
    public class appointmentDto
    {
       

        public int bookId { get; set; }
        public DateTime bookDate { get; set; }
        public string bookReason { get; set; }
        public int patientId { get; set; }
        public int doctorID { get; set; }
        public string departName { get; set;}
          }
}
    
