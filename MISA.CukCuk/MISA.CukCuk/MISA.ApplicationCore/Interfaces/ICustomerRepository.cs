using MISA.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Interfaces
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetCustomers();

        IEnumerable<Customer> GetCustomerById(Guid customerId);

        ServiceResult InsertCustomer(Customer customer);

        Customer GetCustomerByCode(string code);

        CustomerGroup GetCustomerGroupById(Guid id);

        Customer GetCustomerByPhone(string phoneNumber);

        Customer GetCustomerByEmail(string email);

        ServiceResult UpdateCustomer(Guid id, Customer customer);

        ServiceResult DeleteCustomerById(Guid id);
    }
}
