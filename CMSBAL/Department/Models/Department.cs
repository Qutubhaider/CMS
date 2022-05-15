using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.Department.Models
{
    public class Department
    {
        public int inDepartmentId { get; set; }
        public Guid unDepartmentId { get; set; }
        public int inZoneId { get; set; }
        public int inDivisionId { get; set; }
        [Required(ErrorMessage ="Please enter department name.")]
        public string stDepartmentName { get; set; }
    }
}