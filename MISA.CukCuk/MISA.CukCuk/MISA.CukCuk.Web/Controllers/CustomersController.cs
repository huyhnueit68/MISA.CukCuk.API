using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using MISA.ApplicationCore;
using MISA.Infrastructure.Model;
using MISA.Entity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.CukCuk.Web.Controllers
{
    /// <summary>
    /// Api Danh mục khác hàng
    /// CreatedBy: PQ Huy (21/06/2021)
    /// </summary>
    [Route("v1/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        /// <summary>
        /// Lấy toàn bộ danh sách khác hàng
        /// </summary>
        /// <returns>Danh sách khách hàng</returns>
        /// CreatedBy: PQ Huy (24/06/2021)
        [HttpGet]
        public IActionResult Get()
        {
            // kết nối tới service
            var customerService = new CustomerService();

            // gọi function lấy dữ liệu
            var customers = customerService.GetCustomers();

            //trả về dữ liệu
            return Ok(customers);
            
        }

        /// <summary>
        /// Lấy ra danh sách khách hàng theo id và tên
        /// </summary>
        /// <param name="id">Id của khách hàng</param>
        /// <returns>Danh sách khách hàng</returns>
        /// CreatedBy: PQ Huy (21/06/2021)
        [HttpGet("filter")]
        public IActionResult GetCustomerById(Guid id)
        {
            // kết nối tới service
            var customerService = new CustomerService();

            // gọi function lấy dữ liệu
            var customers = customerService.GetCustomerById(id);

            //trả về dữ liệu
            return Ok(customers);
        }

        /// <summary>
        /// Thêm khách hàng
        /// </summary>
        /// <param name="customer">Dữ liệu khách hàng cần thêm</param>
        /// <returns>Trả về dữ liệu khách hàng đã thêm thành công</returns>
        /// CreatedBy: PQ Huy (21/06/2021)
        [HttpPost]
        public IActionResult Post([FromBody]Customer customer)
        {
            // kết nối tới service
            var customerService = new CustomerService();

            // gọi function lấy dữ liệu
            var serviceResult = customerService.InsertCustomer(customer);

            //trả về dữ liệu
            if(serviceResult.MISACode == MISAEnum.NotValid)
            {
                return BadRequest(serviceResult.Data);
            }
            if(serviceResult.MISACode == MISAEnum.IsValid && (int)serviceResult.Data > 0)
            {
                return Created("Customer", customer);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Sửa dữ liệu khách hàng
        /// </summary>
        /// <param name="id">mã khách hàng</param>
        /// <param name="customer">dữ liệu khách hàng cần thay đổi</param>
        /// <returns>Trả về khách hàng đã sửa thành công</returns>
        /// CreatedBy: PQ Huy (21/06/2021)
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] Customer customer)
        {
            // kết nối tới service
            var customerService = new CustomerService();

            // gọi function lấy dữ liệu
            var serviceResult = customerService.UpdateCustomer(id, customer);

            //trả về dữ liệu
            if (serviceResult.MISACode == MISAEnum.NotValid)
            {
                return BadRequest(serviceResult.Data);
            }
            if (serviceResult.MISACode == MISAEnum.IsValid && (int)serviceResult.Data > 0)
            {
                return Created("Customer", customer);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Xóa khách hàng theo id
        /// </summary>
        /// <param name="id">mã khách hàng</param>
        /// <returns>Trạng thái cập nhật</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            // kết nối tới service
            var customerService = new CustomerService();

            // gọi function lấy dữ liệu
            var serviceResult = customerService.DeleteCustomerById(id);

            //trả về dữ liệu
            if (serviceResult.MISACode == MISAEnum.NotValid)
            {
                return BadRequest(serviceResult.Data);
            }
            if (serviceResult.MISACode == MISAEnum.IsValid && (int)serviceResult.Data > 0)
            {
                return Ok(serviceResult);
            }
            else
            {
                return NoContent();
            }
        }
    }
}
