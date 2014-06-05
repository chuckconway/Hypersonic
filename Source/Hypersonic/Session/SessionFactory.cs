namespace Hypersonic.Session
{
    public class SessionFactory
    {
        /// <summary> Sql server. </summary>
        /// <param name="key">        (optional) the key. </param>
        /// <param name="connection"> The connection. </param>
        /// <returns> . </returns>
        public static ISession SqlServer(string key = null, string connection = null)
        {
            return new SqlServerSession(new MsSqlDatabase(key:key, connectionString:connection));
        }
    }
}
