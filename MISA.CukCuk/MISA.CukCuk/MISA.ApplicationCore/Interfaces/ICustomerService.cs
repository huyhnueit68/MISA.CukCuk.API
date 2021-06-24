using MISA.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Interfaces
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetCustomers();

        IEnumerable<Customer> GetCustomerById(Guid customerId);

        ServiceResult InsertCustomer(Customer customer);

        Customer GetCustomerByCode(string code);

        CustomerGroup GetCustomerGroupById(Guid id);

        Customer GetCustomerGroupByPhone(string phoneNumber);

        Customer GetCustomerByEmail(string email);

        ServiceResult UpdateCustomer(Guid id, Customer customer);

        ServiceResult DeleteCustomerById(Guid customerId);
    }
}
