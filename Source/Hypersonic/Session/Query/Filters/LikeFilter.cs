namespace Hypersonic.Session.Query.Filters
{
    public class LikeFilter : Filter, IFilter
    {
        /// <summary> Constructor. </summary>
        /// <param name="query"> The query. </param>
        public LikeFilter(string query) : base(query){}

        /// <summary> Gets the query. </summary>
        /// <returns> . </returns>
        public string Query()
        {
            string sql = _query;

            if(_query.Contains("="))
            {
               sql = _query.Replace("=", "LIKE");
            }

            if (_query.Contains("<>"))
            {
                sql = _query.Replace("<>", "NOT LIKE");
            }

            return sql;
        }
    }
}
