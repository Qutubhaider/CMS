using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.Case.Models
{
    public class CaseListResult
    {
        public int inRecordCount { get; set; }
        public int inRownumber { get; set; }
        public int inCaseId { get; set; }
        public Guid unCaseId { get; set; }
        public string stZoneName { get; set; }
        public string stDivisionName { get; set; }
        public string stDepartmentName { get; set; }
        public string stDesignationName { get; set; }
        public string stFileName { get; set; }
        public string stAssignedBy { get; set; }
        public int inSRId { get; set; }
        public int inStatus { get; set; }
    }
}
