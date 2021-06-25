using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.CukCuk.Web.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Lấy toàn bộ dữ liệu nhân viên
        /// </summary>
        /// <returns>Trả về danh sách nhân viên</returns>
        /// CreatedBy: PQ Huy (25.06.2021)
        [HttpGet]
        public IActionResult Get()
        {
            // gọi function lấy dữ liệu
            var employees = _employeeService.GetEmployees();

            //trả về dữ liệu
            return Ok(employees);
        }

        /// <summary>
        /// Lấy dữ liệu nhân viên theo mã nhân viên
        /// </summary>
        /// <returns>Trả về danh sách nhân viên theo mã tương ứng</returns>
        /// CreatedBy: PQ Huy (25.06.2021)
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            // Gọi function lấy dữ liệu
            var employee = _employeeService.GetEmployeeById(id);

            // Trả về dữ liệu
            return Ok(employee);
        }

        // POST api/<EmployeesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        // PUT api/<EmployeesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EmployeesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
