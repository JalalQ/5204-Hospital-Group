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
    public class review
    {
        //For the rating, numerical values are given by the visitor on a scale of 5.

        [Key]
        public int reviewID { get; set; }
   
        public int knowledge { get; set; }
   
        public int professional { get; set; }

        public int friendly { get; set; }

        //A review is given for one of the doctor.
        [ForeignKey("doctor")]
        public int doctorID { get; set; }
        public virtual doctor doctor{ get; set; }

    }

    public class reviewDto
    {
        [DisplayName("Review ID")]
        public int reviewID { get; set; }

        [DisplayName("Knowledge")]
        [Required(ErrorMessage = "Rating for knowledge is required")]
        [Range(0, 5, ErrorMessage = "Enter a rating between 1 and 5")]
        public int knowledge { get; set; }

        [DisplayName("Professional Conduct")]
        [Required(ErrorMessage = "Rating for professional conduct is required")]
        [Range(0, 5, ErrorMessage = "Enter a rating between 1 and 5")]
        public int professional { get; set; }

        [DisplayName("Friendliness")]
        [Required(ErrorMessage = "Rating for friendliness is required")]
        [Range(0, 5, ErrorMessage = "Enter a rating between 1 and 5")]
        public int friendly { get; set; }

        [DisplayName("Doctor's Name")]
        public string doctorName { get; set; }

        [DisplayName("Doctor ID")]
        [Required(ErrorMessage = "Doctor ID for friendliness is required")]
        //the doctor for whom the review is given for.
        public int doctorID { get; set; }

    }


}