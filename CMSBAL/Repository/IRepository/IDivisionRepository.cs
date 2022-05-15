using CMSUtility.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.Repository.IRepository
{
    public interface IDivisionRepository
    {
        void SaveDivision(Division.Models.Division foDivision, int fiUserId, out int fiSuccess);
        Division.Models.Division GetDivision(Guid unDivisionId);
        void DeleteDivision(Guid unDivisionId, out int fiSuccess);
        List<Division.Models.DivisionListResult> GetAllDivision(string stDivisionTitle, int? finStatus, int? fiSortColumn, string fsSortOrder, int? fiPageNo, int? fiPageSize, int? fiUserId = null);
        List<Select2> GetDivisionDropDown(int fiZoneId);

    }
}
