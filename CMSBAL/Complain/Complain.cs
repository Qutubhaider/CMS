using CMSUtility.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.Complain.Models
{
    public class Complain
    {
        public int inDepartmentId { get; set; }
        public int inCategoryId { get; set; }
        public int inSubCategoryId { get; set; }
        public string stComplainText { get; set; }
        public List<Select2> CategoryList { get; set; }
        public List<Select2> SubCategoryList { get; set; }
        public List<Select2> DepartmentList { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
        public string stUnFileName { get; set; }

        public string stFileName { get; set; }
    }
}
