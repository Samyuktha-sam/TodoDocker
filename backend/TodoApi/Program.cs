using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TodoApi.Data;
using TodoApi.Models.Mapper;
using TodoApi.Repositories;
using TodoApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAutoMapper(typeof(MapperProfile));

builder.Services.AddDbContext<TodoDbContext>(opts =>
    opts.UseNpgsql(builder.Configuration.GetConnectionString("TodoDbContext")));


builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<ITodoService, TodoService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Todo API",
        Description = "CRUD operations for Todo tasks",
    });
});

var app = builder.Build();

// Initialize the database asynchronously
_ = Task.Run(async () =>
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<TodoDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    
    const int maxRetries = 10;
    const int delaySeconds = 5;
    
    for (int i = 0; i < maxRetries; i++)
    {
        try
        {
            logger.LogInformation("Attempting to initialize database (attempt {Attempt}/{MaxRetries})...", i + 1, maxRetries);
            await context.Database.EnsureCreatedAsync();
            logger.LogInformation("Database initialization completed successfully.");
            return;
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Database initialization failed (attempt {Attempt}/{MaxRetries}): {Message}", i + 1, maxRetries, ex.Message);
            
            if (i < maxRetries - 1)
            {
                logger.LogInformation("Retrying database initialization in {DelaySeconds} seconds...", delaySeconds);
                await Task.Delay(TimeSpan.FromSeconds(delaySeconds));
            }
        }
    }
    
    logger.LogError("Failed to initialize database after {MaxRetries} attempts. Application may not function correctly.", maxRetries);
});

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API V1");
        c.RoutePrefix = "swagger";
    });
}

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();