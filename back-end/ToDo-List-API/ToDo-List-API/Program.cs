using ToDo_List_API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") // Your frontend origin
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials(); // Allow cookies, tokens, etc.
        });
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure middleware
app.UseHttpsRedirection(); // Redirects HTTP to HTTPS

app.UseCors("AllowFrontend"); // Apply the CORS policy

app.UseAuthorization();

app.MapControllers();

app.Run();
