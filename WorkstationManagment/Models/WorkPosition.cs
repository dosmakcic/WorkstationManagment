using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkstationManagment.Models
{
    public class WorkPosition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public ICollection<UserWorkPosition> UserWorkPositions { get; set; }


    }
}
