using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Pages.Tasks;

[Authorize]
public class DetailsModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public DetailsModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public TaskItem TaskItem { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var taskitem = await _context.Tasks
            .Include(t => t.AssignedToUser)
            .Include(t => t.CreatedByUser)
            .FirstOrDefaultAsync(m => m.Id == id);
        
        if (taskitem == null)
        {
            return NotFound();
        }
        else
        {
            TaskItem = taskitem;
        }
        return Page();
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

        return RedirectToPage(new { id = id });
    }
}