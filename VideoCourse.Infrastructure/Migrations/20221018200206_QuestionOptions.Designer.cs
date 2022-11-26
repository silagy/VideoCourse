﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using VideoCourse.Infrastructure;

#nullable disable

namespace VideoCourse.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20221018200206_QuestionOptions")]
    partial class QuestionOptions
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc.1.22426.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("VideoCourse.Domain.Entities.Note", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creation_date");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int>("TypeId")
                        .HasColumnType("integer")
                        .HasColumnName("type_id");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("update_date");

                    b.Property<Guid>("VideoId")
                        .HasColumnType("uuid")
                        .HasColumnName("video_id");

                    b.HasKey("Id");

                    b.HasIndex("VideoId");

                    b.ToTable("notes", (string)null);
                });

            modelBuilder.Entity("VideoCourse.Domain.Entities.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<string>("Error")
                        .HasColumnType("text")
                        .HasColumnName("error");

                    b.Property<DateTime>("OccurredOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("occurred_on_utc");

                    b.Property<DateTime?>("PublishedOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("published_on_utc");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.HasKey("Id");

                    b.ToTable("outbox_messages", (string)null);
                });

            modelBuilder.Entity("VideoCourse.Domain.Entities.Question", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creation_date");

                    b.Property<string>("Feedback")
                        .HasColumnType("text")
                        .HasColumnName("feedback");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int>("QuestionTypeId")
                        .HasColumnType("integer")
                        .HasColumnName("question_type_id");

                    b.Property<int>("TypeId")
                        .HasColumnType("integer")
                        .HasColumnName("type_id");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("update_date");

                    b.Property<Guid>("VideoId")
                        .HasColumnType("uuid")
                        .HasColumnName("video_id");

                    b.HasKey("Id");

                    b.HasIndex("VideoId");

                    b.ToTable("questions", (string)null);
                });

            modelBuilder.Entity("VideoCourse.Domain.Entities.QuestionOption", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creation_date");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("is_deleted");

                    b.Property<bool>("IsRight")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("is_right");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uuid")
                        .HasColumnName("question_id");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("update_date");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("question_options", (string)null);
                });

            modelBuilder.Entity("VideoCourse.Domain.Entities.Section", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creation_date");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("update_date");

                    b.Property<Guid>("VideoId")
                        .HasColumnType("uuid")
                        .HasColumnName("video_id");

                    b.HasKey("Id");

                    b.HasIndex("VideoId");

                    b.ToTable("sections", (string)null);
                });

            modelBuilder.Entity("VideoCourse.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creation_date");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("is_deleted");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer")
                        .HasColumnName("role_id");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("update_date");

                    b.HasKey("Id");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("VideoCourse.Domain.Entities.Video", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creation_date");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uuid")
                        .HasColumnName("creator_id");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("is_deleted");

                    b.Property<bool>("IsPublished")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("is_published");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTime?>("PublishedOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("published_on_utc");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("update_date");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("videos", (string)null);
                });

            modelBuilder.Entity("VideoCourse.Domain.Entities.Note", b =>
                {
                    b.HasOne("VideoCourse.Domain.Entities.Video", "Video")
                        .WithMany("Notes")
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("VideoCourse.Domain.ValueObjects.Duration", "Time", b1 =>
                        {
                            b1.Property<Guid>("NoteId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Value")
                                .HasColumnType("integer")
                                .HasColumnName("time");

                            b1.HasKey("NoteId");

                            b1.ToTable("notes");

                            b1.WithOwner()
                                .HasForeignKey("NoteId");
                        });

                    b.Navigation("Time")
                        .IsRequired();

                    b.Navigation("Video");
                });

            modelBuilder.Entity("VideoCourse.Domain.Entities.Question", b =>
                {
                    b.HasOne("VideoCourse.Domain.Entities.Video", "Video")
                        .WithMany("Questions")
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("VideoCourse.Domain.ValueObjects.Duration", "Time", b1 =>
                        {
                            b1.Property<Guid>("QuestionId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Value")
                                .HasColumnType("integer")
                                .HasColumnName("time");

                            b1.HasKey("QuestionId");

                            b1.ToTable("questions");

                            b1.WithOwner()
                                .HasForeignKey("QuestionId");
                        });

                    b.Navigation("Time")
                        .IsRequired();

                    b.Navigation("Video");
                });

            modelBuilder.Entity("VideoCourse.Domain.Entities.QuestionOption", b =>
                {
                    b.HasOne("VideoCourse.Domain.Entities.Question", null)
                        .WithMany("QuestionOptions")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VideoCourse.Domain.Entities.Section", b =>
                {
                    b.HasOne("VideoCourse.Domain.Entities.Video", "Video")
                        .WithMany("Sections")
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("VideoCourse.Domain.ValueObjects.Duration", "EndTime", b1 =>
                        {
                            b1.Property<Guid>("SectionId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Value")
                                .HasColumnType("integer")
                                .HasColumnName("end_time");

                            b1.HasKey("SectionId");

                            b1.ToTable("sections");

                            b1.WithOwner()
                                .HasForeignKey("SectionId");
                        });

                    b.OwnsOne("VideoCourse.Domain.ValueObjects.Duration", "StartTime", b1 =>
                        {
                            b1.Property<Guid>("SectionId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Value")
                                .HasColumnType("integer")
                                .HasColumnName("start_time");

                            b1.HasKey("SectionId");

                            b1.ToTable("sections");

                            b1.WithOwner()
                                .HasForeignKey("SectionId");
                        });

                    b.Navigation("EndTime")
                        .IsRequired();

                    b.Navigation("StartTime")
                        .IsRequired();

                    b.Navigation("Video");
                });

            modelBuilder.Entity("VideoCourse.Domain.Entities.User", b =>
                {
                    b.OwnsOne("VideoCourse.Domain.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("email");

                            b1.HasKey("UserId");

                            b1.ToTable("users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Email")
                        .IsRequired();
                });

            modelBuilder.Entity("VideoCourse.Domain.Entities.Video", b =>
                {
                    b.HasOne("VideoCourse.Domain.Entities.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("VideoCourse.Domain.ValueObjects.Duration", "Duration", b1 =>
                        {
                            b1.Property<Guid>("VideoId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Value")
                                .HasColumnType("integer")
                                .HasColumnName("duration");

                            b1.HasKey("VideoId");

                            b1.ToTable("videos");

                            b1.WithOwner()
                                .HasForeignKey("VideoId");
                        });

                    b.OwnsOne("VideoCourse.Domain.ValueObjects.VideoUrl", "Url", b1 =>
                        {
                            b1.Property<Guid>("VideoId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("url");

                            b1.HasKey("VideoId");

                            b1.ToTable("videos");

                            b1.WithOwner()
                                .HasForeignKey("VideoId");
                        });

                    b.Navigation("Creator");

                    b.Navigation("Duration")
                        .IsRequired();

                    b.Navigation("Url")
                        .IsRequired();
                });

            modelBuilder.Entity("VideoCourse.Domain.Entities.Question", b =>
                {
                    b.Navigation("QuestionOptions");
                });

            modelBuilder.Entity("VideoCourse.Domain.Entities.Video", b =>
                {
                    b.Navigation("Notes");

                    b.Navigation("Questions");

                    b.Navigation("Sections");
                });
#pragma warning restore 612, 618
        }
    }
}
