using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.Repository.IRepository
{
    public interface IComplainRepository
    {
        void SaveComplain(Complain.Models.Complain foComplain,int liZoneId,int liDivisionId, int fiUserId,int fiStoreId, out int fiSuccess);
    }
}
