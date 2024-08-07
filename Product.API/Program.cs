using Microsoft.Extensions.FileProviders;
using Product.API.Extensions;
using Product.API.Middleware;
using Product.Infrastructure;
using StackExchange.Redis;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// 
builder.Services.AddAPIRequesttration();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.InfraStructureConfigration(builder.Configuration);

//builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
//// set root File Img
//builder.Services.AddSingleton<IFileProvider>( new PhysicalFileProvider(Path.Combine(
//    Directory.GetCurrentDirectory(), "wwwroot"
//    )));

//Configure Redis
builder.Services.AddSingleton<IConnectionMultiplexer>(i =>
{
    var configure = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"), true);
    return ConnectionMultiplexer.Connect(configure);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/errors/{0}");
app.UseCors("CorsPolicy");

app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.Run();
