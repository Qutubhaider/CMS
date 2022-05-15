using CMSUtility.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.Shelve.Models
{
    public class Shelve
    {
        public int inShelveId { get; set; }
        public Guid unShelveId { get; set; }
        [Required(ErrorMessage = "Please enter shelve number.")]
        public string stShelveNumber { get; set; }
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
        [Required(ErrorMessage = "Please select almirah")]
        public int inAlmirahId { get; set; }
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
