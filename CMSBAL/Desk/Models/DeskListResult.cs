using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.Desk.Models
{
    public class DeskListResult  
    {
        public int inRecordCount     {get;set;}
        public int inRownumber       {get;set;}
        public int inDeskId          {get;set;}
        public Guid unDeskId          {get;set;}
        public string stDeskName        {get;set;}
        public string stZoneName        {get;set;}
        public string stDivisionName    {get;set;}
        public string stDepartmentName  {get;set;}
        public string stDesignationName { get; set; }
    }
}
