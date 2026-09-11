using ArenaHub.API.Data;
using ArenaHub.API.Mappings;
using ArenaHub.API.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ArenaHubDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ArenaHubConnectionString")));

builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddAutoMapper(options => options.AddProfile<AutoMapperProfiles>());
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
