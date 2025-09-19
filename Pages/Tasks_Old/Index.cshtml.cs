using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Pages.Tasks;

[Authorize]
public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public IndexModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public IList<TaskItem> Tasks { get; set; } = default!;

    public async Task OnGetAsync()
    {
        var currentUserId = _userManager.GetUserId(User);
        
        if (currentUserId != null)
        {
            Tasks = await _context.Tasks
                .Include(t => t.AssignedToUser)
                .Include(t => t.CreatedByUser)
                .Where(t => t.AssignedToUserId == currentUserId)
                .OrderBy(t => t.DueDate)
                .ToListAsync();
        }
    }

    public async Task<IActionResult> OnPostCompleteTaskAsync(int id)
    {
        var currentUserId = _userManager.GetUserId(User);
        var task = await _context.Tasks
            .FirstOrDefaultAsync(t => t.Id == id && t.AssignedToUserId == currentUserId);

        if (task == null)
        {
            return NotFound();
        }

        task.Status = Models.TaskStatus.Completed;
        task.CompletedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return RedirectToPage();
    }
}