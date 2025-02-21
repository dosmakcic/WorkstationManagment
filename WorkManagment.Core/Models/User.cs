using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkstationManagment.Core.Models
{
   public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(500)]
        public string Password { get; set; }
        [Required]
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        public ICollection<UserWorkPosition> UserWorkPositions { get; set; }
    }
}
