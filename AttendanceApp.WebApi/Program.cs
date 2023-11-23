using AttendanceApp.WebApi.DbContexts;
using AttendanceApp.WebApi.Services;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddFastEndpoints()
    .SwaggerDocument(o =>
    {
        o.DocumentSettings = s =>
        {
            s.Title = "Employee Attendance Management System App Api";
            s.Version = "v1";
        };
        o.EnableJWTBearerAuth = true;
    })
    .AddJWTBearerAuth(builder.Configuration["JwtBearerDefaults:SecretKey"])
    .AddAuthorization();


builder.Services.AddDbContext<AttendanceContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();

var app = builder.Build();

app.UseFastEndpoints(c =>
    {
        c.Endpoints.RoutePrefix = "api";
    })
    .UseAuthentication()
    .UseAuthorization()
    .UseSwaggerGen();

app.Run();
