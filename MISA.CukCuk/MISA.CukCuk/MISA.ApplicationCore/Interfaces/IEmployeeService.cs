using MISA.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Interfaces
{
    public interface IEmployeeService
    {
        /// <summary>
        /// Lấy toàn bộ danh sách nhân viên
        /// </summary>
        /// <returns> Trả về danh sách nhân viên </returns>
        /// CreatedBy: PQ Huy (24/06/2021)
        IEnumerable<Employee> GetEmployees();

        /// <summary>
        /// Lấy thông tin nhân viên theo mã
        /// </summary>
        /// <param name="id"> Mã nhân viên</param>
        /// <returns></returns>
        /// CreatedBy: PQ Huy (24/06/2021)
        IEnumerable<Employee> GetEmployeeById(Guid id);

        /// <summary>
        /// Thêm mới nhân viên
        /// </summary>
        /// <param name="employee">Dữ liệu nhân viên</param>
        /// <returns>Trả về số bản ghi được thêm</returns>
        /// CreatedBy: PQ Huy (24/06/2021)
        ServiceResult InsertEmployee(Employee employee);

        /// <summary>
        /// Sửa thông tin nhân viên
        /// </summary>
        /// <param name="id">Mã nhân viên</param>
        /// <param name="employee">Dữ liệu nhân viên cần sửa</param>
        /// <returns>Trả về trạng thái cập nhật dữ liệu</returns>
        ServiceResult UpdateEmployee(Guid id, Employee employee);

        /// <summary>
        /// Xóa thông tin nhân viên theo khóa chính
        /// </summary>
        /// <param name="id">Mã nhân viên</param>
        /// <returns>Trả về thạng thái cập nhật danh sách nhân viên</returns>
        /// CreatedBy: PQ Huy (24.06.2021)
        ServiceResult DeleteEmployeeById(Guid id);
    }
}
