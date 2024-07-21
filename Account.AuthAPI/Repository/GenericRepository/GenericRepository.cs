using Account.AuthAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Account.AuthAPI.Repository.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _db;
        public GenericRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _db.Set<T>().ToListAsync().ConfigureAwait(false);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _db.Set<T>().FindAsync(id).ConfigureAwait(false);
        }

        public async Task AddAsync(T entity)
        {
            await _db.Set<T>().AddAsync(entity).ConfigureAwait(false);
        }

        public void Update(T entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(T entity)
        {
            _db.Set<T>().Remove(entity);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }

        public void Add(T entity)
        {
            _db.Set<T>().Add(entity);
        }

        public virtual IQueryable<T> Table
        {
            get
            {
                return _db.Set<T>();
            }
        }

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        public virtual IQueryable<T> TableNoTracking
        {
            get
            {
                return _db.Set<T>().AsNoTracking();
            }
        }
    }
}
