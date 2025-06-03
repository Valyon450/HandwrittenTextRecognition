using DataAccess;
using WebAPI.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Register dependencies
builder.Services.ConfigureDependency(builder.Configuration);

// Add controllers
builder.Services.AddControllers();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Your API", Version = "v1" });
});

// Configure CORS
builder.Services.AddCors(options =>
{
options.AddPolicy("AllowAnyOrigin", builder =>
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader());
});

var app = builder.Build();

// Use CORS policy
app.UseCors("AllowAnyOrigin");

// Map controllers
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Initialize database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<HTRDbContext>();
    // HTRDbInitializer.Initialize(context);
}

// Run the application
app.Run();
public partial class Program { }