using Dapper;
using Microsoft.Extensions.Configuration;
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
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        #region DECLARE

        #endregion

        #region Contructor
        public CustomerRepository(IConfiguration configuration) : base (configuration)
        {

        }
        #endregion

        #region Method

        public Customer GetCustomerByCode(string code)
        {
            //kết nối database
            _dbConnection.Open();

            //khởi tạo các commandText
            var customer = _dbConnection.Query<Customer>("Proc_GetCustomerByCode", new { CustomerCode = code }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            _dbConnection.Close();

            //Trả về dữ liệu số bản ghi thêm mới
            return customer;
        }

        public Customer GetCustomerByEmail(string email)
        {
            //kết nối database
            _dbConnection.Open();

            //khởi tạo các commandText
            var resCustomerGroup = _dbConnection.Query<Customer>("Proc_GetCustomerByEmail", new { Email = email }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            _dbConnection.Close();

            //Trả về dữ liệu số bản ghi thêm mới
            return resCustomerGroup;
        }

        public CustomerGroup GetCustomerGroupById(Guid id)
        {
            //kết nối database
            _dbConnection.Open();

            //khởi tạo các commandText
            var resCustomerGroup = _dbConnection.Query<CustomerGroup>("Proc_GetCustomerGroupById", new { CustomerGroupId = id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            _dbConnection.Close();

            //Trả về dữ liệu số bản ghi tương ứng
            return resCustomerGroup;
        }

        public Customer GetCustomerByPhone(string phoneNumber)
        {
            //kết nối database
            _dbConnection.Open();

            //khởi tạo các commandText
            var resCustomer = _dbConnection.Query<Customer>("Proc_GetCustomerByPhoneNumber", new { PhoneNumber = phoneNumber }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            _dbConnection.Close();

            //Trả về dữ liệu số bản ghi thêm mới
            return resCustomer;
        }

        public IEnumerable<Customer> GetCustomerPaging(int pageIndex, int pageSize)
        {
            _dbConnection.Open();

            var resCustomer = _dbConnection.Query<Customer>("Proc_GetCustomerPaging", new { pageIndex = pageIndex, pageSize = pageSize }, commandType: CommandType.StoredProcedure);

            return resCustomer;
        }

        #endregion
    }
}
