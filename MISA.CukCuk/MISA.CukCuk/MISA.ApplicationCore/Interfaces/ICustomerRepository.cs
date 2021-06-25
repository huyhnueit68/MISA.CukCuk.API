using MISA.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Interfaces
{
    /// <summary>
    /// Interface danh mục khách hàng
    /// </summary>
    public interface ICustomerRepository
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
        /// Lấy dữ liệu khách hàng theo mã khách hàng
        /// </summary>
        /// <param name="code">Mã khách hàng</param>
        /// <returns>Trả về khách hàng có mã khách hàng tương ứng</returns>
        /// CreatedBy: PQ Huy (24/06/2021)
        Customer GetCustomerByCode(string code);

        /// <summary>
        /// Lấy ra nhóm khách hàng theo id
        /// </summary>
        /// <param name="id">id nhóm khách hàng</param>
        /// <returns>Trả về nhóm khách hàng tương ứng</returns>
        /// CreatedBy: PQ Huy (24/06/2021)
        CustomerGroup GetCustomerGroupById(Guid id);

        /// <summary>
        /// Lấy ra khách hàng bằng số điện thoại
        /// </summary>
        /// <param name="phoneNumber">Số điện thoại khách hàng</param>
        /// <returns>Trả về khách hàng có số điện thoại tương ứng</returns>
        /// CreatedBy: PQ Huy (24/06/2021)
        Customer GetCustomerByPhone(string phoneNumber);

        /// <summary>
        /// Lấy ra khách hàng bằng email
        /// </summary>
        /// <param name="email">Số điện thoại khách hàng</param>
        /// <returns>Trả về khách hàng có số điện thoại tương ứng</returns>
        /// CreatedBy: PQ Huy (24/06/2021)
        Customer GetCustomerByEmail(string email);

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
        ServiceResult DeleteCustomerById(Guid id);
    }
}
