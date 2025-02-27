using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkstationManagment.Core.Models;

namespace WorkstationManagment.Core.Services
{
    public interface IWorkPositionService
    {
        public Task<List<WorkPosition>> GetAllWorkPositionsAsync();
        public  Task AddWorkPositionAsync(WorkPosition workPosition);

        public Task<WorkPosition> GetWorkPositionByIdAsync(int id);

    }
}
