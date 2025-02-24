using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkstationManagment.Core.Data;
using WorkstationManagment.Core.Models;

namespace WorkstationManagment.Core.Services
{
   public class RoleService : IRoleService
    {
        private readonly ApplicationDbContext _context;

     public RoleService(ApplicationDbContext context)
        {
            _context = context;
        }
        public  async Task<List<Role>> GetAllRoles()
        {
            return await _context.Roles.ToListAsync();
        }
    }
}
