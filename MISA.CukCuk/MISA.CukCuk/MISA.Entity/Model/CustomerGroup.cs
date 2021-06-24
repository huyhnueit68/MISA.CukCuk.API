using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Infrastructure.Model
{
    public class CustomerGroup
    {
        #region Property
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid? CustomerGroupId { get; set; }

        /// <summary>
        /// tên nhóm
        /// </summary>
        public string CustomerGroupName { get; set; }

        /// <summary>
        /// parent id
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// mô tả thông tin nhóm
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// người tạo bản ghi
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// ngày thay đổi
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// người sửa gần nhất
        /// </summary>
        public string ModifiedBy { get; set; }
        #endregion
    }
}
