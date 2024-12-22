using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDo_List_API.Models;

namespace ToDo_List_API.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<ToDoTask> tasks { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
        }
    }
}
