using CMSBAL.Room.Models;
using CMSUtility.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.Repository.IRepository
{
    public interface IRoomRepository
    {
        void SaveRoom(Room.Models.Room foRoom, int fiUserId, out int fiSuccess);
        Room.Models.Room GetRoomDetail(Guid fuRoomId);
        void DeleteRoom(Guid fuRoomId, out int fiSuccess);
        List<RoomListResult> GetRoomList(string fsRoomNumber, int? fiSortColumn, string fsSortOrder, int? fiPageNo, int? fiPageSize, int? fiUserId = null);
        List<Select2> GetRoomDropDown(int fiStoreId);
    }
}
