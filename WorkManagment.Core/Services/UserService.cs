using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkstationManagment.Core.Data;
using WorkstationManagment.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices.Marshalling;


namespace WorkstationManagment.Core.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.Include(u => u.Role).ToListAsync();
        }

        public async Task AddUserAsync(User user)
        { 
            var password= BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Password = password;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user1 = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user1 != null)
            {
                _context.Users.Remove(user1);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateUserAsync (User user)
        {
            var existingUser = await _context.Users.FindAsync(user.Id);

            if (existingUser != null)
            {
                existingUser.Username = user.Username;
                existingUser.Password = user.Password;
                existingUser.RoleId = user.RoleId;
            
                _context.Users.Update(existingUser);
                await _context.SaveChangesAsync();
            }
        }

        
    }
}

