using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;

namespace Hypersonic.Session.Query.Expressions
{
    internal class QuotifyValues
    {

        /// <summary>
        /// Checks for SQL injection.
        /// </summary>
        /// <param name="userInput">The user input.</param>
        /// <returns>Boolean.</returns>
        private static bool CheckForSqlInjection(string userInput)
        {
            bool isSqlInjection = false;
            string[] sqlCheckList = { "--",";--",";","/*", "*/", "@@", "@", "char", "nchar","varchar", "nvarchar", "alter", "begin", "cast", "create", "cursor", "declare",
                                       "delete", "drop", "end", "exec", "execute", "fetch", "insert", "kill", "select", "sys", "sysobjects", "syscolumns", "table", "update"
                                       };
            var checkString = userInput.Replace("'", "''");

            for (var i = 0; i <= sqlCheckList.Length - 1; i++)
            {
                if ((checkString.IndexOf(sqlCheckList[i], StringComparison.OrdinalIgnoreCase) >= 0))
                {
                    isSqlInjection = true;
                }
            }

            return isSqlInjection;
        }


        //public string Quotify(object value)
        //{
        //    //if the value is null, then set the string value to null, otherwise the value is rendered as an empty string. In some cases that might be intentional. 
        //    value = value ?? "null";
        //    return QuotifyText();
        //}

        /// <summary>
        /// Quotifies the text.
        /// </summary>
        /// <param name="val">The val.</param>
        /// <returns></returns>
        public string Quotify(object val)
        {
            //if the value is null, then set the string value to null, otherwise the value is rendered as an empty string. In some cases that might be intentional.
            var stringValue = (val == null ? "null" : Convert.ToString(val));

            var checkForSqlInjection = CheckForSqlInjection(stringValue);

            if (checkForSqlInjection)
            {
                throw new SecurityException(string.Format("Potential SQL inject detected. -- {0}", stringValue));
            }

            var types = NumberTypes();
            bool isNumber = types.Any(s => s == val.GetType());

            //escape single quotes
            string escapedValue = stringValue.Replace("'", "''");

            //1. numbers are not quoted.
            //2. All byte arrays start with 0x
            if (!isNumber && !escapedValue.StartsWith("0x"))
            {
                escapedValue = "'" + escapedValue + "'";
            }

            return escapedValue;
        }

        private static IEnumerable<Type> NumberTypes()
        {
            return new[]
            {
                typeof(int),
                typeof(byte),
                typeof(sbyte),
                typeof(sbyte),
                typeof(decimal),
                typeof(double),
                typeof(float),
                typeof(uint),
                typeof(long),
                typeof(uint),
                typeof(long),
                typeof(ulong),
                typeof(short),
                typeof(ushort)

            };
        }
    }
}
