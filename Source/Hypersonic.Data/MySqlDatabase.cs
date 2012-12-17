using Hypersonic.Core;
using MySql.Data.MySqlClient;

namespace Hypersonic.Data
{
    public class MySqlDatabase : DatabaseBase<MySqlConnection, MySqlCommand, MySqlParameter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MsSqlDatabase"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        public MySqlDatabase(string key) : base(key, null) {}


        /// <summary>
        /// Initializes a new instance of the <see cref="MsSqlDatabase"/> class.
        /// </summary>
        public MySqlDatabase()
        {
            CommandType = System.Data.CommandType.Text;
        }
    }
}