using CMSBAL.Data;
using CMSBAL.Repository.IRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly DatabaseContext moDatabaseContext;

        public DashboardRepository(DatabaseContext foDatabaseContext)
        {
            moDatabaseContext = foDatabaseContext;
        }

        public List<Dashboard.Models.DashboardResult> GetDepartmentDashboard(int fiDepartmentId)
        {
            return moDatabaseContext.Set<Dashboard.Models.DashboardResult>().FromSqlInterpolated($"EXEC getDepartmentDashboard @inDepartmentId={fiDepartmentId}").ToList();
        }
    }
}
