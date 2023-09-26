using AppHotel.Api.Settings;
using AppHotel.Domain.ApplicationServiceContracts;
using AppHotel.Application.Services;
using AppHotel.Domain.RepositoryContracts;
using AppHotel.Infraestructure.Repository;
using AppHotel.Infraestructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<Database>(builder.Configuration.GetSection("Database"));

//Services
builder.Services.AddScoped<IHotelService, HotelService>();

//Repositories
builder.Services.AddScoped<PersistenceContext>();
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

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
