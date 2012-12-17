using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hypersonic
{
    public static class Constants
    {
        public static string WhereAlreadyCalledErrorMessage = "Where() has already been called during this session, use And() or Or().";

        public static string WhereMustBeCalledBeforeUsingAnd = "Where() must be called before using And().";

        public static string WhereMustBeCalledBeforeUsingOr = "Where() must be called before using Or().";
    }
}
