using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Resturant_api.Data;
using Resturant_api.Mapper;
using Resturant_api.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add DI for DbContext
builder.Services.AddDbContext<ResturantDbContext>(Options =>
Options.UseSqlServer(builder.Configuration.GetConnectionString("ResturantConnectionString")));

//Add Repository 
builder.Services.AddScoped<IMenuRepository, SqlMenuRepository>();
//Inject Auto Mapper.
builder.Services.AddAutoMapper(typeof(MenuDtoToMapper));

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
