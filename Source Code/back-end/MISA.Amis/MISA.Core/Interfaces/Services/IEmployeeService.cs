using MISA.Core.Entities.Base;
using MISA.Core.Entities.Directory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Services
{
    /// <summary>
    /// Các hàm xử lý cho Service Nhân viên
    /// </summary>
    /// Created By: NTHIEU (12/06/2021)
    public interface IEmployeeService:IBaseService<Employee>
    {
        /// <summary>
        /// Tìm kiếm và phân trang danh sách nhân viên theo yêu cầu
        /// </summary>
        /// <param name="pageIndex">Số thứ tự của trang</param>
        /// <param name="pageSize">Số bản ghi / Trang</param>
        /// <param name="filter">giá trị cần tìm kiếm</param>
        /// <returns>
        /// - 200: Giá trị trả về
        /// - 202: Không có giá trị nào
        /// - 500: Lỗi hệ thống
        /// </returns>
        /// Created By: NTHIEU (13/06/2021)
        public Task<ActionServiceResult> GetEmployeesPagingFilter(int pageIndex, int pageSize, string filter);
    }
}
