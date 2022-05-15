using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSUtility.Utilities
{
    public class CommonConstant
    {
        public class SessionConstant
        {
            public const string Id = "Id";
            public const string unUserId = "unUserId";
            public const string stUserName = "stUserName";
            public const string stEmail = "stEmail";
            public const string RoleId = "RoleId";
            public const string ZoneId = "ZoneId";
            public const string DivisionId = "DivisionId";
            public const string DesignationId = "DesignationId";
            public const string DeskId = "DeskId";
            public const string StoreId = "StoreId";
            public const string DepartmentId = "DepartmentId";
            public const string DepartmentName = "DepartmentName";
            public const string DivisionName = "DivisionName";
            public const string ZoneName = "ZoneName";
            public const string Name = "Name";
        }

        public class RoleConstants
        {

            public const string Admin = "Admin";
            public const string DivisionAdmin = "Division Admin";
            public const string DepartmentAdmin = "Department Admin";
            public const string DeskAdmin = "Desk Admin";
            public const string DeskOP = "Desk OP";
            public const string StoreOP = "Store OP";
        }

        public enum UserType
        {
            Admin = 1,
            DivisionAdmin = 2,
            DepartmentAdmin = 3,
            DeskAdmin = 4,
            DeskOP = 5,
            StoreOP = 6,
            SignUpUser= 7
        }
    }
}