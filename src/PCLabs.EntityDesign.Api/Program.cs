using Microsoft.EntityFrameworkCore;
using PCLabs.EntityDesign.Api;
using PCLabs.EntityDesign.Api.DataAccess;
using PCLabs.EntityDesign.Api.Services;
using PCLabs.EntityDesign.Domain.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AppConnection")));
builder.Services.AddScoped<DataInitializer>();
builder.Services.AddScoped<IDateTime, DateTimeProvider>();
builder.Services.AddScoped<IDocumentNoGenerator, DocumentNoGenerator>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);


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

using (var scope = app.Services.CreateScope())
{
    var dataInitializer = scope.ServiceProvider.GetRequiredService<DataInitializer>();
    await dataInitializer.SeedAsync();
}

app.Run();
