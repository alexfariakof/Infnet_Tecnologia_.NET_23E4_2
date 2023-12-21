using Microsoft.EntityFrameworkCore;
using Migrations_SqlServer;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MsSqlContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnectionString")));

var app = builder.Build();

app.MapGet("/", () => "Migrations SqlServer!");
app.Run();
