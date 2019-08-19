using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading;
using Findo.Framework.Interface.Interface;

namespace Findo.Framework.Persistence.UnitOfWork
{
    /// <summary>
    /// Manages a specific type of ObjectContext. Takes care of its creation, transactions
    /// and disposal. Also takes care of instantiation of the managed repositories.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UnitOfWork<T> : IUnitOfWork
        where T : DbContext
    {
        /// <summary>
        /// Held Object context.
        /// </summary>
        private DbContext _dataContext;

        /// <summary>
        /// Live transaction.
        /// </summary>
        private IDbContextTransaction _transaction { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public UnitOfWork()
        {
            StartTimer();
            _dataContext = (T)Activator.CreateInstance(typeof(T));
        }

        /// <summary>
        /// Unit of work timeout.
        /// </summary>
        public Int32 Timeout { get; private set; }

        /// <summary>
        /// Internal timer used to keep the time.
        /// </summary>
        private Timer CurrentTimer { get; set; }

        /// <summary>
        /// Property to retrieve managed context by the UnitOfWork.
        /// </summary>
        /// <returns>The data context.</returns>
        public DbContext GetDataContext()
        {
            if (_dataContext != null) return _dataContext;

            _dataContext = (T)Activator.CreateInstance(typeof(T));
            StartTimer();
            return _dataContext;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is information transaction.
        /// </summary>
        /// <value><c>true</c> if this instance is information transaction; otherwise, <c>false</c>.</value>
        //public bool IsInTransaction => _transaction != null;

        public bool IsInTransaction
        {
            get { return _transaction != null; }
        }

        /// <summary>
        /// Property to retrieve managed context by the UnitOfWork.
        /// </summary>
        /// <typeparam name="D">The data context desired type.</typeparam>
        /// <returns>Returns the data context in the desired type.</returns>
        public D GetDataContext<D>() where D : class
        {
            return _dataContext as D;
        }

        /// <summary>
        /// Commits this instance.
        /// </summary>
        public void Commit(bool committableTransaction)
        {
            if (_transaction == null) return;

            _dataContext.SaveChanges();

            if (committableTransaction)
            {
                _transaction.Commit();
                _transaction = null;
            }
        }

        /// <summary>
        /// Rollbacks this instance.
        /// </summary>
        public void Rollback()
        {
            if (_transaction == null) return;

            _transaction.Rollback();
            _transaction = null;

            if (_dataContext == null) return;

            if (_dataContext.Database.GetDbConnection() != null && _dataContext.Database.GetDbConnection().State == System.Data.ConnectionState.Open)
            {
                _dataContext.Database.CloseConnection();
            }

            _dataContext.Dispose();

            _dataContext = null;
        }

        /// <summary>
        /// Begins the transaction.
        /// </summary>
        public void BeginTransaction()
        {
            if (_transaction != null) return;

            if (_dataContext == null) _dataContext = GetDataContext();

            if (_dataContext.Database.GetDbConnection().State == System.Data.ConnectionState.Closed)
            {
                _dataContext.Database.OpenConnection();
            }

            _transaction = _dataContext.Database.BeginTransaction();
        }

        /// <summary>
        /// Disposes this instance.
        /// </summary>
        public void Dispose()
        {
            if (CurrentTimer != null)
                CurrentTimer.Dispose();

            Rollback();

            if (_dataContext == null) return;

            try
            {
                if (_dataContext.Database.GetDbConnection() != null && _dataContext.Database.GetDbConnection().State == System.Data.ConnectionState.Open)
                {
                    _dataContext.Database.CloseConnection();
                }
                _dataContext.Dispose();
                _dataContext = null;
            }
            catch (ObjectDisposedException)
            {
                _dataContext = null;
            }
        }

        #region [ Timeout ]

        /// <summary>
        /// Sets the unit of work timeout.
        /// </summary>
        /// <param name="value">The value of the timer.</param>
        public void SetTimeout(Int32 value)
        {
            Timeout = value;

            if (value <= 0)
            {
                throw new ArgumentException("Invalid timeout");
            }
            if (CurrentTimer != null)
                CurrentTimer.Change(value, System.Threading.Timeout.Infinite);
        }

        /// <summary>
        /// Run when the unit of work is created.
        /// </summary>
        private void StartTimer()
        {
            if (Timeout == default(Int32))
            {
                Timeout = 60000;
            }
            if (CurrentTimer != null)
                CurrentTimer.Dispose();

            CurrentTimer = new Timer(OnTimeout, null, Timeout, System.Threading.Timeout.Infinite);
        }

        /// <summary>
        /// Runs when the timer finishes.
        /// </summary>
        /// <param name="state">The timer state.</param>
        private void OnTimeout(Object state)
        {
            var backup = _dataContext;
            Dispose();
            _dataContext = backup;
        }

        #endregion [ Timeout ]
    }
}
