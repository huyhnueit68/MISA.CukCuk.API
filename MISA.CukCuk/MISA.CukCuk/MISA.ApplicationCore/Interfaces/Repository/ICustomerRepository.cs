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
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
       
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
        /// Phân trang dữ liệu
        /// </summary>
        /// <param name="pageIndex">Index của page hiện tại</param>
        /// <param name="pageSize">Kích thước mỗi page</param>
        /// <returns></returns>
        /// CreatedBy: PQ Huy (28.06.2021)
        IEnumerable<Customer> GetCustomerPaging(int pageIndex, int pageSize);

    }
}
