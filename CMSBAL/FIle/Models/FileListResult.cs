using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.FIle.Models
{
    public class FileListResult
    {
        public int inRecordCount { get; set; }
        public int inRownumber { get; set; }
        public int inStoreFileDetailsId { get; set; }
        public Guid unStoreFileDetailsId { get; set; }
        public string stFileName { get; set; }
        public string stUnFileName    {get;set;}
        public string stEmployeeName  {get;set;}
        public string stPPONumber     {get;set;}
        public string stPFNumber      {get;set;}
        public string stEmployeeNumber{get;set;}
        public string stMobile        {get;set;}
        public string stShelveNumber  {get;set;}
        public string stZoneName      {get;set;}
        public string stDivisionName  {get;set;}
        public string stDepartmentName{get;set;}
        public string stStoreName     {get;set;}
        public string stRoomNumber    {get;set;}
        public string stAlmirahNumber { get; set; }

    }
}
