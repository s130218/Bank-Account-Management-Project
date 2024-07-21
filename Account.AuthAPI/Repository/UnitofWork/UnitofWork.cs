using Account.AuthAPI.Data;

namespace Account.AuthAPI.Repository.UnitofWork
{
    public class UnitofWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public UnitofWork(ApplicationDbContext db)
        {
            _db = db;
        }

        public void CommitChanges()
        {
            _db.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

    }
}
