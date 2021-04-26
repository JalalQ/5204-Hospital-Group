using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace team2Geraldton.Models.View_Models
{
    public class showDoctorPatient
    {
        public appointmentDto appointment{ get; set; }
        //information about the doctor
        public doctorDto doctor { get; set; }
        //public ApplicationUser applicationUser { get; set; }
    }
}