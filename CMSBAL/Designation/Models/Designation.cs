using CMSUtility.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.Designation.Models
{
    public class Designation
    {
        public int inDesignationId { get; set; }
        public Guid unDesignationId { get; set; }
        [Required(ErrorMessage ="Please enter designation name.")]
        public string stDesignationName { get; set; }
        public int inZoneId { get; set; }
        public int inDivisionId { get; set; }
        public int inDepartmentId { get; set; }
        [NotMapped]
        public List<Select2> DepartmentList { get; set; }
    }
}
