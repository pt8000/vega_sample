using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using VegaApp.Data;

namespace _2KursVega.Migrations
{
    [DbContext(typeof(VegaDbContext))]
    [Migration("20170509122604_makes")]
    partial class makes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("VegaApp.Modells.Features", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ModelId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("ModelId");

                    b.ToTable("Features");
                });

            modelBuilder.Entity("VegaApp.Modells.Makes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Makes");
                });

            modelBuilder.Entity("VegaApp.Modells.Model", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("MakesId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("MakesId");

                    b.ToTable("Model");
                });

            modelBuilder.Entity("VegaApp.Modells.Features", b =>
                {
                    b.HasOne("VegaApp.Modells.Model")
                        .WithMany("Features")
                        .HasForeignKey("ModelId");
                });

            modelBuilder.Entity("VegaApp.Modells.Model", b =>
                {
                    b.HasOne("VegaApp.Modells.Makes")
                        .WithMany("Modele")
                        .HasForeignKey("MakesId");
                });
        }
    }
}
