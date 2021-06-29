using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Enums;
using MISA.ApplicationCore.Interfaces;
using MISA.ApplicationCore.Service;

namespace MISA.ApplicationCore
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        #region DECLARE
        IEmployeeRepository _employeeRepository;
        #endregion

        #region Construct
        /// <summary>
        /// hàm khỏi tạo cho customer service
        /// </summary>
        /// <param name="employeeRepository"></param>
        /// CreatedBy: PQ Huy (24.06.2021)
        public EmployeeService(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        #endregion 

        #region Method

        #endregion
    }
}
