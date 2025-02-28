using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkstationManagment.Core.Models;

namespace WorkstationManagment.Core.Services
{
    public interface IUserService
    {
        
        Task<User> GetUserByUsernameAsync(string username);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task AddUserAsync(User user);
        Task DeleteUserAsync(int userId);
        Task UpdateUserAsync(User user);
    }
}
