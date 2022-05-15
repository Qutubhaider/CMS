using CMSBAL.Data;
using CMSBAL.Repository.IRepository;
using CMSBAL.Store.Models;
using CMSUtility.Models;
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
    public class StoreRepository : IStoreRepository
    {
        private readonly DatabaseContext moDatabaseContext;

        public StoreRepository(DatabaseContext foDatabaseContext)
        {
            moDatabaseContext = foDatabaseContext;
        }
        public void DeleteStore(Guid fuStoreId, out int fiSuccess)
        {
            SqlParameter loSuccess = new SqlParameter("@inSuccess", SqlDbType.Int) { Direction = ParameterDirection.Output };
            moDatabaseContext.Database.ExecuteSqlInterpolated($"EXEC deleteStore @unStoreId={fuStoreId}, @inSuccess={loSuccess} OUT");
            fiSuccess = Convert.ToInt32(loSuccess.Value);
        }

        public Store.Models.Store GetStoreDetail(Guid fuStoreId)
        {
            return moDatabaseContext.Set<Store.Models.Store>().FromSqlInterpolated($"EXEC getStoreDetail @unStoreId={fuStoreId}").AsEnumerable().FirstOrDefault();
        }

        public List<Select2> GetStoreDropDown(int fiDivisionId)
        {
            return moDatabaseContext.Set<Select2>().FromSqlInterpolated($"EXEC getStoreDropDown @inDivisionId={fiDivisionId}").ToList();
        }

        public List<StoreListResult> GetStoreList(string fsStoreName, int? fiSortColumn, string fsSortOrder, int? fiPageNo, int? fiPageSize, int? fiUserId = null)
        {
            return moDatabaseContext.Set<Store.Models.StoreListResult>().FromSqlInterpolated($"EXEC getStoreList @stStoreName={fsStoreName}, @inSortColumn={fiSortColumn},@stSortOrder={fsSortOrder}, @inPageNo={fiPageNo},@inPageSize={fiPageSize},@inUserId={fiUserId}").ToList();
        }

        public void SaveStore(Store.Models.Store foStore, int fiUserId, out int fiSuccess)
        {
            SqlParameter loSuccess = new SqlParameter("@inSuccess", SqlDbType.Int) { Direction = ParameterDirection.Output };
            moDatabaseContext.Database.ExecuteSqlInterpolated($"EXEC saveStore @inStoreId={foStore.inStoreId}, @stStoreName={foStore.stStoreName},@inZoneId={foStore.inZoneId},@inDivisionId={foStore.inDivisionId},@inDepartmentId={foStore.inDepartmentId},@inCreatedBy={fiUserId},@inSuccess={loSuccess} OUT");
            fiSuccess = Convert.ToInt32(loSuccess.Value);
        }
    }
}
