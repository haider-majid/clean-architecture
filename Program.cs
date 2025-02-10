using System.Text.Json.Serialization;
using clean_architecture.Data;
using clean_architecture.Mappings;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Load configuration
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

builder.Services.AddMediatR(typeof(Program).Assembly);

// configure logging
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/app.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();
// auto mapper
builder.Services.AddAutoMapper(typeof(MapperProfile));


// Configure services 
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);



builder.Services.AddControllers();

// configure FluentValidation
builder.Services.AddControllers()
    .AddFluentValidation(fv =>
        fv.RegisterValidatorsFromAssemblyContaining<Program>());

var app = builder.Build();

if (app.Environment.IsDevelopment())

{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();