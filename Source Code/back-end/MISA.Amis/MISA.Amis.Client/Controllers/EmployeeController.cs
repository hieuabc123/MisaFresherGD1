using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities.Directory;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Amis.Client.Controllers
{
    /// <summary>
    /// Controller API Nhân Viên
    /// </summary>
    /// Created By: NTHIEU (13/06/2021)
    [Route("api/[controller]s")]
    [ApiController]
    public class EmployeeController : BaseController<Employee>
    {
        #region Declare
        IEmployeeService _employeeService;
        #endregion

        #region Constructor
        public EmployeeController(IEmployeeService p_employeeService, IBaseService<Employee> baseService):base(baseService)
        {
            _employeeService = p_employeeService;
        }
        #endregion

        #region Get data
        [HttpGet("filter")]
        public async Task<IActionResult> GetEmployeesPagingFilter(int pageInt, int pageSize, string dataFilter )
        {
            var res = await _employeeService.GetEmployeesPagingFilter(pageInt, pageSize, dataFilter);
            return Ok(res);
        }
        #endregion
    }
}
