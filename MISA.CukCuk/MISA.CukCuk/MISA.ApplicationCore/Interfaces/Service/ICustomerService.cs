using MISA.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Interfaces
{
    public interface ICustomerService : IBaseService<Customer>
    {
        /// <summary>
        /// Lấy dữ liệu khách hàng theo mã code khách hàng
        /// </summary>
        /// <param name="code">Mã code khách hàng</param>
        /// <returns>Lấy danh sách khách hàng theo code</returns>
        /// CreatedBy: PQ Huy (26.06.2021)
        IEnumerable<Customer> GetCustomerByCode(string code);

        /// <summary>
        /// Phân trang dữ liệu
        /// </summary>
        /// <param name="pageNumber">Index của page hiện tại</param>
        /// <param name="pageSize">Kích thước mỗi page</param>
        /// <returns></returns>
        /// CreatedBy: PQ Huy (26.06.2021)
        IEnumerable<Customer> GetCustomerPaging(int pageNumber, int pageSize);
    }
}
