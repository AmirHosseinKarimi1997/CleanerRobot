using CleanerRobot.Application;
using CleanerRobot.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ICleanerRobotService, CleanerRobotService>();
builder.Services.AddScoped<ICleaningResultRepository, CleaningResultRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = $@"Host=tibberdb;Username={Environment.GetEnvironmentVariable("POSTGRES_USER")};
                       Password={Environment.GetEnvironmentVariable("POSTGRES_PASSWORD")};
                       Database={Environment.GetEnvironmentVariable("POSTGRES_DB")}";
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();