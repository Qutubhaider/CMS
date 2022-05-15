using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.Shelve.Models
{
   public  class ShelveListResult
    {
        public int inRecordCount { get; set; }
        public int inRownumber { get; set; }
        public int inShelveId { get; set; }
        public Guid unShelveId { get; set; }
        public string stShelveNumber { get; set; }
        public string stZoneName { get; set; }
        public string stDivisionName { get; set; }
        public string stDepartmentName { get; set; }
        public string stStoreName { get; set; }
        public string stRoomNumber { get; set; }
        public string stAlmirahNumber { get; set; }
    }
}
