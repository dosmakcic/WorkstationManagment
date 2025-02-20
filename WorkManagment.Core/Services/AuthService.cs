using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkstationManagment.Core.Models;
using WorkstationManagment.Core.Services;
using WorkstationManagment.Core.Data;

namespace WorkstationManagment.Core.Services
{
    public class AuthService : IAuthService
    {
       
        private readonly ApplicationDbContext _context;

        public AuthService()
        {
            
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
                 return await _context.Users
                .Where(u => u.Username == username && u.Password == password)
                .FirstOrDefaultAsync();
        }
    }
}
