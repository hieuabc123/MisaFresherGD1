using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Core.Entities.Base;
using MISA.Core.Interfaces.Infrastructures;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure.Data
{
    public class BaseRepository<Entity> : IBaseRepository<Entity> where Entity:BaseEntity
    {
        #region Declare
        /// <summary>
        /// Tên của Entity
        /// </summary>
        string _entityName;

        /// <summary>
        /// Chuỗi connectionString
        /// </summary>
        string _connectionString;

        /// <summary>
        /// Đối tượng kết nối với DB
        /// </summary>
        protected IDbConnection dbConnection;

        /// <summary>
        /// Đối tượng kết nối với Appsetting.JSON
        /// </summary>
        IConfiguration _configuration;

        /// <summary>
        /// Các parameter cho Store
        /// </summary>
        protected DynamicParameters _dynamicParameters;
        #endregion

        #region Constructor
        /// <summary>
        ///     - Hàm khởi tạo Constructor
        /// </summary>
        /// <param name="p_configuration">đối tượng kết nối với appsetting.JSON để lấy dữ liệu</param>
        /// Created By: NTHIEU (28/05/2021)
        public BaseRepository(IConfiguration p_configuration) //Hàm khởi tạo đối tượng
        {
            //1. Lấy ra Tên của Entity
            _entityName = typeof(Entity).Name; 

            //2. gán đối tượng iconfiguration để lấy dữ liệu trong Appsetting.JSON
            _configuration = p_configuration;

            //3. Gán connectionString bằng giá trị lấy trên Appsetting.JSON
            _connectionString = _configuration.GetConnectionString("DefaultConnection");

            //4. Khởi tạo kết nối
            InitConnection();
        }
        #endregion

        #region Method implement
        public async Task<int> DeleteData(Guid id)
        {
            //1. Gán giá trị đầu vào cho tham số trong StoreProceduce
            _dynamicParameters.Add($"@p_{_entityName}Id", id);

            //2. Thực thi truy vấn Xóa dữ liệu
            var rowAffect = await dbConnection.ExecuteAsync($"Proc_Delete{_entityName}", param: _dynamicParameters, commandType: CommandType.StoredProcedure);

            //3. Kết quả trả về
            return rowAffect;
        }

        public async Task<IEnumerable<Entity>> GetAll()
        {
            //1. Thực hiện truy vấn lấy dữ liệu
            var entities = await dbConnection.QueryAsync<Entity>($"Proc_Get{_entityName}s", commandType: CommandType.StoredProcedure);

            //2. Kết quả trả về
            return entities;
        }

        public async Task<Entity> GetById(Guid id)
        {
            //1. Gán giá trị đầu vào cho tham số trong StoreProceduce
            _dynamicParameters.Add($"@p_{_entityName}Id", id);

            //2. Thực hiện truy vấn lấy dữ liệu trên DB
            var entity = await dbConnection.QueryFirstOrDefaultAsync<Entity>($"Proc_Get{_entityName}s", commandType: CommandType.StoredProcedure);

            //3. Kết quả trả về
            return entity;
        }

        public async Task<int> InsertData(Entity entity)
        {
            //1. Mapping dữ liệu vào param cho Store
            MappingParameterWithObjectValue(entity);

            //2. Truy vấn thực hiện Thêm mới dữ liệu
            var rowAffect = await dbConnection.ExecuteAsync($"Proc_Insert{_entityName}", param: _dynamicParameters, commandType: CommandType.StoredProcedure);

            //3. Kết quả trả về
            return rowAffect;
        }

        public async Task<int> UpdateData(Entity entity, Guid id)
        {
            //0. Gán lại Id cho đối tượng (Form Sửa)
            entity.GetType().GetProperty($"{_entityName}Id").SetValue(entity, id);

            //1. Mapping dữ liệu vào param cho Store
            MappingParameterWithObjectValue(entity);

            //2. Truy vấn thực hiện Sửa dữ liệu
            var rowAffect = await dbConnection.ExecuteAsync($"Proc_Update{_entityName}", param: _dynamicParameters, commandType: CommandType.StoredProcedure);

            //3. Kết quả trả về
            return rowAffect;
        }
        #endregion

        #region Method not implement
        /// <summary>
        /// - Khởi tạo kết nối -> DB
        /// </summary>
        /// Created By: NTHIEU (28/05/2021)
        public void InitConnection()
        {
            _dynamicParameters = new DynamicParameters();
            dbConnection = new MySqlConnection(_connectionString);
        }

        /// <summary>
        ///     - Mapping tự động các tham số từ Object -> parameter in StoreProceduce
        /// </summary>
        /// <param name="customer"></param>
        /// Created By: NTHIEU (22/05/2021)
        void MappingParameterWithObjectValue(Entity entity)
        {
            //1. lấy các giá trị properties từ object
            var properties = typeof(Entity).GetProperties();
            //2. Duyệt từng property trong properties
            foreach (var property in properties)
            {
                //2.1 Lấy value
                var value = property.GetValue(entity);

                //2.2 Lấy ra tên của Property
                var propertyName = property.Name;

                //2.3 Đặt tên cho Parameter (Tham số đầu vào)
                var paraName = $"@p_{propertyName}";

                //2.4 Gán giá trị tương ứng cho parameter
                _dynamicParameters.Add(paraName, value);
            }
        }
        #endregion
    }
}
