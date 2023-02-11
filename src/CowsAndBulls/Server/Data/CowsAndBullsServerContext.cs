using Microsoft.EntityFrameworkCore;

namespace CowsAndBulls.Server.Data;

public class CowsAndBullsServerContext : DbContext
{
    public CowsAndBullsServerContext(DbContextOptions<CowsAndBullsServerContext> options)
        : base(options)
    {
    }

    public DbSet<CowsAndBulls.Shared.GameLog> GameLog { get; set; } = default!;
}
