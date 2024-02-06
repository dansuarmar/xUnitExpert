using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Repositories;

namespace TestProject.WebApiTests.Fakes
{
    public class FakeCustomerRepository : ICustomerRepository
    {
        private readonly List<Customer> _customerList = new List<Customer>();

        public Task<Customer> Add(Customer customer)
        {
            _customerList.Add(customer);
            return Task.FromResult(customer);
        }

        public Task<bool> Delete(int id)
        {
            _customerList.RemoveAll(x => x.Id == id);
            return Task.FromResult(true);
        }

        public Task<IEnumerable<Customer>> GetAllAsync()
        {
            return Task.FromResult(_customerList as IEnumerable<Customer>);
        }

        public Task<Customer?> GetByIdAsync(int id)
        {
            return Task.FromResult(_customerList.FirstOrDefault(x => x.Id == id));
        }

        public Task<Customer> Update(Customer customer)
        {
            _customerList.Add(customer);
            return Task.FromResult(customer);
        }
    }
}
