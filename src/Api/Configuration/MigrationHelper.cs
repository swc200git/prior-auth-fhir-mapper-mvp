using Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Configuration;

public static class MigrationHelper
{
    public static void MigrateDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.Migrate();
    }
}
