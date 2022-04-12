using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OGWeb.Core.Entities;

namespace OGWeb.Infrastructure.Configurations;

public class SliderConfiguraiton : IEntityTypeConfiguration<Slider>
{
    public void Configure(EntityTypeBuilder<Slider> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Title).HasMaxLength(150);
        builder.Property(x => x.ImageUrl).HasMaxLength(150);
    }
}