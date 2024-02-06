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
            _logger.LogInformation("Retriving customer with Id {0}", id);
            var sw = Stopwatch.StartNew();
            try
            {
                var customer = await _repository.GetById(id);
                return customer;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Something went wrong when retriving customer with Id {0}.", id);
                throw;
            }
            finally
            {
                sw.Stop();
                _logger.LogInformation("Customer with Id {1} retrived in {0}ms", sw.ElapsedMilliseconds, id);
            }
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

