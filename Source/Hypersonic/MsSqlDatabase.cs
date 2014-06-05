using System.Data.SqlClient;
using Hypersonic.Core;

namespace Hypersonic
{
    /// <summary>
    /// 
    /// </summary>
    public class MsSqlDatabase : DatabaseBase<SqlConnection, SqlCommand, SqlParameter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MsSqlDatabase" /> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public MsSqlDatabase(HypersonicSettings settings) : base(settings) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsSqlDatabase"/> class.
        /// </summary>
        public MsSqlDatabase(){}
       
    }
}