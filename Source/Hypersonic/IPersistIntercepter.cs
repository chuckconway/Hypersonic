﻿using System;
using Hypersonic.Session.Persistance;

namespace Hypersonic
{
    public interface IPersistIntercepter
    {
        /// <summary>
        /// Use expressions to create condition for the intercepter
        /// </summary>
        Func<string,bool> Condition { get; }

        Persist Intercept(Persist persist, ISession session);
    }
}
