using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using CMSUtility.Models;
using System.Text.RegularExpressions;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Drawing;
using System.ComponentModel.DataAnnotations;

namespace CMSUtility.Utilities
{
    public class CommonFunctions
    {
        public const string gsEmailValidationRegex = @"^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";
        public enum ActionResponse
        {
            [StringValue("RecordAdded")]
            Add = 101,
            [StringValue("RecordUpdated")]
            Update = 102,
            [StringValue("RecordDeleted")]
            Delete = 103,
            [StringValue("RecordExists")]
            Exists = 104,
            [StringValue("Error")]
            Error = 0,
            [StringValue("Warning")]
            Warning = 1
        }

        public enum ProjectStatus
        {
            [StringValue("Sanctioned")]
            Sanctioned = 1,
            [StringValue("Ongoing")]
            Ongoing = 2,
            [StringValue("Completed")]
            Completed = 3,
            [StringValue("Cancelled")]
            Cancelled = 3
        }
        public enum UserStatus
        {
            Active = 1,
            InActive = 0
        }
        public enum FileStatus
        {
            Accepted = 1,
            Pending = 0
        }

        public enum CaseStatus
        {
            Open = 1,
            Forward = 2,
            Completed = 3
        }

        #region retrive enum string
        public static class StringEnum
        {
            public static string getStringValue(Enum foValue)
            {
                string lsOutPut = null;
                Type lsType = foValue.GetType();
                FieldInfo loFieldInfo = lsType.GetField(foValue.ToString());
                if (loFieldInfo != null)
                {
                    StringValue[] loAryAttibutes = loFieldInfo.GetCustomAttributes(typeof(StringValue), false) as StringValue[];
                    if (loAryAttibutes.Length > 0)
                        lsOutPut = loAryAttibutes[0].Value;
                }
                return lsOutPut;
            }
            public static Dictionary<int, string> getListOfDescription<T>()
            {
                List<string> loEnumList = new List<string>();
                Dictionary<int, string> loEnumInfo = new Dictionary<int, string>();
                var loType = typeof(T);
                if (!loType.IsEnum)
                    throw new ArgumentException();
                string[] laEnumNames = Enum.GetNames(loType);
                foreach (string lsEnum in laEnumNames)
                {
                    var loMemInfo = loType.GetMember(lsEnum);
                    var loAttributes = loMemInfo[0].GetCustomAttributes(typeof(StringValue), false);
                    int liKey = Convert.ToInt32(((T)Enum.Parse(loType, lsEnum)));
                    string lsValue = loAttributes.Length > 0 ? (((StringValue)(loAttributes[0])).Value) : liKey.ToString();
                    loEnumInfo.Add(liKey, lsValue);
                }
                return loEnumInfo;
            }
        }
        #endregion retrive enum string

        #region Enum Classes & function
        public class StringValue : System.Attribute
        {
            private string _value;

            public StringValue(string value)
            {
                _value = value;
            }

            public string Value
            {
                get { return _value; }
            }
        }
        #endregion
    }
}
