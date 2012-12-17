namespace Hypersonic.Session.Query.Filters
{
    public class AndFilter : Filter, IFilter
    {
        /// <summary> Constructor. </summary>
        /// <param name="query"> The query. </param>
        public AndFilter(string query) : base(query) {}

        /// <summary> Gets the query. </summary>
        /// <returns> . </returns>
        public string Query()
        {
            return _query;
        }
    }
}
