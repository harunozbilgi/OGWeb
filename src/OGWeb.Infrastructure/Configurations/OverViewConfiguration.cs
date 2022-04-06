using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OGWeb.Core.Entities;

namespace OGWeb.Infrastructure.Configurations;

public class OverViewConfiguration : IEntityTypeConfiguration<OverView>
{
    public void Configure(EntityTypeBuilder<OverView> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Title).HasMaxLength(150);
        builder.Property(x => x.Description).HasMaxLength(250);
        builder.Property(x => x.ImageUrl_One).HasMaxLength(150);
        builder.Property(x => x.ImageUrl_Two).HasMaxLength(150);
    }
}
