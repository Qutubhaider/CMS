using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.Almirah.Models
{
    public class AlmirahListResult
    {
        public int inRecordCount { get; set; }
        public int inRownumber { get; set; }
        public int inAlmirahId { get; set; }
        public Guid unAlmirahId { get; set; }
        public string stAlmirahNumber { get; set; }
        public string stStoreName { get; set; }
        public string stZoneName { get; set; }
        public string stDivisionName { get; set; }
        public string stDepartmentName { get; set; }
        public string stRoomNumber { get; set; }
    }
}
