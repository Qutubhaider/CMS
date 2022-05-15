using CMSUtility.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.Almirah.Models
{
    public class Almirah
    {
        public int inAlmirahId { get; set; }
        public Guid unAlmirahId { get; set; }
        [Required(ErrorMessage ="Please enter almirah number.")]
        public string stAlmirahNumber { get; set; }
        [Required(ErrorMessage = "Please select zone")]
        public int inZoneId { get; set; }
        [Required(ErrorMessage = "Please select division")]
        public int inDivisionId { get; set; }
        [Required(ErrorMessage = "Please select department")]
        public int inDepartmentId { get; set; }
        [Required(ErrorMessage = "Please select store")]
        public int inStoreId { get; set; }
        [Required(ErrorMessage = "Please select room")]
        public int inRoomId { get; set; }
        [NotMapped]
        public List<Select2> ZoneList { get; set; }
        [NotMapped]
        public List<Select2> DivisionList { get; set; }
        [NotMapped]
        public List<Select2> DepartmentList { get; set; }
        [NotMapped]
        public List<Select2> DesignationList { get; set; }
        [NotMapped]
        public List<Select2> StoreList { get; set; }
        [NotMapped]
        public List<Select2> RoomList { get; set; }
    }
}
