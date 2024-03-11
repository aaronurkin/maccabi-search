using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MaccabiSearch.Domain.Services.Implementations
{
    /// <summary>
    /// Basic implementation of the generic repository.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity the repository interacting with.</typeparam>
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private readonly MaccabiSearchDbContext context;

        public RepositoryBase(MaccabiSearchDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Inserts provided instances into the database table.
        /// </summary>
        /// <param name="insertValues">Values to be inserted.</param>
        /// <returns>Inserted values.</returns>
        public virtual async Task<IEnumerable<TEntity>> Create(IEnumerable<TEntity> insertValues)
        {
            var entities = insertValues.Select(value =>
            {
                var entry = context.Entry(value);
                entry.State = EntityState.Added;
                return entry.Entity;
            });
            context.Set<TEntity>().AddRange(entities);
            await context.SaveChangesAsync();
            return entities;
        }

        /// <summary>
        /// Retrieves data filtered by <paramref name="predicate"/>
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>Found entities collection.</returns>
        public virtual Task<IEnumerable<TEntity>> Fetch(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = context.Set<TEntity>()
                .AsNoTracking()
                .Where(predicate)
                .AsEnumerable();
            return Task.FromResult(entities);
        }

        /// <summary>
        /// Updates data filtered by <paramref name="predicate"/> with values provided within the <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity">Values to update found entities with.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>Updated values.</returns>
        public virtual async Task<IEnumerable<TEntity>> Update(TEntity entity, Expression<Func<TEntity, bool>> predicate)
        {
            var existingEntities = await Fetch(predicate);
            var entities = existingEntities.Select(existing =>
            {
                var entry = context.Entry(existing);

                entry.CurrentValues.SetValues(entity);
                entry.State = EntityState.Modified;

                return entry.Entity;
            });

            context.Set<TEntity>().UpdateRange(entities);

            await context.SaveChangesAsync();
            return entities;
        }

        /// <summary>
        /// Deletes entries filtered by <paramref name="predicate"/>
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        public virtual async Task Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var existingEntities = await Fetch(predicate);
            var entities = existingEntities.Select(existing =>
            {
                var entry = context.Entry(existing);
                entry.State = EntityState.Deleted;
                return entry.Entity;
            });
            context.Set<TEntity>().RemoveRange(entities);
            await context.SaveChangesAsync();
        }
    }
}
