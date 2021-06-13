using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities.Base;
using MISA.Core.Entities.Directory;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Amis.Client.Controllers
{
    /// <summary>
    /// Class Base Controller
    /// </summary>
    /// <typeparam name="Entity">Đối tượng cần xử lý</typeparam>
    /// Created By: NTHIEU (13/06/2021)
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<Entity> : ControllerBase where Entity:BaseEntity
    {
        /// <summary>
        /// Interface reference tới BaseService
        /// </summary>
        protected readonly IBaseService<Entity> _baseService;

        /// <summary>
        /// Khởi tạo service chung
        /// </summary>
        /// <param name="baseEntityService"></param>
        /// CreateBy: NVMANH (20/04/2020)
        public BaseController(IBaseService<Entity> baseService)
        {
            _baseService = baseService;
        }
        #region Get Data
        /// <summary>
        /// Lấy toàn bộ dữ liệu của đối tượng
        /// </summary>
        /// <returns>
        /// - 200 : load dữ liệu thành công
        /// - 202 : Không có dữ liệu
        /// - 400 : Lỗi dữ liệu không hợp lệ
        /// - 500 : Lỗi hệ thống
        /// </returns>
        /// Created By: NTHIEU (13/06/2021)
        [HttpGet]
        public virtual async Task <IActionResult> GetAllData()
        {
            var res = await _baseService.GetAllData();
            return Ok(res);
        }

        /// <summary>
        /// Lấy Dữ liệu theo Id
        /// </summary>
        /// <returns>
        /// - 200 : load dữ liệu thành công
        /// - 202 : Không có dữ liệu
        /// - 400 : Lỗi dữ liệu không hợp lệ
        /// - 500 : Lỗi hệ thống
        /// </returns>
        /// Created By: NTHIEU (13/06/2021)
        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetById(Guid id)
        {
            var res = await _baseService.GetById(id);
            return Ok(res);
        }
        #endregion

        #region Post Data
        /// <summary>
        ///  Thêm mới Nhân viên
        /// </summary>
        /// <param name="entity">Đối tượng cần thêm mới</param>
        /// <returns>
        /// - 200: Thêm mới thành công
        /// - 400: Thông tin dữ liệu không hợp lệ
        /// - 500: Lỗi Hệ thống
        /// </returns>
        /// Created By: NTHIEU (13/06/2021)
        [HttpPost]
        public virtual async Task<IActionResult> PostData([FromBody] Entity entity)
        {
            var res = await _baseService.InsertData(entity);
            return Ok(res);
        }
        #endregion

        #region Put Data
        /// <summary>
        ///  Sửa thông tin Nhân viên
        /// </summary>
        /// <param name="entity">Đối tượng cần Sửa</param>
        /// <param name="id">Khóa chính của đối tượng</param>
        /// <returns>
        /// - 200: Thêm mới thành công
        /// - 400: Thông tin dữ liệu không hợp lệ
        /// - 500: Lỗi Hệ thống
        /// </returns>
        /// Created By: NTHIEU (13/06/2021)
        [HttpPut("{id}")]
        public virtual async Task<IActionResult> PutData([FromBody] Entity entity, Guid id)
        {
            var res = await _baseService.UpdateData(entity, id);
            return Ok(res);
        }
        #endregion

        #region Delete Data
        /// <summary>
        /// Xóa 1 bản ghi theo khóa chính
        /// </summary>
        /// <param name="id">Khóa chính của đối tượng</param>
        /// <returns>
        /// - 200: Xóa thành công
        /// - 400: Thông tin dữ liệu không hợp lệ
        /// - 500: Lỗi Hệ thống 
        /// </returns>
        /// Created By: NTHIEU (13/06/2021)
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(Guid id)
        {
            var res = await _baseService.DeleteData(id);
            return Ok(res);
        }
        #endregion
    }
}
