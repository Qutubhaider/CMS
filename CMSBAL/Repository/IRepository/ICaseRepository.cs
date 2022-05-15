using CMSBAL.Case.Models;
using CMSUtility.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.Repository.IRepository
{
    public interface ICaseRepository
    {
        void SaveCase(Case.Models.Case foCase, int fiUserId, out int fiSuccess);
        void DeleteCase(Guid fuCaseId, out int fiSuccess);
        List<CaseListResult> GetCaseList(string fsFileName,int inStatus, int? fiSortColumn, string fsSortOrder, int? fiPageNo, int? fiPageSize, int? fiUserId = null);
        CaseDetailResult GetCaseDetail(Guid fuCaseId);
        //List<Select2> GetCaseDropDown(int fiRoomId);
    }
}
