using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entities
{
    /// <summary>
    /// Thông tin nhân viên
    /// </summary>
    /// CreatedBy: PQ Huy (25.06.2021)
    public class Employee
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid? EmployeeId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string? EmployeeCode { get; set; }

        /// <summary>
        /// Họ và tên nhân viên
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// Ngày sinh nhân viên
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        ///Số điện thoại
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Địa chỉ email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Địa chỉ nhân viên
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Số chứng minh nhân dân của nhân viên
        /// </summary>
        public string IdentityNumber { get; set; }

        /// <summary>
        /// Mức lương
        /// </summary>
        public string Salary { get; set; }

        /// <summary>
        /// Trạng thái làm việc
        /// </summary>
        public int WorkStatus { get; set; }

        /// <summary>
        /// Mã phòng ban
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Tên giới tính
        /// </summary>
        public string GenderName { get; set; }

        /// <summary>
        /// Tên trạng thái làm việc
        /// </summary>
        public string WorkStatusName { get; set; }

        /// <summary>
        /// Ngày tạo nhân viên
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Tạo bởi
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Ngày thay đổi
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Thay đổi bởi
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Mã chức vụ
        /// </summary>
        public Guid PositionId { get; set; }

    }
}
