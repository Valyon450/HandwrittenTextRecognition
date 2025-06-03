using BusinessLogic.Mappings;
using DataAccess.Interfaces;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Services;
using BusinessLogic.Services.Interfaces;

namespace WebAPI.DependencyInjection
{
    public static class DependencyConfiguration
    {
        public static IServiceCollection ConfigureDependency(this IServiceCollection services, IConfiguration configuration)
        {
            // Database context
            services.AddDbContext<HTRDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IHTRDbContext, HTRDbContext>();

            // AutoMapper
            services.AddAutoMapper(typeof(AutomapperProfile));

            // Services
            services.AddScoped<IInFileService, InFileService>();
            services.AddScoped<IRecognitionModelService, RecognitionModelService>();
            services.AddScoped<IRecognitionResultService, RecognitionResultService>();
            services.AddScoped<ITextBlockService, TextBlockService>();
            services.AddScoped<ITuningService, TuningService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUSRSService, USRSService>();

            return services;
        }
    }
}
