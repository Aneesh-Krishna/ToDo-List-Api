using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ToDo_List_API.Data;
using ToDo_List_API.Models;

namespace ToDo_List_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public TaskController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetTasks()
        {
            var tasks = _context.tasks.ToList();
            if(tasks == null)
                return NoContent();
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> AddTask(ToDoTask newTask)
        {
            var tasks = await _context.tasks.ToListAsync();
            //newTask.ToDoTaskId = tasks.Count > 0 ? _context.tasks.Max(t => t.ToDoTaskId) + 1 : 1;
            _context.tasks.Add(newTask);
            await _context.SaveChangesAsync();

            return Ok(newTask);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ToggleTask(int id)
        {
            var task = await _context.tasks.FindAsync(id);
            if(task == null) return NotFound();

            task.isCompleted = !task.isCompleted;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.tasks.FindAsync(id);
            if(task == null) return NotFound();

            _context.tasks.Remove(task);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
