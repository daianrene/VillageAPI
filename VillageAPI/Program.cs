using Microsoft.EntityFrameworkCore;
using VillageAPI;
using VillageAPI.DataAccess;
using VillageAPI.Repository;
using VillageAPI.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Add Bd Connection
var connectionString = builder.Configuration.GetConnectionString("VillageConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

// Add AutoMapper ID
builder.Services.AddAutoMapper(typeof(MappingConfig));

// Add Repo injection;
builder.Services.AddScoped<IVillageRepository, VillageRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
