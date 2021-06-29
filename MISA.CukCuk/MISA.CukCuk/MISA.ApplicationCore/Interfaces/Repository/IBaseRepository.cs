﻿using MISA.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Interfaces
{
    public interface IBaseRepository<Generic>
    {
        /// <summary>
        /// Lấy toàn bộ danh sách
        /// </summary>
        /// <returns> Trả về danh sách </returns>
        /// CreatedBy: PQ Huy (25/06/2021)
        IEnumerable<Generic> Get();

        /// <summary>
        /// Lấy thông tin theo mã
        /// </summary>
        /// <param name="id"> Mã bản ghi</param>
        /// <returns>Trả về bản ghi tương ứng</returns>
        /// CreatedBy: PQ Huy (25/06/2021)
        IEnumerable<Generic> GetById(Guid id);

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="data">Dữ liệu bản ghi</param>
        /// <returns>Trả về số bản ghi được thêm</returns>
        /// CreatedBy: PQ Huy (25/06/2021)
        ServiceResult Insert(Generic data);

        /// <summary>
        /// Sửa thông tin bản ghi
        /// </summary>
        /// <param name="id">Mã bản ghi</param>
        /// <param name="data">Dữ liệu bản ghi cần cập nhật</param>
        /// <returns>Trả về trạng thái cập nhật dữ liệu</returns>
        ServiceResult Update(Guid id, Generic data);

        /// <summary>
        /// Xóa thông tin khách hàng theo khóa chính
        /// </summary>
        /// <param name="id">Mã bản ghi</param>
        /// <returns>Trả về thạng thái cập nhật danh sách bản ghi</returns>
        /// CreatedBy: PQ Huy (25.06.2021)
        ServiceResult DeleteById(Guid id);

        /// <summary>
        /// Lấy dữ liệu theo một tiêu chí
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        /// CreatedBy: PQ Huy (28.06.2021)
        IEnumerable<Generic> GetEntityByProperty(Generic generic, PropertyInfo property);

        /// <summary>
        /// Nập khẩu dữ liệu
        /// </summary>
        /// <param name="data">Dữ liệu nhận khẩu</param>
        /// <returns>Trả về số bản ghi nhập khẩu thành công</returns>
        /// CreatedBy: PQ Huy (29.06.2021)
        ServiceResult ImportData(Generic[] data);

    }
}
