using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.Repository.IRepository
{
    public interface IDashboardRepository
    {
        public List<Dashboard.Models.DashboardResult> GetDepartmentDashboard(int fiDepartmentId);
    }
}
