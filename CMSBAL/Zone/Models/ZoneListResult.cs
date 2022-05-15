using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.Zone.Models
{
   public class ZoneListResult
    {
        public int inRecordCount { get; set; }
        public int inRownumber { get; set; }
        public int inZoneId { get; set; }
        public Guid unZoneId { get; set; }
        public string stZoneName { get; set; }
    }
}
