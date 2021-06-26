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
    /// <summary>
    /// Api danh mục nhân viên
    /// </summary>
    /// CreatedBy: PQ Huy (26.06.2021)
    public class EmployeesController : BaseEntityController<Employee>
    {
        #region DECLARE
        IBaseService<Employee> _baseService;
        #endregion

        #region Contructor
        public EmployeesController(IBaseService<Employee> baseService):base(baseService)
        {
            _baseService = baseService;
        }
        #endregion

        #region Method

        #endregion

    }
}
