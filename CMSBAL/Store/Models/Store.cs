using CMSUtility.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.Store.Models
{
    public class Store
    {
        public int inStoreId { get; set; }
        public Guid unStoreId { get; set; }
        public int inZoneId { get; set; }
        public int inDivisionId { get; set; }
        public int inDepartmentId { get; set; }
        public string stStoreName { get; set; }
        [NotMapped]
        public List<Select2> ZoneList { get; set; }
        [NotMapped]
        public List<Select2> DivisionList { get; set; }
        [NotMapped]
        public List<Select2> DepartmentList { get; set; }
    }
}
