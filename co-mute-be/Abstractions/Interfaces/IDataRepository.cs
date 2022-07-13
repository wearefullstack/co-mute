namespace co_mute_be.Abstractions.Interfaces
{
    public interface IDataRepository<T>
    {

        Task<List<T>> GetManyAsync(int skip, int limit);

        Task<T> GetByIdAsync(long id);

        Task<T> Create(T item);

        Task<T> Delete(long id);

        Task<T> Update(T update);

    }
}
