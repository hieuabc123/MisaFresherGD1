using MISA.Core.Entities.Base;
using MISA.Core.Entities.Directory;
using MISA.Core.Interfaces.Infrastructures;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        #region Declare
        IEmployeeRepository _employeeRepository;
        #endregion

        #region Constructor
        public EmployeeService(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        
        #endregion

        #region Method Implement
        public async Task<ActionServiceResult> GetEmployeesPagingFilter(int pageIndex, int pageSize, string dataFilter)
        {
            var employees = await _employeeRepository.GetEmployeePagingFilter(pageIndex, pageSize, dataFilter);
            var totalEmployees = await _employeeRepository.GetTotalEmployeesFilter(dataFilter);
            if (employees.Count() > 0)
            {
                var res = new ActionServiceResult
                {
                    StatusCode = 200,
                    UserMsg = "Load dữ liệu thành công",
                    DevMsg = "Load dữ liệu thành công",
                    Data = new {
                        employees = employees,
                        total = totalEmployees
                    }
                };
                return res;
            }
            else
            {
                var res = new ActionServiceResult
                {
                    StatusCode = 200,
                    UserMsg = "Load dữ liệu thành công",
                    DevMsg = "Load dữ liệu thành công",
                    Data = null,
                };
                return res;
            }

        }

        public async Task<ActionServiceResult> GetNewEmployee()
        {
            var employee = new Employee();
            employee.EmployeeCode = await _employeeRepository.GetNewEmployeeCode();
            if (employee != null)
            {
                var res = new ActionServiceResult
                {
                    StatusCode = 200,
                    UserMsg = "Load dữ liệu thành công",
                    DevMsg = "Load dữ liệu thành công",
                    Data = employee,
                };
                return res;
            }
            else
            {
                var res = new ActionServiceResult
                {
                    StatusCode = 200,
                    UserMsg = "Load dữ liệu không thành công",
                    DevMsg = "Load dữ liệu không thành công",
                    Data = null,
                };
                return res;
            }

        }
        public async Task<ActionServiceResult> GetDuplicateEmployee(Guid id)
        {
            var employee = await _employeeRepository.GetById(id);

            //2. lấy mã code mới
            employee.EmployeeCode = await _employeeRepository.GetNewEmployeeCode();
            if (employee != null)
            {
                var res = new ActionServiceResult
                {
                    StatusCode = 200,
                    UserMsg = "Load dữ liệu thành công",
                    DevMsg = "Load dữ liệu thành công",
                    Data = employee,
                };
                return res;
            }
            else
            {
                var res = new ActionServiceResult
                {
                    StatusCode = 200,
                    UserMsg = "Load dữ liệu không thành công",
                    DevMsg = "Load dữ liệu không thành công",
                    Data = null,
                };
                return res;
            }
        }
        #endregion

        #region Method Not Emplement

        #endregion
    }
}
