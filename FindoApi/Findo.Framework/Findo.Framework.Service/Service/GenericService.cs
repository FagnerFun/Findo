using Findo.Framework.Interface.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Findo.Framework.Interface.Enum;

namespace Findo.Framework.Service.Service
{
    /// <summary>   
    /// Abstract class implementing IMaintainable interface. Has necessary methods to
    /// execute a full CRUD operation.
    /// </summary>
    public abstract class GenericService<R, T> : IService<T>
        where R : IRepository<T>
        where T : class
    {
        public ILogger<GenericService<R, T>> logger;

        /// <summary>
        /// The unit of work.
        /// </summary>
        protected IUnitOfWork unitOfWork;

        /// <summary>
        /// The repository.
        /// </summary>
        public IRepository<T> repository;

        /// <summary>
        /// The service error create message.
        /// </summary>
        public const string ServiceErrorCreate = "SERVICE_ERROR_CREATE";

        /// <summary>
        /// The service error retrieve message.
        /// </summary>
        public const string ServiceErrorRetrieve = "SERVICE_ERROR_RETRIEVE";

        /// <summary>
        /// The service error update message.
        /// </summary>
        public const string ServiceErrorUpdate = "SERVICE_ERROR_UPDATE";

        /// <summary>
        /// The service error delete message.
        /// </summary>
        public const string ServiceErrorDelete = "SERVICE_ERROR_DELETE";

        /// <summary>
        /// The service error null model message.
        /// </summary>
        public const string ServiceErrorNullModel = "SERVICE_ERROR_NULL_MODEL";

        /// <summary>
        /// The service error null model message.
        /// </summary>
        public const string ServiceErrorNullArray = "SERVICE_ERROR_NULL_ARRAY ";

        /// <summary>
        /// Initializes a new instance of the <see cref="MaintenanceService{R, T}"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <exception cref="System.ArgumentNullException">Unit of work couldn't create the repository.</exception>
        public GenericService(IUnitOfWork unitOfWork, IRepository<T> repository, ILogger<GenericService<R, T>> logger)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("The unit of work can't be null.");
            }
            if (repository == null)
            {
                throw new ArgumentNullException("Unit of work couldn't create the repository.");
            }
            this.unitOfWork = unitOfWork;
            this.repository = repository;
            this.logger = logger;
        }

        #region Create
        /// <summary>
        /// Action to be performed before executing Create method. Best place to add specific business logic.
        /// </summary>
        /// <param name="model">Entity in context.</param>
        protected virtual void BeforeCreate(T model)
        {
            this.CheckNull(model);
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public virtual void Create(T model)
        {

            var transactionStarterMethod = BeginTransaction();
            BeforeCreate(model);
            if (Validate(model, OperationEnum.Insert))
            {
                repository.InsertObject(model);
                AfterCreate(model);
                CommitTransaction(transactionStarterMethod);
            }
            else
            {
                //TODO: check the implementation.
            }

        }

        /// <summary>
        /// If there is some actions to be done after creation. This is the place.
        /// </summary>
        /// <param name="model">Entity in context.</param>
        protected virtual void AfterCreate(T model) { }

        /// <summary>
        /// Action to be performed before executing InsertObjectArray method. Best place to add specific business logic.
        /// </summary>
        /// <param name="entities">A list of entities.</param>
        protected virtual void BeforeCreateObjectArray(IList<T> entities)
        {
            this.CheckNull(entities);
        }

        /// <summary>
        /// Insert the specified list of model.
        /// </summary>
        /// <param name="entities">List of objects.</param>
        public virtual void CreateObjectArray(IList<T> entities)
        {

            var transactionStarterMethod = BeginTransaction();
            BeforeCreateObjectArray(entities);
            if (Validate(entities, OperationEnum.Insert))
            {
                repository.InsertObjectArray(entities);
                AfterCreateObjectArray(entities);
                CommitTransaction(transactionStarterMethod);
            }
            else
            {
                //TODO: check the implementation
            }

        }

        /// <summary>
        /// Action to be performed after executing InsertObjectArray method. Best place to add specific business logic.
        /// </summary>
        /// <param name="entities">A list of entities.</param>
        protected virtual void AfterCreateObjectArray(IList<T> entities)
        {
        }
        #endregion

        #region Retrieve

        /// <summary>
        /// Retrieves the specified unique identifier.
        /// </summary>
        /// <typeparam name="TKey">The type of the attribute key.</typeparam>
        /// <param name="keys">The TKey element keys.</param>
        /// <returns>Returns a model.</returns>
        /// <exception cref="System.ArgumentNullException">Id cannot be null</exception>
        public virtual T Retrieve<TKey>(params TKey[] keys)
        {
            T model = null;
            if (keys == null || keys.Length < 1)
            {
                throw new ArgumentNullException("The keys array must be different from null and have one or more keys.");
            }
            foreach (var key in keys)
            {
                if (key == null)
                {
                    throw new ArgumentNullException("One of the keys was null.");
                }
            }


            model = this.repository.Get(keys);
            return model;
        }

        /// <summary>
        /// Retrieves the specified unique identifier.
        /// </summary>
        /// <typeparam name="TKey">The type of the attribute key.</typeparam>
        /// <param name="keys">The TKey element keys.</param>
        /// <returns>Returns a model.</returns>
        /// <exception cref="System.ArgumentNullException">Id cannot be null</exception>
        public virtual IQueryable<T> Retrieve()
        {
            //string keyCache = $"Retrieve-{typeof(T).GUID}";

            return repository.Get();
        }

        #endregion

        #region Update

        /// <summary>
        /// Action to be performed before executing Update method. Best place to add specific business logic.
        /// </summary>
        /// <param name="model">Entity in context.</param>
        protected virtual void BeforeUpdate(T model)
        {
            this.CheckNull(model);
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public virtual void Update(T model)
        {
            var transactionStarterMethod = BeginTransaction();
            BeforeUpdate(model);
            if (Validate(model, OperationEnum.Update))
            {
                repository.UpdateObject(model);
                AfterUpdate(model);
                CommitTransaction(transactionStarterMethod);
            }
            else
            {
                //TODO: check the implementation
            }
        }

        /// <summary>
        /// If there is some actions to be done after update. This is the place.
        /// </summary>
        /// <param name="model">Entity in context.</param>
        protected virtual void AfterUpdate(T model) { }

        #endregion

        #region Delete

        /// <summary>
        /// Action to be performed before executing Delete method. Best place to add specific business logic.
        /// </summary>
        /// <param name="model">Entity in context.</param>
        protected virtual void BeforeDelete(T model)
        {
            this.CheckNull(model);
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public virtual void Delete(T model)
        {
            var transactionStarterMethod = BeginTransaction();
            BeforeDelete(model);
            if (Validate(model, OperationEnum.Delete))
            {
                repository.DeleteObject(model);
                AfterDelete(model);
                CommitTransaction(transactionStarterMethod);
            }
            else
            {
                //TODO: check the implementation
            }
        }

        /// <summary>
        /// If there is some actions to be done after delete. This is the place.
        /// </summary>
        /// <param name="model">Entity in context.</param>
        protected virtual void AfterDelete(T model) { }

        /// <summary>
        /// Action to be performed before executing Delete method. Best place to add specific business logic.
        /// </summary>
        /// <param name="models">Entity in context.</param>
        protected virtual void BeforeDelete(IList<T> models)
        {
            this.CheckNull(models);
        }

        /// <summary>
        /// Deletes an array of entities.
        /// </summary>
        /// <param name="entities">The entity list.</param>
        public virtual void DeleteObjectArray(IList<T> entities)
        {
            var transactionStarterMethod = BeginTransaction();
            if (entities.Count < 1)
                return;
            BeforeDelete(entities);
            if (Validate(entities, OperationEnum.Delete))
            {
                repository.DeleteObjectArray(entities);
                AfterDelete(entities);
                CommitTransaction(transactionStarterMethod);
            }
            else
            {
                //TODO: check the implementation
            }

        }

        /// <summary>
        /// If there is some actions to be done after delete. This is the place.
        /// </summary>
        /// <param name="models">Entity in context.</param>
        protected virtual void AfterDelete(IList<T> models) { }

        #endregion

        /// <summary>
        /// Validates the model (The base returne always true).
        /// </summary>
        /// <param name="model">Instance to be validated</param>
        public virtual bool Validate(T model, OperationEnum operation)
        {
            return true;
        }

        /// <summary>
        /// Validates the state of the list of entities (The base returne always true).
        /// </summary>
        /// <param name="models">List to be verified.</param>
        public virtual bool Validate(IList<T> models, OperationEnum operation)
        {
            return true;
        }

        /// <summary>
        /// Helper method to verify if the entity instance is null.
        /// </summary>
        /// <param name="list">The list of models to be verified.</param>
        protected void CheckNull(IList<T> list)
        {
            if (list == null)
                throw new ArgumentNullException("Array cannot be null", ServiceErrorNullArray);
        }

        /// <summary>
        /// Helper method to verify if the entity instance is null.
        /// </summary>
        /// <param name="model">To be verified.</param>
        protected void CheckNull(T model)
        {
            if (model == null)
                throw new ArgumentNullException("Entity cannot be null", ServiceErrorNullModel);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.repository = default(R);
            if (this.unitOfWork != null)
            {
                this.unitOfWork.Dispose();
            }
            this.unitOfWork = null;
        }

        /// <summary>
        /// Allows inhereted class to implement a meaning of beginning a new trasaction internally
        /// </summary>
        /// <returns></returns>
        protected virtual bool BeginTransaction()
        {
            if (!unitOfWork.IsInTransaction)
            {
                unitOfWork.BeginTransaction();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Allows inhereted class to implement a meaning of controlling when to commit a transaction internally
        /// </summary>
        /// <param name="committableTransaction">Indicates whether this transaction can be committed</param>
        protected virtual void CommitTransaction(bool committableTransaction)
        {
            unitOfWork.Commit(committableTransaction);
        }
        /// <summary>
        /// Update the specified list of model.
        /// </summary>
        /// <param name="entities">List of objects.</param>
        public void UpdateObjectArray(IList<T> entities)
        {
            var transactionStarterMethod = BeginTransaction();
            BeforeUpdateObjectArray(entities);
            if (Validate(entities, OperationEnum.Update))
            {
                repository.UpdateObjectArray(entities);
                AfterUpdateObjectArray(entities);
                CommitTransaction(transactionStarterMethod);
            }
            else
            {
                //TODO: check the implementation
            }
        }
        /// <summary>
        /// Action to be performed after executing UpdateObjectArray method. Best place to add specific business logic.
        /// </summary>
        /// <param name="entities">A list of entities.</param>
        public virtual void AfterUpdateObjectArray(IList<T> entities)
        {
        }
        /// <summary>
        /// Action to be performed before executing UpdateObjectArray method. Best place to add specific business logic.
        /// </summary>
        /// <param name="entities">A list of entities.</param>
        public virtual void BeforeUpdateObjectArray(IList<T> entities)
        {
        }
    }
}
