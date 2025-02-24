﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WorkstationManagment.Core.Data;

#nullable disable

namespace WorkstationManagment.Core.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("WorkstationManagment.Core.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Administrator",
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Regular user",
                            Name = "User"
                        });
                });

            modelBuilder.Entity("WorkstationManagment.Core.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "John",
                            LastName = "Doe",
                            Password = "$2a$11$1FCKrCh4XgvVMftI7SmQluUS/YKVGfZtbWyZteX7oipZ8OI2D.QGC",
                            RoleId = 1,
                            Username = "john123"
                        },
                        new
                        {
                            Id = 2,
                            FirstName = "Marko",
                            LastName = "Markovic",
                            Password = "$2a$11$mUDZ4cxrlhND9DWgSydrtutKTwLh.hM/DO8wC5dL.dq8RL7vV9mUu",
                            RoleId = 2,
                            Username = "marko1"
                        },
                        new
                        {
                            Id = 3,
                            FirstName = "Ivan",
                            LastName = "Ivanovic",
                            Password = "$2a$11$OuQ1.T223rdY5v2INkeWZObm47FvXnSF5jiqePJPbBnlRnWY4T27a",
                            RoleId = 2,
                            Username = "ivan1"
                        },
                        new
                        {
                            Id = 4,
                            FirstName = "Ana",
                            LastName = "Ivic",
                            Password = "$2a$11$gsN8QZit8NGX6e95cYhjWuqOKhV27zalr3fLuILnI3DuDfqtyg/p.",
                            RoleId = 2,
                            Username = "ana1"
                        });
                });

            modelBuilder.Entity("WorkstationManagment.Core.Models.UserWorkPosition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ProductName")
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("WorkPositionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("WorkPositionId");

                    b.ToTable("UserWorkPositions");

                    b.HasData(
                        new
                        {
                            Id = -1,
                            Date = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = 1,
                            WorkPositionId = 1
                        });
                });

            modelBuilder.Entity("WorkstationManagment.Core.Models.WorkPosition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("WorkPositions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Software Engineer"
                        },
                        new
                        {
                            Id = 2,
                            Name = "System Administrator"
                        });
                });

            modelBuilder.Entity("WorkstationManagment.Core.Models.User", b =>
                {
                    b.HasOne("WorkstationManagment.Core.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("WorkstationManagment.Core.Models.UserWorkPosition", b =>
                {
                    b.HasOne("WorkstationManagment.Core.Models.User", "User")
                        .WithMany("UserWorkPositions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkstationManagment.Core.Models.WorkPosition", "WorkPosition")
                        .WithMany("UserWorkPositions")
                        .HasForeignKey("WorkPositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("WorkPosition");
                });

            modelBuilder.Entity("WorkstationManagment.Core.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("WorkstationManagment.Core.Models.User", b =>
                {
                    b.Navigation("UserWorkPositions");
                });

            modelBuilder.Entity("WorkstationManagment.Core.Models.WorkPosition", b =>
                {
                    b.Navigation("UserWorkPositions");
                });
#pragma warning restore 612, 618
        }
    }
}
