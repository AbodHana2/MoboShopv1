
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoboShopv1.Models;

namespace MoboShopv1.Areas.Identity.Data;

public class MoboShopContext : IdentityDbContext<User>
{
    public MoboShopContext(DbContextOptions<MoboShopContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        builder.ApplyConfiguration(new AppUserEntityConfigration());
    }
}

public class AppUserEntityConfigration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.FirstName).HasMaxLength(128);
        builder.Property(u => u.LastName).HasMaxLength(128);
        builder.Property(u => u.ImageURL).HasMaxLength(500);
    }
}