namespace Hypersonic.Session.Persistence
{
    public class Persist
    {
        public Persist(string sql, object instance, string tableName)
        {
            Sql = sql;
            Instance = instance;
            TableName = tableName;
        }

        public string Sql { get; set; }

        public object Instance { get; set; }
        
        public string TableName { get; set; }
         
    }
}