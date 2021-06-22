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

            var sqlCommand = "SELECT * FROM Customer";
            dbConnection.Open();
            /*var customers = dbConnection.Query<Customer>(sqlCommand);*/
            var customers = dbConnection.Query<Customer>("Proc_GetCustomers", commandType: CommandType.StoredProcedure);     
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
            var sqlCommand = $"SELECT * FROM Customer WHERE CustomerId='{id.ToString()}'";
            /*var customers = dbConnection.QueryFirstOrDefault<Customer>(sqlCommand);*/
            /*0a730933-b227-11eb-8a1f-00163e047e89*/
            var customers = dbConnection.Query<Customer>("Proc_GetCustomerById", new { CustomerId = id }, commandType: CommandType.StoredProcedure);

            return Ok(customers);
        }

        /// <summary>
        /// Thêm khách hàng
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(Customer customer)
        {
            var connectionString = "User Id=dev;Host=47.241.69.179;Port=3306;Password=12345678;Database=MISACukCuk_Demo;Database=MISACukCuk_Demo;Character Set=utf8";

            IDbConnection dbConnection = new MySqlConnection(connectionString);

            dbConnection.Open();

            /*var customers = dbConnection.QueryFirstOrDefault<Customer>(sqlCommand);*/
            /*0a730933-b227-11eb-8a1f-00163e047e89*/
            var rowAffects = dbConnection.Execute("Proc_InsertCustomer", customer, commandType: CommandType.StoredProcedure);

            if(rowAffects > 0)
            {

            }
            return Ok(customers);
        }

        // PUT api/<CustomersController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            return Ok(1);
        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
    }
}
