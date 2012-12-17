using System.Data;

namespace Hypersonic.Session
{
    public class Transaction: ITransaction
    {
        private readonly IDatabase _database;
        private IDbTransaction _transaction;

        /// <summary>   Constructor. </summary>
        ///
        /// <param name="database"> The database. </param>
        public Transaction(IDatabase database)
        {
            _database = database;
        }

        /// <summary>   Dispose of this object, cleaning up any resources it uses. </summary>
        public void Dispose()
        {
            _transaction.Dispose();
        }

        /// <summary>   Begins the transaction with the selected isolation level. </summary>
        ///
        /// <param name="isoLationLevel">   The isolation level of the transaction. </param>
        public void Begin(IsolationLevel isoLationLevel)
        {
            _transaction = _database.BeginTransaction(isoLationLevel);
        }

        /// <summary>Commits the current transaction. It does not close the connection. </summary>
        public void Commit()
        {
            _transaction.Commit();
        }

        /// <summary>Roll back the current transaction. </summary>
        public void Rollback()
        {
            _transaction.Rollback();
        }

        /// <summary>   Gets the connection. </summary>
        ///
        /// <value> The connection. </value>
        public IDbConnection Connection
        {
            get { return _transaction.Connection; }
        }

        /// <summary>   Gets the isolation level. </summary>
        ///
        /// <value> The isolation level. </value>
        public IsolationLevel IsolationLevel
        {
            get { return _transaction.IsolationLevel; }
        }
    }
}
