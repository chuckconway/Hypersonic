using System.Data.SQLite;
using Hypersonic.Core;

namespace Hypersonic.SqLite
{
    public class SQLiteDatabase : DatabaseBase<SQLiteConnection, SQLiteCommand, SQLiteParameter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteDatabase"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        public SQLiteDatabase(string key) : base(key, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteDatabase"/> class.
        /// </summary>
        public SQLiteDatabase()
        {
            CommandType = System.Data.CommandType.Text;
        }
    }
}
