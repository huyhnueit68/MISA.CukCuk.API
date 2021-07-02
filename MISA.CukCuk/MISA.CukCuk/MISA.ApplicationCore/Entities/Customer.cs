using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entities
{
    /// <summary>
    /// Customer
    /// </summary>
    /// Created by: PQ Huy (21.06.2021)
    public class Customer : BaseEntity
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
        [DisplayName("Khóa chính khách hàng")]
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Mã khách hàng
        /// </summary>
        [CheckDuplicate]
        [Required]
        [DisplayName("Mã khách hàng")]
        public string CustomerCode { get; set; }
        
        /// <summary>
        /// Họ và tên
        /// </summary>
        [DisplayName("Họ và tên")]
        public string? FullName { get; set; }

        /// <summary>
        /// Ngày tháng năm sinh
        /// </summary>
        [DisplayName("Ngày sinh")]
        public DateTime? DateOfBirth { get; set; }


        /// <summary>
        /// Địa chỉ
        /// </summary>
        [DisplayName("Địa chỉ")]
        public string? Adress { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        [CheckDuplicate]
        [DisplayName("Số điện thoại")]
        public string PhoneNumber { get; set; }


        /// <summary>
        /// Giới tính (0- nữ, 1- nam, 2- khác)
        /// </summary>
        [DisplayName("Giới tính")]
        public int? Gender { get; set; }

        /// <summary>
        /// email khách hàng
        /// </summary>
        [CheckDuplicate]
        [DisplayName("Email")]
        public string Email { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        [DisplayName("Ngày tạo")]
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Tạo bỏi
        /// </summary>
        [DisplayName("Tạo bởi")]
        public string? CreatedBy { get; set; }

        [DisplayName("Ngày thay đổi")]
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Ngày thay đổi
        /// </summary>
        [DisplayName("Thay đổi bởi")]
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// Mã nhóm khách hàng
        /// </summary>
        [DisplayName("Mã nhóm khách hàng")]
        public Guid? CustomerGroupId { get; set; }

        /// <summary>
        /// Tên nhóm khách hàng
        /// </summary>
        [DisplayName("Tên nhóm khách hàng")]
        public string? CustomerGroupName { get; set; }

        /// <summary>
        /// Mã số thẻ thành viên
        /// </summary>
        [DisplayName("Mã thẻ thành viên")]
        public string? MemberCardCode { get; set; }

        /// <summary>
        /// note
        /// </summary>
        [DisplayName("Ghi chú")]
        public string? Note { get; set; }

        /// <summary>
        /// Tên công ty
        /// </summary>
        [DisplayName("Tên công ty")]
        public string? CompanyName { get; set; }

        /// <summary>
        /// Mã số thuế công ty
        /// </summary>
        [DisplayName("Mã số thuế")]
        public string? CompanyTaxCode { get; set; }

        #endregion

        #region Method

        #endregion
    }
}
