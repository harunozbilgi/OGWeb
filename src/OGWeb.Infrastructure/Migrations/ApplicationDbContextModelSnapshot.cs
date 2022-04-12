﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OGWeb.Infrastructure.Context;

#nullable disable

namespace OGWeb.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("OGWeb.Core.Entities.AppSeo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Keyword")
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Page")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Title")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.HasKey("Id");

                    b.ToTable("AppSeos");
                });

            modelBuilder.Entity("OGWeb.Core.Entities.AppSetting", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("FaceBook")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("GooglePixel")
                        .HasColumnType("longtext");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Instagram")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("LinkedIn")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("LogoUrl")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.Property<string>("Twitter")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("YouTube")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.HasKey("Id");

                    b.ToTable("AppSettings");
                });

            modelBuilder.Entity("OGWeb.Core.Entities.OverView", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<string>("ImageUrl_One")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("ImageUrl_Two")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Title")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.HasKey("Id");

                    b.ToTable("OverViews");
                });

            modelBuilder.Entity("OGWeb.Core.Entities.Video", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Title")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Url")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("OGWeb.Core.Entities.Work", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Description_Seo")
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<bool?>("IsActived")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Keyword_Seo")
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<string>("SlugUrl")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Title")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Works");
                });

            modelBuilder.Entity("OGWeb.Core.Entities.WorkFile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<bool?>("IsCover")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("WorkId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("WorkId");

                    b.ToTable("WorkFiles");
                });

            modelBuilder.Entity("OGWeb.Core.Entities.WorkFile", b =>
                {
                    b.HasOne("OGWeb.Core.Entities.Work", "Work")
                        .WithMany("WorkFiles")
                        .HasForeignKey("WorkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Work");
                });

            modelBuilder.Entity("OGWeb.Core.Entities.Work", b =>
                {
                    b.Navigation("WorkFiles");
                });
#pragma warning restore 612, 618
        }
    }
}
