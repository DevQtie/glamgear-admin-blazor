using Microsoft.EntityFrameworkCore;

namespace GlamGearAdmin.Data.SQLServer
{
    public class BlazorSQLServerContext(DbContextOptions<BlazorSQLServerContext> options) : DbContext(options)
    {
        public DbSet<Models.SQLServer.RandText> RandText { get; set; } = default!;
        public DbSet<Models.SQLServer.Admin> Admin { get; set; } = default!;
        public DbSet<Models.SQLServer.SqlOutput> SqlOutput { get; set; } // Works well with OUTPUT parameter in SQL Server
        public DbSet<Models.SQLServer.UserListDM> UserListDM { get; set; } = default!;
        public DbSet<Models.SQLServer.SimulateKYCImg> ReviewUserMD { get; set; } = default!;
        public DbSet<Models.SQLServer.UserListSingleDM> UserListSingleDM { get; set; } = default!;
        public DbSet<Models.SQLServer.ProductList> ProductList { get; set; } = default!;
        public DbSet<Models.SQLServer.UserRoles> UserRoles { get; set; } = default!;
        public DbSet<Models.SQLServer.UserRemarks> UserRemarks { get; set; } = default!;
        public DbSet<Models.SQLServer.UserListImageSingleDM> UserListImageSingleDM { get; set; } = default!;
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

            modelBuilder.Entity<Models.SQLServer.SimulateKYCImg>()
            .HasNoKey().ToView(null);

            modelBuilder.Entity<Models.SQLServer.UserListSingleDM>()
            .HasNoKey().ToView(null);

            modelBuilder.Entity<Models.SQLServer.ProductList>()
            .HasNoKey().ToView(null);

            modelBuilder.Entity<Models.SQLServer.UserRoles>()
            .HasNoKey().ToView(null);

            modelBuilder.Entity<Models.SQLServer.UserRemarks>()
            .HasNoKey().ToView(null);

            modelBuilder.Entity<Models.SQLServer.UserListImageSingleDM>()
            .HasNoKey().ToView(null);

            // to add another modelBuilder
        }
    }
}
