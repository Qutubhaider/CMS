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
        public void getDeskOperatorCount(int inUserId, int inRoleId, out int inDeskOperatorCount)
        {
            SqlParameter loDeskOPCount = new SqlParameter("@inDeskOpCount", SqlDbType.Int) { Direction = ParameterDirection.Output };
            moDatabaseContext.Database.ExecuteSqlInterpolated($"EXEC getDeskOperatorCount @inUserId={inUserId},@inRoleId={inRoleId}, @inDeskOpCount={loDeskOPCount} OUT");
            inDeskOperatorCount = Convert.ToInt32(loDeskOPCount.Value);
        }

        public void getPendingAcceptFileCount(int inUserId, int inRoleId, out int inPendingAcceptFileCount)
        {
            SqlParameter loFileCount = new SqlParameter("@inPendingAcceptFileCount", SqlDbType.Int) { Direction = ParameterDirection.Output };
            moDatabaseContext.Database.ExecuteSqlInterpolated($"EXEC getPendingAcceptFileCount @inUserId={inUserId},@inRoleId={inRoleId}, @inPendingAcceptFileCount={loFileCount} OUT");
            inPendingAcceptFileCount = Convert.ToInt32(loFileCount.Value);
        }

        public void getPendingCaseCount(int inUserId, int inRoleId, out int inPendingCaseCount)
        {
            SqlParameter loCaseCount = new SqlParameter("@inPendingCaseCount", SqlDbType.Int) { Direction = ParameterDirection.Output };
            moDatabaseContext.Database.ExecuteSqlInterpolated($"EXEC getPendingCaseCount @inUserId={inUserId},@inRoleId={inRoleId}, @inPendingCaseCount={loCaseCount} OUT");
            inPendingCaseCount = Convert.ToInt32(loCaseCount.Value);
        }

        public void getStoreUserCount(int inUserId, int inRoleId, out int inStoreUserCount)
        {
            SqlParameter loStoreUserCount = new SqlParameter("@inStoreUserCount", SqlDbType.Int) { Direction = ParameterDirection.Output };
            moDatabaseContext.Database.ExecuteSqlInterpolated($"EXEC getStoreUserCount @inUserId={inUserId},@inRoleId={inRoleId}, @inStoreUserCount={loStoreUserCount} OUT");
            inStoreUserCount = Convert.ToInt32(loStoreUserCount.Value);
        }
    }
}
