using Repositories;
using Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// builder.Services.AddControllers().AddNewtonsoftJson(options =>
//     options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
// );

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ISurvivorRepos, SurvivorsRepos>();

builder.Services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("NERD_MemoryDatabase"));
 
builder.Services.AddMvc(options =>
{
   options.SuppressAsyncSuffixInActionNames = false;
});

// ikke optimalt, men bare for at det virker.
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

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
