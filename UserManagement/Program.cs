using UserManagement.Cfg;
using UserManagement.Repos;
using UserManagement.Repos.Interfaces;
using UserManagement.Services;
using UserManagement.Services.Interfaces;
using UserManagement.DTO;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddControllers();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.Configure<AppConfig>(builder.Configuration.GetSection("AppConfiguration"));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IUserRepo, UserRepo>();
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

app.Run();
