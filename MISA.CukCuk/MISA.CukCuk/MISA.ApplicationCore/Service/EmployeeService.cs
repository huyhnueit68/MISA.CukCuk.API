using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore
{
    public class EmployeeService : IEmployeeService
    {
        #region DECLARE
        IEmployeeRepository _employeeRepository;
        #endregion

        #region Contructor
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        #endregion

        #region Method
        public IEnumerable<Employee> GetEmployees()
        {
            return _employeeRepository.GetEmployees();
        }

        public IEnumerable<Employee> GetEmployeeById(Guid id)
        {
            return _employeeRepository.GetEmployeeById(id);
        }

        public ServiceResult InsertEmployee(Employee employee)
        {
            /* Validate data*/

            return _employeeRepository.InsertEmployee(employee);
            //Trả về trạng thái khi thêm nhân viên thành công
        }

        public ServiceResult UpdateEmployee(Guid id, Employee employee)
        {
            /*Validate data*/

            return _employeeRepository.UpdateEmployee(id, employee);
            //Trả về trạng thái khi cập nhật nhân viên thành công
        }

        public ServiceResult DeleteEmployeeById(Guid id)
        {
            // Trả về trạng thái khi xóa nhân viên thành công
            return _employeeRepository.DeleteEmployeeById(id);
        }
        #endregion
    }
}
