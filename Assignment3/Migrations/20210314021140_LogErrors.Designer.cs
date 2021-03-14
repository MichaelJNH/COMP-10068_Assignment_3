﻿// <auto-generated />
using System;
using Assignment3.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Assignment3.Migrations
{
    [DbContext(typeof(Assignment3Context))]
    [Migration("20210314021140_LogErrors")]
    partial class LogErrors
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Assignment3.Models.ErrorResponse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StatusCode")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ErrorResponse");
                });

            modelBuilder.Entity("Assignment3.Models.Immunization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreationTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("ExpirationDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("LotNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("OfficialName")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("TradeName")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.ToTable("Immunization");
                });

            modelBuilder.Entity("Assignment3.Models.Organization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreationTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Organization");
                });

            modelBuilder.Entity("Assignment3.Models.Patient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreationTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DateOfBirth")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.HasKey("Id");

                    b.ToTable("Patient");
                });

            modelBuilder.Entity("Assignment3.Models.Provider", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreationTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<long>("LicenseNumber")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Provider");
                });
#pragma warning restore 612, 618
        }
    }
}
