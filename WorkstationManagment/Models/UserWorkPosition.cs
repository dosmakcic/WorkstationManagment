using System;
using WorkstationManagment.Models;

public class UserWorkPosition
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }

    public int WorkPositionId { get; set; }
    public WorkPosition WorkPosition { get; set; }

    public string ProductName { get; set; }
    public DateTime Date { get; set; }
}