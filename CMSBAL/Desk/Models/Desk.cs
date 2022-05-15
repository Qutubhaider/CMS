using CMSUtility.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.Desk.Models
{
   public class Desk
    {
        public int inDeskId           {get;set;}
        public Guid unDeskId           {get;set;}
        public string stDeskName         {get;set;}
        public int inZoneId           {get;set;}
        public int inDivisionId       {get;set;}
        public int inDepartmentId     {get;set;}
        public int inDesignationId    {get;set;}
        public int inStatus { get; set; }
        [NotMapped]
        public List<Select2> ZoneList { get; set; }
        [NotMapped]
        public List<Select2> DivisionList { get; set; }
        [NotMapped]
        public List<Select2> DepartmentList { get; set; }
        [NotMapped]
        public List<Select2> DesignationList { get; set; }
    }
}
