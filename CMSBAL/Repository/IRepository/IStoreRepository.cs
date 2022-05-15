using CMSUtility.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.Repository.IRepository
{
    public interface IStoreRepository
    {
        void SaveStore(Store.Models.Store foStore, int fiUserId, out int fiSuccess);
        Store.Models.Store GetStoreDetail(Guid fuStoreId);
        void DeleteStore(Guid fuStoreId, out int fiSuccess);
        List<Store.Models.StoreListResult> GetStoreList(string fsStoreName, int? fiSortColumn, string fsSortOrder, int? fiPageNo, int? fiPageSize, int? fiUserId = null);
        List<Select2> GetStoreDropDown(int fiDivisionId);
    }
}
