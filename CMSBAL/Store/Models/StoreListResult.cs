using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.Store.Models
{
   public class StoreListResult
    {
        public int inRecordCount { get; set; }
        public int inRownumber { get; set; }
        public int inStoreId { get; set; }
        public Guid unStoreId { get; set; }
        public string stStoreName { get; set; }
        public string stZoneName { get; set; }
        public string stDivisionName { get; set; }
        public string stDepartmentName { get; set; }
    }
}
