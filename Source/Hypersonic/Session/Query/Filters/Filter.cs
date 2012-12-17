namespace Hypersonic.Session.Query.Filters
{
    public abstract class Filter
    {
        protected readonly string _query;

        /// <summary> Specialised constructor for use only by derived classes. </summary>
        /// <param name="query"> The query. </param>
        protected Filter(string query)
        {
            _query = query;
        }
    }
}
