using Microsoft.EntityFrameworkCore;

namespace GlamGearAdmin.Data.SQLServer
{
    public class BlazorSQLServerContext(DbContextOptions<BlazorSQLServerContext> options) : DbContext(options)
    {
        public DbSet<Models.SQLServer.RandText> RandText { get; set; } = default!;
        public DbSet<Models.SQLServer.Admin> Admin { get; set; } = default!;
        public DbSet<Models.SQLServer.SqlOutput> SqlOutput { get; set; } // Works well with OUTPUT parameter in SQL Server
        public DbSet<Models.SQLServer.UserListDM> UserListDM { get; set; } = default!;
        // to add another data model

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Models.SQLServer.SqlOutput>()
            //     .HasNoKey()
            //     .ToView(null); // prevent EF from expecting a table

            modelBuilder.Entity<Models.SQLServer.SqlOutput>()
            .HasNoKey();

            modelBuilder.Entity<Models.SQLServer.UserListDM>()
            .HasNoKey().ToView(null);

            // to add another modelBuilder
        }
    }
}
