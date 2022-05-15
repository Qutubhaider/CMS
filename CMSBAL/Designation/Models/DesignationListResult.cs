using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.Designation.Models
{
    public class DesignationListResult
    {
        public int inRecordCount { get; set; }
        public int inRownumber { get; set; }
        public int inDesignationId { get; set; }
        public Guid unDesignationId { get; set; }
        public string stDesignationName { get; set; }
        public string stDepartmentName { get; set; }
    }
}
