using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TaskManager.Models;

public class TaskItem
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;
    
    [StringLength(1000)]
    public string? Description { get; set; }
    
    [Required]
    public DateTime DueDate { get; set; }
    
    [Required]
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;
    
    [Required]
    public TaskStatus Status { get; set; } = TaskStatus.Pending;
    
    [Required]
    public string AssignedToUserId { get; set; } = string.Empty;
    
    public IdentityUser? AssignedToUser { get; set; }
    
    public string? CreatedByUserId { get; set; }
    
    public IdentityUser? CreatedByUser { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? CompletedAt { get; set; }
    
    public string? Notes { get; set; }
}

public enum TaskPriority
{
    Low = 1,
    Medium = 2,
    High = 3,
    Critical = 4
}

public enum TaskStatus
{
    Pending = 1,
    InProgress = 2,
    Completed = 3,
    Cancelled = 4
}