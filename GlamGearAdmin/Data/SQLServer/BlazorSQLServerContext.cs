using Microsoft.EntityFrameworkCore;

namespace GlamGearAdmin.Data.SQLServer
{
    public class BlazorSQLServerContext(DbContextOptions<BlazorSQLServerContext> options) : DbContext(options)
    {
        public DbSet<Models.SQLServer.RandText> RandText { get; set; } = default!;
    }
}
