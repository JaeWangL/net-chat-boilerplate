namespace NetChatBoilerplate.Domain.SeedWork
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBaseRepository<T> where T : BaseEntity
    {
        IUnitOfWork UnitOfWork { get; }

        Task<T> FindByIdAsync(string id);

        Task<IReadOnlyList<T>> FindAllAsync();

        Task<T> CreateAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
}
