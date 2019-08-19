using System;
using System.Collections.Generic;
using System.Linq;

namespace Findo.Framework.Interface.Interface
{
    /// <summary>
    /// Entity Framework IRepository interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>
          where T : class
    {
        /// <summary>
        /// Inserts the object.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void InsertObject(T entity);

        /// <summary>
        /// Updates the object.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void UpdateObject(T entity);

        /// <summary>
        /// Deletes the object.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void DeleteObject(T entity);

        /// <summary>
        ///  Get a single record by Id.
        /// </summary>
        /// <typeparam name="TKey">The record Id type, could be and integer, string or other class.</typeparam>
        /// <param name="id">The id value</param>
        /// <returns>Instance of the entity with the given Id</returns>
        T Get<TKey>(params TKey[] id);

        /// <summary>
        ///  Get a single record by Id.
        /// </summary>
        /// <typeparam name="TKey">The record Id type, could be and integer, string or other class.</typeparam>
        /// <param name="id">The id value</param>
        /// <returns>Instance of the entity with the given Id</returns>
        IQueryable<T> Get();

        /// <summary>
        /// Inserts an array of objects.
        /// </summary>
		/// <param name="entities">A list of entities.</param>
        void InsertObjectArray(IList<T> entities);

        /// <summary>
        /// Updates an array of objects.
        /// </summary>
		/// <param name="entities">A list of entities.</param>
		void UpdateObjectArray(IList<T> entities);

        /// <summary>
        /// Deletes an array of objects.
        /// </summary>
		/// <param name="entities">A list of entities.</param>
		void DeleteObjectArray(IList<T> entities);
        /// <summary>
        /// Get an array of objects by filter
        /// </summary>
        /// <param name="expression">function for filter</param>
        /// <returns>A Queryable of objects</returns>
        IQueryable<T> List(System.Linq.Expressions.Expression<Func<T, bool>> expression);
    }
}
