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
        public DbSet<Models.SQLServer.ProductPlainTextList> ProductPlainTextList { get; set; } = default!;
        public DbSet<Models.SQLServer.ProductImgList> ProductImgList { get; set; } = default!;
        public DbSet<Models.SQLServer.UserRoles> UserRoles { get; set; } = default!;
        public DbSet<Models.SQLServer.UserRemarks> UserRemarks { get; set; } = default!;
        public DbSet<Models.SQLServer.UserListImageSingleDM> UserListImageSingleDM { get; set; } = default!;
        public DbSet<Models.SQLServer.ProductMainForReview> ProductMainForReview { get; set; } = default!;
        public DbSet<Models.SQLServer.ProductPromoTagFR> ProductPromoTagFR { get; set; } = default!;
        public DbSet<Models.SQLServer.ProductSpecsFR> ProductSpecsFR { get; set; } = default!;
        public DbSet<Models.SQLServer.ProductVariantsFR> ProductVariantsFR { get; set; } = default!;
        public DbSet<Models.SQLServer.ProductVariantSpecsFR> ProductVariantSpecsFR { get; set; } = default!;
        public DbSet<Models.SQLServer.ProductImgFR> ProductImgFR { get; set; } = default!;
        public DbSet<Models.SQLServer.ProductVarImgFR> ProductVarImgFR { get; set; } = default!;
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

            modelBuilder.Entity<Models.SQLServer.ProductPlainTextList>()
            .HasNoKey().ToView(null);

            modelBuilder.Entity<Models.SQLServer.ProductImgList>()
            .HasNoKey().ToView(null);

            modelBuilder.Entity<Models.SQLServer.UserRoles>()
            .HasNoKey().ToView(null);

            modelBuilder.Entity<Models.SQLServer.UserRemarks>()
            .HasNoKey().ToView(null);

            modelBuilder.Entity<Models.SQLServer.UserListImageSingleDM>()
            .HasNoKey().ToView(null);

            modelBuilder.Entity<Models.SQLServer.ProductMainForReview>()
            .HasNoKey().ToView(null);

            modelBuilder.Entity<Models.SQLServer.ProductPromoTagFR>()
            .HasNoKey().ToView(null);

            modelBuilder.Entity<Models.SQLServer.ProductSpecsFR>()
            .HasNoKey().ToView(null);

            modelBuilder.Entity<Models.SQLServer.ProductVariantsFR>()
            .HasNoKey().ToView(null);

            modelBuilder.Entity<Models.SQLServer.ProductVariantSpecsFR>()
            .HasNoKey().ToView(null);

            modelBuilder.Entity<Models.SQLServer.ProductImgFR>()
            .HasNoKey().ToView(null);

            modelBuilder.Entity<Models.SQLServer.ProductVarImgFR>()
            .HasNoKey().ToView(null);

            // to add another modelBuilder
        }
    }
}
