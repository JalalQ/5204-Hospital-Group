using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace team2Geraldton.Models.View_Models
{
    public class listAppointment
    {
        public IEnumerable<appointmentDto> appointments { get; set; }
    }
}