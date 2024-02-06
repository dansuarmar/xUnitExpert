using WebApi.Entities;

namespace WebApi.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> Add(Customer customer);
        Task<bool> Delete(int id);
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(int id);
        Task<Customer> Update(Customer customer);
    }
}