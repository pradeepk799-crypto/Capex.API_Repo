// <copyright file="Constants.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Capex.Infrastructure.Common
{
    /// <summary>
    /// Constants.
    /// </summary>
    public struct Constants
    {

        /// <summary>
        /// The environment.
        /// </summary>
        public const string Environment = "Environment";

        /// <summary>
        /// The region.
        /// </summary>
        public const string Region = "Region";

        

        /// <summary>
        /// The API version 1 0.
        /// </summary>
        public const string APIVersion10 = "1.0";

        /// <summary>
        /// The API version 1 1.
        /// </summary>
        public const string APIVersion11 = "1.1";

        /// <summary>
        /// The API version 2 0.
        /// </summary>
        public const string APIVersion20 = "2.0";

        /// <summary>
        /// The SVC login user.
        /// </summary>
        public const string SVCLoginUser = "bcssapi";

        
        

       
        /// <summary>
        /// The audit clientip.
        /// </summary>
        public const string AuditClientIp = "ClientIp";

        /// <summary>
        /// The audit source.
        /// </summary>
        public const string AuditSource = "Source";

        /// <summary>
        /// The audit forwardedip.
        /// </summary>
        public const string AuditForwardedIp = "ForwardedIp";

        /// <summary>
        /// The audit sessionid.
        /// </summary>
        public const string AuditSessionId = "SessionId";

        /// <summary>
        /// The audit service.
        /// </summary>
        public const string AuditService = "Service";

        /// <summary>
        /// The audit apiversion.
        /// </summary>
        public const string AuditAPIVersion = "APIVersion";

        /// <summary>
        /// The audit userid.
        /// </summary>
        public const string AuditUserId = "USERID";

       

        /// <summary>
        /// ConfigConstants.
        /// </summary>
        public struct ConfigConstants
        {
            /// <summary>
            /// The database connection string.
            /// </summary>
            public const string DBConnectionString = "DBConnectionString";

            /// <summary>
            /// The domain name.
            /// </summary>
            public const string DomainName = "DomainName";
        }

        /// <summary>
        /// DBcodeValues.
        /// </summary>
        public struct DBcodeValues
        {
            // Short.MinValue is used to use this null value for short also.

            /// <summary>
            /// The null int.
            /// </summary>
            public const int NullInt = short.MinValue; // To be used to represent null / unassigned int

            /// <summary>
            /// The null byte.
            /// </summary>
            public const int NullByte = byte.MinValue; // To be used to represent null

            /// <summary>
            /// The null byte array.
            /// </summary>
            public const byte[] NullByteArray = null;

            /// <summary>
            /// The yes string.
            /// </summary>
            public const string YesString = "Y";

            /// <summary>
            /// The no string.
            /// </summary>
            public const string NoString = "N";

            /// <summary>
            /// The null bool.
            /// </summary>
            public const bool NullBool = false; // To be used to represent null

            /// <summary>
            /// The null date.
            /// </summary>
            public static DateTime NullDate = DateTime.MinValue; // To be used to represent null date

            /// <summary>
            /// The dt picker minimum date.
            /// </summary>
            public static DateTime DatePickerMinDate = new DateTime(1753, 1, 1);

            /// <summary>
            /// The null float.
            /// </summary>
            public static float NullFloat = float.MinValue;

            /// <summary>
            /// The null decimal.
            /// </summary>
            public static decimal NullDecimal = decimal.MinValue;
        }

        /// <summary>
        /// General.
        /// </summary>
        public struct General
        {
            /// <summary>
            /// The zero.
            /// </summary>
            public const string Zero = "0";

            /// <summary>
            /// All.
            /// </summary>
            public const string All = "All";

            /// <summary>
            /// The yes.
            /// </summary>
            public const string Yes = "Yes";

            /// <summary>
            /// The no.
            /// </summary>
            public const string No = "No";

            /// <summary>
            /// The minus one.
            /// </summary>
            public const string MinusOne = "-1";

            /// <summary>
            /// The one.
            /// </summary>
            public const string One = "1";

            /// <summary>
            /// The request status change action identifier.
            /// </summary>
            public const string RequestStatusChangeActionId = "24";

            /// <summary>
            /// The date format string.
            /// </summary>
            public const string DateFormatString = "{0:MM/dd/yyyy}";

            /// <summary>
            /// The no data text.
            /// </summary>
            public const string NoDataText = "No rows found...";

            /// <summary>
            /// The sp executed ok.
            /// </summary>
            public const string SPExecutedOk = "1001";
        }

        /// <summary>
        /// ProjectSetUpConstants.
        /// </summary>
        public struct ProjectSetUpConstants
        {
            /// <summary>
            /// The no result found.
            /// </summary>
            public const string NoResultFound = "No Result Found";
        }
        public static class UserCredential
        {
            public const string SubscriberPassword = "Test123!";
        }


        
    }

    /// <summary>
    /// AsTableValuedParameterExtension.
    /// </summary>
    public static class AsTableValuedParameterExtension
    {
        /// <summary>
        /// This extension converts an enumerable set to a Dapper TVP.
        /// </summary>
        /// <typeparam name="T">type of enumerbale.</typeparam>
        /// <param name="enumerable">list of values.</param>
        /// <param name="typeName">database type name.</param>
        /// <param name="orderedColumnNames">if more than one column in a TVP,
        /// columns order must mtach order of columns in TVP.</param>
        /// <returns>a custom query parameter.</returns>
        public static SqlMapper.ICustomQueryParameter AsTableValuedParameter<T>(
            this IEnumerable<T> enumerable, string typeName, IEnumerable<string> orderedColumnNames = null)
        {
            var dataTable = new DataTable();
            if (typeof(T).IsValueType || typeof(T).FullName.Equals("System.string"))
            {
                dataTable.Columns.Add(
                    orderedColumnNames == null ?
                    "NONAME" : orderedColumnNames.First(), typeof(T));
                foreach (T obj in enumerable)
                {
                    dataTable.Rows.Add(obj);
                }
            }
            else
            {
                PropertyInfo[] properties = typeof(T).GetProperties(
                    BindingFlags.Public | BindingFlags.Instance);
                PropertyInfo[] readableProperties = properties.Where(
                    w => w.CanRead).ToArray();
                if (readableProperties.Length > 1 && orderedColumnNames == null)
                    throw new ArgumentException("Ordered list of column names must be provided when TVP contains more than one column");

                var columnNames = (orderedColumnNames ??
                    readableProperties.Select(s => s.Name)).ToArray();
                for (int i = 0; i < columnNames.Length; i++)
                {
                    string name = columnNames[i];
                    dataTable.Columns.Add(name, readableProperties.Single(s => s.Name.Equals(name)).PropertyType);
                }

                foreach (T obj in enumerable)
                {
                    dataTable.Rows.Add(
                        columnNames.Select(s => readableProperties.Single(s2 => s2.Name.Equals(s)).GetValue(obj))
                            .ToArray());
                }
            }

            return dataTable.AsTableValuedParameter(typeName);
        }
    }


    
    
    public static class UserStatus
    {
        public const int UserActive = 0;
        public const int UserLock = 1;
    }
}
