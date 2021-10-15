namespace NetChatBoilerplate.Domain.SeedWork
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBaseRepository<T> where T : BaseEntity
    {
        IUnitOfWork UnitOfWork { get; }

        Task<T> FindByIdAsync(string id);

        Task<IReadOnlyList<T>> FindAllAsync();

        T Create(T entity);

        T Update(T entity);

        void Delete(T entity);
    }
}
