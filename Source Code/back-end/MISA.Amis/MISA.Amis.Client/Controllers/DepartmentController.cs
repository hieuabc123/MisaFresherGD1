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
    [Route("api/v1/[controller]s")]
    [ApiController]
    public class DepartmentController : BaseController<Department>
    {
        public DepartmentController(IBaseService<Department> baseService):base(baseService)
        {

        }
    }
}
