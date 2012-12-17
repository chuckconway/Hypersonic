using System.Data.SqlServerCe;
using Hypersonic.Core;

namespace Hypersonic.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class MsSqlCeDatabase : DatabaseBase<SqlCeConnection, SqlCeCommand, SqlCeParameter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MsSqlCeDatabase"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        public MsSqlCeDatabase(string key) : base(key) { }


        /// <summary>
        /// Initializes a new instance of the <see cref="MsSqlCeDatabase"/> class.
        /// </summary>
        public MsSqlCeDatabase()
        {
            CommandType = System.Data.CommandType.Text;
        }
    }
}