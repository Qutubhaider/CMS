using CMSUtility.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.Category.Models
{
    public  class Category
    {
        public int inCategoryId { get; set; }
        public Guid unCategoryId { get; set; }
        public int inParentCategoryId { get; set; }
        [Required(ErrorMessage ="Please Enter Category Name")]
        public string stCategoryName { get; set; }
        public int inStatus { get; set; }
        public int inCreatedBy { get; set; }
        [Required(ErrorMessage = "Please Select Department")]
        public int inDepartmentId { get; set; }
        [NotMapped]
        public List<Select2> DepartmentList { get; set; }
        [NotMapped]
        public List<Select2> CategoryList { get; set; }

    }
}
