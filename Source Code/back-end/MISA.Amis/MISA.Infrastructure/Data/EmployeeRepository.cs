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
            var employees = await dbConnection.QueryAsync<Employee>("Proc_GetEmployees_Paging_Filter", param: _dynamicParameters, commandType: CommandType.StoredProcedure);

            //3. Kết quả trả về
            return employees;
        }

        public async Task<string> GetNewEmployeeCode()
        {
            //1. Lấy Mã nhân viên lớn nhất
            var employeeCodeMax = await dbConnection.QueryFirstOrDefaultAsync<string>("Proc_GetEmployeeCodeMax", commandType: CommandType.StoredProcedure);

            //2.  Tách lấy giá trị số trong chuỗi
            var numberOfString = employeeCodeMax.Replace("NV-", string.Empty);

            //3. Tăng giá trị lên +1
            int newNumberCode = int.Parse(numberOfString) + 1;

            //- Gán giá trị mới cho mã nhân viên (employeeCode)
            var newEmployeeCode = "";
            if (newNumberCode < 10)
            {
                newEmployeeCode = "NV-0000" + newNumberCode.ToString();
            }
            else if (newNumberCode < 100)
            {
                newEmployeeCode = "NV-000" + newNumberCode.ToString();
            }
            else if (newNumberCode < 1000)
            {
                newEmployeeCode = "NV-00" + newNumberCode.ToString();
            }
            else if (newNumberCode < 10000)
            {
                newEmployeeCode = "NV-0" + newNumberCode.ToString();
            }
            else if (newNumberCode < 100000)
            {
                newEmployeeCode = "NV-" + newNumberCode.ToString();
            }
            return newEmployeeCode;
        }
        #endregion

    }
}
