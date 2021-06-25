using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using MISA.ApplicationCore;
using MISA.ApplicationCore.Interfaces;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Enums;

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
        //Khởi tạo interface cho class
        ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            // Gán inter được định nghĩa
            _customerService = customerService;
        }

        /// <summary>
        /// Lấy toàn bộ danh sách khác hàng
        /// </summary>
        /// <returns>Danh sách khách hàng</returns>
        /// CreatedBy: PQ Huy (24/06/2021)
        [HttpGet]
        public IActionResult Get()
        {
            // gọi function lấy dữ liệu
            var customers = _customerService.GetCustomers();

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

            // gọi function lấy dữ liệu
            var customers = _customerService.GetCustomerById(id);

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

            // gọi function lấy dữ liệu
            var serviceResult = _customerService.InsertCustomer(customer);

            //trả về dữ liệu
            if(serviceResult.MISACode == MISAEnum.NotValid)
            {
                return BadRequest(serviceResult.Data);
            }
            if(serviceResult.MISACode == MISAEnum.IsValid)
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

            // gọi function lấy dữ liệu
            var serviceResult = _customerService.UpdateCustomer(id, customer);

            //trả về dữ liệu
            if (serviceResult.MISACode == MISAEnum.NotValid)
            {
                return BadRequest(serviceResult.Data);
            }
            if (serviceResult.MISACode == MISAEnum.IsValid)
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

            // gọi function lấy dữ liệu
            var serviceResult = _customerService.DeleteCustomerById(id);

            //trả về dữ liệu
            if (serviceResult.MISACode == MISAEnum.NotValid)
            {
                return BadRequest(serviceResult.Data);
            }
            if (serviceResult.MISACode == MISAEnum.IsValid)
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
