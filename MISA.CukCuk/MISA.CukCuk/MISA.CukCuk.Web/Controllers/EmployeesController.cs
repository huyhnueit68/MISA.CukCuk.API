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
    public class EmployeesController : BaseEntityController<Employee>
    {
        #region DECLARE
        IEmployeeService _employeeService;
        #endregion

        #region Contructor
        public EmployeesController(IEmployeeService employeeService) : base(employeeService)
        {
            _employeeService = employeeService;
        }
        #endregion

        #region Method

        #endregion
    }
}
