using Microsoft.Extensions.Configuration;
using MISA.Core.Entities.Directory;
using MISA.Core.Interfaces.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure.Data
{
    public class DepartmentRepository: BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(IConfiguration configuration) : base (configuration)
        {

        }
    }
}
