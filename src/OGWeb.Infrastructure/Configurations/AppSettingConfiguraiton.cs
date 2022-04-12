using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OGWeb.Core.Entities;

namespace OGWeb.Infrastructure.Configurations;

public class AppSettingConfiguraiton : IEntityTypeConfiguration<AppSetting>
{
    public void Configure(EntityTypeBuilder<AppSetting> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.LogoUrl).HasMaxLength(150);
        builder.Property(x => x.FaceBook).HasMaxLength(150);
        builder.Property(x => x.Instagram).HasMaxLength(150);
        builder.Property(x => x.LinkedIn).HasMaxLength(150);
        builder.Property(x => x.YouTube).HasMaxLength(150);
        builder.Property(x => x.Twitter).HasMaxLength(150);

    }
}
