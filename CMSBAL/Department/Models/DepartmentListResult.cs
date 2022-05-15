using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.Department.Models
{
    public class DepartmentListResult
    {
        public int inRecordCount { get; set; }
        public int inRownumber { get; set; }
        public int inDepartmentId { get; set; }
        public Guid unDepartmentId { get; set; }
        public string stDepartmentName { get; set; }
    }
}
