using Api.Data;
using Api.Services;
using Microsoft.EntityFrameworkCore;

namespace Api.Configuration;

public static class ServiceRegistration
{
    public static IServiceCollection AddAppDatabase(this IServiceCollection services, IConfiguration config, IWebHostEnvironment env)
    {
        var conn = config.GetConnectionString("DefaultConnection");
        services.AddDbContext<AppDbContext>(opt =>
        {
            if (env.IsEnvironment("Test"))
                opt.UseInMemoryDatabase("TestDb");
            else if (!string.IsNullOrWhiteSpace(conn))
                opt.UseSqlServer(conn);
            else
                opt.UseSqlite("Data Source=pa.db");
        });
        return services;
    }
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddHealthChecks();
        services.AddScoped<FhirMappingService>();
        services.AddScoped<PriorAuthService>();
        return services;
    }
}
