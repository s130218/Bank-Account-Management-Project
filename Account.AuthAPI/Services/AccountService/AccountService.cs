using Account.AuthAPI.Models.BankAccount;
using Account.AuthAPI.Repository.Repository;
using Account.AuthAPI.Repository.UnitofWork;
using Account.AuthAPI.Service.Common;

namespace BankAccountAPI.Services.Service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitofWork;

        public AccountService(IAccountRepository accountRepository, IUnitOfWork unitofWork)
        {
            _accountRepository = accountRepository;
            _unitofWork = unitofWork;
        }

        public async Task<ServiceResult<List<CustomerAccount>>> GetAllAsync()
        {
            var resp = await _accountRepository.GetAllAsync().ConfigureAwait(false);
            return new ServiceResult<List<CustomerAccount>>(true) 
            { Data = resp.ToList() };
        }

        public async Task<ServiceResult<CustomerAccount>> GetCustomerByIdAsync(int id)
        {
            var resp = _accountRepository.Table.FirstOrDefault(x => x.Id == id);
            if (resp == null)
                return ServiceResult<CustomerAccount>.Fail("Customer account is not available");

            return new ServiceResult<CustomerAccount>(true) { Data = resp };
        }


        public async Task<ServiceResult<CustomerAccount>> AddCustomerAccountAsync(CustomerAccount entity)
        {
            var isExist = _accountRepository.Table.Any(x => x.PhoneNumber == entity.PhoneNumber);
            if (isExist)
                return ServiceResult<CustomerAccount>.Fail("RecordAlreadyExist");

            await _accountRepository.AddAsync(entity).ConfigureAwait(false);
            await _unitofWork.CommitAsync().ConfigureAwait(false);
            return ServiceResult<CustomerAccount>.Success("AddNewRecord");
        }

        public async Task<ServiceResult<CustomerAccount>> UpdateCustomerAccountAsync(CustomerAccount entity)
        {
            var data = _accountRepository.Table.FirstOrDefault(x => x.Id == entity.Id);
            if (data == null)
                return ServiceResult<CustomerAccount>.Fail("Customer account is not available");
            _accountRepository.Update(entity);
            await _unitofWork.CommitAsync().ConfigureAwait(false);
            return ServiceResult<CustomerAccount>.Success("Customer record updated successfully");
        }

        public async Task<ServiceResult<CustomerAccount>> UpdateCustomerStatusAsync(int id)
        {
            var entity = _accountRepository.Table.FirstOrDefault(x => x.Id == id);
            if (entity == null)
                return ServiceResult<CustomerAccount>.Fail("Customer account is not available");
            entity.Status = !entity.Status;
            _accountRepository.Update(entity);
            await _unitofWork.CommitAsync().ConfigureAwait(false);
            return ServiceResult<CustomerAccount>.Success("Customer status updated successfully");
        }
    }


}
