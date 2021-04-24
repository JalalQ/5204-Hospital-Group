using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace team2Geraldton.Models.ViewModels
{
    public class UpdateStaff
    {
        public StaffDto staff { get; set; }
        //Needed for a dropdownlist which presents the player with a choice of teams to play for
        public IEnumerable<DepartmentDto> alldepartments { get; set; }
    }
}