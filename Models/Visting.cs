using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Simran_hospital.Models
{
    public class Visting
    {
        [Key]
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
    }
    public class VisitingDto
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
    }
}