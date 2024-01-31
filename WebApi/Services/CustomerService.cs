using WebApi.Entities;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
using WebApi.Repositories;

namespace WebApi.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
            _logger = new Logger<CustomerService>(new LoggerFactory());
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            _logger.LogInformation("GetAllAsync Called");
            return await _repository.GetAllAsync();
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            _logger.LogInformation("GetByIdAsync Called");
            return await _repository.GetById(id);
        }

        public async Task<Customer> AddAsync(Customer customer)
        {
            _logger.LogInformation("AddAsync Called");
            return await _repository.Add(customer);
        }

        public async Task<Customer> UpdateAsync(Customer customer)
        {
            _logger.LogInformation("UpdateAsync Called");
            return await _repository.Update(customer);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation("DeleteAsync Called");
            return await _repository.Delete(id);
        }
    }
}

