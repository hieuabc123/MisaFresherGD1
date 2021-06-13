using MISA.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities.Directory
{
    /// <summary>
    /// Thông tin Đối tượng nhân viên
    /// </summary>
    /// Created By: NTHIEU (12/6/2021)
    public class Employee:BaseEntity
    {
        /// <summary>
        /// Khoá chính
        /// </summary>
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Họ và tên nhân viên
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public int Gender { get; set; }
        
        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Số CMT, CMND
        /// </summary>
        public string IdentityNo { get; set; }

        /// <summary>
        /// Ngày cấp CMT, CMND
        /// </summary>
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Nơi cấp CMT, CMND
        /// </summary>
        public string IdentityPlace { get; set; }

        /// <summary>
        /// Khóa ngoại tới phòng ban bảng (Department)
        /// </summary>
        public Guid DepartmentId { get; set; }
        
        /// <summary>
        /// Vị trí trong công ty
        /// </summary>
        public string PositionName { get; set; }

        /// <summary>
        /// Số tài khoản Ngân Hàng
        /// </summary>
        public string AccountNumber { get; set; }
        
        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// Chi Nhánh Ngân hàng
        /// </summary>
        public string BankBranch { get; set; }

        /// <summary>
        /// Địa chỉ nơi ở
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Số điện thoại di động
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Số điện thoại cố định
        /// </summary>
        public string FixedLine { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        #region NotMap
        public string GenderName { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
        #endregion

    }
}
