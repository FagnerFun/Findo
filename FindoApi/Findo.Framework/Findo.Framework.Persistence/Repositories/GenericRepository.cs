using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Findo.Framework.Interface.Interface;

namespace Findo.Framework.Persistence.Repositories
{
    /// <summary>
    /// Entity Framework implementation of IRepository interface.
    /// </summary>
    public class GenericRepository<T> : IRepository<T>
        where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository{T}"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public GenericRepository(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        /// <summary>
        /// The unit of work.
        /// </summary>
        protected IUnitOfWork UnitOfWork { get; set; }

        #region [ Commom Methods ]

        /// <summary>
        /// Inserts the object.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void InsertObject(T entity)
        {
            var entityObj = UnitOfWork.GetDataContext<DbContext>().Set<T>();
            entityObj.Add(entity);

            if (UnitOfWork.IsInTransaction) return;


            UnitOfWork.GetDataContext<DbContext>().SaveChanges(true);
        }

        public IQueryable<T> Get()
        {

            IQueryable<T> result = UnitOfWork.GetDataContext<DbContext>().Set<T>();

            return result;
        }

        /// <summary>
        /// Inserts an array of objects.
        /// </summary>
        /// <param name="entities">A list of entities.</param>
        public void InsertObjectArray(IList<T> entities)
        {
            var entityObject = UnitOfWork.GetDataContext<DbContext>().Set<T>();
            entityObject.AddRange(entities);

            if (UnitOfWork.IsInTransaction) return;

            UnitOfWork.GetDataContext<DbContext>().SaveChanges(true);

        }

        /// <summary>
        /// Updates the object.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void UpdateObject(T entity)
        {
            if (UnitOfWork.GetDataContext<DbContext>().Entry(entity).State == EntityState.Detached)
            {
                var entityObj = UnitOfWork.GetDataContext<DbContext>().Set<T>();
                entityObj.Attach(entity);
                UnitOfWork.GetDataContext<DbContext>().Entry(entity).State = EntityState.Modified;
            }

            if (UnitOfWork.IsInTransaction) return;

            UnitOfWork.GetDataContext<DbContext>().SaveChanges(true);
        }

        /// <summary>
        /// Updates an array of objects.
        /// </summary>
        /// <param name="entities">A list of entities.</param>
        public void UpdateObjectArray(IList<T> entities)
        {
            foreach (var entity in entities)
            {
                if (UnitOfWork.GetDataContext<DbContext>().Entry(entity).State == EntityState.Detached)
                {
                    var entityObj = UnitOfWork.GetDataContext<DbContext>().Set<T>();
                    entityObj.Attach(entity);
                    UnitOfWork.GetDataContext<DbContext>().Entry(entity).State = EntityState.Modified;
                }
            }

            if (UnitOfWork.IsInTransaction) return;

            UnitOfWork.GetDataContext<DbContext>().SaveChanges(true);
        }

        /// <summary>
        /// Deletes the object.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void DeleteObject(T entity)
        {
            if (UnitOfWork.GetDataContext<DbContext>().Entry(entity).State == EntityState.Detached)
            {
                var entityObj = UnitOfWork.GetDataContext<DbContext>().Set<T>();
                entityObj.Attach(entity);
            }
            UnitOfWork.GetDataContext<DbContext>().Entry(entity).State = EntityState.Deleted;
            UnitOfWork.GetDataContext<DbContext>().Set<T>().Remove(entity);

            if (UnitOfWork.IsInTransaction) return;
            UnitOfWork.GetDataContext<DbContext>().SaveChanges();
        }

        /// <summary>
        /// Deletes an array of objects.
        /// </summary>
        /// <param name="entities">A list of entities.</param>
        public void DeleteObjectArray(IList<T> entities)
        {
            var entityObject = UnitOfWork.GetDataContext<DbContext>().Set<T>();

            foreach (var entity in entities)
            {
                if (UnitOfWork.GetDataContext<DbContext>().Entry(entity).State == EntityState.Detached)
                {
                    var entityObj = UnitOfWork.GetDataContext<DbContext>().Set<T>();
                    entityObj.Attach(entity);
                }
                UnitOfWork.GetDataContext<DbContext>().Entry(entity).State = EntityState.Deleted;
            }

            entityObject.RemoveRange(entities);

            if (UnitOfWork.IsInTransaction) return;

            UnitOfWork.GetDataContext<DbContext>().SaveChanges(true);
        }

        /// <summary>
        ///  Get a single record by Id.
        /// </summary>
        /// <typeparam name="TKey">The record Id type, could be and integer, string or other class.</typeparam>
        /// <param name="id">The id value</param>
        /// <returns>Instance of the entity with the given Id</returns>
        public T Get<TKey>(params TKey[] id)
        {
            var entityObj = UnitOfWork.GetDataContext<DbContext>().Set<T>();
            var result = entityObj.Find(id.Select(item => (object)item).ToArray());

            if (result != null)
                UnitOfWork.GetDataContext<DbContext>().Entry(result).State = EntityState.Detached;

            return result;
        }

        public IQueryable<T> List(Expression<Func<T, bool>> expression)
        {
            IQueryable<T> result = UnitOfWork.GetDataContext<DbContext>().Set<T>().Where(expression);

            return result;
        }
        #endregion
    }
}
