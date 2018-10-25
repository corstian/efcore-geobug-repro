﻿// <auto-generated />
using System;
using GeoAPI.Geometries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using efcore_spatial_aggregateException;

namespace efcore_spatial_aggregateException.Migrations
{
    [DbContext(typeof(BuggyDbContext))]
    [Migration("20181025143933_1")]
    partial class _1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35544")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("efcore_spatial_aggregateException.Airfield", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Continent");

                    b.Property<string>("Country");

                    b.Property<bool>("HasScheduledService");

                    b.Property<string>("HomePage");

                    b.Property<string>("Iata");

                    b.Property<string>("Icao");

                    b.Property<IPoint>("Location");

                    b.Property<string>("Muncipality");

                    b.Property<string>("Name");

                    b.Property<int?>("OurAirfieldsId");

                    b.Property<string>("Region");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.ToTable("Airfields");
                });
#pragma warning restore 612, 618
        }
    }
}