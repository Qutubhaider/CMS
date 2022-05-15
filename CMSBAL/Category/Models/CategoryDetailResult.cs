using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.Category.Models
{
    public class CategoryDetailResult
    {
        public int inCategoryId { get; set; }
        public Guid unCategoryId { get; set; }
        public int inParentCategoryId { get; set; }
        public string stCategoryName { get; set; }
        public bool flgIsActive { get; set; }
        public int inCreatedBy { get; set; }
    }
}
