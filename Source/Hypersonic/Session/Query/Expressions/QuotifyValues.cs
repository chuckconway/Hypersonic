using System;

namespace Hypersonic.Session.Query.Expressions
{
    internal class QuotifyValues
    {
        public string Quotify(object value)
        {
            //if the value is null, then set the string value to null, otherwise the value is rendered as an empty string. In some cases that might be intentional. 
            value = value ?? "null";
            return QuotifyText(Convert.ToString(value));
        }

        /// <summary>
        /// Quotifies the text.
        /// </summary>
        /// <param name="val">The val.</param>
        /// <returns></returns>
        public string QuotifyText(string val)
        {
            decimal dec;
            bool isDecimal = Decimal.TryParse(val, out dec);

            //escape single quotes
           val = val.Replace("'", "''");

            //1. numbers are not quoted.
            //2. All byte arrays start with 0x
            if (!isDecimal && !val.StartsWith("0x"))
            {
                val = "'" + val + "'";
            }

            return val;
        }
    }
}
