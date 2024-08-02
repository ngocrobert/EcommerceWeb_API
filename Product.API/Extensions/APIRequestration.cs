using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Product.API.Errors;
using System.Reflection;

namespace Product.API.Extensions
{
    public static class APIRequestration
    {
        public static IServiceCollection AddAPIRequesttration(this IServiceCollection services)
        {

            //automapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            // FileProvide (set root File Img)
            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot"
                )));

            // tuy chinh phan hoi API khi xay ra ngoai le
            services.Configure<ApiBehaviorOptions>(opt =>
            {

                opt.InvalidModelStateResponseFactory = context =>
                {
                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = context.ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage).ToArray()
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });

            return services;


            //Enable CORS
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", pol =>
                {
                    pol.AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins("http://localhost:4200");
                });
            });
            return services;
        }


    }
}
