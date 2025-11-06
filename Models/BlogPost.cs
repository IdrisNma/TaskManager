using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TaskManager.Models;

public class BlogPost
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    public string Content { get; set; } = string.Empty;
    
    [StringLength(500)]
    public string? Excerpt { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Category { get; set; } = "General";
    
    public string? Tags { get; set; }
    
    [Required]
    public string AuthorId { get; set; } = string.Empty;
    
    public IdentityUser? Author { get; set; }
    
    public DateTime PublishedDate { get; set; } = DateTime.UtcNow;
    
    public DateTime? LastModifiedDate { get; set; }
    
    public bool IsPublished { get; set; } = true;
    
    public int ViewCount { get; set; } = 0;
}
