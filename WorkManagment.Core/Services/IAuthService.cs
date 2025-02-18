using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkstationManagment.Core.Models;

namespace WorkstationManagment.Core.Services
{
    public interface IAuthService
    {
        public  Task<User> AuthenticateAsync(string username, string password);

    }
}
