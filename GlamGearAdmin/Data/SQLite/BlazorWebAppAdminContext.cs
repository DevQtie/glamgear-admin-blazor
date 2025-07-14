using Microsoft.EntityFrameworkCore;

namespace GlamGearAdmin.Data.SQLite
{
    public class BlazorWebAppAdminContext(DbContextOptions<BlazorWebAppAdminContext> options) : DbContext(options)
    {
        public DbSet<Models.SQLite.Admin> Admin { get; set; } = default!;
    }
}
