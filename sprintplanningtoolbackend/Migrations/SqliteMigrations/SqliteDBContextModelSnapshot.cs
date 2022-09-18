﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using sprintplanningtoolbackend.Helpers;

#nullable disable

namespace sprintplanningtoolbackend.Migrations.SqliteMigrations
{
    [DbContext(typeof(SqliteDBContext))]
    partial class SqliteDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.9");

            modelBuilder.Entity("sprintplanningtoolbackend.Models.SprintReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AbsentDays")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Capacity")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CarriedSP")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CreatedByUser")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<int?>("NewSP")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("SprintEndDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("SprintGoal")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("SprintStartDate")
                        .HasColumnType("TEXT");

                    b.Property<int?>("TeamSize")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("TotalSP")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("WorkDays")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("SprintReports");
                });

            modelBuilder.Entity("sprintplanningtoolbackend.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("IsAdmin")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<int?>("SprintReportId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SprintReportId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("sprintplanningtoolbackend.Models.User", b =>
                {
                    b.HasOne("sprintplanningtoolbackend.Models.SprintReport", null)
                        .WithMany("TeamMembers")
                        .HasForeignKey("SprintReportId");
                });

            modelBuilder.Entity("sprintplanningtoolbackend.Models.SprintReport", b =>
                {
                    b.Navigation("TeamMembers");
                });
#pragma warning restore 612, 618
        }
    }
}
