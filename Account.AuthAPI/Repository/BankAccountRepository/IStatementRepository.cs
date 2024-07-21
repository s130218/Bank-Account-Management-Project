using Account.AuthAPI.Models.BankAccount;
using Account.AuthAPI.Repository.GenericRepository;
using BankAccountAPI.Model;

namespace AccountManagement.API.Repository.BankAccountRepository
{
    public interface IStatementRepository : IGenericRepository<Statement>
    {
    }
}
