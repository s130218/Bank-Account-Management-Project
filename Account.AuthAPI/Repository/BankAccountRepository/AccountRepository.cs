using Account.AuthAPI.Data;
using Account.AuthAPI.Models.BankAccount;
using Account.AuthAPI.Repository.GenericRepository;


namespace Account.AuthAPI.Repository.Repository
{
    public class AccountRepository : GenericRepository<CustomerAccount>, IAccountRepository
    {
        public AccountRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

    }
}
