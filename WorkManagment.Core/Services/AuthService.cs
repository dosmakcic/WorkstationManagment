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

        public AuthService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            // Ako korisnik ne postoji ili lozinka nije validna, vrati null
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
                return null;

            // Ako je lozinka tačna, vrati korisnika
            return user;
        }
    }
}
