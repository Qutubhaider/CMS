using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.User.Models
{
    public class UserDropDownDetailResult
    {
        public int inUserProfileId   {get;set;}
        public int inUserId          {get;set;}
        public string stFirstName       {get;set;}
        public string stLastName        {get;set;}
        public string stEmail           {get;set;}
        public string stMobile          {get;set;}
        public string stAddress { get; set; }
        public int inRole { get; set; }
    }
}
