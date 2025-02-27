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
    public class WorkPositionService : IWorkPositionService
    {
        private readonly ApplicationDbContext _context;


        public WorkPositionService(ApplicationDbContext context)
        {
            _context = context;
        }


       public async Task<List<WorkPosition>> GetAllWorkPositionsAsync()
        {
            return await _context.WorkPositions.ToListAsync();
        }

        public async Task AddWorkPositionAsync(WorkPosition workPosition)
        {
            var isThereWorkPosition = _context.WorkPositions.FirstOrDefaultAsync(wp => wp.Name == workPosition.Name);

            if (isThereWorkPosition == null)
            {
                return;
            }
           

            _context.WorkPositions.Add(workPosition);
            await _context.SaveChangesAsync();
        }

        public async Task<WorkPosition> GetWorkPositionByIdAsync(int id)
        {
          return  await _context.WorkPositions.FindAsync(id);
        }

    }
}
