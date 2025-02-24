using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkstationManagment.Core.Models;

namespace WorkstationManagment.Core.Services
{
    public interface IRoleService
    {
        public Task<List<Role>> GetAllRoles();
    }
}
