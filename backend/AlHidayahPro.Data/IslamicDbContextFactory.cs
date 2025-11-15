using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AlHidayahPro.Data;

/// <summary>
/// Design-time factory for creating DbContext instances during migrations
/// </summary>
public class IslamicDbContextFactory : IDesignTimeDbContextFactory<IslamicDbContext>
{
    public IslamicDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<IslamicDbContext>();
        
        // Use SQLite by default for migrations
        optionsBuilder.UseSqlite("Data Source=alhidayah.db");

        return new IslamicDbContext(optionsBuilder.Options);
    }
}
