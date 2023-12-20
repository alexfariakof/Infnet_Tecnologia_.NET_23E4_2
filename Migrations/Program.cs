using Microsoft.EntityFrameworkCore;
using Migrations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RegisterContext>(options =>
options.UseMySQL(builder.Configuration.GetConnectionString("MySqlConnectionString")));



var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
