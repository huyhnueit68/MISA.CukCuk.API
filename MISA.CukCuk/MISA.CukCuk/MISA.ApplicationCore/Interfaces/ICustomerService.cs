using MISA.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Interfaces
{
    public interface ICustomerService
    {
        /// <summary>
        /// Lấy toàn bộ danh sách khách hàng
        /// </summary>
        /// <returns> Trả về danh sách khách hàng </returns>
        /// CreatedBy: PQ Huy (24/06/2021)
        IEnumerable<Customer> GetCustomers();

        /// <summary>
        /// Lấy thông tin khách hàng theo mã
        /// </summary>
        /// <param name="customerId"> Mã khách hàng</param>
        /// <returns></returns>
        /// CreatedBy: PQ Huy (24/06/2021)
        IEnumerable<Customer> GetCustomerById(Guid customerId);

        /// <summary>
        /// Thêm mới khách hàng
        /// </summary>
        /// <param name="customer">Dữ liệu khách hàng</param>
        /// <returns>Trả về số bản ghi được thêm</returns>
        /// CreatedBy: PQ Huy (24/06/2021)
        ServiceResult InsertCustomer(Customer customer);

        /// <summary>
        /// Sửa thông tin khách hàng
        /// </summary>
        /// <param name="id">Mã khách hàng</param>
        /// <param name="customer">Dữ liệu khách hàng cần sửa</param>
        /// <returns>Trả về trạng thái cập nhật dữ liệu</returns>
        ServiceResult UpdateCustomer(Guid id, Customer customer);

        /// <summary>
        /// Xóa thông tin khách hàng theo khóa chính
        /// </summary>
        /// <param name="id">Mã khách hàng</param>
        /// <returns>Trả về thạng thái cập nhật danh sách khách hàng</returns>
        /// CreatedBy: PQ Huy (24.06.2021)
        ServiceResult DeleteCustomerById(Guid customerId);
    }
}
