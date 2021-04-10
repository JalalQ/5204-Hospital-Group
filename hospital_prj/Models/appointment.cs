using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace hospital_prj.Models
{
    public class appointment
    {

        [Key]
        public int bookId { get; set; }
        public DateTime bookDate { get; set; }
        public string bookReason { get; set; }

        [ForeignKey("user")]
        public string userId { get; set; }
        public virtual user user { get; set; }
    }
    public class appointmentDto
    {
        public int bookID { get; set; }
        public string bookDate { get; set; }
        public string bookReason { get; set; }
    }
}
