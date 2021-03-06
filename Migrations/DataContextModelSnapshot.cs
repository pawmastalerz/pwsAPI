﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using pwsAPI.Data;

namespace pwsAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("pwsAPI.Models.Event", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd();

                    b.Property<short>("Accepted");

                    b.Property<string>("Description");

                    b.Property<string>("EventName");

                    b.Property<DateTime>("HappensAt");

                    b.Property<string>("PosterPhotoUrl");

                    b.Property<string>("SignUpLink");

                    b.Property<string>("Story");

                    b.HasKey("EventId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("pwsAPI.Models.EventPhoto", b =>
                {
                    b.Property<int>("EventPhotoId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EventId");

                    b.Property<int>("EventPhotoUrl");

                    b.HasKey("EventPhotoId");

                    b.HasIndex("EventId");

                    b.ToTable("EventPhoto");
                });

            modelBuilder.Entity("pwsAPI.Models.Thought", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<short>("Accepted");

                    b.Property<string>("Author");

                    b.Property<string>("Quote");

                    b.Property<string>("ThoughtPhotoUrl");

                    b.HasKey("Id");

                    b.ToTable("Thoughts");
                });

            modelBuilder.Entity("pwsAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("pwsAPI.Models.EventPhoto", b =>
                {
                    b.HasOne("pwsAPI.Models.Event", "Event")
                        .WithMany("EventPhoto")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
