using MISA.Core.Attributes;
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
                    UserMsg = entity.Status,
                    DevMsg = entity.Status,
                    Data = await _baseRepository.UpdateData(entity, id)
                };
            }
            else
            {
                return new ActionServiceResult
                {
                    StatusCode = 402,
                    UserMsg = entity.Status,
                    DevMsg = entity.Status,
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
            var isValid = CheckNullorEmpty(entity);
            //var isValid = CheckDuplicateInDataBase(entity);
            return isValid;
        }

        /// <summary>
        ///     - Check Value (Required) is Null in Object
        ///     - entity is (Object)
        /// </summary>
        /// <param name="entity">Đối tượng cần xử lý</param>
        /// <returns>
        ///     -bool
        ///     -True or False
        /// </returns>
        /// Created By: NTHIEU (28/05/2021)
        public bool CheckNullorEmpty(Entity entity)
        {
            //1. Declare
            var isValid = true;

            //2. Lấy các property của đối tượng
            var properties = entity.GetType().GetProperties();

            //3. Duyệt từng property
            foreach (var prop in properties)
            {
                //3.1 lấy giá trị
                var propValue = prop.GetValue(entity);
                //3.2 Lấy tên property
                var propName = prop.Name;

                //3.3 Lấy type của prop tương ứng
                var propType = prop.PropertyType;

                //3.4 Gán giá trị cho những property có Required
                var attributeRequired = prop.GetCustomAttributes(typeof(Required), true);

                //3.5 Nếu có trường Required (Check Trống)
                if (attributeRequired.Length > 0)
                {
                    var msg = (attributeRequired[0] as Required).Msg;
                    if ((propType == typeof(Guid?) || propType == Nullable.GetUnderlyingType(typeof(Guid?))) && propValue == null )
                    {
                        entity.Status += $"{msg}";
                        isValid = false;
                    }
                    else if (string.IsNullOrEmpty(propValue.ToString())|| propValue == null)
                    {
                        entity.Status = msg;
                        isValid = false;
                    }
                }
            };

            return isValid;
        }

        /// <summary>
        ///     - Check Value is Duplicate in Database
        ///     - entity is (Object)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>
        ///     -bool
        ///     -True or False
        /// </returns>
        /// Created By: NTHIEU (14/06/2021)
        public bool CheckDuplicateInDataBase(Entity entity)
        {
            var isValid = true;
            //1. Lấy toàn bộ property của entity
            var properties = entity.GetType().GetProperties();

            //2. Duyệt từng property trong properties
            foreach (var prop in properties)
            {
                //2.1 Lấy value của property
                var propValue = prop.GetValue(entity);

                //2.2 Lấy tên của property
                var propName = prop.Name;

                //2.3 Lấy Type của property
                var propType = prop.PropertyType;

                //2.4 Lấy attribute tương ứng
                var attributeRCheckDuplicateDB = prop.GetCustomAttributes(typeof(CheckDuplicateDB), true);

                //2.5 Kiểm tra nếu có attribute thì thực hiện công việc
                if (attributeRCheckDuplicateDB.Length > 0 && propValue != null)
                {
                    var msg = (attributeRCheckDuplicateDB[0] as CheckDuplicateDB).Msg;

                    var isExists = _baseRepository.CheckDuplicate(propName, propValue.ToString(), entity);
                    if (isExists == true)
                    {
                        isValid = false;
                        entity.Status += $"\n{msg}";
                    }
                }
            };

            return isValid;
        }
        #endregion


    }
}
