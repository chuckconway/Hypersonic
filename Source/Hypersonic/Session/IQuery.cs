using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Hypersonic.Session.Query.Restrictions;

namespace Hypersonic.Session
{
    public interface IQuery<T> where T : class 
    {
        /// <summary>
        /// Queries the specified where.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where">The where.</param>
        /// <returns></returns>
        IQuery<T> Where(Expression<Func<T, bool>> @where);

        /// <summary>
        /// Queries the specified where.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where">The where.</param>
        /// <returns></returns>
        IQuery<T> Where(string @where);

        IQuery<T> And(Expression<Func<T, bool>> and);

        IQuery<T> And(string and);

        IQuery<T> Or(Expression<Func<T, bool>> or);

        IQuery<T> Or(params IRestriction[] restrictions);

        IQuery<T> Or(string or);

        IQuery<T> Like(Expression<Func<T, bool>> like);

        IQuery<T> Like(string like);

        IOrderBy<T> OrderBy(Expression<Func<T, object>> field);

        T Single();

        IList<T> List();
    }
}
