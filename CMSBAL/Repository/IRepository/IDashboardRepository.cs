using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.Repository.IRepository
{
    public interface IDashboardRepository
    {
        public void getStoreUserCount(int inUserId, int inRoleId, out int inStoreUserCount);
        public void getDeskOperatorCount(int inUserId, int inRoleId, out int inDeskOperatorCount);
        public void getPendingAcceptFileCount(int inUserId, int inRoleId, out int inPendingAcceptFileCount);
        public void getPendingCaseCount(int inUserId, int inRoleId, out int inPendingCaseCount);
    }
}
