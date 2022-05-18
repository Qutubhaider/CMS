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
    public class ComplainRepository : IComplainRepository
    {
        private readonly DatabaseContext moDatabaseContext;
        private int fiSuccess;

        public ComplainRepository(DatabaseContext foDatabaseContext)
        {
            moDatabaseContext = foDatabaseContext;
        }
        public void SaveComplain(Complain.Models.Complain foComplain, int liZoneId, int liDivisionId, int fiUserId, out int fiSuccess)
        {
            SqlParameter loSuccess = new SqlParameter("@inSuccess", SqlDbType.Int) { Direction = ParameterDirection.Output };
            moDatabaseContext.Database.ExecuteSqlInterpolated($"EXEC saveAlmirah ");
            fiSuccess = Convert.ToInt32(loSuccess.Value);
        }
    }
}
