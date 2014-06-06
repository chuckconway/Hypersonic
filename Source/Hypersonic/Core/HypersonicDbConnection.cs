using System;
using System.Configuration;
using System.Data.Common;

namespace Hypersonic.Core
{
    public class HypersonicDbConnection<TConnection> : IDisposable where TConnection : DbConnection, new()
    {
        private readonly HypersonicSettings _settings;

        public DbConnection DbConnection { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is manual.
        /// </summary>
        /// <value><c>true</c> if this instance is manual; otherwise, <c>false</c>.</value>
        /// 
        public bool IsManual { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HypersonicDbConnection{TConnection}"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public HypersonicDbConnection(HypersonicSettings settings)
        {
            _settings = settings;
            DbConnection = GetConnection(settings);
        }

        /// <summary>
        /// Gets first connection in the connection section of the blog file.
        /// </summary>
        /// <returns>A non opened SqlConnection</returns>
        private DbConnection GetConnection(HypersonicSettings settings)
        {
            _settings.ConnectionString = settings.ConnectionString ?? string.Empty;

            if (string.IsNullOrEmpty(_settings.ConnectionString))
            {
                _settings.ConnectionString = GetConnectionString(settings.ConnectionStringName);
            }

            DbConnection connection = new TConnection { ConnectionString = _settings.ConnectionString };

            return connection;
        }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        private static ConnectionStringSettings GetSettings(string key)
        {
            ConnectionStringSettings settings = null;
            const int zero = 0;

            if (string.IsNullOrEmpty(key))
            {
                if (ConfigurationManager.ConnectionStrings.Count > zero)
                {
                    settings = ConfigurationManager.ConnectionStrings[zero];
                }
            }
            else
            {

                settings = ConfigurationManager.ConnectionStrings[key];
            }

            return settings;
        }

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        private static string GetConnectionString(string key)
        {
            ConnectionStringSettings settings = GetSettings(key);

            if (settings == null)
            {
                throw new Exception("Connection string is not set. <clear /> and set the first connection string in the connectionString section in the config OR provide a connection string name.");
            }

            string connectionString = settings.ConnectionString;
            return connectionString;
        }

        public void Dispose()
        {
            DbConnection.Close();
            DbConnection.Dispose();
        }
    }
}
