using Microsoft.EntityFrameworkCore;
using Migrations_MySqlServer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MySqlContext>(options =>
options.UseMySQL(builder.Configuration.GetConnectionString("MySqlConnectionString")));

var app = builder.Build();
app.MapGet("/", () => "Migrations Mysql Server!");
app.Run();