using Domain.Entities;
using Infrastructure.AutomapperProfiles;
using Infrastructure.Context;
using Infrastructure.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<AuthorService>();
builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<BookAuthorService>();
builder.Services.AddScoped<BookEditorService>();
builder.Services.AddScoped<EditorService>();
builder.Services.AddScoped<PulisherService>();
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(conf => conf.UseNpgsql(connection));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(ServiceProfile));


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
