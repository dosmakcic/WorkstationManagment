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

        public async Task AssignWorkPositionAsync(int userId, int workPositionId)
        {
            var userWorkPosition = new UserWorkPosition
            {
                UserId = userId,
                WorkPositionId = workPositionId,
                Date = DateTime.Now
            };

            _context.UserWorkPositions.Add(userWorkPosition);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveWorkPositionAsync(int userId, int workPositionId)
        {
            var userWorkPosition = await _context.UserWorkPositions
                .FirstOrDefaultAsync(uwp => uwp.UserId == userId && uwp.WorkPositionId == workPositionId);

            if (userWorkPosition != null)
            {
                _context.UserWorkPositions.Remove(userWorkPosition);
                await _context.SaveChangesAsync();
            }
        }
    }
}
