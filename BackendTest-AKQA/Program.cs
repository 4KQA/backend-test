using BackendTest_AKQA.Models;
using BackendTest_AKQA.Repositories;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IPersonRepo, PersonRepo>();

builder.Services.AddDbContext<PersonContext>(opt => opt.UseInMemoryDatabase("People"));

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();


app.UseCors(x => x
       .AllowAnyOrigin()
       .AllowAnyMethod()
       .AllowAnyHeader());

app.MapControllers();

app.Run();
