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

        public string biography { get; set; }

    }

    public class doctorDto
    {
        public int doctorID { get; set; }

        public string fullName { get; set; }

        public int cpsoReg { get; set; }

        public string email { get; set; }

        public string education { get; set; }

        public string expertise { get; set; }

        public string biography { get; set; }

    }

}