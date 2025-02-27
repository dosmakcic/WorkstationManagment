using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkstationManagment.Core.Models;

namespace WorkstationManagment.Core.Services
{
   public  interface IUserWorkPositionService
    {
        public Task<List<UserWorkPosition>> GetAllUserWorkPositionsAsync();
        public Task AssignWorkPositionAsync(int userId, int workPositionId,string productName);
        public  Task RemoveWorkPositionAsync(int userId, int workPositionId);
        public  Task<UserWorkPosition> FindUserWorkPositionByIdAsync(int id);

      
    }
}
