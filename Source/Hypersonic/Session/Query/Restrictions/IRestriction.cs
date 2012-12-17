using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hypersonic.Session.Query.Restrictions
{
    public interface IRestriction
    {
        string Sql();
    }
}
