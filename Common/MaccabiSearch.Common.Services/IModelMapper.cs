namespace MaccabiSearch.Common.Services
{
    /// <summary>
    /// Contract of the Model Mapper
    /// </summary>
    /// <typeparam name="TSource">The type of the source to map from.</typeparam>
    /// <typeparam name="TTarget">The type of the target to map to.</typeparam>
    public interface IModelMapper<TSource, TTarget>
    {
        /// <summary>
        /// Initializes the target instance and maps the data to it from the source.
        /// </summary>
        /// <param name="source">The instance to map data from.</param>
        /// <returns>The mapped instance of the <typeparamref name="TTarget"/> type.</returns>
        TTarget Map(TSource source);

        /// <summary>
        /// Maps the data from the source instance into the target.
        /// </summary>
        /// <param name="source">The instance to map data from.</param>
        /// <param name="target">The instance to map data to.</param>
        /// <returns>The mapped instance of the <typeparamref name="TTarget"/> type.</returns>
        TTarget Map(TSource source, TTarget target);
    }
}
