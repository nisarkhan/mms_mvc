using Republic.Core.Data.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Rhml.Mms.Data
{
    /// <summary>
    /// An Entity Framwork implementation of the <see cref="IRepository&lt;T&gt;"/> contract.
    /// </summary>
    /// <typeparam name="T">The type of model.</typeparam>
    public class DbSetRepository<T> : IRepository<T> where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DbSetRepository&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="context">The associated <see cref="DbContext"/>.</param>
        public DbSetRepository(MmsModels context)
        {
            // The context argument must be specified.
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            // Hold onto argument values.
            this.DbContext = context;

            // Get a db set for the given model type.
            this.DbSet = context.Set<T>();
        }

        /// <summary>
        /// Gets the context of the <see cref="DbSetRepository&lt;T&gt;"/>.
        /// </summary>
        protected DbContext DbContext { get; private set; }

        /// <summary>
        /// Gets the db set of the <see cref="DbSetRepository&lt;T&gt;"/>.
        /// </summary>
        protected DbSet<T> DbSet { get; private set; }

        #region IRepository<T> Members

        /// <summary> 
        /// Finds an entity with the given primary key values.  If an entity with the
        /// given primary key values exists in the context, then it is returned immediately
        /// without making a request to the store. Otherwise, a request is made to the
        /// store for an entity with the given primary key values and this entity, if
        /// found, is attached to the context and returned. If no entity is found in
        /// the context or the store, then null is returned.
        /// </summary>
        /// <param name="keyValues">The values of the primary key for the entity to be found.</param>
        /// <returns>The entity found, or null.</returns>
        public T GetByKey(params object[] keyValues)
        {
            // Delegate responsibility to the db set.
            return this.DbSet.Find(keyValues);
        }

        /// <summary> 
        /// Inserts a new item (entity) into the collection.  This collection is held by the repository.
        /// </summary>
        /// <param name="entity">The new item of type <typeparamref name="T"/> to insert into the collection.</param>
        /// <returns>The new item of type <typeparamref name="T"/>.</returns>
        public T Insert(T entity)
        {
            // Get the state of the entity.
            var entry = this.DbContext.Entry(entity);

            // Base action on the entity's detached state.
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                entity = this.DbSet.Add(entity);
            }

            // Return the entity to the caller.
            return entity;
        }

        /// <summary> 
        /// Updates an existing item (entity) in the collection.  This collection is held by the repository.
        /// </summary>
        /// <param name="entity">The existing item of type <typeparamref name="T"/> to update in the collection.</param>
        /// <returns>The updated item of type <typeparamref name="T"/>.</returns>
        public T Update(T entity)
        {
            // Get the state of the entity.
            var entry = this.DbContext.Entry(entity);

            // Attach the entity if not already.
            if (entry.State == EntityState.Detached)
            {
                entity = this.DbSet.Attach(entity);
            }

            // Always change the modification state of the entity.
            entry.State = EntityState.Modified;

            // Return the entity to the caller.
            return entity;
        }

        /// <summary> 
        /// Deletes an existing item (entity) in the collection.  This collection is held by the repository.
        /// </summary>
        /// <param name="entity">The existing item of type <typeparamref name="T"/> to delete from the collection.</param>
        /// <returns>The deleted item of type <typeparamref name="T"/>.</returns>
        public T Delete(T entity)
        {
            // Get the state of the entity.
            var entry = this.DbContext.Entry(entity);

            // Base action on the entity's deleted state.
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                entity = this.DbSet.Attach(entity);
                entity = this.DbSet.Remove(entity);
            }

            // Return the entity to the caller.
            return entity;
        }

        /// <summary> 
        /// Deletes an existing item (entity) in the collection.  This collection is held by the repository.
        /// </summary>
        /// <param name="keyValues">The values of the primary key for the entity to be deleted.</param>
        /// <returns>The deleted item of type <typeparamref name="T"/>.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the request item is not found.</exception>
        public T Delete(params object[] keyValues)
        {
            // Attempt to find the entity by it's key values.
            var entity = this.GetByKey(keyValues);
            if (entity == null)
            {
                throw new InvalidOperationException("The entity requesting to be deleted could not be found within the context.");
            }
            
            // Call overloaded method.
            return this.Delete(entity);
        }

        /// <summary> 
        /// Get the IRepositoryContext.  This is typically used so you can persist
        /// all changes managed by the associated unit of work for this repository.
        /// </summary>
        /// <returns>The repositories context that allows you to save changes</returns>
        public IRepositoryContext GetContext()
        {
            return this.DbContext as IRepositoryContext;
        }

        #endregion

        #region IQueryable Members

        /// <summary>
        /// Gets the type of the element(s) that are returned when the expression tree
        /// associated with this instance of <see cref="IQueryable"/> is executed.
        /// </summary>
        public Type ElementType
        {
            get { return this.DbSet.AsQueryable().ElementType; }
        }

        /// <summary>
        /// Gets the expression tree that is associated with the instance of <see cref="IQueryable"/>.
        /// </summary>
        public Expression Expression
        {
            get { return this.DbSet.AsQueryable().Expression; }
        }

        /// <summary>
        /// Gets the query provider that is associated with this data source.
        /// </summary>
        public IQueryProvider Provider
        {
            get { return this.DbSet.AsQueryable().Provider; }
        }

        #endregion

        #region IEnumerable<T> Members

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An <see cref="IEnumerator&lt;T&gt;" /> that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)this.DbSet).GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An <see cref="IEnumerator" /> that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)this.DbSet).GetEnumerator();
        }

        #endregion

        #region Object Contract

        /// <summary>
        /// Determines whether the specified <see cref="DbSetRepository&lt;T&gt;" /> is equal to the current <see cref="DbSetRepository&lt;T&gt;" />.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><b>true</b> if the specified <see cref="DbSetRepository&lt;T&gt;" /> is equal to the current <see cref="DbSetRepository&lt;T&gt;" />; otherwise, <b>false</b>.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj) { return this.DbSet.Equals(obj); }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>A hash code for the current <see cref="DbSetRepository&lt;T&gt;" />.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() { return this.DbSet.GetHashCode(); }

        /// <summary>
        /// Returns a <see cref="String"/> representation of the underlying query.
        /// </summary>
        /// <returns>The query string.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string ToString() { return this.DbSet.ToString(); }

        #endregion
    }
}
