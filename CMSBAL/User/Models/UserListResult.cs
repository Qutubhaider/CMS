using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.User.Models
{
    public class UserListResult
    {
        public int inRecordCount { get; set; }
        public int inRownumber { get; set; }
        public string stUserName { get; set; }
        public Guid unUserProfileId { get; set; }
        public string stZoneName { get; set; }
        public string stDivisionName { get; set; }
        public string stDepartmentName { get; set; }
        public string stDesignationName { get; set; }
        public string stDeskName { get; set; }
        public int? inRole { get; set; }
        public int inStatus { get; set; }

    }
}
