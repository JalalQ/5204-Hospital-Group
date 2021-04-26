using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace team2Geraldton.Models
{
    public class doctor
    {
        [Key]
        public int doctorID { get; set; }

        public string fullName { get; set; }

        public int cpsoReg { get; set; }

        public string email { get; set; }

        public string education { get; set; }

        public string expertise { get; set; }

        [AllowHtml]
        public string biography { get; set; }

        //A doctor can have many reviews
        public ICollection<review> reviews { get; set; }

    }

    public class doctorDto
    {
        public int doctorID { get; set; }

        [DisplayName("Full Name")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Doctor Name is required. Do not enter numbers/ special characters.")]
        [Required(ErrorMessage = "Rating for friendliness is required")]
        public string fullName { get; set; }

        [DisplayName("CPSO Registration No.")]
        public int cpsoReg { get; set; }

        [DisplayName("Email")]
        public string email { get; set; }

        [DisplayName("Education")]
        public string education { get; set; }

        [DisplayName("Medical Expertise")]
        public string expertise { get; set; }

        [DisplayName("Biography")]
        public string biography { get; set; }

    }

}