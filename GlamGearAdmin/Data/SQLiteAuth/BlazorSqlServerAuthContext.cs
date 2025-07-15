using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace GlamGearAdmin.Data.SQLiteAuth
{
    public class BlazorAuthContext(DbContextOptions<BlazorAuthContext> options) : IdentityDbContext<IdentityUser>(options)
    {
        public DbSet<Models.SQLiteAuth.Admin> Admin { get; set; } = default!;
    }
}
