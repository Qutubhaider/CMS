using CMSBAL.Category.Models;
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
    public class CategoryRepository: ICategoryRepository
    {
        private readonly DatabaseContext moDatabaseContext;
        public CategoryRepository(DatabaseContext foDatabaseContext)
        {
            moDatabaseContext = foDatabaseContext;
        }
        public List<CategoryListResult> GetCategoriesList(string categoryName, int? finStatus, int? fiSortColumn, string fsSortOrder, int? fiPageNo, int? fiPageSize)
        {
            return moDatabaseContext.Set<CategoryListResult>().FromSqlInterpolated($"EXEC getCategoryList @stCategoryName={categoryName}, @inStatus={finStatus},@inSortColumn={fiSortColumn},@stSortOrder={fsSortOrder},@inPageNo={fiPageNo},@inPageSize={fiPageSize}").ToList();
        }
        public CMSBAL.Category.Models.Category GetCategory(Guid unCategoryId)
        {

            return moDatabaseContext.Set<CMSBAL.Category.Models.Category>().FromSqlInterpolated($"EXEC getCategoryDetail @unCategoryId={unCategoryId}").AsEnumerable().FirstOrDefault();

        }

        public List<Select2> GetCategoryDropDown()
        {
            return moDatabaseContext.Set<Select2>().FromSqlInterpolated($"EXEC getCategoryDropDown").ToList();
        }

        public List<Select2> GetCategory(int fiDepartmentId)
        {
            return moDatabaseContext.Set<Select2>().FromSqlInterpolated($"EXEC getCategory @inDepartment={fiDepartmentId}").ToList();
        }
        public List<Select2> GetSubCategoryDropDown(int fiParentCategoryId)
        {
            return moDatabaseContext.Set<Select2>().FromSqlInterpolated($"EXEC getSubCategoryDropDown @inParentCategory={fiParentCategoryId}").ToList();
        }
        public void SaveCategory(Category.Models.Category foCategory, int fiUserId,out int fiSuccesss)
        {
            SqlParameter loSuccess = new SqlParameter("@inSuccess", SqlDbType.Int) { Direction = ParameterDirection.Output };
            moDatabaseContext.Database.ExecuteSqlInterpolated($"EXEC saveCategory @unCategoryId={foCategory.unCategoryId}, @inCategoryId={foCategory.inCategoryId},@inDepartmentId={foCategory.inDepartmentId},@inStatus={foCategory.inStatus},@inParentCategoryId={foCategory.inParentCategoryId},@stCategoryName={foCategory.stCategoryName},@inCreatedBy={fiUserId},@inSuccess={loSuccess} OUT");
            fiSuccesss = Convert.ToInt32(loSuccess.Value);
        }
    }
}
