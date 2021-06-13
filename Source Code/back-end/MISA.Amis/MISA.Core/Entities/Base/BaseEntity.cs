using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities.Base
{
    /// <summary>
    /// Thông tin chung của các đối tượng
    /// </summary>
    /// Created By: NTHIEU (12/06/2021)
    public class BaseEntity
    {
        /// <summary>
        /// Ngày tạo dữ liệu
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Người tạo tạo dữ liệu
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Ngày sửa dữ liệu
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Người sửa dữ liệu
        /// </summary>
        public string ModifiedBy { get; set; }
    }
}
