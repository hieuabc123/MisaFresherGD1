using MISA.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Infrastructures
{
    /// <summary>
    /// Base interface dùng chung cho các đối tượng Infrastructure 
    /// </summary>
    /// Created By: NTHIEU (12/06/2021) //Get Post Put Delete (data)
    public interface IBaseRepository<Entity> where Entity:BaseEntity
    {
        #region Get Data
        /// <summary>
        /// Lấy toàn bộ dữ liệu của đối tượng
        /// </summary>
        /// <returns>
        /// Objects (IEnumrable)
        /// </returns>
        /// Created By: NTHIEU (12/06/2021)
        Task <IEnumerable<Entity>>  GetAll();

        /// <summary>
        /// Lấy dữ liệu của đối tượng theo Id
        /// </summary>
        /// <param name="id">Khóa chính</param>
        /// <returns>
        /// Object
        /// </returns>
        /// Created By: NTHIEU (12/06/2021)
        Task <Entity> GetById(Guid id);
        #endregion

        #region Post Data
        /// <summary>
        /// Thêm mới dữ liệu 1 bản ghi
        /// </summary>
        /// <param name="entity">Object</param>
        /// <returns>
        /// (int) - Số bản ghi thêm thành công
        /// </returns>
        /// Created By: NTHIEU (12/06/2021)
        Task <int>  InsertData(Entity entity);
        #endregion

        #region Put Data
        /// <summary>
        /// Sửa dữ liệu 1 bản ghi
        /// </summary>
        /// <param name="entity">Thông tin cần sửa của đối tượng</param>
        /// <param name="id">Khóa chính của đối tượng cần sửa</param>
        /// <returns>
        /// (int) - Số bản ghi sửa thành công
        /// </returns>
        /// Created By: NTHIEU (12/06/2021)
        Task <int> UpdateData(Entity entity, Guid id);
        #endregion

        #region Delete Data
        /// <summary>
        /// Xóa 1 bản ghi
        /// </summary>
        /// <param name="id">Khóa chính</param>
        /// <returns>
        /// (int) - Số bản ghi xóa thành công
        /// </returns>
        /// Created By: NTHIEU (12/06/2021)
        Task <int> DeleteData(Guid id);
        #endregion

        #region Another
        /// <summary>
        ///     - Check Duplicate data in database
        ///     - propName is (string)
        ///     - propValue is (string)
        /// </summary>
        /// <param name="entity"> Đối tượng cần check</param>
        /// <param name="propName">Tên của property cần check</param>
        /// <param name="propValue">Giá trị của property truyền vào</param>
        /// <returns>
        ///     -(bool)
        ///     -True or False
        /// </returns>
        /// Created By: NTHIEU (14/06/2021)
        public bool CheckDuplicate(string propName, string propValue, Entity entity);
        #endregion
    }
}
