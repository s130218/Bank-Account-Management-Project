using Account.AuthAPI.Models.BankAccount;
using Account.AuthAPI.Repository.GenericRepository;
using BankAccountAPI.Model;

namespace Account.AuthAPI.Repository.Repository
{
    public interface IAccountRepository : IGenericRepository<CustomerAccount>
    {

    }
}
