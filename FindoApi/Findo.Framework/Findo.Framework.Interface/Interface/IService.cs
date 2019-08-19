using Findo.Framework.Interface.Enum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Findo.Framework.Interface.Interface
{
    public interface IService<T> : IDisposable where T : class
    {

        /// <summary>
        /// Handles business logic to create a entity.
        /// </summary>
        /// <returns>Retrieve the entity</returns>
        IQueryable<T> Retrieve();

        /// <summary>
        /// Handles business logic to create a entity.
        /// </summary>
        /// <typeparam name="TKey">Entity key type.</typeparam>
        /// <param name="id">Entity identifier.</param>
        /// <returns>Retrieve the entity</returns>
        T Retrieve<TKey>(params TKey[] id);

        /// <summary>
        /// Handles business logic to create an entity.
        /// </summary>
        /// <param name="model">To be created</param>
        void Create(T model);

        /// <summary>
        /// Insert the specified list of model.
        /// </summary>
        /// <param name="entities">List of objects.</param>
        void CreateObjectArray(IList<T> entities);

        /// <summary>
        /// Handles business logic to update a entity.
        /// </summary>
        /// <param name="model">To be updated.</param>
        void Update(T model);

        /// <summary>
        /// Update the specified list of model.
        /// </summary>
        /// <param name="entities">List of objects.</param>
        void UpdateObjectArray(IList<T> entities);

        /// <summary>
        /// Handles business logic to delete a entity.
        /// </summary>
        /// <param name="model">To be deleted</param>
        void Delete(T model);

        /// <summary>
        /// Delete the specified list of model.
        /// </summary>
        /// <param name="entities">List of objects.</param>
        void DeleteObjectArray(IList<T> entities);

        /// <summary>
        /// Validates the state of the entity (The base returne always true).
        /// </summary>
        /// <param name="model">To be verified.</param>
        bool Validate(T model, OperationEnum operation);

        /// <summary>
        /// Validates the state of the list of entities (The base returne always true).
        /// </summary>
        /// <param name="models">List to be verified.</param>
        bool Validate(IList<T> models, OperationEnum operation);

    }
}
