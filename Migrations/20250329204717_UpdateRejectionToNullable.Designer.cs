﻿// <auto-generated />
using System;
using IRIS_Web_Rec25_241EC152.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IRIS_Web_Rec25_241EC152.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250329204717_UpdateRejectionToNullable")]
    partial class UpdateRejectionToNullable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.14");

            modelBuilder.Entity("IRIS_Web_Rec25_241EC152.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Branch")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("IRIS_Web_Rec25_241EC152.Models.Court", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Availability")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Capacity")
                        .HasColumnType("INTEGER");

                    b.Property<TimeSpan>("ClosingTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("OpeningTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Courts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Availability = 0,
                            Capacity = 4,
                            ClosingTime = new TimeSpan(0, 22, 0, 0, 0),
                            Location = "Old Sports Complex",
                            Name = "Badminton Court 1",
                            OpeningTime = new TimeSpan(0, 7, 0, 0, 0),
                            Type = "Badminton"
                        },
                        new
                        {
                            Id = 2,
                            Availability = 0,
                            Capacity = 4,
                            ClosingTime = new TimeSpan(0, 21, 0, 0, 0),
                            Location = "Old Sports Complex",
                            Name = "Badminton Court 2",
                            OpeningTime = new TimeSpan(0, 7, 0, 0, 0),
                            Type = "Badminton"
                        },
                        new
                        {
                            Id = 3,
                            Availability = 1,
                            Capacity = 4,
                            ClosingTime = new TimeSpan(0, 21, 0, 0, 0),
                            Location = "Old Sports Complex",
                            Name = "Badminton Court 3",
                            OpeningTime = new TimeSpan(0, 7, 0, 0, 0),
                            Type = "Badminton"
                        },
                        new
                        {
                            Id = 4,
                            Availability = 0,
                            Capacity = 2,
                            ClosingTime = new TimeSpan(0, 20, 0, 0, 0),
                            Location = "Opposite Mechaical Department",
                            Name = "Tennis Court 1",
                            OpeningTime = new TimeSpan(0, 7, 0, 0, 0),
                            Type = "Tennis"
                        },
                        new
                        {
                            Id = 5,
                            Availability = 0,
                            Capacity = 10,
                            ClosingTime = new TimeSpan(0, 21, 0, 0, 0),
                            Location = "Opposite Mechaical Department",
                            Name = "Basketball Court",
                            OpeningTime = new TimeSpan(0, 6, 0, 0, 0),
                            Type = "Basketball"
                        },
                        new
                        {
                            Id = 6,
                            Availability = 2,
                            Capacity = 2,
                            ClosingTime = new TimeSpan(0, 22, 0, 0, 0),
                            Location = "New Sports Complex",
                            Name = "Squash Court 1",
                            OpeningTime = new TimeSpan(0, 10, 0, 0, 0),
                            Type = "Squash"
                        },
                        new
                        {
                            Id = 7,
                            Availability = 0,
                            Capacity = 2,
                            ClosingTime = new TimeSpan(0, 20, 0, 0, 0),
                            Location = "New Sports Complex",
                            Name = "Table Tennis Area",
                            OpeningTime = new TimeSpan(0, 8, 0, 0, 0),
                            Type = "Table Tennis"
                        },
                        new
                        {
                            Id = 8,
                            Availability = 3,
                            Capacity = 2,
                            ClosingTime = new TimeSpan(0, 18, 0, 0, 0),
                            Location = "New Sports Complex",
                            Name = "Kabaddi Room",
                            OpeningTime = new TimeSpan(0, 8, 0, 0, 0),
                            Type = "Kabaddi"
                        });
                });

            modelBuilder.Entity("IRIS_Web_Rec25_241EC152.Models.CourtBooking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("BookingDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("CourtId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("HourSlot")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RejectionReason")
                        .HasColumnType("TEXT");

                    b.Property<bool>("ReminderSent")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CourtId");

                    b.HasIndex("UserId");

                    b.ToTable("CourtBookings");
                });

            modelBuilder.Entity("IRIS_Web_Rec25_241EC152.Models.Equipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AvailableQuantity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Condition")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TotalQuantity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Equipments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AvailableQuantity = 14,
                            Condition = "Good",
                            Name = "Cricket Bat",
                            Status = 1,
                            TotalQuantity = 14,
                            Type = "Cricket"
                        },
                        new
                        {
                            Id = 2,
                            AvailableQuantity = 10,
                            Condition = "Good",
                            Name = "Football",
                            Status = 1,
                            TotalQuantity = 10,
                            Type = "Football"
                        },
                        new
                        {
                            Id = 3,
                            AvailableQuantity = 8,
                            Condition = "Good",
                            Name = "Badminton Racket",
                            Status = 1,
                            TotalQuantity = 8,
                            Type = "Badminton"
                        },
                        new
                        {
                            Id = 4,
                            AvailableQuantity = 4,
                            Condition = "Slightly Used",
                            Name = "Tennis Racket",
                            Status = 1,
                            TotalQuantity = 4,
                            Type = "Tennis"
                        },
                        new
                        {
                            Id = 5,
                            AvailableQuantity = 6,
                            Condition = "New",
                            Name = "Basketball",
                            Status = 1,
                            TotalQuantity = 6,
                            Type = "Basketball"
                        },
                        new
                        {
                            Id = 6,
                            AvailableQuantity = 12,
                            Condition = "Good",
                            Name = "Table Tennis Paddle",
                            Status = 1,
                            TotalQuantity = 12,
                            Type = "Table Tennis"
                        },
                        new
                        {
                            Id = 8,
                            AvailableQuantity = 4,
                            Condition = "New",
                            Name = "Volleyball",
                            Status = 1,
                            TotalQuantity = 4,
                            Type = "Volleyball"
                        },
                        new
                        {
                            Id = 9,
                            AvailableQuantity = 10,
                            Condition = "Good",
                            Name = "Skipping Rope",
                            Status = 1,
                            TotalQuantity = 10,
                            Type = "Fitness"
                        },
                        new
                        {
                            Id = 10,
                            AvailableQuantity = 5,
                            Condition = "New",
                            Name = "Dumbbells (5kg)",
                            Status = 1,
                            TotalQuantity = 5,
                            Type = "Fitness"
                        });
                });

            modelBuilder.Entity("IRIS_Web_Rec25_241EC152.Models.EquipmentBooking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("BookingDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("DurationHours")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EquipmentId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("HourSlot")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RejectionReason")
                        .HasColumnType("TEXT");

                    b.Property<bool>("ReminderSent")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EquipmentId");

                    b.HasIndex("UserId");

                    b.ToTable("EquipmentBookings");
                });

            modelBuilder.Entity("IRIS_Web_Rec25_241EC152.Models.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CourtBookingId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int?>("EquipmentBookingId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsRead")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CourtBookingId");

                    b.HasIndex("EquipmentBookingId");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("IRIS_Web_Rec25_241EC152.Models.CourtBooking", b =>
                {
                    b.HasOne("IRIS_Web_Rec25_241EC152.Models.Court", "Court")
                        .WithMany()
                        .HasForeignKey("CourtId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IRIS_Web_Rec25_241EC152.Models.ApplicationUser", "User")
                        .WithMany("CourtBookings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Court");

                    b.Navigation("User");
                });

            modelBuilder.Entity("IRIS_Web_Rec25_241EC152.Models.EquipmentBooking", b =>
                {
                    b.HasOne("IRIS_Web_Rec25_241EC152.Models.Equipment", "Equipment")
                        .WithMany()
                        .HasForeignKey("EquipmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IRIS_Web_Rec25_241EC152.Models.ApplicationUser", "User")
                        .WithMany("EquipmentBookings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Equipment");

                    b.Navigation("User");
                });

            modelBuilder.Entity("IRIS_Web_Rec25_241EC152.Models.Notification", b =>
                {
                    b.HasOne("IRIS_Web_Rec25_241EC152.Models.CourtBooking", "CourtBooking")
                        .WithMany()
                        .HasForeignKey("CourtBookingId");

                    b.HasOne("IRIS_Web_Rec25_241EC152.Models.EquipmentBooking", "EquipmentBooking")
                        .WithMany()
                        .HasForeignKey("EquipmentBookingId");

                    b.HasOne("IRIS_Web_Rec25_241EC152.Models.ApplicationUser", "User")
                        .WithMany("Notifications")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CourtBooking");

                    b.Navigation("EquipmentBooking");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("IRIS_Web_Rec25_241EC152.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("IRIS_Web_Rec25_241EC152.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IRIS_Web_Rec25_241EC152.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("IRIS_Web_Rec25_241EC152.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IRIS_Web_Rec25_241EC152.Models.ApplicationUser", b =>
                {
                    b.Navigation("CourtBookings");

                    b.Navigation("EquipmentBookings");

                    b.Navigation("Notifications");
                });
#pragma warning restore 612, 618
        }
    }
}
