﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Pogulum.Data.Migrations
{
    [DbContext(typeof(PogulumDbContext))]
    partial class PogulumDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Pogulum.Data.Models.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ActorId")
                        .HasColumnType("int");

                    b.Property<string>("BroadcasterSubjectId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ClipSubjectId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("GameSubjectId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ActorId");

                    b.HasIndex("BroadcasterSubjectId");

                    b.HasIndex("ClipSubjectId");

                    b.HasIndex("GameSubjectId");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("Pogulum.Data.Models.Broadcaster", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OfflineImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Popularity")
                        .HasColumnType("int");

                    b.Property<string>("ProfileImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("ViewCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Broadcasters");
                });

            modelBuilder.Entity("Pogulum.Data.Models.ChatMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ChatMessages");
                });

            modelBuilder.Entity("Pogulum.Data.Models.Clip", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BroadcasterId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<double>("Duration")
                        .HasColumnType("float");

                    b.Property<string>("EmbedUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GameId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("SavedClipId")
                        .HasColumnType("int");

                    b.Property<string>("ThumbnailUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("ViewCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BroadcasterId");

                    b.HasIndex("GameId");

                    b.HasIndex("SavedClipId");

                    b.HasIndex("UserId");

                    b.ToTable("Clips");
                });

            modelBuilder.Entity("Pogulum.Data.Models.Game", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BoxArtUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Popularity")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Pogulum.Data.Models.SavedClip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClipDurations")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("SavedClips");
                });

            modelBuilder.Entity("Pogulum.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClipCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePictureSrc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Pogulum.Data.Models.Activity", b =>
                {
                    b.HasOne("Pogulum.Data.Models.User", "Actor")
                        .WithMany()
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pogulum.Data.Models.Broadcaster", "BroadcasterSubject")
                        .WithMany()
                        .HasForeignKey("BroadcasterSubjectId");

                    b.HasOne("Pogulum.Data.Models.Clip", "ClipSubject")
                        .WithMany()
                        .HasForeignKey("ClipSubjectId");

                    b.HasOne("Pogulum.Data.Models.Game", "GameSubject")
                        .WithMany()
                        .HasForeignKey("GameSubjectId");

                    b.Navigation("Actor");

                    b.Navigation("BroadcasterSubject");

                    b.Navigation("ClipSubject");

                    b.Navigation("GameSubject");
                });

            modelBuilder.Entity("Pogulum.Data.Models.Broadcaster", b =>
                {
                    b.HasOne("Pogulum.Data.Models.User", null)
                        .WithMany("FavoriteBroadcasters")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Pogulum.Data.Models.ChatMessage", b =>
                {
                    b.HasOne("Pogulum.Data.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Pogulum.Data.Models.Clip", b =>
                {
                    b.HasOne("Pogulum.Data.Models.Broadcaster", "Broadcaster")
                        .WithMany()
                        .HasForeignKey("BroadcasterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pogulum.Data.Models.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId");

                    b.HasOne("Pogulum.Data.Models.SavedClip", null)
                        .WithMany("Clips")
                        .HasForeignKey("SavedClipId");

                    b.HasOne("Pogulum.Data.Models.User", null)
                        .WithMany("FavoriteClips")
                        .HasForeignKey("UserId");

                    b.Navigation("Broadcaster");

                    b.Navigation("Game");
                });

            modelBuilder.Entity("Pogulum.Data.Models.Game", b =>
                {
                    b.HasOne("Pogulum.Data.Models.User", null)
                        .WithMany("FavoriteGames")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Pogulum.Data.Models.SavedClip", b =>
                {
                    b.HasOne("Pogulum.Data.Models.User", "Creator")
                        .WithMany("SavedClips")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("Pogulum.Data.Models.SavedClip", b =>
                {
                    b.Navigation("Clips");
                });

            modelBuilder.Entity("Pogulum.Data.Models.User", b =>
                {
                    b.Navigation("FavoriteBroadcasters");

                    b.Navigation("FavoriteClips");

                    b.Navigation("FavoriteGames");

                    b.Navigation("SavedClips");
                });
#pragma warning restore 612, 618
        }
    }
}
