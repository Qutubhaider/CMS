using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.Complain.Models
{
    public class ComplainListResultUser
    {
        public int inRecordCount { get; set; }
        public int inRownumber { get; set; }
        public int inComplainId { get; set; }
        public Guid unComplainId { get; set; }
        public string stAssignTo { get; set; }
        public string stDepartment { get; set; }
        public string stAssignedBy { get; set; }
        public string stComplain { get; set; }
        public string dtIssueDate { get; set; }

    }
}
