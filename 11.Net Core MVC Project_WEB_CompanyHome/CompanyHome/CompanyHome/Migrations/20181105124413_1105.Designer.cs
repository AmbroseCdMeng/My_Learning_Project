﻿// <auto-generated />
using System;
using CompanyHome.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CompanyHome.Migrations
{
    [DbContext(typeof(MyDBContent))]
    [Migration("20181105124413_1105")]
    partial class _1105
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CompanyHome.Models.Admin", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CREATETIME");

                    b.Property<DateTime?>("LASTLOGINTIME");

                    b.Property<string>("PASSWORD")
                        .IsRequired();

                    b.Property<string>("USERNAME")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.HasKey("ID");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("CompanyHome.Models.Category", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ADDTIME");

                    b.Property<string>("NAME")
                        .HasMaxLength(15);

                    b.Property<int>("ORDER");

                    b.HasKey("ID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("CompanyHome.Models.Comment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ADDTIME");

                    b.Property<string>("AUTHOR");

                    b.Property<string>("CONTENT")
                        .HasMaxLength(400);

                    b.Property<string>("IP");

                    b.Property<int>("LEVEL");

                    b.Property<int>("NID");

                    b.HasKey("ID");

                    b.HasIndex("NID");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("CompanyHome.Models.News", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ADDTIME");

                    b.Property<string>("AUTHOR");

                    b.Property<int>("CID");

                    b.Property<string>("CONTENT");

                    b.Property<int>("MAXLEVEL");

                    b.Property<string>("TITLE")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.HasIndex("CID");

                    b.ToTable("News");
                });

            modelBuilder.Entity("CompanyHome.Models.Comment", b =>
                {
                    b.HasOne("CompanyHome.Models.News", "News")
                        .WithMany()
                        .HasForeignKey("NID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CompanyHome.Models.News", b =>
                {
                    b.HasOne("CompanyHome.Models.Category", "Category")
                        .WithMany("News")
                        .HasForeignKey("CID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
