using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.Room.Models
{
    public class RoomListResult
    {
        public int inRecordCount { get; set; }
        public int inRownumber { get; set; }
        public int inRoomId { get; set; }
        public Guid unRoomId { get; set; }
        public string stRoomNumber { get; set; }
        public string stStoreName { get; set; }
        public string stZoneName { get; set; }
        public string stDivisionName { get; set; }
        public string stDepartmentName { get; set; }
    }
}
