using System.Linq.Expressions;

namespace Account.AuthAPI.Repository.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();

        void Add(T entity);

        Task AddAsync(T entity);

        void Remove(T entity);

        void Update(T entity);

        void SaveChanges();

        IQueryable<T> Table { get; }

        IQueryable<T> TableNoTracking { get; }
    }
}
