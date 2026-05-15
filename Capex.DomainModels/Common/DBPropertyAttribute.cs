using System;
using System.Data;
using System.Runtime.Serialization;

namespace Capex.DomainModels.Common
{
    [DataContract]
    [AttributeUsage(AttributeTargets.Property)]
    public class DBPropertyAttribute : Attribute
    {
        [DataMember]
        public string DBElementName { get; set; }
        public SqlDbType DBElementType { get; set; }
        public bool IgnoreProperty { get; set; }

        public bool IgnoreDefaultValue { get; set; }


        public DBPropertyAttribute(string dbelementname = null, SqlDbType dbElementType = SqlDbType.VarChar, bool ignoreProperty = false, bool ignoreDefaultValue = true)
        {
            DBElementName = dbelementname;
            DBElementType = dbElementType;
            IgnoreProperty = ignoreProperty;
            IgnoreDefaultValue = ignoreDefaultValue;
        }
    }
}
