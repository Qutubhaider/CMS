using CMSUtility.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.Repository.IRepository
{
   public interface IDepartmentRepository
   {
        void SaveDepartment(Department.Models.Department foDepartment, int fiUserId, out int fiSuccess);
        Department.Models.Department GetDepartment(Guid unDivisionId);
        void DeleteDepartment(Guid fuDepartmentId, out int fiSuccess);
        List<Department.Models.DepartmentListResult> GetDepartmentList(string stDivisionTitle, int? fiSortColumn, string fsSortOrder, int? fiPageNo, int? fiPageSize, int? fiUserId = null);
        List<Select2> GetDepartmentDropDown();

    }
}
