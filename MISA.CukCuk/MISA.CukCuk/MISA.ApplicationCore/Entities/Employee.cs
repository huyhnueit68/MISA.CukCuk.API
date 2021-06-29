using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entities
{
    /// <summary>
    /// Thông tin nhân viên
    /// </summary>
    /// CreatedBy: PQ Huy (25.06.2021)
    public class Employee : BaseEntity
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [PrimaryKey]
        [DisplayName("Khóa chính nhân viên")]
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [CheckDuplicate]
        [Required]
        [DisplayName("Mã nhân viên")]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Họ và tên nhân viên
        /// </summary>
        [DisplayName("Họ và tên")]
        public string? FullName { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        [DisplayName("Giới tính")]
        public int? Gender { get; set; }

        /// <summary>
        /// Ngày sinh nhân viên
        /// </summary>
        [DisplayName("Ngày sinh")]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        ///Số điện thoại
        /// </summary>
        [CheckDuplicate]
        [DisplayName("Số điện thoại")]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Địa chỉ email
        /// </summary>
        [CheckDuplicate]
        [DisplayName("Email")]
        public string? Email { get; set; }

        /// <summary>
        /// Địa chỉ nhân viên
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Số chứng minh nhân dân của nhân viên
        /// </summary>
        [DisplayName("Mã chứng minh nhân dân")]
        public string? IdentityNumber { get; set; }

        /// <summary>
        /// Mức lương
        /// </summary>
        public string? Salary { get; set; }

        /// <summary>
        /// Trạng thái làm việc
        /// </summary>
        public int? WorkStatus { get; set; }

        /// <summary>
        /// Mã phòng ban
        /// </summary>
        public Guid? DepartmentId { get; set; }

        /// <summary>
        /// Tên giới tính
        /// </summary>
        public string? GenderName { get; set; }

        /// <summary>
        /// Tên trạng thái làm việc
        /// </summary>
        public string? WorkStatusName { get; set; }

        /// <summary>
        /// Ngày tạo nhân viên
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Tạo bởi
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Ngày thay đổi
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Thay đổi bởi
        /// </summary>
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// Mã chức vụ
        /// </summary>
        public Guid? PositionId { get; set; }

    }
}
