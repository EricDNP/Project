using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PracticePackAPI.Context;
using PracticePackAPI.Models;
using PracticePackAPI.Repositories;
using PracticePackAPI.Services;

namespace PracticePackAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options => 
            {
                options.AddPolicy("ChooseMenuPolicy", builder => 
                {
                    builder.WithOrigins("http://localhost:54327")
                                        .AllowAnyHeader()
                                        .AllowAnyMethod();
                });
            });
        }

        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            string stringConnection = String.Format("Filename={0}/DataBase.db", AppDomain.CurrentDomain.BaseDirectory);
            services.AddDbContext<ApiContext>(opt => opt.UseSqlite(stringConnection));
        }

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PracticePackAPI", Version = "v1" });
            });
        }

        public static void AddRepostories(this IServiceCollection services)
        {
            services.AddScoped<iUnitOfWork, UnitOfWork>();
            services.AddTransient(typeof(iRepository<>), typeof(GenericRepository<>));
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<iService<Card>, CardService>();
            services.AddScoped<iService<Address>, AddressService>();
            services.AddScoped<iService<Branch>, BranchService>();
            services.AddScoped<iService<Package>, PackageService>();
            services.AddScoped<iService<User>, UserService>();
        }
    }
}