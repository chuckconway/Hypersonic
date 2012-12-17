using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hypersonic.Session.Query.Expressions
{
    public abstract class ExpressionBase 
    {
        /// <summary> Replace '= null' with IS NULL. </summary>
        /// <param name="val">         The value. </param>
        /// <param name="twoFromTail"> The two from tail. </param>
        /// <param name="state">       The state. </param>
        /// <returns> . </returns>
        protected string ReplaceEqualsNullWithIsNull(string val, int twoFromTail, StringBuilder state)
        {
            if (val.Contains("null"))
            {
                //remove hanging equals sign
                state.Remove(twoFromTail, 2);
                val = "IS NULL";
            }

            return val;
        }

        /// <summary> Gets a value. </summary>
        /// <param name="state"> The state. </param>
        /// <param name="eval">  The eval. </param>
        /// <returns> The value. </returns>
        protected string GetRightHandValue(StringBuilder state, Func<char, string> eval)
        {
            const int skipBlankSpace = 2;

            //Length is 1 based, index is 0 based. Then skip one more space for the extra space to see if this is on the right-side of an equal-sign.
            int twoFromTail = (state.Length > 2 ? state.Length - skipBlankSpace : 0);

            char c = state[twoFromTail];

            string val = eval(c);
            return ReplaceEqualsNullWithIsNull(val, twoFromTail, state);
        }
    }
}
