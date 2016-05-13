using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using ZCKT.DBHelpers;

namespace ZCKT.Infrastructure
{
    public class DefaultUnitOfWork : IUnitOfWork
    {
        private readonly List<object> localNewCollection = new List<object>();
        private readonly List<object> localModifiedCollection = new List<object>();
        private readonly List<object> localDeletedCollection = new List<object>();

        private bool isCompleteCalledBefore;
        private bool isDisposed;

        protected DBHelper dbContext;
        protected TransactionScope CurrentTransaction;

        public string Id { get; private set; }
        public UnitOfWorkOptions Options { get; private set; }

        public DefaultUnitOfWork(UnitOfWorkOptions options, DBHelper dbContext)
        {
            this.Id = Guid.NewGuid().ToString("N");
            this.dbContext = dbContext;
        }

        #region
        /// <summary>
        /// Registers a new object to the repository context.
        /// </summary>
        /// <param name="entity">The object to be registered.</param>
        public virtual void RegisterNew<TEntity>(TEntity entity) where TEntity : class
        {
            if (localModifiedCollection.Contains(entity))
                throw new InvalidOperationException("The object cannot be registered as a new object since it was marked as modified.");
            if (localNewCollection.Contains(entity))
                throw new InvalidOperationException("The object has already been registered as a new object.");
            localNewCollection.Add(entity);
        }
        /// <summary>
        /// Registers a modified object to the repository context.
        /// </summary>
        /// <param name="entity">The object to be registered.</param>
        public virtual void RegisterDirty<TEntity>(TEntity entity) where TEntity : class
        {
            if (localDeletedCollection.Contains(entity))
                throw new InvalidOperationException("The object cannot be registered as a modified object since it was marked as deleted.");
            if (!localModifiedCollection.Contains(entity) && !localNewCollection.Contains(entity))
                localModifiedCollection.Add(entity);
        }

        public void RegisterClean<TEntity>(TEntity entity) where TEntity : class
        {
            if (localModifiedCollection.Contains(entity))
                localModifiedCollection.Remove(entity);
            if (localDeletedCollection.Contains(entity))
                localDeletedCollection.Remove(entity);
        }

        /// <summary>
        /// Registers a deleted object to the repository context.
        /// </summary>
        /// <param name="entity">The object to be registered.</param>
        public virtual void RegisterDeleted<TEntity>(TEntity entity) where TEntity : class
        {
            if (localNewCollection.Contains(entity))
            {
                if (localNewCollection.Remove(entity))
                    return;
            }
            bool removedFromModified = localModifiedCollection.Remove(entity);
            bool addedToDeleted = false;
            if (!localDeletedCollection.Contains(entity))
            {
                localDeletedCollection.Add(entity);
                addedToDeleted = true;
            }
        }
        #endregion

        public void Commit()
        {
            try
            {
                if (this.isCompleteCalledBefore)
                    throw new InvalidOperationException("Complete is called before!");
                this.isCompleteCalledBefore = true;

                //Transactional
                if (Options.IsTransactional)
                {
                    this.CurrentTransaction = new TransactionScope(Options.Scope, new TransactionOptions
                    {
                        IsolationLevel = Options.IsolationLevel,
                        Timeout = Options.Timeout
                    });
                }

                //Core Process
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Rollback()
        {
        }

        public void Dispose()
        {
            if (this.isDisposed)
                return;

            this.isDisposed = true;

            if (this.CurrentTransaction != null)
                this.CurrentTransaction.Dispose();
        }
        
    }
}
