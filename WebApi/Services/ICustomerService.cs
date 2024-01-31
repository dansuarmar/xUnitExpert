using WebApi.Entities;

namespace WebApi.Services
{
    public interface ICustomerService
    {
        Task<Customer> AddAsync(Customer customer);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(int id);
        Task<Customer> UpdateAsync(Customer customer);
    }
}