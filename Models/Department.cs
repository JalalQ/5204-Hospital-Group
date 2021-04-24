using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace team2Geraldton.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
       
        //A department can have many staff members
        public ICollection<Staff> Staffs { get; set; }
    }
    public class DepartmentDto
    {
        public int DepartmentId { get; set; }
        [DisplayName("Department Name")]
        public string DepartmentName { get; set; }
    }
}
