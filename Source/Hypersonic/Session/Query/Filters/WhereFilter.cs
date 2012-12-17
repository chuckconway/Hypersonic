namespace Hypersonic.Session.Query.Filters
{
    public class WhereFilter : Filter, IFilter
    {
        public WhereFilter(string query) : base(query){}

        /// <summary> Gets the query. </summary>
        /// <returns> . </returns>
        public string Query()
        {
            return _query;
        }
    }
}
