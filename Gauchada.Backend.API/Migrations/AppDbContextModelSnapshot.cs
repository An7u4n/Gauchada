﻿// <auto-generated />
using System;
using Gauchada.Backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Gauchada.Backend.API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Gauchada.Backend.Model.Entity.CarEntity", b =>
                {
                    b.Property<string>("CarPlate")
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("MaxPassengers")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)");

                    b.Property<string>("OwnerUserName")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("CarPlate");

                    b.HasIndex("OwnerUserName");

                    b.ToTable("Cars", (string)null);
                });

            modelBuilder.Entity("Gauchada.Backend.Model.Entity.DriverEntity", b =>
                {
                    b.Property<string>("UserName")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<DateTime>("Birth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserName");

                    b.ToTable("Drivers", (string)null);
                });

            modelBuilder.Entity("Gauchada.Backend.Model.Entity.PassengerEntity", b =>
                {
                    b.Property<string>("UserName")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<DateTime>("Birth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserName");

                    b.ToTable("Passenger", (string)null);
                });

            modelBuilder.Entity("Gauchada.Backend.Model.Entity.TripEntity", b =>
                {
                    b.Property<int>("TripId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TripId"));

                    b.Property<string>("Destination")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("DriverUserName")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Origin")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("TripId");

                    b.HasIndex("DriverUserName");

                    b.ToTable("Trips", (string)null);
                });

            modelBuilder.Entity("PassengerEntityTripEntity", b =>
                {
                    b.Property<string>("PassengersUserName")
                        .HasColumnType("nvarchar(32)");

                    b.Property<int>("TripsTripId")
                        .HasColumnType("int");

                    b.HasKey("PassengersUserName", "TripsTripId");

                    b.HasIndex("TripsTripId");

                    b.ToTable("PassengerTrips", (string)null);
                });

            modelBuilder.Entity("Gauchada.Backend.Model.Entity.CarEntity", b =>
                {
                    b.HasOne("Gauchada.Backend.Model.Entity.DriverEntity", "Owner")
                        .WithMany("Cars")
                        .HasForeignKey("OwnerUserName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Gauchada.Backend.Model.Entity.TripEntity", b =>
                {
                    b.HasOne("Gauchada.Backend.Model.Entity.DriverEntity", "Driver")
                        .WithMany("Trips")
                        .HasForeignKey("DriverUserName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Driver");
                });

            modelBuilder.Entity("PassengerEntityTripEntity", b =>
                {
                    b.HasOne("Gauchada.Backend.Model.Entity.PassengerEntity", null)
                        .WithMany()
                        .HasForeignKey("PassengersUserName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gauchada.Backend.Model.Entity.TripEntity", null)
                        .WithMany()
                        .HasForeignKey("TripsTripId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Gauchada.Backend.Model.Entity.DriverEntity", b =>
                {
                    b.Navigation("Cars");

                    b.Navigation("Trips");
                });
#pragma warning restore 612, 618
        }
    }
}
