using MISA.Core.Entities.Base;
using MISA.Core.Interfaces.Infrastructures;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    /// <summary>
    /// Service dùng chung
    /// </summary>
    /// <typeparam name="Entity"> Đối tượng cần xử lý</typeparam>
    /// Created By: NTHIEU (12/06/2021)
    public class BaseService<Entity> : IBaseService<Entity> where Entity:BaseEntity
    {
        #region Declare
        /// <summary>
        /// đối tượng kết nối với base repository
        /// </summary>
        protected IBaseRepository<Entity> _baseRepository;
        #endregion

        #region Constructor
        /// <summary>
        /// Hàm khởi tạo service
        /// </summary>
        /// <param name="p_baseRepository"></param>
        public BaseService(IBaseRepository<Entity> p_baseRepository)
        {
            _baseRepository = p_baseRepository;
        }
        #endregion

        #region Method implement
        public async virtual Task<ActionServiceResult> DeleteData(Guid id)
        {
            var rowAffect = await _baseRepository.DeleteData(id);
            return new ActionServiceResult(200, rowAffect.ToString(), "devMsg");
        }

        public Task<IEnumerable<Entity>> GetAllData()
        {
            var entities = _baseRepository.GetAll();
            return entities;
        }

        public async virtual Task<Entity> GetById(Guid Id)
        {
            var entity = await _baseRepository.GetById(Id);
            return entity;
        }

        public async virtual Task<ActionServiceResult> InsertData(Entity entity)
        {
            if (Validate(entity)==true)
            {
                return new ActionServiceResult
                {
                    StatusCode = 200,
                    UserMsg = "Thêm mới thành công",
                    DevMsg = "Thêm mới thành công",
                    Data = await _baseRepository.InsertData(entity)
                };
            }
            else
            {
                return new ActionServiceResult
                {
                    StatusCode = 402,
                    UserMsg = "Dữ liệu không hợp lệ",
                    DevMsg = "Dữ liệu không hợp lệ",
                    Data = null
                };
            }
        }

        public async virtual Task<ActionServiceResult> UpdateData(Entity entity, Guid id)
        {
            if (Validate(entity) == true)
            {
                return new ActionServiceResult
                {
                    StatusCode = 200,
                    UserMsg = "Sửa thành công",
                    DevMsg = "Sửa thành công",
                    Data = await _baseRepository.UpdateData(entity, id)
                };
            }
            else
            {
                return new ActionServiceResult
                {
                    StatusCode = 402,
                    UserMsg = "Dữ liệu không hợp lệ",
                    DevMsg = "Dữ liệu không hợp lệ",
                    Data = null
                };
            }
        }
        #endregion

        #region Function, Method not implement
        /// <summary>
        /// Xử lý validate dữ liệu đối tượng
        /// </summary>
        /// <param name="entity"> đối tượng cần xử lý </param>
        /// <returns>
        /// - true: Dữ liệu hợp lệ
        /// - false: Dữ liệu không hợp lệ
        /// </returns>
        public virtual bool Validate(Entity entity)
        {
            return true;
        }
        #endregion


    }
}
