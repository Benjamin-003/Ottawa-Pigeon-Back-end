using Ottawa.Pigeon.Application.Common.Interfaces;

using Microsoft.EntityFrameworkCore;

using Ottawa.Pigeon.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Ottawa.Pigeon.Infrastructure.Authorization;
using Ottawa.Pigeon.Infrastructure;
using Ottawa.Pigeon.Application.Interfaces;
using Ottawa.Pigeon.Infrastructure.ExternalAPI.Articles;
using Ottawa.Pigeon.Infrastructure.Email;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
  
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<OttawaPigeonDbContext>(options =>
                options.UseInMemoryDatabase("OttawaPigeonDb"));
        }
        else
        {
            services.AddDbContext<OttawaPigeonDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(OttawaPigeonDbContext).Assembly.FullName)));
        }

        services.AddScoped<IOttawaPigeonDbContext>(provider => provider.GetRequiredService<OttawaPigeonDbContext>());

        services.AddScoped<OttawaPigeonDbContextInitialiser>();

        services.Configure<AppSettings>(appSettings => configuration.GetSection("AppSettings").Bind(appSettings));

        services.AddScoped<IJwtUtils, JwtUtils>();

        services.AddScoped<IUserService, UserService>();

        services.AddScoped<IArticleService, ArticleService>();

        services.Configure<EmailConfiguration>(emailConfig => configuration.GetSection("EmailConfiguration").Bind(emailConfig));

        services.AddScoped<IEmailService, EmailService>();

        services.AddScoped<IUserAccessor, UserAccessor>();

        services.AddScoped<IForgotPasswordService, ForgotPasswordService>();

        using (var serviceProvider = services.BuildServiceProvider())
        {
            var context = serviceProvider.GetRequiredService<OttawaPigeonDbContext>();

            context.Database.EnsureCreated();
        }
        return services;
    }
}
