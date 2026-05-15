// <copyright file="DbHelper.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Capex.DomainModels.Common;
using Capex.DomainModels.DomainRequestModel;

using Capex.Utilities.Common;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;


namespace Capex.Infrastructure.Common
{
    /// <summary>
    /// DbHelper.
    /// </summary>
    public class DbHelper
    {
        private DbHelper()
        {
            // Static methods only. Hence, making constructor as private.
        }

        #region "Converting DB values to Code values and vice-versa"



        /// <summary>
        /// Gets the date database value.
        /// </summary>
        /// <param name="objDate">The object date.</param>
        /// <returns>object.</returns>
        public static object GetDateDBValue(DateTime objDate) => Constants.DBcodeValues.NullDate == objDate || Constants.DBcodeValues.DatePickerMinDate == objDate ? DBNull.Value : (object)objDate;

        /// <summary>
        /// Gets the int database value.
        /// </summary>
        /// <param name="objLong">The object long.</param>
        /// <returns>object.</returns>
        public static object GetIntDBValue(long objLong) => objLong == Constants.DBcodeValues.NullInt ? DBNull.Value : (object)objLong;

        /// <summary>
        /// Checks the database null date.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>DateTime.</returns>
        internal static DateTime? CheckDbNullDate(object obj) => obj == DBNull.Value || obj == null ? null : (DateTime?)Convert.ToDateTime(obj);



        /// <summary>
        /// Checks the database null long.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>long.</returns>
        internal static long CheckDbNullLong(object obj) => obj == DBNull.Value || obj == null ? Constants.DBcodeValues.NullInt : Convert.ToInt64(obj);

        /// <summary>
        /// Checks the database null int.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>int.</returns>
        internal static int CheckDbNullInt(object obj) => obj == DBNull.Value || obj == null ? Constants.DBcodeValues.NullInt : Convert.ToInt32(obj);

        /// <summary>
        /// Checks the database null string.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>string.</returns>
        internal static string CheckDbNullString(object obj) => obj == DBNull.Value || obj == null ? string.Empty : (string)obj;

        /// <summary>
        /// Checks the database null short.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>short.</returns>
        internal static short CheckDbNullShort(object obj) => obj == DBNull.Value || obj == null ? (short)Constants.DBcodeValues.NullInt : (short)obj;

        /// <summary>
        /// Checks the database null tinyint.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>short.</returns>
        internal static short CheckDbNullTinyint(object obj) => obj == DBNull.Value || obj == null ? (short)Constants.DBcodeValues.NullInt : (byte)obj;

        /// <summary>
        /// Checks the database null float.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>float.</returns>
        internal static float CheckDbNullFloat(object obj)
        {
            if (obj == DBNull.Value || obj == null)
                return Constants.DBcodeValues.NullFloat;
            else
            {
                float.TryParse(Convert.ToString(obj), out float returnResult);
                return returnResult;
            }
        }

        /// <summary>
        /// Checks the database null bool.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>bool.</returns>
        internal static bool CheckDbNullBool(object obj) => obj == DBNull.Value || obj == null ? Constants.DBcodeValues.NullBool : (bool)obj;

        /// <summary>
        /// Checks the database null decimal.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>decimal.</returns>
        internal static decimal CheckDbNullDecimal(object obj) => obj == DBNull.Value || obj == null ? Constants.DBcodeValues.NullDecimal : (decimal)obj;

        /// <summary>
        /// Checks the database null byte array.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>byte.</returns>
        internal static byte[] CheckDbNullByteArray(object obj) => obj == DBNull.Value || obj == null ? Constants.DBcodeValues.NullByteArray : (byte[])obj;

        /// <summary>
        /// Checks the database null unique identifier.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>Guid.</returns>
        internal static Guid CheckDbNullGuid(object obj) => obj == DBNull.Value || obj == null ? Guid.Empty : (Guid)obj;

        #endregion

        /// <summary>
        /// Get SQLDBType based on proprty type.
        /// </summary>
        /// <param name="type">Type of Request Proprty.</param>
        /// <returns>SqlDbType.</returns>
        internal static SqlDbType GetDBType(string type)
        {
            SqlDbType sqlDbType = SqlDbType.NVarChar;
            switch (type)
            {
                case "System.Int32":
                    sqlDbType = SqlDbType.Int;
                    break;
                case "System.Int64":
                    sqlDbType = SqlDbType.BigInt;
                    break;
                case "System.Decimal":
                    sqlDbType = SqlDbType.Decimal;
                    break;
                case "System.DateTime":
                    sqlDbType = SqlDbType.DateTime;
                    break;
                case "System.Float":
                    sqlDbType = SqlDbType.Float;
                    break;
                case "System.Int16":
                    sqlDbType = SqlDbType.TinyInt;
                    break;
                case "System.Byte":
                case "System.Byte[]":
                    sqlDbType = SqlDbType.VarBinary;
                    break;
                case "System.Boolean":
                    sqlDbType = SqlDbType.Bit;
                    break;
                case "System.string":
                    sqlDbType = SqlDbType.NVarChar;
                    break;

            }

            return sqlDbType;
        }

        /// <summary>
        /// The AddParameters method used to Add parameters in SqlParameter array.
        /// </summary>
        /// <param name="sqlDbType">SqlDbType.</param>
        /// <param name="elementName">string Element Name.</param>
        /// <param name="value">Element Value.</param>
        /// <param name="ignoreDefaultValue">Ignore Defalut value of a property.</param>
        /// <returns>SqlParameter.</returns>
        internal static SqlParameter AddParameters(SqlDbType sqlDbType, string elementName, object value, bool ignoreDefaultValue = true)
        {
            SqlParameter sqlParameter = null;
            switch (sqlDbType)
            {
                case SqlDbType.Int:
                    if (int.Parse(value.ToString()) != 0 || int.Parse(value.ToString()) == 0 || !ignoreDefaultValue)
                        sqlParameter = new SqlParameter(elementName, SqlDbType.Int) { Value = CheckDbNullInt(value) };
                    break;
                case SqlDbType.TinyInt:
                    if (int.Parse(value.ToString()) != 0 || !ignoreDefaultValue)
                        sqlParameter = new SqlParameter(elementName, SqlDbType.TinyInt) { Value = CheckDbNullShort(value) };
                    break;

                case SqlDbType.BigInt:
                    if (long.Parse(value.ToString()) == 0 || long.Parse(value.ToString()) != 0)
                        sqlParameter = new SqlParameter(elementName, SqlDbType.BigInt) { Value = CheckDbNullLong(value) };
                    break;

                case SqlDbType.Decimal:
                    if (decimal.Parse(value.ToString()) != 0)
                        sqlParameter = new SqlParameter(elementName, SqlDbType.Decimal) { Value = CheckDbNullDecimal(value) };
                    break;

                case SqlDbType.Float:
                    if (decimal.Parse(value.ToString()) != 0)
                        sqlParameter = new SqlParameter(elementName, SqlDbType.Float) { Value = CheckDbNullFloat(value) };
                    break;

                case SqlDbType.DateTime:
                    sqlParameter = new SqlParameter(elementName, SqlDbType.DateTime) { Value = CheckDbNullDate(value) };
                    break;
                case SqlDbType.VarBinary:
                    sqlParameter = new SqlParameter(elementName, SqlDbType.VarBinary) { Value = value };
                    break;
                case SqlDbType.Bit:
                    sqlParameter = new SqlParameter(elementName, SqlDbType.Bit) { Value = value };
                    break;

                default:
                    sqlParameter = new SqlParameter(elementName, SqlDbType.NVarChar) { Value = CheckDbNullString(value) };
                    break;
            }

            return sqlParameter;
        }

        /// <summary>
        /// The AddSQLParameters method used to add SQL parameters and return parameters array for SQLCommand.
        /// </summary>
        /// <typeparam name="T">Generic type of Reuqest Parameters.</typeparam>
        /// <param name="request">Reuqest Parameters.</param>
        /// <param name="spName">sp name.</param>
        /// <returns>SqlParameter Array.</returns>
        internal static SqlParameter[] AddSQLParameters<T>(DomainRequestModelBase request, string spName = null)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter sqlParameter = null;
            BindingFlags instancePublicAndNot = BindingFlags.Instance |
            BindingFlags.Public |
            BindingFlags.NonPublic;
            bool isAddDefaultParam = false;
            var memberNames = typeof(T)
              .GetProperties(instancePublicAndNot)
              .OfType<MemberInfo>()
              .Where(x => !Attribute.IsDefined(x, typeof(NonSerializedAttribute)))
              .Select(x => x.Name);

            foreach (var memberName in memberNames)
            {
                var propertyName = request.GetType().GetProperty(memberName);
                if (propertyName != null)
                {
                    sqlParameter = null;
                    var value = propertyName.GetValue(request, null);
                    if (value != null)
                    {
                        string type = propertyName.PropertyType.Name == "Nullable`1" ? propertyName.PropertyType.GenericTypeArguments.FirstOrDefault().FullName : propertyName.PropertyType.FullName;
                        SqlDbType dbType = GetDBType(type);
                        var customAttributes = propertyName.GetCustomAttributes(true);
                        string parameterName = string.Empty;
                        if (customAttributes != null && customAttributes.Any(x => x.GetType().Name == APIConstants.DBPropertyAttribute))
                        {
                            DBPropertyAttribute de = (DBPropertyAttribute)customAttributes.FirstOrDefault(x => x.GetType().Name == APIConstants.DBPropertyAttribute);
                            if (de != null && !string.IsNullOrEmpty(de.DBElementName))
                                parameterName = de.DBElementName;
                            else
                                parameterName = "@" + memberName;
                            if (propertyName.DeclaringType.Name != APIConstants.ServiceRequestBase)
                            {
                                if (de != null && !de.IgnoreProperty)
                                    sqlParameter = AddParameters(dbType, parameterName, value, de.IgnoreDefaultValue);
                                if (sqlParameter != null)
                                    parameters.Add(sqlParameter);
                            }
                            else if (propertyName.DeclaringType.Name == APIConstants.ServiceRequestBase)
                            {


                            }
                        }
                        else
                        {
                            if (propertyName.DeclaringType.Name != APIConstants.ServiceRequestBase)
                            {
                                sqlParameter = AddParameters(dbType, "@" + memberName, value);
                                if (sqlParameter != null)
                                {
                                    parameters.Add(sqlParameter);
                                }
                            }
                        }
                    }
                }
            }



            return parameters.ToArray();
        }




    }
}