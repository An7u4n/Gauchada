﻿// <auto-generated />
using Gauchada.Backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Gauchada.Backend.API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240714233615_db01")]
    partial class db01
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Gauchada.Backend.Model.Entity.PassengerEntity", b =>
                {
                    b.Property<string>("UserName")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("UserName");

                    b.ToTable("Passenger", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}