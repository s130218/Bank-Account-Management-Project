using Account.AuthAPI.Data;
using Account.AuthAPI.Repository.GenericRepository;
using BankAccountAPI.Model;

namespace AccountManagement.API.Repository.BankAccountRepository
{
    public class StatementRepository: GenericRepository<Statement>, IStatementRepository
    {
        public StatementRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
