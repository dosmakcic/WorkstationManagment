using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WorkstationManagment.Core.Models;
using WorkstationManagment.Core.Models;


namespace WorkstationManagment.Core.Models
{
    public class UserWorkPosition
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [Required]
        public int WorkPositionId { get; set; }

        [ForeignKey("WorkPositionId")]
        public WorkPosition WorkPosition { get; set; }

        public string? ProductName { get; set; }
        public DateTime Date { get; set; }
    }
}
