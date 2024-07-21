namespace Account.AuthAPI.Repository.UnitofWork
{
    public interface IUnitOfWork : IDisposable
    {
        void CommitChanges();

        Task CommitAsync();
    }
}
