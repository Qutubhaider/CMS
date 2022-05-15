using CMSBAL.Almirah.Models;
using CMSBAL.Data;
using CMSBAL.Repository.IRepository;
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
    class AlmirahRepository : IAlmirahRepository
    {
        private readonly DatabaseContext moDatabaseContext;
        private int fiSuccess;

        public AlmirahRepository(DatabaseContext foDatabaseContext)
        {
            moDatabaseContext = foDatabaseContext;
        }
        public void DeleteAlmirah(Guid fuAmirahId, out int fiSuccess)
        {
            SqlParameter loSuccess = new SqlParameter("@inSuccess", SqlDbType.Int) { Direction = ParameterDirection.Output };
            moDatabaseContext.Database.ExecuteSqlInterpolated($"EXEC deleteAlmiarh @unAlmirahId={fuAmirahId}, @inSuccess={loSuccess} OUT");
            fiSuccess = Convert.ToInt32(loSuccess.Value);
        }

        public Almirah.Models.Almirah GetAlmirahDetail(Guid fuAlmirahId)
        {
            return moDatabaseContext.Set<Almirah.Models.Almirah>().FromSqlInterpolated($"EXEC getAlmirahDetail @unAlmirahId={fuAlmirahId}").AsEnumerable().FirstOrDefault();
        }

        public List<Select2> GetAlmirahDropDown(int fiRoomId)
        {
            return moDatabaseContext.Set<Select2>().FromSqlInterpolated($"EXEC getAlmirahDropDown @inRoomId={fiRoomId}").ToList();
        }

        public List<AlmirahListResult> GetAlmirahList(string fsAlmirahNumber, int? fiSortColumn, string fsSortOrder, int? fiPageNo, int? fiPageSize, int? fiUserId = null)
        {
            return moDatabaseContext.Set<AlmirahListResult>().FromSqlInterpolated($"EXEC getAlmirahList @stAlmirahNumber={fsAlmirahNumber}, @inSortColumn={fiSortColumn},@stSortOrder={fsSortOrder}, @inPageNo={fiPageNo},@inPageSize={fiPageSize},@inUserId={fiUserId}").ToList();
        }

        public void SaveAlmirah(Almirah.Models.Almirah foAmirah, int fiUserId, out int fiSuccess)
        {
            SqlParameter loSuccess = new SqlParameter("@inSuccess", SqlDbType.Int) { Direction = ParameterDirection.Output };
            moDatabaseContext.Database.ExecuteSqlInterpolated($"EXEC saveAlmirah @inAmirahId={foAmirah.inAlmirahId},@stAmirahNumber={foAmirah.stAlmirahNumber} ,@inStoreId={foAmirah.inStoreId},@inZoneId={foAmirah.inZoneId},@inDivisionId={foAmirah.inDivisionId},@inDepartmentId={foAmirah.inDepartmentId},@inRoomId={foAmirah.inRoomId},@inCreatedBy={fiUserId},@inSuccess={loSuccess} OUT");
            fiSuccess = Convert.ToInt32(loSuccess.Value);
        }
    }
}
