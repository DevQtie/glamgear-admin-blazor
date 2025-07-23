using Microsoft.EntityFrameworkCore;

namespace GlamGearAdmin.Data.SQLServer
{
    public class BlazorSQLServerContext(DbContextOptions<BlazorSQLServerContext> options) : DbContext(options)
    {
        public DbSet<Models.SQLServer.RandText> RandText { get; set; } = default!;
        public DbSet<Models.SQLServer.Admin> Admin { get; set; } = default!;
        public DbSet<Models.SQLServer.SqlOutput> SqlOutput { get; set; }
        // to add another data model

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Models.SQLServer.SqlOutput>()
            //     .HasNoKey()
            //     .ToView(null); // prevent EF from expecting a table

            modelBuilder.Entity<Models.SQLServer.SqlOutput>()
            .HasNoKey();

            // to add another modelBuilder
        }
    }
}
