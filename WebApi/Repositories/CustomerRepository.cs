using WebApi.Entities;

namespace WebApi.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        List<Customer> _customers;
        public CustomerRepository()
        {
            _customers = new List<Customer>()
            {
                new Customer
                {
                    Id = 1,
                    Name = "First Customer",
                    CreatedDate = new DateTime(2023,10,12),
                    Credit = 1000m,
                },
                new Customer
                {
                    Id = 2,
                    Name = "Another Customer",
                    CreatedDate = new DateTime(2023,12,1),
                    Credit = 10000m,
                },
                new Customer
                {
                    Id = 3,
                    Name = "Big Customer",
                    CreatedDate = new DateTime(2024,1,1),
                    Credit = 100000m,
                },
            };
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            await Task.Delay(0);
            return _customers;
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            await Task.Delay(0);
            Customer? customer = _customers.FirstOrDefault(x => x.Id == id);
            return customer;
        }

        public async Task<Customer> Add(Customer customer)
        {
            await Task.Delay(0);
            var nextId = _customers.LastOrDefault();
            customer.Id = nextId is null ? 1 : nextId.Id + 1;
            _customers.Add(customer);
            return customer;
        }

        public async Task<Customer> Update(Customer customer)
        {
            await Task.Delay(0);
            var customerToRemove = _customers.FirstOrDefault(m => m.Id == customer.Id);
            if (customerToRemove is null)
                throw new Exception("Customer not found");
            _customers.Remove(customerToRemove);
            _customers.Add(customer);
            return customer;
        }

        public async Task<bool> Delete(int id)
        {
            await Task.Delay(0);
            var customerToRemove = _customers.FirstOrDefault(m => m.Id == id);
            if (customerToRemove is null)
                throw new Exception("Customer not found");
            _customers.Remove(customerToRemove);
            return true;
        }
    }
}
