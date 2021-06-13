using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Core.Entities.Directory;
using MISA.Core.Interfaces.Infrastructures;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure.Data
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        #region Declare
        #endregion

        #region Constructor
        public EmployeeRepository(IConfiguration p_configuration):base(p_configuration)
        {
        }
        #endregion

        #region Method Implement
        public async Task<IEnumerable<Employee>> GetEmployeePagingFilter(int PageIndex, int PageSize , string DataFilter)
        {
            //1. Gán giá trị cho tham số đầu vào trong Store
            this._dynamicParameters.Add("p_PageInt", PageIndex);
            this._dynamicParameters.Add("p_PageSize", PageSize);
            this._dynamicParameters.Add("p_Filter", DataFilter);

            //2. Thực hiện truy vấn tới Database
            var employees = await this.dbConnection.QueryAsync<Employee>("Proc_GetEmployees_Paging_Filter", param: _dynamicParameters, commandType: CommandType.StoredProcedure);

            //3. Kết quả trả về
            return employees;
        }
        #endregion

    }
}
