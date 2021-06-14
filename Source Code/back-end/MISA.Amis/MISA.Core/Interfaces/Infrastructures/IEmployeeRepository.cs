using MISA.Core.Entities.Directory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Infrastructures
{
    /// <summary>
    /// Interface Infrastructure nhân viên (Employee)
    /// </summary>
    /// Created By: NTHIEU (12/06/2021)
    public interface IEmployeeRepository:IBaseRepository<Employee>
    {
        #region Get Data
        /// <summary>
        /// Lấy dữ liệu tìm kiếm, phân trang cho nhân viên
        /// </summary>
        /// <param name="PageIndex">Số thứ tự của trang</param>
        /// <param name="PageSize">Số bản ghi/trang</param>
        /// <param name="DataFilter">Giá trị cần tìm kiếm</param>
        /// <returns>
        /// Objects (IEnumrable)
        /// </returns>
        /// Created By: NTHIEU (12/06/2021)
        Task <IEnumerable<Employee>> GetEmployeePagingFilter(int PageIndex, int PageSize, string DataFilter);

        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>
        /// (String) Mã nhân viên
        /// </returns>
        Task<string> GetNewEmployeeCode();
        #endregion
    }
}
