using Dapper;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure
{
    public class CustomerRepository : ICustomerRepository
    {
        public ServiceResult DeleteCustomerById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomerByCode(string code)
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomerByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetCustomerById(Guid customerId)
        {
            throw new NotImplementedException();
        }

        public CustomerGroup GetCustomerGroupById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomerGroupByPhone(string phoneNumber)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetCustomers()
        {
            //kết nối database
            var connectionString = "User Id=dev;Host=47.241.69.179;Port=3306;Password=12345678;Database=MISACukCuk_Demo;Database=MISACukCuk_Demo;Character Set=utf8";
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            dbConnection.Open();

            //khởi tạo các commandText
            var customers = dbConnection.Query<Customer>("Proc_GetCustomers", commandType: CommandType.StoredProcedure);
            dbConnection.Close();

            //Trả về dữ liệu
            return customers;
        }

        public ServiceResult InsertCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public ServiceResult UpdateCustomer(Guid id, Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
