using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace team2Geraldton.Models
{
    public class Staff
    {
        //This class describes a staff entity.
        //It is used for actually generating a database table
        [Key]
        public int StaffId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string Position { get; set; }
    }
    public class StaffDto
    {
        public int StaffId { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Contact")]
        public string Contact { get; set; }
        [DisplayName("Position")]
        public string Position { get; set; }

    }

}
