using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MISA.CukCuk.Web.Model;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.CukCuk.Web.Controllers
{
    /// <summary>
    /// Api Danh mục khác hàng
    /// PQ Huy 21.06.2021
    /// </summary>
    [Route("v1/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        /// <summary>
        /// Lấy toàn bộ danh sách khác hàng
        /// PQ Huy 21.06.2021
        /// </summary>
        /// <returns>Danh sách khách hàng</returns>
        [HttpGet]
        public IActionResult Get()
        {
            // khởi tạo đường dẫn kết nối db misacukcuk_demo
            var connectionString = "User Id=dev;Host=47.241.69.179;Port=3306;Password=12345678;Database=MISACukCuk_Demo;Database=MISACukCuk_Demo;Character Set=utf8";  

            IDbConnection dbConnection = new MySqlConnection(connectionString);

            /*var sqlCommand = "SELECT * FROM Customer";*/
            dbConnection.Open();
            /*var customers = dbConnection.Query<Customer>(sqlCommand);*/
            var customers = dbConnection.Query<Customer>("Proc_GetCustomers", commandType: CommandType.StoredProcedure);
            dbConnection.Close();

            return Ok(customers);
        }

        /// <summary>
        /// Lấy ra danh sách khách hàng theo id và tên
        /// </summary>
        /// <param name="id">Id của khách hàng</param>
        /// <param name="name">Tên của khách hàng</param>
        /// <returns>Danh sách khách hàng</returns>
        /// PQ Huy 21.06.2021
        [HttpGet("filter")]
        public IActionResult GetByID(Guid id)
        {
            // khởi tạo đường dẫn kết nối db misacukcuk_demo
            var connectionString = "User Id=dev;Host=47.241.69.179;Port=3306;Password=12345678;Database=MISACukCuk_Demo;Database=MISACukCuk_Demo;Character Set=utf8";

            IDbConnection dbConnection = new MySqlConnection(connectionString);

            dbConnection.Open();
            /*var sqlCommand = $"SELECT * FROM Customer WHERE CustomerId='{id.ToString()}'";*/
            /*var customers = dbConnection.QueryFirstOrDefault<Customer>(sqlCommand);*/
            
            var customers = dbConnection.Query<Customer>("Proc_GetCustomerById", new { CustomerId = id }, commandType: CommandType.StoredProcedure);
            dbConnection.Close();

            return Ok(customers);
        }

        /// <summary>
        /// Thêm khách hàng
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>trả về dữ liệu khách hàng đã thêm thành công</returns>
        [HttpPost]
        public IActionResult Post([FromBody]Customer customer)
        {
            /*connect data base*/

            var connectionString = "User Id=dev;Host=47.241.69.179;Port=3306;Password=12345678;Database=MISACukCuk_Demo;Database=MISACukCuk_Demo;Character Set=utf8";
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            dbConnection.Open();

            /* validate data */
            // validate field not null
            if(string.IsNullOrEmpty(customer.CustomerCode))
            {
                var msg = new
                {
                    devMsg = new
                    {
                        fieldName = "CustomerCode",
                        msg = "Mã khách hàng không được phép để trống",
                        Code = "400"
                    },
                    userMsg = "Mã khách hàng không được phép để trống",
                };
                return BadRequest(msg);
            }

            // validate duplicate code
            var res = dbConnection.Query<Customer>("Proc_GetCustomerByCode", new { CustomerCode = customer.CustomerCode }, commandType: CommandType.StoredProcedure);
            if(res.Count() > 0)
            {
                var msg = new
                {
                    devMsg = new
                    {
                        fieldName = "CustomerCode",
                        msg = "Mã khách hàng đã tồn tại",
                        Code = "401"
                    },
                    userMsg = "Mã khách hàng đã tồn tại",
                };
                return BadRequest(msg);
            }

            // validate customer group id
            var resCustomerGroup = dbConnection.Query<CustomerGroup>("Proc_GetCustomerGroupById", new { CustomerGroupId = customer.CustomerGroupId }, commandType: CommandType.StoredProcedure);
            if(resCustomerGroup.Count() <= 0)
            {
                var msg = new
                {
                    devMsg = new
                    {
                        fieldName = "CustomerGroupId",
                        msg = "Không tồn tại mã nhóm",
                        Code = "401"
                    },
                    userMsg = "Không tồn tại mã nhóm",
                };
                return BadRequest(msg);
            }

            /* insert record */
            var rowAffects = dbConnection.Execute("Proc_InsertCustomer", customer, commandType: CommandType.StoredProcedure);
            dbConnection.Close();

            /* check row affects*/
            if (rowAffects > 0)
            {
                return Created("Customer", customer);
            } else
            {
                return NoContent();
            }

        }

        /// <summary>
        /// Sửa dữ liệu khách hàng
        /// </summary>
        /// <param name="CustomerId">mã khách hàng</param>
        /// <param name="customer">dữ liệu khách hàng cần thay đổi</param>
        /// <returns>trả về khách hàng đã sửa thành công</returns>
        [HttpPut("{CustomerId}")]
        public IActionResult Put(Guid CustomerId, [FromBody] Customer customer)
        {
            var connectionString = "User Id=dev;Host=47.241.69.179;Port=3306;Password=12345678;Database=MISACukCuk_Demo;Database=MISACukCuk_Demo;Character Set=utf8";
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            dbConnection.Open();

            /* validate data */
            // validate field not null
            if (string.IsNullOrEmpty(customer.CustomerCode))
            {
                var msg = new
                {
                    devMsg = new
                    {
                        fieldName = "CustomerCode",
                        msg = "Mã khách hàng không được phép để trống",
                        Code = "400"
                    },
                    userMsg = "Mã khách hàng không được phép để trống",
                };
                return BadRequest(msg);
            }

            //validate code record
            var oldCustomer = dbConnection.Query<Customer>("Proc_GetCustomerById", new { CustomerId = CustomerId }, commandType: CommandType.StoredProcedure);
            var oldCustomerCode = oldCustomer.ToArray()[0].CustomerCode;
           
            if(customer.CustomerCode != oldCustomerCode)
            {
                // validate duplicate code
                var customerCode = dbConnection.Query<Customer>("Proc_GetCustomerByCode", new { CustomerCode = customer.CustomerCode }, commandType: CommandType.StoredProcedure);
                if (customerCode.Count() > 0)
                {
                    var msg = new
                    {
                        devMsg = new
                        {
                            fieldName = "CustomerCode",
                            msg = "Mã khách hàng đã tồn tại",
                            Code = "401"
                        },
                        userMsg = "Mã khách hàng đã tồn tại",
                    };
                    return BadRequest(msg);
                }

            } else
            {
                // validate customer groud id
                var res = dbConnection.Query<Customer>("Proc_GetCustomerGroupById", new { CustomerGroupId = customer.CustomerGroupId }, commandType: CommandType.StoredProcedure);
                if (res.Count() <= 0)
                {
                    var msg = new
                    {
                        devMsg = new
                        {
                            fieldName = "CustomerGroupId",
                            msg = "Không tồn tại mã nhóm",
                            Code = "401"
                        },
                        userMsg = "Không tồn tại mã nhóm",
                    };
                    return BadRequest(msg);
                }

                var upDateCustomer = dbConnection.Query<Customer>("Proc_UpdateCustomer", new { Customer = customer }, commandType: CommandType.StoredProcedure);

                dbConnection.Close();

                /* check row affects*/
                if (upDateCustomer.Count() > 0)
                {
                    return Created("Customer", upDateCustomer);
                }
                else
                {
                    return NoContent();
                }

            }

            return NoContent();
        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
    }
}
