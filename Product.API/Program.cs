using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using Product.API.Extensions;
using Product.API.Middleware;
using Product.API.Models;
using Product.Core.Services;
using Product.Infrastructure;
using Product.Infrastructure.Repository;
using StackExchange.Redis;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// 
builder.Services.AddAPIRequesttration();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//set authen with Jwt
builder.Services.AddSwaggerGen(s =>
{
    var securitySchema = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Jwt Auth Bearer",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        Reference = new OpenApiReference
        {
            Id = "Bearer",
            Type = ReferenceType.SecurityScheme
        }
    };
    s.AddSecurityDefinition("Bearer", securitySchema);
    var securityRequirement = new OpenApiSecurityRequirement { { securitySchema, new[] { "Bearer" } } };
    s.AddSecurityRequirement(securityRequirement);
});

builder.Services.InfraStructureConfigration(builder.Configuration);

//builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
//builder.Services.AddAutoMapper(cfg =>
//{
//    // Assuming MappingOrders is a class where you've defined your mappings.
//    cfg.AddProfile<MappingOrders>();
//    // Add additional profiles here if needed.
//});

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

//Configuration Order Services
builder.Services.AddScoped<IOrderServices, OrderServices>();

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

app.UseAuthentication();

app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

InfrastructureRequistration.infrastructureConfigMiddleware(app);

app.Run();
