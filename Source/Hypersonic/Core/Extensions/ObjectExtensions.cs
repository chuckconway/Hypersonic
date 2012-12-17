using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hypersonic.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsCollection(this object obj)
        {
            Type type = obj.GetType();

            bool isCollection = (typeof(ICollection).IsAssignableFrom(type) || typeof(ICollection<>).IsAssignableFrom(type));
            return isCollection;
        }
    }
}
