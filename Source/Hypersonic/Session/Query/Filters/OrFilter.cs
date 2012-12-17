namespace Hypersonic.Session.Query.Filters
{
    public class OrFilter : Filter, IFilter
    {
        /// <summary> Constructor. </summary>
        /// <param name="query"> The query. </param>
        public OrFilter(string query) : base(query){}

        /// <summary> Gets the query. </summary>
        /// <returns> . </returns>
        public string Query()
        {
            return _query;
        }
    }
}
