using CMSUtility.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.Repository.IRepository
{
    public interface IZoneRepository
    {
        void SaveZone(Zone.Models.Zone foZone, int fiUserId, out int fiSuccess);
        Zone.Models.Zone GetZoneDetail(Guid unZoneId);
        void DeleteZone(Guid fuZoneId, out int fiSuccess);
        List<Zone.Models.ZoneListResult> GetZoneList(string fsZoneName, int? finStatus, int? fiSortColumn, string fsSortOrder, int? fiPageNo, int? fiPageSize, int? fiUserId = null);
        List<Select2> GetZoneDropDown();
    }
}
