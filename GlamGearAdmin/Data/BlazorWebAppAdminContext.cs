using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GlamGearAdmin.Models;

namespace GlamGearAdmin.Data
{
    public class BlazorWebAppAdminContext : DbContext
    {
        public BlazorWebAppAdminContext (DbContextOptions<BlazorWebAppAdminContext> options)
            : base(options)
        {
        }

        public DbSet<GlamGearAdmin.Models.Admin> Admin { get; set; } = default!;
    }
}
