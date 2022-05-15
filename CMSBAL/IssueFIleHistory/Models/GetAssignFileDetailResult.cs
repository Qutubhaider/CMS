using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.IssueFIleHistory.Models
{
    public class GetAssignFileDetailResult
    {
        public Guid unlssueFileId { get; set; }
        public string stComment { get; set; }
        public string stFileName { get; set; }
        public string stDivisionName { get; set; }
        public string stUserName { get; set; }
        public string stDepartmentName { get; set; }
        public int inStoreFileDetailsId { get; set; }
        public int inAssignUserId { get; set; }
        public int inlssueFileId { get; set; }
        public string stUnFileName { get; set; }
        public int inUserProfileId { get; set; }
        public int inCreatedBy { get; set; }
        public int inSRId { get; set; }
    }
}
