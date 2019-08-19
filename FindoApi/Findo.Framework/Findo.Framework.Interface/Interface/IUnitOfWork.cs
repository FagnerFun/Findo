using System;

namespace Findo.Framework.Interface.Interface
{
    /// <summary>
    /// Contract for unit of work classes.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {

        /// <summary>
        /// Check if unit of work is within a open transaction.
        /// </summary>
        bool IsInTransaction { get; }

        /// <summary>
        /// Unit of work timeout.
        /// </summary>
        Int32 Timeout { get; }

        /// <summary>
        /// Begins a transaction.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Commit all changes.
        /// </summary>
        void Commit(bool committableTransaction);

        /// <summary>
        /// Revert all changes.
        /// </summary>
        void Rollback();

        /// <summary>
        /// Sets the unit of work timeout.
        /// </summary>
        /// <param name="value">The value of the timer.</param>
        void SetTimeout(Int32 value);

        /// <summary>
        /// Property to retrieve managed context by the UnitOfWork.
        /// </summary>
        /// <typeparam name="D">The data context desired type.</typeparam>
        /// <returns>Returns the data context in the desired type.</returns>
        D GetDataContext<D>() where D : class;

    }
}
