using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Product.Core;
using Product.Core.Entities;
using Product.Core.Interface;
using Product.Infrastructure.Data;
using Product.Infrastructure.Data.Config;
using Product.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure
{
    public static class InfrastructureRequistration
    {
        public static IServiceCollection InfraStructureConfigration (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //services.AddScoped<ICategoryRepository, CategoryRepository>();
            //services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<ApplicationDbContext>(options =>
             options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
             );

            //configuration Token Services
            services.AddScoped<ITokenServices, TokenServices>();

            services.AddIdentity<AppUsers, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            services.AddMemoryCache();
            //services.AddAuthentication(opt =>
            //{
            //    opt.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //});

            //cấu hình xác thực JWT (JSON Web Token)

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"])),
                        ValidIssuer = configuration["Token:issuer"],
                        ValidateIssuer = true,
                        ValidateAudience = false
                    };
                });

            return services;
        }

        //hàm này tạo ra một scope dịch vụ (service scope) mới và sử dụng scope này để lấy đối tượng UserManager<AppUsers>, sau đó gọi một phương thức seeding để khởi tạo dữ liệu người dùng vào cơ sở dữ liệu
        public static async void infrastructureConfigMiddleware(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var usermanager = scope.ServiceProvider.GetRequiredService<UserManager<AppUsers>>();
                await IdentitySeed.SeedUserAsync(usermanager);
            }
        }

    }
}
