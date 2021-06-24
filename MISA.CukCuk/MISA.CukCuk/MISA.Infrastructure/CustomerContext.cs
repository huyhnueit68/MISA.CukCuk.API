using Dapper;
using MISA.ApplicationCore.Entities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MISA.Infrastructure
{
    public class CustomerContext
    {
        #region Method

        /// <summary>
        /// Lấy toàn bộ danh sách khách hàng
        /// </summary>
        /// <returns> Trả về danh sách khách hàng </returns>
        /// CreatedBy: PQ Huy (24/06/2021)
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

        /// <summary>
        /// Lấy thông tin khách hàng theo mã
        /// </summary>
        /// <param name="id"> Mã khách hàng</param>
        /// <returns></returns>
        /// CreatedBy: PQ Huy (24/06/2021)
        public IEnumerable<Customer> GetCustomerById(Guid id)
        {
            //kết nối database
            var connectionString = "User Id=dev;Host=47.241.69.179;Port=3306;Password=12345678;Database=MISACukCuk_Demo;Database=MISACukCuk_Demo;Character Set=utf8";
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            dbConnection.Open();

            //khởi tạo các commandText
            var customers = dbConnection.Query<Customer>("Proc_GetCustomerById", new { CustomerId = id }, commandType: CommandType.StoredProcedure);
            dbConnection.Close();

            //Trả về dữ liệu
            return customers;
        }

        /// <summary>
        /// Thêm mới khách hàng
        /// </summary>
        /// <param name="customer">Dữ liệu khách hàng</param>
        /// <returns>Trả về số bản ghi được thêm</returns>
        /// CreatedBy: PQ Huy (24/06/2021)
        public int InsertCustomer(Customer customer)
        {
            //kết nối database
            var connectionString = "User Id=dev;Host=47.241.69.179;Port=3306;Password=12345678;Database=MISACukCuk_Demo;Database=MISACukCuk_Demo;Character Set=utf8";
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            dbConnection.Open();

            //khởi tạo các commandText
            var rowAffects = dbConnection.Execute("Proc_InsertCustomer", customer, commandType: CommandType.StoredProcedure);
            dbConnection.Close();

            //Trả về dữ liệu số bản ghi thêm mới
            return rowAffects;

        }

        /// <summary>
        /// Lấy dữ liệu khách hàng theo mã khách hàng
        /// </summary>
        /// <param name="code">Mã khách hàng</param>
        /// <returns>Trả về khách hàng có mã khách hàng tương ứng</returns>
        /// CreatedBy: PQ Huy (24/06/2021)
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

        /// <summary>
        /// Lấy ra nhóm khách hàng theo id
        /// </summary>
        /// <param name="id">id nhóm khách hàng</param>
        /// <returns>Trả về nhóm khách hàng tương ứng</returns>
        /// CreatedBy: PQ Huy (24/06/2021)
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

        /// <summary>
        /// Lấy ra khách hàng bằng số điện thoại
        /// </summary>
        /// <param name="phoneNumber">Số điện thoại khách hàng</param>
        /// <returns>Trả về khách hàng có số điện thoại tương ứng</returns>
        /// CreatedBy: PQ Huy (24/06/2021)
        public Customer GetCustomerGroupByPhone(string phoneNumber)
        {
            //kết nối database
            var connectionString = "User Id=dev;Host=47.241.69.179;Port=3306;Password=12345678;Database=MISACukCuk_Demo;Database=MISACukCuk_Demo;Character Set=utf8";
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            dbConnection.Open();

            //khởi tạo các commandText
            var resCustomerGroup = dbConnection.Query<Customer>("Proc_GetCustomerByPhoneNumber", new { PhoneNumber = phoneNumber }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            dbConnection.Close();

            //Trả về dữ liệu số bản ghi thêm mới
            return resCustomerGroup;
        }

        /// <summary>
        /// Lấy ra khách hàng bằng email
        /// </summary>
        /// <param name="email">Số điện thoại khách hàng</param>
        /// <returns>Trả về khách hàng có số điện thoại tương ứng</returns>
        /// CreatedBy: PQ Huy (24/06/2021)
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

        /// <summary>
        /// Sửa thông tin khách hàng
        /// </summary>
        /// <param name="id">Mã khách hàng</param>
        /// <param name="customer">Dữ liệu khách hàng cần sửa</param>
        /// <returns>Trả về trạng thái cập nhật dữ liệu</returns>
        public int UpdateCustomer(Guid id, Customer customer)
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
            return rowAffects;
        }

        /// <summary>
        /// Xóa thông tin khách hàng theo khóa chính
        /// </summary>
        /// <param name="id">Mã khách hàng</param>
        /// <returns>Trả về thạng thái cập nhật danh sách khách hàng</returns>
        /// CreatedBy: PQ Huy (24.06.2021)
        public int DeleteCustomerById(Guid id)
        {
            //kết nối database
            var connectionString = "User Id=dev;Host=47.241.69.179;Port=3306;Password=12345678;Database=MISACukCuk_Demo;Database=MISACukCuk_Demo;Character Set=utf8";
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            dbConnection.Open();

            //khởi tạo các commandText
            var rowAffects = dbConnection.Execute("Proc_DeleteCustomerById", new { CustomerId = id }, commandType: CommandType.StoredProcedure);
            dbConnection.Close();

            //Trả về dữ liệu số bản ghi thêm mới
            return rowAffects;
        }

        #endregion
    }
}
