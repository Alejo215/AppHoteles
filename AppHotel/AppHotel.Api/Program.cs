using AppHotel.Api.Settings;
using AppHotel.Infraestructure.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<Database>(builder.Configuration.GetSection("Database"));
builder.Services.Configure<Notification>(builder.Configuration.GetSection("Notification"));

builder.Services.AddServices();

builder.Services.AddAutoMapper(Assembly.Load("AppHotel.ApplicationService"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.AddMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
