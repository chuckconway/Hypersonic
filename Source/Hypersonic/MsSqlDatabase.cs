using System.Data;
using System.Data.SqlClient;
using Hypersonic.Core;

namespace Hypersonic
{
    /// <summary>
    /// 
    /// </summary>
    public class MsSqlDatabase : DatabaseBase<SqlConnection, SqlCommand, SqlParameter>
    {
        /// <summary> Initializes a new instance of the <see cref="MsSqlDatabase"/> class. </summary>
        /// <param name="key">              The key. </param>
        /// <param name="connectionString"> (optional) the connection string. </param>
        public MsSqlDatabase(string key = null, string connectionString = null) : base(key, connectionString) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsSqlDatabase"/> class.
        /// </summary>
        public MsSqlDatabase(){}

        /// <summary>
        /// Initializes a new instance of the <see cref="MsSqlDatabase"/> class.
        /// </summary>
        /// <param name="commandType">Type of the command.</param>
        public MsSqlDatabase(CommandType commandType) :base(commandType) { }

        
    }
}