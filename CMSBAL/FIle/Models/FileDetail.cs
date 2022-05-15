using CMSUtility.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.FIle.Models
{
    public class FileDetail
    {
        public int inStoreFileDetail  {get;set;}
        public Guid unStoreFileDetail { get; set; }
        public int inStoreId          {get;set;}
        public int inUserId           {get;set;}
        [Required(ErrorMessage ="Please select zone.")]
        public int inZoneId           {get;set;}
        [Required(ErrorMessage = "Please select division.")]
        public int inDivisionId       {get;set;}
        [Required(ErrorMessage = "Please select department.")]
        public int inDepartmentId     {get;set;}
        [Required(ErrorMessage = "Please select room.")]
        public int inRoomId           {get;set;}
        [Required(ErrorMessage = "Please select almirah.")]
        public int inAlmirahId        {get;set;}
        [Required(ErrorMessage = "Please select shelve.")]
        public int inShelvesId        {get;set;}
        [Required(ErrorMessage = "Please select file.")]
        public string stFileName      {get;set;}
        public string stUnFileName      {get;set;}
        [Required(ErrorMessage = "Please enter employee name.")]
        public string stEmployeeName  {get;set;}
        [Required(ErrorMessage = "Please enter PPO number.")]
        public string stPPONumber     {get;set;}
        [Required(ErrorMessage = "Please enter PF number")]
        public string stPFNumber      {get;set;}
        [Required(ErrorMessage = "Please enter employee number")]
        public string stEmployeeNumber{get;set;}
        [Required(ErrorMessage = "Please enter mobile number.")]
        public string stMobile        {get;set;}
        public int inStatus { get; set; }
        [NotMapped]
        public List<Select2> ZoneList { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
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
