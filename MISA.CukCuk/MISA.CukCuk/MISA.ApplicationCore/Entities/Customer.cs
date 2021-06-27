using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entities
{
    /// <summary>
    /// Customer
    /// </summary>
    /// Created by: PQ Huy (21.06.2021)
    public class Customer
    {
        #region Declart

        #endregion

        #region Constructor

        #endregion

        #region Property
        /// <summary>
        /// Khóa chính
        /// </summary>
        [PrimaryKey]
        public Guid? CustomerId { get; set; }

        /// <summary>
        /// Mã khách hàng
        /// </summary>
        [CheckDuplicate]
        [Required]
        public string CustomerCode { get; set; }
        
        /// <summary>
        /// Họ và tên
        /// </summary>
        public string FullName { get; set; }


        /// <summary>
        /// Ngày tháng năm sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }


        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Adress { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string PhoneNumber { get; set; }


        /// <summary>
        /// Giới tính (0- nữ, 1- nam, 2- khác)
        /// </summary>
        public int? Gender { get; set; }

        /// <summary>
        /// email khách hàng
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Tạo bỏi
        /// </summary>
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Ngày thay đổi
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Mã nhóm khách hàng
        /// </summary>
        public Guid? CustomerGroupId { get; set; }

        /// <summary>
        /// Mã số thẻ thành viên
        /// </summary>
        public string MemberCardCode { get; set; }

        /// <summary>
        /// note
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Tên công ty
        /// </summary>
        public string CompanyName { get; set; }
        
        /// <summary>
        /// Mã số thuế công ty
        /// </summary>
        public string CompanyTaxCode { get; set; }

        #endregion

        #region Method

        #endregion
    }
}
