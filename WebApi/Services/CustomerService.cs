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
                var customer = await _repository.GetByIdAsync(id);
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
            _logger.LogInformation("Creating new customer with id {0}", customer.Id);
            var sw = Stopwatch.StartNew();
            try
            {
                return await _repository.Add(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong when creating customer with id {0}.", customer.Id);
                throw;
            }
            finally
            {
                sw.Stop();
                _logger.LogInformation("Customer with Id {1} created in {0}ms", sw.ElapsedMilliseconds, customer.Id);
            }
        }

        public async Task<Customer> UpdateAsync(Customer customer)
        {
            _logger.LogInformation("UpdateAsync Called");
            return await _repository.Update(customer);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation("Deleting customer with id {0}", id);
            var sw = Stopwatch.StartNew();
            try
            {
                return await _repository.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong when deleting customer with id {0}.", id);
                throw;
            }
            finally
            {
                sw.Stop();
                _logger.LogInformation("Customer with Id {1} deleted in {0}ms", sw.ElapsedMilliseconds, id);
            }
        }
    }
}

