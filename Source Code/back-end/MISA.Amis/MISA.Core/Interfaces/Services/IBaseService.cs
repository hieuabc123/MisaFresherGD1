using MISA.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Services
{
    /// <summary>
    /// Base Service xử lý các logic chung
    /// </summary>
    /// <typeparam name="Entity">Đối tượng cần xử lý</typeparam>
    /// Created By: NTHIEU (12/06/2021)
    public interface IBaseService<Entity> where Entity:BaseEntity
    {
        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns>
        /// - 200: nếu Có dữ liệu trả về
        /// - 201: nếu không có dữ liệu trả về
        /// </returns>
        /// Created By: NTHIEU (12/06/2021)
        Task<IEnumerable<Entity>> GetAllData();

        /// <summary>
        /// Lấy dữ liệu theo Khóa chính (ID)
        /// </summary>
        /// <param name="Id">khóa chính</param>
        /// <returns>
        /// - 200: nếu Có dữ liệu trả về
        /// - 201: nếu không có dữ liệu trả về
        /// </returns>
        /// Created By: NTHIEU (12/06/2021)
        Task<Entity> GetById(Guid Id);

        /// <summary>
        /// - Xử lý Thêm mới dữ liệu
        /// </summary>
        /// <param name="entity">Đối tượng cần xử lý</param>
        /// <returns>
        /// - 200: nếu Thêm mới dữ liệu thành công
        /// - 400: nếu có lỗi validate
        /// - 500: nếu có lỗi từ Hệ thống
        /// </returns>
        /// Created By: NTHIEU (12/06/2021)
        Task<ActionServiceResult> InsertData(Entity entity);

        /// <summary>
        /// - Xử lý Sửa dữ liệu 1 bản ghi
        /// </summary>
        /// <param name="entity">Đối tượng cần xử lý</param>
        /// <param name="id">Khóa chính của đối tượng</param>
        /// <returns>
        /// - 200: nếu Sửa dữ liệu thành công
        /// - 400: nếu có lỗi validate
        /// - 500: nếu có lỗi từ Hệ thống
        /// </returns>
        /// Created By: NTHIEU (12/06/2021)
        Task<ActionServiceResult> UpdateData(Entity entity, Guid id);

        /// <summary>
        /// Xóa dữ liệu theo khóa chính
        /// </summary>
        /// <param name="id">PK bản ghi trong CSDL</param>
        /// <returns>Số bản ghi được xóa trong Database</returns>
        /// CreateBy: NVMANH (20/04/2020)
        Task<ActionServiceResult> DeleteData(Guid id);
    }
}
