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

        ServiceResult UpdateCustomer(Guid id, Customer customer);

        ServiceResult DeleteCustomerById(Guid customerId);
    }
}
