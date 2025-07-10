using Microsoft.EntityFrameworkCore;

namespace GlamGearAdmin.Data
{
    public class BlazorWebAppAdminContext(DbContextOptions<BlazorWebAppAdminContext> options) : DbContext(options)
    {
        public DbSet<Models.Admin> Admin { get; set; } = default!;
    }
}
