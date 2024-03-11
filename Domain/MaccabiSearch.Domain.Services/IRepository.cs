using System.Linq.Expressions;

namespace MaccabiSearch.Domain.Services
{
    /// <summary>
    /// Contract of the generic repository.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity the repository interacting with.</typeparam>
    public interface IRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Inserts provided instances into the database table.
        /// </summary>
        /// <param name="insertValues">Values to be inserted.</param>
        /// <returns>Inserted values.</returns>
        Task<IEnumerable<TEntity>> Create(IEnumerable<TEntity> insertValues);

        /// <summary>
        /// Retrieves data filtered by <paramref name="predicate"/>
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>Found entities collection.</returns>
        Task<IEnumerable<TEntity>> Fetch(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Updates data filtered by <paramref name="predicate"/> with values provided within the <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity">Values to update found entities with.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>Updated values.</returns>
        Task<IEnumerable<TEntity>> Update(TEntity entity, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Deletes entries filtered by <paramref name="predicate"/>
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        Task Delete(Expression<Func<TEntity, bool>> predicate);
    }
}
