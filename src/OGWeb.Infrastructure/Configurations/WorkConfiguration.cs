using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OGWeb.Core.Entities;

namespace OGWeb.Infrastructure.Configurations;

public class WorkConfiguration : IEntityTypeConfiguration<Work>
{
    public void Configure(EntityTypeBuilder<Work> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Title).HasMaxLength(150);
        builder.Property(x => x.SlugUrl).HasMaxLength(150);
        builder.Property(x => x.CreatedDate).HasColumnType("datetime");
        builder.Property(x => x.Keyword_Seo).HasMaxLength(250);
        builder.Property(x => x.Description_Seo).HasMaxLength(250);

    }
}
