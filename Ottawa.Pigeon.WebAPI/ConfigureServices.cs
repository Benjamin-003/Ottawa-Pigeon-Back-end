using Microsoft.AspNetCore.Mvc;

using Ottawa.Pigeon.Extensions;
using Ottawa.Pigeon.Filters;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Configure Services (IOC) for Web API
/// </summary>
public static class ConfigureServices
{
    /// <summary>
    /// Add Web UI Services
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddWebUIServices(this IServiceCollection services)
    {

        services.AddHttpContextAccessor();

        services.AddControllersWithViews(options =>
            options.Filters.Add<ApiExceptionFilterAttribute>());

        services.AddRazorPages();

        services.AddRouting(options => options.LowercaseUrls = true);

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        services.AddApiVersioning(setup =>
        {
            setup.DefaultApiVersion = new ApiVersion(1, 0);
            setup.AssumeDefaultVersionWhenUnspecified = true;
            setup.ReportApiVersions = true;
        });

        services.AddVersionedApiExplorer(setup =>
        {
            setup.GroupNameFormat = "'v'VVV";
            setup.SubstituteApiVersionInUrl = true;
        });

        services.AddSwaggerGen();
        services.ConfigureOptions<ConfigureSwaggerOptions>();

        var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

        services.AddCors(options =>
        {
            options.AddPolicy(name: myAllowSpecificOrigins,
                              policy =>
                              {
                                  policy.WithOrigins("http://localhost:4200")
                                        .AllowAnyHeader()
                                        .AllowAnyMethod();
                              });
        });

        return services;
    }
}
