using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Pages.Tasks;

[Authorize]
public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public EditModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [BindProperty]
    public TaskItem TaskItem { get; set; } = default!;

    public SelectList Users { get; set; } = default!;

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
        
        TaskItem = taskitem;
        
        var users = await _userManager.Users.ToListAsync();
        Users = new SelectList(users, "Id", "Email", TaskItem.AssignedToUserId);
        
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            var users = await _userManager.Users.ToListAsync();
            Users = new SelectList(users, "Id", "Email", TaskItem.AssignedToUserId);
            return Page();
        }

        // Update completion timestamp if status changed to completed
        if (TaskItem.Status == Models.TaskStatus.Completed && TaskItem.CompletedAt == null)
        {
            TaskItem.CompletedAt = DateTime.UtcNow;
        }
        else if (TaskItem.Status != Models.TaskStatus.Completed)
        {
            TaskItem.CompletedAt = null;
        }

        _context.Attach(TaskItem).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TaskItemExists(TaskItem.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }

    public async Task<IActionResult> OnPostDeleteAsync()
    {
        var taskitem = await _context.Tasks.FindAsync(TaskItem.Id);
        if (taskitem != null)
        {
            _context.Tasks.Remove(taskitem);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }

    private bool TaskItemExists(int id)
    {
        return _context.Tasks.Any(e => e.Id == id);
    }
}