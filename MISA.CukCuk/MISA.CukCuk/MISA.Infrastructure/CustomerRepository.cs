using Dapper;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Enums;
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
            //kết nối database
            var connectionString = "User Id=dev;Host=47.241.69.179;Port=3306;Password=12345678;Database=MISACukCuk_Demo;Database=MISACukCuk_Demo;Character Set=utf8";
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            dbConnection.Open();

            //khởi tạo các commandText
            var rowAffects = dbConnection.Execute("Proc_DeleteCustomerById", new { CustomerId = id }, commandType: CommandType.StoredProcedure);
            dbConnection.Close();

            //Trả về dữ liệu số bản ghi xóa
            var serviceResult = new ServiceResult();
            serviceResult.Data = rowAffects;
            serviceResult.MISACode = MISAEnum.IsValid;
            serviceResult.Messenger = "Cập nhật thành công";
            return serviceResult;
        }

        public Customer GetCustomerByCode(string code)
        {
            //kết nối database
            var connectionString = "User Id=dev;Host=47.241.69.179;Port=3306;Password=12345678;Database=MISACukCuk_Demo;Database=MISACukCuk_Demo;Character Set=utf8";
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            dbConnection.Open();

            //khởi tạo các commandText
            var customer = dbConnection.Query<Customer>("Proc_GetCustomerByCode", new { CustomerCode = code }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            dbConnection.Close();

            //Trả về dữ liệu số bản ghi thêm mới
            return customer;
        }

        public Customer GetCustomerByEmail(string email)
        {
            //kết nối database
            var connectionString = "User Id=dev;Host=47.241.69.179;Port=3306;Password=12345678;Database=MISACukCuk_Demo;Database=MISACukCuk_Demo;Character Set=utf8";
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            dbConnection.Open();

            //khởi tạo các commandText
            var resCustomerGroup = dbConnection.Query<Customer>("Proc_GetCustomerByEmail", new { Email = email }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            dbConnection.Close();

            //Trả về dữ liệu số bản ghi thêm mới
            return resCustomerGroup;
        }

        public IEnumerable<Customer> GetCustomerById(Guid customerId)
        {
            //kết nối database
            var connectionString = "User Id=dev;Host=47.241.69.179;Port=3306;Password=12345678;Database=MISACukCuk_Demo;Database=MISACukCuk_Demo;Character Set=utf8";
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            dbConnection.Open();

            //khởi tạo các commandText
            var customers = dbConnection.Query<Customer>("Proc_GetCustomerById", new { CustomerId = customerId }, commandType: CommandType.StoredProcedure);
            dbConnection.Close();

            //Trả về dữ liệu
            return customers;
        }

        public CustomerGroup GetCustomerGroupById(Guid id)
        {
            //kết nối database
            var connectionString = "User Id=dev;Host=47.241.69.179;Port=3306;Password=12345678;Database=MISACukCuk_Demo;Database=MISACukCuk_Demo;Character Set=utf8";
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            dbConnection.Open();

            //khởi tạo các commandText
            var resCustomerGroup = dbConnection.Query<CustomerGroup>("Proc_GetCustomerGroupById", new { CustomerGroupId = id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            dbConnection.Close();

            //Trả về dữ liệu số bản ghi tương ứng
            return resCustomerGroup;
        }

        public Customer GetCustomerByPhone(string phoneNumber)
        {
            //kết nối database
            var connectionString = "User Id=dev;Host=47.241.69.179;Port=3306;Password=12345678;Database=MISACukCuk_Demo;Database=MISACukCuk_Demo;Character Set=utf8";
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            dbConnection.Open();

            //khởi tạo các commandText
            var resCustomer = dbConnection.Query<Customer>("Proc_GetCustomerByPhoneNumber", new { PhoneNumber = phoneNumber }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            dbConnection.Close();

            //Trả về dữ liệu số bản ghi thêm mới
            return resCustomer;
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
            //kết nối database
            var connectionString = "User Id=dev;Host=47.241.69.179;Port=3306;Password=12345678;Database=MISACukCuk_Demo;Database=MISACukCuk_Demo;Character Set=utf8";
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            dbConnection.Open();

            //khởi tạo các commandText
            var rowAffects = dbConnection.Execute("Proc_InsertCustomer", customer, commandType: CommandType.StoredProcedure);
            dbConnection.Close();
            var serviceResult = new ServiceResult();
            serviceResult.Data = rowAffects;
            serviceResult.MISACode = MISAEnum.IsValid;
            serviceResult.Messenger = "Thêm mới thành công";
            //Trả về dữ liệu số bản ghi thêm mới
            return serviceResult;
        }

        public ServiceResult UpdateCustomer(Guid id, Customer customer)
        {
            //kết nối database
            var connectionString = "User Id=dev;Host=47.241.69.179;Port=3306;Password=12345678;Database=MISACukCuk_Demo;Database=MISACukCuk_Demo;Character Set=utf8";
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            dbConnection.Open();

            //khởi tạo các commandText
            var rowAffects = dbConnection.Execute("Proc_UpdateCustomer", new
            {
                CustomerId = id,
                FullName = customer.FullName,
                CustomerGroupId = customer.CustomerGroupId,
                DateOfBirth = customer.DateOfBirth,
                Gender = customer.Gender,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber
            }, commandType: CommandType.StoredProcedure);
            dbConnection.Close();

            //Trả về dữ liệu số bản ghi thêm mới
            var serviceResult = new ServiceResult();
            serviceResult.Data = rowAffects;
            serviceResult.MISACode = MISAEnum.IsValid;
            serviceResult.Messenger = "Cập nhật thành công";

            return serviceResult;
        }
    }
}
