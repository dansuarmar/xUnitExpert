using WebApi.Entities;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
using WebApi.Repositories;
using System.Diagnostics;

namespace WebApi.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly ILoggerAdapter<CustomerService> _logger;

        public CustomerService(ICustomerRepository repository, ILoggerAdapter<CustomerService>? logger = default)
        {
            _repository = repository;

            if (logger is null)
            {
                var realLogger = new Logger<CustomerService>(new LoggerFactory());
                _logger = new LoggerAdapter<CustomerService>(realLogger);
            }
            else
                _logger = logger;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            _logger.LogInformation("Retriving all Customers.");
            var sw = Stopwatch.StartNew();
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Something went wrong when retriving Customers.");
                throw;
            }
            finally
            {
                sw.Stop();
                _logger.LogInformation("Customers retrived in {0}ms", sw.ElapsedMilliseconds);
            }
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

