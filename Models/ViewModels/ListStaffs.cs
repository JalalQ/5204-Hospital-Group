using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace team2Geraldton.Models.ViewModels
{
    public class ListStaffs
    {
        public bool isadmin { get; set; }

        public IEnumerable<StaffDto> staffs { get; set; }
    }
}