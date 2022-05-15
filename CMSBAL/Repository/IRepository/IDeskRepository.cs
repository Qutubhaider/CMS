using CMSUtility.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.Repository.IRepository
{
    public interface IDeskRepository
    {
        void SaveDesk(Desk.Models.Desk foDesk, int fiUserId, out int fiSuccess);
        Desk.Models.Desk GetDesk(Guid fuDeskId);
        void DeleteDesk(Guid fuDeskId, out int fiSuccess);
        List<Desk.Models.DeskListResult> GetDeskList(string stDeskTitle, int? finStatus, int? fiSortColumn, string fsSortOrder, int? fiPageNo, int? fiPageSize, int? fiUserId = null);
        public List<Select2> GetDeskDropDown(int fiDivision);
    }
}
