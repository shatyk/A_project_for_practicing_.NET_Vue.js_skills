﻿// <auto-generated />
using System;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Database.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230814135523_OMG_Genius_Migrate_RenameCaptureToCaption")]
    partial class OMG_Genius_Migrate_RenameCaptureToCaption
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Database.Models.Fundraising", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("ActivityStatus")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("VisabilityStatus")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Fundraising", "public");
                });

            modelBuilder.Entity("Database.Models.FundraisingContent", b =>
                {
                    b.Property<long>("FundraisingId")
                        .HasColumnType("bigint");

                    b.Property<int>("LanguageId")
                        .HasColumnType("integer");

                    b.Property<string>("Caption")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("FundraisingId", "LanguageId");

                    b.HasIndex("LanguageId");

                    b.ToTable("FundraisingContent", "public");
                });

            modelBuilder.Entity("Database.Models.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Language", "public");
                });

            modelBuilder.Entity("Database.Models.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshToken", "public");
                });

            modelBuilder.Entity("Database.Models.Report", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("FundraisingId")
                        .HasColumnType("bigint");

                    b.Property<int>("VisabilityStatus")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FundraisingId");

                    b.ToTable("Report", "public");
                });

            modelBuilder.Entity("Database.Models.ReportContent", b =>
                {
                    b.Property<long>("ReportId")
                        .HasColumnType("bigint");

                    b.Property<int>("LanguageId")
                        .HasColumnType("integer");

                    b.Property<string>("Caption")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ReportId", "LanguageId");

                    b.HasIndex("LanguageId");

                    b.ToTable("ReportContent", "public");
                });

            modelBuilder.Entity("Database.Models.ReportTag", b =>
                {
                    b.Property<long>("ReportId")
                        .HasColumnType("bigint");

                    b.Property<int>("TagId")
                        .HasColumnType("integer");

                    b.HasKey("ReportId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("ReportTag", "public");
                });

            modelBuilder.Entity("Database.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Text")
                        .IsUnique();

                    b.ToTable("Tag", "public");
                });

            modelBuilder.Entity("Database.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("User", "public");
                });

            modelBuilder.Entity("Database.Models.FundraisingContent", b =>
                {
                    b.HasOne("Database.Models.Fundraising", null)
                        .WithMany("Contents")
                        .HasForeignKey("FundraisingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Models.Language", null)
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Database.Models.RefreshToken", b =>
                {
                    b.HasOne("Database.Models.User", null)
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Database.Models.Report", b =>
                {
                    b.HasOne("Database.Models.Fundraising", "Fundraising")
                        .WithMany("Reports")
                        .HasForeignKey("FundraisingId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Fundraising");
                });

            modelBuilder.Entity("Database.Models.ReportContent", b =>
                {
                    b.HasOne("Database.Models.Language", null)
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Models.Report", null)
                        .WithMany("Contents")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Database.Models.ReportTag", b =>
                {
                    b.HasOne("Database.Models.Report", "Report")
                        .WithMany("ReportTags")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Models.Tag", "Tag")
                        .WithMany("ReportTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Report");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("Database.Models.Fundraising", b =>
                {
                    b.Navigation("Contents");

                    b.Navigation("Reports");
                });

            modelBuilder.Entity("Database.Models.Report", b =>
                {
                    b.Navigation("Contents");

                    b.Navigation("ReportTags");
                });

            modelBuilder.Entity("Database.Models.Tag", b =>
                {
                    b.Navigation("ReportTags");
                });

            modelBuilder.Entity("Database.Models.User", b =>
                {
                    b.Navigation("RefreshTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
