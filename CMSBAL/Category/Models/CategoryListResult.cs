using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.Category.Models
{
    public class CategoryListResult
    {
        public int inRecordCount { get; set; }
        public int inRownumber { get; set; }
        public int inCategoryId { get; set; }
        public Guid unCategoryId { get; set; }
        public string stParentCategoryName { get; set; }
        public string stDepartmentName { get; set; }
        public int inStatus { get; set; }
        public string stCategoryName { get; set; }

    }
}
