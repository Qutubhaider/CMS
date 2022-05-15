using CMSBAL.IssueFIleHistory.Models;
using CMSUtility.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.Case.Models
{
    public class CaseDetailResult
    {
        public int inCaseId { get; set; }
        public Guid unCaseId { get; set; }
        public string stFileName { get; set; }
        public string stEmployeeName { get; set; }
        public string stEmployeeNumber { get; set; }
        public string stPFNumber { get; set; }
        public string stPPONumber { get; set; }
        public string inEmployeeType { get; set; }
        public string stMobile { get; set; }
        public int inStatus { get; set; }
        public int inStoreFileDetailId { get; set; }
        public int inSRId { get; set; }
        public int? inDepartmentId { get; set; }

        [NotMapped]
        public string stComment { get; set; }

        [NotMapped]
        public int assignedTo { get; set; }

        [NotMapped]
        public List<Select2> UserList { get; set; }

        [NotMapped]
        public List<IssueFileListResult> IssueFileListResult { get; set; }

        [NotMapped]
        public List<Select2> DepartmentList { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
    }
}
