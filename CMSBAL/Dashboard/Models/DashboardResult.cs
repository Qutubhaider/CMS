using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.Dashboard.Models
{
    public class DashboardResult
    {
        public int inStoreUserCount { get; set; }
        public int inDeskOperatorCount { get; set; }
        public int inPendingAcceptFileCount { get; set; }
        public int inPendingCaseCount { get; set; }
    }
}
