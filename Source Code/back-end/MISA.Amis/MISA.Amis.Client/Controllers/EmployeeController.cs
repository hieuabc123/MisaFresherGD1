using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities.Directory;
using MISA.Core.Interfaces.Services;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MISA.Amis.Client.Controllers
{
    /// <summary>
    /// Controller API Nhân Viên
    /// </summary>
    /// Created By: NTHIEU (13/06/2021)
    [Route("api/v1/[controller]s")]
    [ApiController]
    public class EmployeeController : BaseController<Employee>
    {
        #region Declare
        /// <summary>
        /// Đối tượng Service
        /// </summary>
        IEmployeeService _employeeService;
        #endregion

        #region Constructor
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="p_employeeService"> Đối tượng service truyền vào</param>
        /// <param name="baseService"> Đối tượng BaseService</param>
        public EmployeeController(IEmployeeService p_employeeService, IBaseService<Employee> baseService) : base(baseService)
        {
            _employeeService = p_employeeService;
        }
        #endregion

        #region Get data
        /// <summary>
        /// Tìm kiếm phân trang dữ liệu nhân viên
        /// </summary>
        /// <param name="pageInt">Số thứ tự của trang</param>
        /// <param name="pageSize">Số bản ghi / trang</param>
        /// <param name="dataFilter">Giá trị cần tìm kiếm</param>
        /// <returns>
        /// - 200: Lấy dữ liệu thành công
        /// - 400: Thông tin dữ liệu không hợp lệ
        /// - 500: Lỗi Hệ thống
        /// </returns>
        /// Created By: NTHIEU (14/06/2021)
        [HttpGet("filter")]
        public async Task<IActionResult> GetEmployeesPagingFilter(int pageInt, int pageSize, string dataFilter)
        {
            var res = await _employeeService.GetEmployeesPagingFilter(pageInt, pageSize, dataFilter);
            return Ok(res);
        }

        /// <summary>
        /// Lấy thông tin khởi tạo nhân viên mới
        /// </summary>
        /// <returns>
        /// - 200: Thêm mới thành công
        /// - 400: Thông tin dữ liệu không hợp lệ
        /// - 500: Lỗi Hệ thống
        /// </returns>
        /// Created By: NTHIEU (14/06/2021)
        [HttpGet("new")]
        public async Task<IActionResult> GetNewEmployee()
        {
            var res = await _employeeService.GetNewEmployee();
            return Ok(res);
        }

        /// <summary>
        /// Lấy thông tin Nhân bản nhân viên
        /// </summary>
        /// <param name="id">Khóa chính của Nhân viên cần lấy thông tin</param>
        /// <returns>
        /// - 200: Thêm mới thành công
        /// - 400: Thông tin dữ liệu không hợp lệ
        /// - 500: Lỗi Hệ thống 
        /// </returns>
        /// Created By: NTHIEU (14/06/2021)
        [HttpGet("Duplicate/{id}")]
        public async Task<IActionResult> GetDuplicateEmployee(Guid id)
        {
            var res = await _employeeService.GetDuplicateEmployee(id);
            return Ok(res);
        }
        #endregion

        #region Export
        /// <summary>
        /// Export dữ liệu nhân viên ra định dạng Excel
        /// </summary>
        /// <returns>
        /// File Excel (.xlsx)
        /// </returns>
        /// Created By: NTHIEU (18/06/2021)
        [HttpGet("export")]
        public async Task<IActionResult> Export()
        {
            //1. Xử lý tạo fileExcel dữ liệu nhân viên
            var stream = await _employeeService.ExportEmployees();
            //2. Đặt tên cho file
            string excelName = $"Danh sách nhân viên.xlsx";
            //3. Trả về cho Client 
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
        #endregion
    }
}
