using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OGWeb.Core.Entities;

namespace OGWeb.Infrastructure.Configurations;

public class WorkFileConfiguration : IEntityTypeConfiguration<WorkFile>
{
    public void Configure(EntityTypeBuilder<WorkFile> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.ImageUrl).HasMaxLength(150);
    }
}
