using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.User.Models
{
    public class UserEmailResult
    {
        public int inUserId { get; set; }
        public Guid unUserId { get; set; }
        public string stUsername { get; set; }
        public string stPassword { get; set; }
        public int inRole { get; set; }
        public string stEmail { get; set; }
        public string stMobile { get; set; }
        public int inStatus { get; set; }
        public int? inZoneId { get; set; }
        public int? inDesignationId { get; set; }
        public int? inDeskId { get; set; }
        public int? inDivisionId { get; set; }
        public int? inStoreId { get; set; }
        public int? inDepartmentId { get; set; }
        public string stDepartmentName { get; set; }
        public string stFirstName { get; set; }
        public string stZoneName { get; set; }
        public string stDivisionName { get; set; }

    }
}
