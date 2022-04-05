using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OGWeb.Core.Entities;

namespace OGWeb.Infrastructure.Configurations;

public class AppSeoConfiguration : IEntityTypeConfiguration<AppSeo>
{
    public void Configure(EntityTypeBuilder<AppSeo> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Page).HasMaxLength(50);
        builder.Property(x => x.Title).HasMaxLength(150);
        builder.Property(x => x.Keyword).HasMaxLength(250);
        builder.Property(x => x.Description).HasMaxLength(250);
    }
}
