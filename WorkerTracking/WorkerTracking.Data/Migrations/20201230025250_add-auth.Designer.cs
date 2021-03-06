﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WorkerTracking.Data;

namespace WorkerTracking.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20201230025250_add-auth")]
    partial class addauth
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("WorkerTracking.Entities.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("role_id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Abbreviation")
                        .HasColumnName("abbreviation")
                        .HasColumnType("character varying(3)")
                        .HasMaxLength(3);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("character varying(40)")
                        .HasMaxLength(40);

                    b.HasKey("RoleId")
                        .HasName("pk_roles");

                    b.ToTable("roles");

                    b.HasData(
                        new
                        {
                            RoleId = 5001,
                            Abbreviation = "PO",
                            Name = "Product Owner"
                        },
                        new
                        {
                            RoleId = 5002,
                            Abbreviation = "PM",
                            Name = "Project Manager"
                        },
                        new
                        {
                            RoleId = 5003,
                            Abbreviation = "TL",
                            Name = "Team Leader"
                        },
                        new
                        {
                            RoleId = 5004,
                            Abbreviation = "FD",
                            Name = "Frontend Developer"
                        },
                        new
                        {
                            RoleId = 5005,
                            Abbreviation = "BD",
                            Name = "Backend Developer"
                        },
                        new
                        {
                            RoleId = 5006,
                            Abbreviation = "FS",
                            Name = "Fullstack Developer"
                        },
                        new
                        {
                            RoleId = 5007,
                            Abbreviation = "QA",
                            Name = "Quality Assurance"
                        },
                        new
                        {
                            RoleId = 5008,
                            Abbreviation = "UX",
                            Name = "User Experience"
                        },
                        new
                        {
                            RoleId = 5009,
                            Abbreviation = "FA",
                            Name = "Functional Analyst"
                        },
                        new
                        {
                            RoleId = 5010,
                            Abbreviation = "GD",
                            Name = "Graphic Designer"
                        },
                        new
                        {
                            RoleId = 5011,
                            Abbreviation = "HR",
                            Name = "Human Resources"
                        },
                        new
                        {
                            RoleId = 5012,
                            Abbreviation = "TS",
                            Name = "Technical Support"
                        });
                });

            modelBuilder.Entity("WorkerTracking.Entities.Status", b =>
                {
                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("status_id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("character varying(30)")
                        .HasMaxLength(30);

                    b.HasKey("StatusId")
                        .HasName("pk_status");

                    b.ToTable("status");

                    b.HasData(
                        new
                        {
                            StatusId = 100,
                            Name = "Active"
                        },
                        new
                        {
                            StatusId = 101,
                            Name = "Inactive"
                        },
                        new
                        {
                            StatusId = 103,
                            Name = "Pause"
                        },
                        new
                        {
                            StatusId = 104,
                            Name = "In a meeting"
                        },
                        new
                        {
                            StatusId = 105,
                            Name = "Vacations"
                        });
                });

            modelBuilder.Entity("WorkerTracking.Entities.Team", b =>
                {
                    b.Property<Guid>("TeamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("team_id")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("character varying(30)")
                        .HasMaxLength(30);

                    b.HasKey("TeamId")
                        .HasName("pk_teams");

                    b.ToTable("teams");
                });

            modelBuilder.Entity("WorkerTracking.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnName("id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnName("access_failed_count")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnName("concurrency_stamp")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnName("email")
                        .HasColumnType("text");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnName("email_confirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsAdmin")
                        .HasColumnName("is_admin")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnName("lockout_enabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnName("lockout_end")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnName("normalized_email")
                        .HasColumnType("text");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnName("normalized_user_name")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnName("password_hash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnName("phone_number")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnName("phone_number_confirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnName("security_stamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnName("two_factor_enabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasColumnName("user_name")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.ToTable("users");
                });

            modelBuilder.Entity("WorkerTracking.Entities.Worker", b =>
                {
                    b.Property<Guid>("WorkerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("worker_id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Birthday")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("birthday")
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValue(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("email")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnName("first_name")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("is_active")
                        .HasColumnType("boolean")
                        .HasDefaultValueSql("true");

                    b.Property<DateTime>("LastModificationTime")
                        .HasColumnName("last_modification_time")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnName("last_name")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("PhotoUrl")
                        .HasColumnName("photo_url")
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnName("role_id")
                        .HasColumnType("integer");

                    b.Property<int>("StatusId")
                        .HasColumnName("status_id")
                        .HasColumnType("integer");

                    b.HasKey("WorkerId")
                        .HasName("pk_workers");

                    b.HasIndex("RoleId")
                        .HasName("ix_workers_role_id");

                    b.HasIndex("StatusId")
                        .HasName("ix_workers_status_id");

                    b.ToTable("workers");
                });

            modelBuilder.Entity("WorkerTracking.Entities.WorkersByTeam", b =>
                {
                    b.Property<Guid>("WorkersByTeamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("workers_by_team_id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TeamId")
                        .HasColumnName("team_id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("WorkerId")
                        .HasColumnName("worker_id")
                        .HasColumnType("uuid");

                    b.HasKey("WorkersByTeamId")
                        .HasName("pk_workers_by_teams");

                    b.HasIndex("TeamId")
                        .HasName("ix_workers_by_teams_team_id");

                    b.HasIndex("WorkerId")
                        .HasName("ix_workers_by_teams_worker_id");

                    b.ToTable("workers_by_teams");
                });

            modelBuilder.Entity("WorkerTracking.Entities.Worker", b =>
                {
                    b.HasOne("WorkerTracking.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("fk_workers_roles_role_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkerTracking.Entities.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .HasConstraintName("fk_workers_status_status_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WorkerTracking.Entities.WorkersByTeam", b =>
                {
                    b.HasOne("WorkerTracking.Entities.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .HasConstraintName("fk_workers_by_teams_teams_team_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkerTracking.Entities.Worker", "Worker")
                        .WithMany()
                        .HasForeignKey("WorkerId")
                        .HasConstraintName("fk_workers_by_teams_workers_worker_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
