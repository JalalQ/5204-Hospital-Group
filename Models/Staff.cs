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


        //A staff member can belong to 1 department
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
    public class StaffDto
    {
        public int StaffId { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required]
        [RegularExpression(".+\\@.+\\..+",ErrorMessage ="Please entewr valid Email")]
        [DisplayName("Email")]
        public string Email { get; set; }
        [Required]
        [DisplayName("Contact")]
        public string Contact { get; set; }
        [Required]
        [DisplayName("Position")]
        public string Position { get; set; }
        [Required]
        [DisplayName("Department")]
        public int DepartmentId { get; set; }
    }
}
