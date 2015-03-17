using System;
using System.Collections.Generic;
using Republic.Core.Data.Interfaces;
using Republic.Core.Interfaces;

namespace Rhml.Mms.Data
{
    /// <summary> Encapsulates the Unit of Work pattern. 
    /// All repositories requested from the container will work within the same transaction.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")] // class is injected
    public sealed class WorkContainer : IUnitOfWork, IDisposable
    {
        private List<IRepositoryContext> _contexts;
        private IServiceLocator _service;
        private bool _isDisposed = false;


        /// <summary> Initializes a new instance of the <see cref="WorkContainer"/> class.
        /// </summary>
        /// <param name="service">The service locator used to find and manage the contained repositories</param>
        public WorkContainer(IServiceLocator service)
        {
            _service = service;
            _contexts = new List<IRepositoryContext>();
        }


        /// <summary> Retrieve the specified repository
        /// </summary>
        /// <typeparam name="T">The type of repository to retrieve</typeparam>
        /// <returns>The matching repository object</returns>
        /// <exception cref="System.InvalidOperationException">Cannot locate the repository requested</exception>
        public IRepository<T> GetRepository<T>() where T : class
        {
            var output = _service.Locate<IRepository<T>>();
            if (output == null)
            {
                throw new InvalidOperationException("Cannot locate the repository requested");
            }
            var work = output.GetContext();
            if (!_contexts.Contains(work))
            {
                _contexts.Add(work);
            }
            return output;
        }

        /// <summary> Save all current changes by any contained repository
        /// managed by the work container.
        /// </summary>
        public void Save()
        {
            using (var tran = new System.Transactions.TransactionScope(
                 System.Transactions.TransactionScopeOption.Required,
                 new System.Transactions.TransactionOptions()
                 {
                     IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                 }))
            {
                foreach (var work in this._contexts)
                {
                    work.Save();
                }
                tran.Complete();
            }
        }

        #region IDisposeable
        /// <summary>
        /// Disposes this instance.
        /// </summary>
        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }
            if (_contexts != null)
            {
                foreach (var context in _contexts)
                {
                    var disposeable = context as IDisposable;
                    if (disposeable != null)
                    {
                        disposeable.Dispose();
                    }
                }
            }

            _isDisposed = true;
        }
        #endregion
    }
}
