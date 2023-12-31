using UserManagement.Cfg;
using UserManagement.Repos;
using UserManagement.Repos.Interfaces;
using UserManagement.Services;
using UserManagement.Services.Interfaces;
using UserManagement.DTO;
using UserManagement.Middleware;
using UserManagement.Model;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "User Management", Version = "v1" });
    c.EnableAnnotations();

    c.UseAllOfForInheritance();
    c.UseInlineDefinitionsForEnums();
});


builder.Services.AddControllers();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.Configure<AppConfig>(builder.Configuration.GetSection("AppConfiguration"));
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<ISecurityService, SecurityService>();
builder.Services.AddScoped<IEntityRepo<User>, UserRepo>();
builder.Services.AddScoped<IEntityService<CreateUserRequest, UserResponse>, UserService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<RequestLoggingMiddleware>();

app.Run();
