using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.Trace.Models
{
    public class TraceFileResults
    {
        public int inRecordCount { get; set; }
        public int inRownumber { get; set; }
        public int inSRId { get; set; }
        public Guid unSRId { get; set; }
        public string stEmployeeName { get; set; }
        public string stEmployeeNumber { get; set; }
        public string stPFNumber { get; set; }
        public string stPPONumber { get; set; }
        public string stMobile { get; set; }
        public int inStatus { get; set; }
        public DateTime dtSRdate { get; set; }
    }
}
