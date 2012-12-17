using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Snappy
{
    public class MsSqlData : IData
    {
        public void Save<T>(T item)
        {
            throw new NotImplementedException();
        }

        public void Save<T>(T item, Expression<Action<T>> filter)
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(Expression<Action<T>> filter)
        {
            throw new NotImplementedException();
        }

        public List<T> Select<T>(Expression<Action<T>> filter)
        {
            ReadOnlyCollection<ParameterExpression> parameters = filter.Parameters;
            Expression expression = filter.Body;
            
        }

        public List<T> Select<T>()
        {
            throw new NotImplementedException();
        }

        public List<T> Select<T>(string filter)
        {
            throw new NotImplementedException();
        }

        public List<T> Query<T>()
        {
            throw new NotImplementedException();
        }
    }
}
