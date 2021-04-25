using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace team2Geraldton.Models.ViewModels
{
    public class UpdateStaff
    {
        public StaffDto staff { get; set; }
        //Needed for a dropdownlist which presents the staff members with a choice of department they belong
        public IEnumerable<DepartmentDto> alldepartments { get; set; }
    }
}