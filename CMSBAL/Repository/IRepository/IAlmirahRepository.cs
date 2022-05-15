using CMSBAL.Almirah.Models;
using CMSUtility.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.Repository.IRepository
{
   public interface IAlmirahRepository
    {
        void SaveAlmirah(Almirah.Models.Almirah foAmirah, int fiUserId, out int fiSuccess);
        Almirah.Models.Almirah GetAlmirahDetail(Guid fuAlmirahId);
        void DeleteAlmirah(Guid fuAmirahId, out int fiSuccess);
        List<AlmirahListResult> GetAlmirahList(string fsAlmirahNumber, int? fiSortColumn, string fsSortOrder, int? fiPageNo, int? fiPageSize, int? fiUserId = null);
        List<Select2> GetAlmirahDropDown(int fiRoomId); 
    }
}
