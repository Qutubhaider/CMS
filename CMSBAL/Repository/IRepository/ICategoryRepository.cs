using CMSUtility.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.Repository.IRepository
{
    public interface ICategoryRepository
    {
        public void SaveCategory(Category.Models.Category foCategory,int fiUserId, out int fiSuccesss);
        public Category.Models.Category GetCategory(Guid unCategoryId);
        public List<Category.Models.CategoryListResult> GetCategoriesList();
        public List<Select2> GetCategoryDropDown();
        public List<Select2> GetCategory(int fiDepartmentId);
        public List<Select2> GetSubCategoryDropDown(int fiParentCategoryId);
    }
}
