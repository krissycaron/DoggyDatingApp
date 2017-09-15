using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using serverSideCapstone.Models;

namespace serverSideCapstone.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Message>()
                .Property(c => c.TimeMessageSent)
                .HasDefaultValueSql("strftime('%Y-%m-%d %H:%M:%S')");
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<serverSideCapstone.Models.ActivityLevel> ActivityLevel { get; set; }

        public DbSet<serverSideCapstone.Models.Message> Message { get; set; }

        public DbSet<serverSideCapstone.Models.UserLike> UserLike { get; set; }
        public DbSet<ApplicationUser> ApplicationUser {get; set;}
    }
}
