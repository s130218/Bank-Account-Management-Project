using Account.AuthAPI.Models.BankAccount;
using Account.AuthAPI.Models.Common;
using Account.AuthAPI.Repository.Repository;
using Account.AuthAPI.Repository.UnitofWork;
using Account.AuthAPI.Service.Common;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BankAccountAPI.Services.Service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitofWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountService(IAccountRepository accountRepository, IUnitOfWork unitofWork, IHttpContextAccessor httpContextAccessor)
        {
            _accountRepository = accountRepository;
            _unitofWork = unitofWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResult<List<CustomerAccount>>> GetAllAsync()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRole = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;

            if (userRole == FixedRoles.Customer)
            {
                // Fetch only the account for the specific customer
                var customerAccount = await _accountRepository.Table.Where(x => x.UserId == userId).FirstOrDefaultAsync();
                var result = new List<CustomerAccount> { customerAccount };
                return new ServiceResult<List<CustomerAccount>>(customerAccount != null)
                {
                    Data = result
                };
            }

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

            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            entity.UserId = userId;
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
